using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AnimArch.Visualization.Diagrams;
using Assets.Scripts.AnimationControl.OAL;
using OALProgramControl;
using TMPro;
using UMSAGL.Scripts;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI.Extensions;
using Visualisation.Animation;
using Visualization.ClassDiagram;
using Visualization.ClassDiagram.ClassComponents;
using Visualization.ClassDiagram.ComponentsInDiagram;
using Visualization.ClassDiagram.Diagrams;
using Visualization.ClassDiagram.Relations;
using Visualization.UI;
using AnimArch.Extensions;

namespace Visualization.Animation
{
    //Controls the entire animation process
    public class Animation : Singleton<Animation>
    {
        private ClassDiagram.Diagrams.ClassDiagram classDiagram;
        private ObjectDiagram objectDiagram;
        public Color classColor;
        public Color methodColor;
        public Color relationColor;
        public GameObject LineFill;
        private int BarrierSize;
        private int CurrentBarrierFill;
        [HideInInspector] public bool AnimationIsRunning = false;
        [HideInInspector] public bool isPaused = false;
        [HideInInspector] public bool standardPlayMode = true;
        [HideInInspector] private EXEExecutionResult executionSuccess = EXEExecutionResult.Success();
        public bool nextStep = false;
        private bool prevStep = false;
        private List<GameObject> Fillers;
        [HideInInspector]
        public string ReadValue;

        public string startClassName;
        public string startMethodName;

        [HideInInspector] private OALProgram currentProgramInstance = new OALProgram();
        [HideInInspector] public OALProgram CurrentProgramInstance { get { return currentProgramInstance; } }

        private void Awake()
        {
            classDiagram = GameObject.Find("ClassDiagram").GetComponent<ClassDiagram.Diagrams.ClassDiagram>();
            objectDiagram = GameObject.Find("ObjectDiagram").GetComponent<ObjectDiagram>();
            standardPlayMode = true;
        }

        private void ShowError() {
            Debug.Log("Error panel shown!");
            UI.MenuManager.Instance.ShowErrorPanel(executionSuccess);
        }

        // Main Couroutine for compiling the OAL of Animation script and then starting the visualisation of Animation
        public IEnumerator Animate()
        {

            Fillers = new List<GameObject>();

            if (AnimationIsRunning)
            {
                yield break;
            }

            AnimationIsRunning = true;

            UI.MenuManager.Instance.HideErrorPanelOnStopButton();

            Anim selectedAnimation = AnimationData.Instance.selectedAnim;

            List<AnimClass> MethodsCodes = selectedAnimation.GetMethodsCodesList(); //Filip

            foreach (AnimClass classItem in MethodsCodes) //Filip
            {
                CDClass Class = CurrentProgramInstance.ExecutionSpace.getClassByName(classItem.Name);

                foreach (AnimMethod methodItem in classItem.Methods)
                {
                    CDMethod Method = Class.GetMethodByName(methodItem.Name);

                    EXEScopeMethod MethodBody = OALParserBridge.Parse(methodItem.Code);
                    Method.ExecutableCode = MethodBody;
                }
            }

            CDClass startClass = CurrentProgramInstance.ExecutionSpace.getClassByName(startClassName);
            if (startClass == null)
            {
                Debug.LogError(string.Format("Error, Class \"{0}\" not found", startClassName ?? "NULL"));
            }

            CDMethod startMethod = startClass.GetMethodByName(startMethodName);
            if (startMethod == null)
            {
                Debug.LogError(string.Format("Error, Method \"{0}\" not found", startMethodName ?? "NULL"));
            }

            //najdeme startMethod z daneho class stringu a method stringu, ak startMethod.ExecutableCode je null tak return null alebo yield break
            EXEScopeMethod MethodExecutableCode = CurrentProgramInstance.ExecutionSpace.getClassByName(startClassName)
                .GetMethodByName(startMethodName).ExecutableCode;
            if (MethodExecutableCode == null)
            {
                Debug.Log("Warning, EXEScopeMethod of selected Method is null");
                yield break;
            }

            CurrentProgramInstance.SuperScope = MethodExecutableCode; //StartMethod.ExecutableCode
            //OALProgram.Instance.SuperScope = OALParserBridge.Parse(Code); //Method.ExecutableCode dame namiesto OALParserBridge.Parse(Code) pre metodu ktora bude zacinat
            UI.MenuManager.Instance.AnimateSourceCodeAtMethodStart(MethodExecutableCode);

            Debug.Log("Abt to execute program");
            int i = 0;

            string currentClassName = startClassName;
            string currentMethodName = startMethodName;

            CDClassInstance startingInstance = MethodExecutableCode.MethodDefinition.OwningClass.CreateClassInstance();
            MethodExecutableCode.OwningObject = startingInstance;
            objectDiagram.ShowObject(AddObjectToDiagram(" ", startingInstance));

            MethodExecutableCode.InitializeVariables(currentProgramInstance);

            while (executionSuccess.IsSuccess && CurrentProgramInstance.CommandStack.HasNext())
            {
                EXECommand CurrentCommand = CurrentProgramInstance.CommandStack.Next();
                executionSuccess = CurrentCommand.PerformExecution(CurrentProgramInstance);

                Debug.Log("Command " + i++ + executionSuccess.ToString());

                if (!executionSuccess.IsSuccess)
                {
                    ShowError();
                    break;
                }

                if (!executionSuccess.IsDone)
                {
                    continue;
                }

                CurrentCommand.ToggleActiveRecursiveBottomUp(true);

                if (!(CurrentCommand is EXECommandMulti))
                {
                    EXEScopeMethod CurrentMethodScope = CurrentCommand.GetCurrentMethodScope();

                    UI.MenuManager.Instance.AnimateSourceCodeAtMethodStart(CurrentMethodScope);
                }
                yield return AnimateCommand(CurrentCommand);

                CurrentCommand.ToggleActiveRecursiveBottomUp(false);
            }

            Debug.Log("Over");
            AnimationIsRunning = false;
            executionSuccess = EXEExecutionResult.Success();
        }

        private IEnumerator AnimateCommand(EXECommand CurrentCommand)
        {
            if (CurrentCommand.GetType() == typeof(EXECommandCall))
            {
                EXECommandCall exeCommandCall = (EXECommandCall)CurrentCommand;
                MethodInvocationInfo methodCallInfo = exeCommandCall.CallInfo;

                BarrierSize = 1;
                CurrentBarrierFill = 0;

                objectDiagram.AddRelation(methodCallInfo.CallerObject, methodCallInfo.CalledObject, "ASSOCIATION");

                StartCoroutine(ResolveCallFunct(methodCallInfo));

                yield return StartCoroutine(BarrierFillCheck());

                UI.MenuManager.Instance.AnimateSourceCodeAtMethodStart(exeCommandCall.InvokedMethod);
            }
            else if (CurrentCommand.GetType().Equals(typeof(EXECommandMulti)))
            {
                EXECommandMulti multicallCommand = (EXECommandMulti)CurrentCommand;
                BarrierSize = multicallCommand.Commands.Count;
                CurrentBarrierFill = 0;

                // For now let us show source code of method owning the first command

                foreach (EXECommand command in multicallCommand.Commands)
                {
                    if (command is EXECommandCall)
                    {
                        StartCoroutine(ResolveCallFunct(((EXECommandCall)command).CallInfo));
                    }
                    else if (command is EXECommandQueryCreate)
                    {
                        StartCoroutine(ResolveCreateObject(command));
                    }
                }

                foreach (EXECommandCall callCommand in multicallCommand.Commands.Where(command => command is EXECommandCall))
                {
                    CDClassInstance caller = callCommand.CallInfo.CallerObject;
                    CDClassInstance called = callCommand.CallInfo.CalledObject;

                    objectDiagram.AddRelation(caller, called, "ASSOCIATION");
                }

                // Debug.LogError(start.VariableName + " " + end.VariableName);
                yield return StartCoroutine(BarrierFillCheck());
            }
            else if (CurrentCommand.GetType() == typeof(EXECommandQueryCreate))
            {
                BarrierSize = 1;
                CurrentBarrierFill = 0;
                StartCoroutine(ResolveCreateObject(CurrentCommand));
                yield return StartCoroutine(BarrierFillCheck());
            }
            else if (CurrentCommand.GetType() == typeof(EXECommandAssignment))
            {
                ResolveAssignment(CurrentCommand);
            }
            else if (CurrentCommand.GetType() == typeof(EXECommandAddingToList))
            {
                EXECommandAddingToList addingToList = (EXECommandAddingToList)CurrentCommand;
                CDClassInstance listOwnerInstance = addingToList.GetAssignmentTargetOwner();
                CDClassInstance appendedInstance = addingToList.GetAppendedElementInstance();

                if (listOwnerInstance == null || appendedInstance == null) yield return null;

                objectDiagram.AddRelation(listOwnerInstance, appendedInstance, "ASSOCIATION");

                objectDiagram.AddListAttributeValue(listOwnerInstance);

            }
            else if (CurrentCommand.GetType().Equals(typeof(EXECommandRead)))
            {
                BarrierSize = 1;
                CurrentBarrierFill = 0;

                ConsolePanel.Instance.ActivateInputField();

                yield return StartCoroutine(BarrierFillCheck());

                executionSuccess = ((EXECommandRead)CurrentCommand).AssignReadValue(this.ReadValue, CurrentProgramInstance);
                this.ReadValue = null;
            }
            else
            {
                yield return new WaitForSeconds(0.3f);
            }
        }

        private void ResolveAssignment(EXECommand currentCommand)
        {
            EXECommandAssignment assignment = (EXECommandAssignment)currentCommand;
            CDClassInstance classInstance = assignment.GetAssignmentTargetOwner();
            if (classInstance == null) return;

            objectDiagram.AddAttributeValue(classInstance);
        }
        private ObjectInDiagram AddObjectToDiagram(string name, CDClassInstance newObject)
        {
            ObjectInDiagram objectInDiagram = objectDiagram.AddObjectInDiagram(name, newObject);
            DiagramPool.Instance.ObjectDiagram.AddObject(objectInDiagram);
            return objectInDiagram;
        }
        private IEnumerator ResolveCreateObject(EXECommand currentCommand)
        {
            EXECommandQueryCreate createCommand = (EXECommandQueryCreate)currentCommand;

            CDClassInstance callerObject = currentCommand.GetCurrentMethodScope().OwningObject;
            CDClassInstance createdObject = createCommand.GetCreatedInstance();
            VisitorCommandToString visitor = VisitorCommandToString.BorrowAVisitor();
            createCommand.Accept(visitor);
            string targetVariableName = visitor.GetCommandStringAndResetStateNow();


            var objectInDiagram = AddObjectToDiagram(targetVariableName, createdObject);

            var relation = FindInterGraphRelation(createdObject.UniqueID);


            #region Object creation animation

            int step = 0;
            float speedPerAnim = AnimationData.Instance.AnimSpeed;
            float timeModifier = 1f;
            while (step < 7)
            {
                if (isPaused)
                {
                    yield return new WaitForFixedUpdate();
                }
                else
                {
                    switch (step)
                    {
                        case 0:
                            HighlightClass(createdObject.OwningClass.Name, true);
                            break;
                        case 1:
                            // yield return StartCoroutine(AnimateFillInterGraph(relation));
                            timeModifier = 0f;
                            break;
                        case 3:
                            // relation.Show();
                            // relation.Highlight();
                            timeModifier = 1f;
                            break;
                        case 2:
                            objectDiagram.ShowObject(objectInDiagram);
                            timeModifier = 0.5f;
                            break;
                        case 6:
                            HighlightClass(createdObject.OwningClass.Name, false);
                            relation.UnHighlight();
                            timeModifier = 1f;
                            break;
                    }

                    step++;
                    if (standardPlayMode)
                    {
                        yield return new WaitForSeconds(AnimationData.Instance.AnimSpeed * timeModifier);
                    }
                    //Else means we are working with step animation
                    else
                    {
                        if (step == 1) step = 2;
                        nextStep = false;
                        prevStep = false;
                        yield return new WaitUntil(() => nextStep);
                        if (prevStep)
                        {
                            if (step > 0) step--;
                            step = UnhighlightObjectCreationStepAnimation(step, createdObject.OwningClass.Name, objectInDiagram, relation);

                            if (step > -1) step--;
                            step = UnhighlightObjectCreationStepAnimation(step, createdObject.OwningClass.Name, objectInDiagram, relation);
                        }

                        yield return new WaitForFixedUpdate();
                        nextStep = false;
                        prevStep = false;
                    }
                }
            }

            IncrementBarrier();

            #endregion

            objectDiagram.AddRelation(callerObject, createdObject, "ASSOCIATION");
        }

        private IEnumerator AnimateFillInterGraph(InterGraphRelation relation)
        {
            relation.Animate(AnimationData.Instance.AnimSpeed * 20);
            yield return new WaitForSeconds(AnimationData.Instance.AnimSpeed);
        }

        private static InterGraphRelation FindInterGraphRelation(long instanceId)
        {
            InterGraphRelation relation = null;
            foreach (var interGraphRelation in DiagramPool.Instance.RelationsClassToObject)
            {
                if (interGraphRelation.Object.Instance.UniqueID == instanceId)
                {
                    relation = interGraphRelation;
                }
            }

            return relation;
        }

        private int UnhighlightObjectCreationStepAnimation(int step, string className, ObjectInDiagram od,
            InterGraphRelation relation)
        {
            if (step == 1) step = 2;
            switch (step)
            {
                case 0:
                    HighlightClass(className, false);
                    break;
                case 2:
                    relation.UnHighlight();
                    break;
                case 3:
                    break;
            }

            return step;
        }

        public void IncrementBarrier()
        {
            this.CurrentBarrierFill++;
        }

        public IEnumerator BarrierFillCheck()
        {
            yield return new WaitUntil(() => CurrentBarrierFill >= BarrierSize);
        }

        public void StartAnimation()
        {
            isPaused = false;
            StartCoroutine("Animate");
        }

        //Couroutine that can be used to Highlight class for a given duration of time
        public IEnumerator AnimateClass(string className, float animationLength)
        {
            HighlightClass(className, true);
            yield return new WaitForSeconds(animationLength);
            HighlightClass(className, false);
        }

        public IEnumerator AnimateObject(long objectId, float animationLength)
        {
            HighlightObject(objectId, true);
            yield return new WaitForSeconds(animationLength);
            HighlightObject(objectId, false);
        }

        //Couroutine that can be used to Highlight method for a given duration of time
        public IEnumerator AnimateMethod(CDMethod method, float animationLength)
        {
            HighlightMethod(method, true);
            yield return new WaitForSeconds(animationLength);
            HighlightMethod(method, false);
        }

        //Couroutine that can be used to Highlight edge for a given duration of time
        public IEnumerator AnimateEdge(string relationshipName, float animationLength, MethodInvocationInfo call)
        {
            HighlightEdge(relationshipName, true, call);
            yield return new WaitForSeconds(animationLength);
            HighlightEdge(relationshipName, false, call);
        }

        public IEnumerator AnimateFill(MethodInvocationInfo Call)
        {
            //Debug.Log("Filip, hrana: " + Call.RelationshipName); //Filip
            GameObject edge = classDiagram.FindEdge(Call.Relation.RelationshipName);
            if (edge != null)
            {
                if (edge.CompareTag("Generalization") || edge.CompareTag("Implements") ||
                    edge.CompareTag("Realisation"))
                {
                    HighlightEdge(Call.Relation.RelationshipName, true, Call);
                    yield return new WaitForSeconds(AnimationData.Instance.AnimSpeed / 2);
                }
                else
                {
                    yield return FillNewFiller(classDiagram.FindOwnerOfRelation(Call.Relation.RelationshipName),
                        Call.CalledMethod.OwningClass.Name, edge, Call);
                }
            }
        }

        private object FillNewFiller(string ownerOfRelation, string calledClassName, GameObject edge, MethodInvocationInfo Call)
        {
            GameObject newFiller = Instantiate(LineFill);
            Fillers.Add(newFiller);

            newFiller.transform.position = classDiagram.graph.units.GetChild(0).transform.position;
            newFiller.transform.SetParent(classDiagram.graph.units);
            newFiller.transform.localScale = new Vector3(1, 1, 1);


            GameObject newFiller1 = Instantiate(LineFill);
            Fillers.Add(newFiller1);

            newFiller1.transform.position = objectDiagram.graph.units.GetChild(0).transform.position;
            newFiller1.transform.SetParent(objectDiagram.graph.units);
            newFiller1.transform.localScale = new Vector3(1, 1, 1);

            LineFiller lf = newFiller.GetComponent<LineFiller>();
            bool flip = ownerOfRelation.Equals(calledClassName);

            LineFiller lf1 = newFiller1.GetComponent<LineFiller>();
            var classInDiagram = DiagramPool.Instance.ClassDiagram.FindClassByName(Call.CallerMethod.OwningClass.Name);
            foreach (var callerInstance in classInDiagram.ClassInfo.Instances)
            {
                var objectRelation =
                    DiagramPool.Instance.ObjectDiagram.FindRelation(callerInstance.UniqueID, Call.CalledObject.UniqueID)
                        .GameObject;
                lf1.StartCoroutine(lf1.AnimateFlow(objectRelation.GetComponent<UILineRenderer>().Points, false));
            }

            return lf.StartCoroutine(lf.AnimateFlow(edge.GetComponent<UILineRenderer>().Points, flip));
        }

        private GameObject classGameObject(string className)
        {
            if (!UI.UIEditorManager.Instance.isNetworkDisabledOrIsServer())
            {
                var objects = NetworkManager.Singleton.SpawnManager.SpawnedObjects;
                var values = objects.Values;
                foreach (var value in values)
                {
                    if (value.name == className)
                        return value.gameObject;
                }
            }
            return classDiagram.FindNode(className);
        }

        private void HighlightBackground(BackgroundHighlighter backgroundHighlighter, bool isToBeHighlighted)
        {
            if (isToBeHighlighted)
                backgroundHighlighter.HighlightBackground();
            else
                backgroundHighlighter.UnhighlightBackground();
        }

        //Method used to Highlight/Unhighlight single class by name, depending on bool value of argument 
        public void HighlightClass(string className, bool isToBeHighlighted, long instanceID = -1)
        {
            GameObject node = classGameObject(className);

            BackgroundHighlighter backgroundHighlighter = null;
            if (node != null)
            {
                backgroundHighlighter = node.GetComponent<BackgroundHighlighter>();
            }
            else
            {
                Debug.Log("Node " + className + " not found");
                return;
            }
            HighlightBackground(backgroundHighlighter, isToBeHighlighted);
        }

        public void HighlightObjects(MethodInvocationInfo call, bool isToBeHighlighted)
        {
            ClassInDiagram classByName = classDiagram.FindClassByName(call.CallerMethod.OwningClass.Name);

            if (classByName == null)
            {
                Debug.Log("Node " + call.CallerMethod.OwningClass.Name + " not found");
            }

            if (classByName != null)
            {
                foreach (var classInfoInstance in classByName.ClassInfo.Instances)
                {
                    long id = classInfoInstance.UniqueID;
                    HighlightObject(id, isToBeHighlighted);
                }
            }
            else
            {
                Debug.Log("Highlighter component not found");
            }
        }

        //Method used to Highlight/Unhighlight single class by name, depending on bool value of argument 
        public void HighlightObject(long objectUniqueId, bool isToBeHighlighted)
        {
            GameObject node = objectDiagram.FindByID(objectUniqueId).VisualObject;
            BackgroundHighlighter backgroundHighlighter = null;
            if (node != null)
            {
                backgroundHighlighter = node.GetComponent<BackgroundHighlighter>();
            }
            else
            {
                Debug.Log("Node " + objectUniqueId + " not found");
                return;
            }

            HighlightBackground(backgroundHighlighter, isToBeHighlighted);
        }

        //Method used to Highlight/Unhighlight single method by name, depending on bool value of argument 
        public void HighlightMethod(Class _class, Method method, bool isToBeHighlighted)
        {
            HighlightMethod(_class.Name, method.Name, isToBeHighlighted);
        }
        public void HighlightMethod(CDMethod method, bool isToBeHighlighted)
        {
            HighlightMethod(method.OwningClass.Name, method.Name, isToBeHighlighted);
        }
        public void HighlightMethod(string className, string methodName, bool isToBeHighlighted)
        {
            var node = classDiagram.FindNode(className);
            if (node)
            {
                ClassTextHighligter classTextHighligter = node.GetComponent<ClassTextHighligter>();
                if (classTextHighligter)
                {
                    if (isToBeHighlighted)
                    {
                        if (DateTime.Now.IsJune()) {
                            classTextHighligter.HighlightClassNameLine();
                        }
                        classTextHighligter.HighlightClassLine(methodName);
                    }
                    else
                    {
                        if (DateTime.Now.IsJune()) {
                            classTextHighligter.UnhighlightClassNameLine();
                        }
                        classTextHighligter.UnhighlightClassLine(methodName);
                    }
                }
                else
                {
                    Debug.LogError("TextHighlighter component not found");
                }
            }
            else
            {
                Debug.LogError("Node " + className + " not found");
            }
        }

        private void HighlightInstancesMethod(MethodInvocationInfo call, bool isToBeHighlighted)
        {
            foreach (CDClassInstance cdClassInstance in call.CallerMethod.OwningClass.Instances)
            {
                HighlightObjectMethod(call.CallerMethod.Name, cdClassInstance.UniqueID, isToBeHighlighted);
            }
        }

        private void HighlightObjectMethod(string methodName, long cdClassInstanceId, bool isToBeHighlighted)
        {
            var textHighlighter = objectDiagram.FindByID(cdClassInstanceId).VisualObject
                .GetComponent<ObjectTextHighlighter>();
            if (textHighlighter != null)
            {
                if (isToBeHighlighted)
                    textHighlighter.HighlightObjectLine(methodName);
                else
                    textHighlighter.UnHighlightObjectLine(methodName);
            }
        }

        //Method used to Highlight/Unhighlight single edge by name, depending on bool value of argument 
        public void HighlightEdge(string relationshipName, bool isToBeHighlighted, MethodInvocationInfo Call)
        {
            RelationInDiagram relationInDiagram = classDiagram.FindEdgeInfo(relationshipName);

            GameObject edge = relationInDiagram?.VisualObject;

            var callerClassInDiagram =
                DiagramPool.Instance.ClassDiagram.FindClassByName(relationInDiagram?.RelationInfo.FromClass);
            var calledClassInDiagram =
                DiagramPool.Instance.ClassDiagram.FindClassByName(relationInDiagram?.RelationInfo.ToClass);

            if (edge != null)
            {
                if (isToBeHighlighted)
                {
                    edge.GetComponent<UEdge>().ChangeColor(relationColor);
                    edge.GetComponent<UILineRenderer>().LineThickness = 8;
                    HighlightInstancesRelations(Call, callerClassInDiagram, calledClassInDiagram, true);
                }
                else
                {
                    edge.GetComponent<UEdge>().ChangeColor(Color.white);
                    edge.GetComponent<UILineRenderer>().LineThickness = 5;
                    HighlightInstancesRelations(Call, callerClassInDiagram, calledClassInDiagram, false);
                }
            }
            else
            {
                Debug.Log(relationshipName + " NULL Edge ");
            }
        }

        private void HighlightInstancesRelations(MethodInvocationInfo Call, ClassInDiagram callerClassName,
                    ClassInDiagram calledClassName, bool isToBeHighlighted)
        {
            if (Call == null) // unhighlight all
            {
                foreach (var callerInstance in callerClassName.ClassInfo.Instances)
                {
                    foreach (var calledInstance in calledClassName.ClassInfo.Instances)
                    {
                        HighlightObjectRelation(callerInstance.UniqueID, calledInstance.UniqueID, false);
                    }
                }
            }
            else
            {
                var classInDiagram = DiagramPool.Instance.ClassDiagram.FindClassByName(Call.CallerMethod.OwningClass.Name);
                foreach (var callerInstance in classInDiagram.ClassInfo.Instances)
                {
                    HighlightObjectRelation(callerInstance.UniqueID, Call.CalledObject.UniqueID, isToBeHighlighted);
                }
            }
        }

        private void HighlightObjectRelation(long callerInstanceId, long calledInstanceId, bool isToBeHighlighted)
        {
            if (callerInstanceId == calledInstanceId)
            {
                return;
            }

            if (DiagramPool.Instance.ObjectDiagram == null)
                return;

            var objectRelation = DiagramPool.Instance.ObjectDiagram.FindRelation(callerInstanceId, calledInstanceId);
            
            if (objectRelation == null)
                return;
            
            var objectRelationGo = objectRelation.GameObject;

            if (isToBeHighlighted)
            {
                objectRelationGo.GetComponent<UEdge>().ChangeColor(relationColor);
                objectRelationGo.GetComponent<UILineRenderer>().LineThickness = 8;
            }
            else
            {
                objectRelationGo.GetComponent<UEdge>().ChangeColor(Color.white);
                objectRelationGo.GetComponent<UILineRenderer>().LineThickness = 5;
            }
        }

        // Couroutine used to Resolve one OALCall consisting of Caller class, caller method, edge, called class, called method
        // Same coroutine is called for play or step mode
        public IEnumerator ResolveCallFunct(MethodInvocationInfo Call)
        {
            Debug.Log(Call.ToString());
            int step = 0;
            float speedPerAnim = AnimationData.Instance.AnimSpeed;
            float timeModifier = 1f;
            while (step < 7)
            {
                if (isPaused)
                {
                    yield return new WaitForFixedUpdate();
                }
                else
                {
                    switch (step)
                    {
                        case 0:
                            HighlightClass(Call.CallerMethod.OwningClass.Name, true);
                            HighlightObjects(Call, true);
                            break;
                        case 1:
                            HighlightMethod(Call.CallerMethod, true);
                            HighlightInstancesMethod(Call, true);
                            break;
                        case 2:
                            //yield return StartCoroutine(AnimateFill(Call)); // Lukas - commented this out to prevent unwanted extra line artifact
                            timeModifier = 0f;
                            break;
                        case 3:
                            HighlightEdge(Call.Relation?.RelationshipName, true, Call);
                            timeModifier = 0.5f;
                            break;
                        case 4:
                            HighlightClass(Call.CalledMethod.OwningClass.Name, true, Call.CalledObject.UniqueID);
                            HighlightObject(Call.CalledObject.UniqueID, true);
                            timeModifier = 1f;
                            break;
                        case 5:
                            HighlightMethod(Call.CalledMethod, true);
                            HighlightObjectMethod(Call.CalledMethod.Name, Call.CalledObject.UniqueID, true);
                            timeModifier = 1.25f;
                            break;
                        case 6:
                            HighlightClass(Call.CallerMethod.OwningClass.Name, false);
                            HighlightObjects(Call, false);
                            HighlightMethod(Call.CallerMethod, false);
                            HighlightInstancesMethod(Call, false);
                            HighlightClass(Call.CalledMethod.OwningClass.Name, false, Call.CalledObject.UniqueID);
                            HighlightObject(Call.CalledObject.UniqueID, false);
                            HighlightMethod(Call.CalledMethod, false);
                            HighlightObjectMethod(Call.CalledMethod.Name, Call.CalledObject.UniqueID, false);
                            HighlightEdge(Call.Relation?.RelationshipName, false, Call);
                            timeModifier = 1f;
                            break;
                    }

                    step++;
                    if (standardPlayMode)
                    {
                        yield return new WaitForSeconds(AnimationData.Instance.AnimSpeed * timeModifier);
                    }
                    //Else means we are working with step animation
                    else
                    {
                        if (step == 2) step = 3;
                        nextStep = false;
                        prevStep = false;
                        yield return new WaitUntil(() => nextStep);
                        if (prevStep)
                        {
                            if (step > 0) step--;
                            step = UnhighlightAllStepAnimation(Call, step);

                            if (step > -1) step--;
                            step = UnhighlightAllStepAnimation(Call, step);
                        }

                        yield return new WaitForFixedUpdate();
                        nextStep = false;
                        prevStep = false;
                    }
                }
            }

            IncrementBarrier();
        }

        private int UnhighlightAllStepAnimation(MethodInvocationInfo Call, int step)
        {
            if (step == 2) step = 1;
            switch (step)
            {
                case 0:
                    HighlightClass(Call.CallerMethod.OwningClass.Name, false);
                    HighlightObjects(Call, false);
                    break;
                case 1:
                    HighlightMethod(Call.CallerMethod, false);
                    HighlightInstancesMethod(Call, false);
                    break;
                case 3:
                    HighlightEdge(Call.Relation.RelationshipName, false, Call);
                    break;
                case 4:
                    HighlightClass(Call.CalledMethod.OwningClass.Name, false, Call.CalledObject.UniqueID);
                    HighlightObject(Call.CalledObject.UniqueID, false);
                    break;
                case 5:
                    HighlightMethod(Call.CalledMethod, false);
                    HighlightObjectMethod(Call.CalledMethod.Name, Call.CalledObject.UniqueID, false);
                    break;
            }

            return step;
        }

        public string GetColorCode(string type)
        {
            if (type == "class")
            {
                return ColorUtility.ToHtmlStringRGB(classColor);
            }

            if (type == "method")
            {
                return ColorUtility.ToHtmlStringRGB(methodColor);
            }

            if (type == "relation")
            {
                return ColorUtility.ToHtmlStringRGB(relationColor);
            }

            return "";
        }

        //Method used to stop all animations and unhighlight all objects
        public void UnhighlightAll()
        {
            isPaused = false;
            StopAllCoroutines();
            if (DiagramPool.Instance.ClassDiagram.GetClassList() != null)
                foreach (Class c in DiagramPool.Instance.ClassDiagram.GetClassList())
                {
                    HighlightClass(c.Name, false);
                    if (c.Methods != null)
                    {
                        foreach (Method m in c.Methods)
                        {
                            HighlightMethod(c, m, false);
                        }
                    }
                }

            if (DiagramPool.Instance.ClassDiagram.GetRelationList() != null)
                foreach (Relation r in DiagramPool.Instance.ClassDiagram.GetRelationList())
                {
                    HighlightEdge(r.OALName, false, null);
                }

            AnimationIsRunning = false;
        }

        public void Pause()
        {
            if (isPaused)
            {
                isPaused = false;
            }
            else
            {
                isPaused = true;
            }
        }

        public void NextStep()
        {
            if (AnimationIsRunning == false)
                StartAnimation();
            else
                nextStep = true;
        }

        public void PrevStep()
        {
            nextStep = true;
            prevStep = true;
        }
    }
}

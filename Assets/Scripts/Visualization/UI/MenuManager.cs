using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Util;
using OALProgramControl;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Visualisation.Animation;
using Visualization.Animation;
using Visualization.ClassDiagram;
using Visualization.ClassDiagram.ClassComponents;
using Visualization.ClassDiagram.ComponentsInDiagram;
using UnityEngine.Localization.Settings;
using AnimArch.Extensions;
using UnityEditor;
using Visualization.ClassDiagram.Editors;

namespace Visualization.UI
{
    public class MenuManager : Singleton<MenuManager>
    {
        FileLoader fileLoader;

        //UI Panels
        [SerializeField] private GameObject animationScreen;
        [SerializeField] private GameObject mainScreen;
        [SerializeField] private Button saveBtn;
        [SerializeField] private TMP_Text diagramPathLabel;
        [SerializeField] private Button diagramRemoveBtn;
        [SerializeField] private TMP_Text animationLabel;
        [SerializeField] private Button animationRemoveBtn;
        [SerializeField] private TMP_InputField scriptCode;
        [SerializeField] private GameObject PanelColors;
        [SerializeField] private GameObject PanelInteractiveIntro;
        [SerializeField] private GameObject PanelInteractive;
        [SerializeField] private GameObject PanelMethod;
        [SerializeField] public Button generatePythonBtn;
        public bool isCreating = false;
        [SerializeField] private List<GameObject> methodButtons;
        [SerializeField] private TMP_Text ClassNameTxt;
        [SerializeField] private GameObject InteractiveText; // Modify to have static text - Click class in the diagram to start editing (InteractiveText.GetComponent<DotsAnimation>().currentText)
        [SerializeField] private GameObject PanelInteractiveShow; // To be deleleted
        [SerializeField] private TMP_Text classFromTxt; // To be deleleted
        [SerializeField] private TMP_Text classToTxt; // To be deleleted
        [SerializeField] private TMP_Text methodFromTxt; // To be deleleted
        [SerializeField] private TMP_Text methodToTxt; // To be deleleted
        [SerializeField] private GameObject PanelInteractiveCompleted;
        [SerializeField] private Slider speedSlider;
        [SerializeField] private TMP_Text speedLabel;
        private string interactiveSource = "source";
        private string sourceClassName = "";
        [SerializeField] public GameObject panelAnimationPlay;
        [SerializeField] public GameObject panelStepMode;
        [SerializeField] public GameObject panelPlayMode;
        [SerializeField] private TMP_InputField sepInput;
        [SerializeField] private TMP_Text classTxt;
        [SerializeField] private TMP_Text methodTxt;
        [SerializeField] private Toggle hideRelToggle;
        [SerializeField] public GameObject PanelChooseAnimationStartMethod;
        [SerializeField] public GameObject PanelSourceCodeAnimation;
        [SerializeField] public GameObject ShowErrorBtn;
        [SerializeField] public GameObject ErrorPanel;

        // [SerializeField] public TMP_Text MaskingFileLabel;
        // [SerializeField] public Button RemoveMaskingBtn;
        public Anim createdAnim;
        public bool isPlaying = false;
        public Button[] playBtns;
        public GameObject playIntroTexts;
        public List<AnimMethod> animMethods;
        public bool isSelectingNode;

        public void SetLanguage(int language)
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[language];
            // pokus o zmenu jazyka na slovencinu pri animacii select class...
            // var tableReference = LocalizationSettings.StringDatabase.GetTable("StringTable");
            // var tableEntryReference = tableReference.GetEntry("playIntro1Key");
            // InteractiveText.GetComponent<DotsAnimation>().currentText = tableEntryReference.GetLocalizedString();
            // Debug.LogError("SetLanguage()");
            // Debug.LogError(tableEntryReference.GetLocalizedString());
        }

        // executed on pressing show error button

        private MethodPagination SourceCodeMethodPagination;
        private MethodPagination StartingMethodPagination;

        public void ShowErrorPanel()
        {
            ShowErrorPanel(null);
        }

        public void ShowErrorPanel(EXEExecutionResult executionResult = null) 
        {
            ShowErrorBtn.GetComponent<Button>().interactable = false;
            ErrorPanel.SetActive(true);
            ErrorPanel.GetComponent<ExecutionErrorPanel>().FillPanel(executionResult);
        }

        // executed on pressing X button
        public void HideErrorPanel() 
        {
            ShowErrorBtn.GetComponent<Button>().interactable = true;
            ErrorPanel.SetActive(false);
        }

        // executed after stopping or rerunning animation
        public void HideErrorPanelOnStopButton() 
        {
            Debug.Log("Error panel Removed!");
            ErrorPanel.SetActive(false);
            ShowErrorBtn.GetComponent<Button>().interactable = false;
        }

        class InteractiveData
        {
            public Subject<string> ClassClickedInClassDiagram;
            public Subject<string> CurrentMethodOwningClass;
            public Subject<string> CurrentMethod;

            public InteractiveData()
            {
                this.ClassClickedInClassDiagram = new Subject<string>(string.Empty);
                this.CurrentMethodOwningClass = new Subject<string>(string.Empty);
                this.CurrentMethod = new Subject<string>(string.Empty);
            }

            public void Clear()
            {
                this.ClassClickedInClassDiagram.SetValue(string.Empty);
                this.CurrentMethodOwningClass.SetValue(string.Empty);
                this.CurrentMethod.SetValue(string.Empty);
            }
        }


        InteractiveData interactiveData = new InteractiveData();

        private void Awake()
        {
            fileLoader = GameObject.Find("FileManager").GetComponent<FileLoader>();

            this.interactiveData = new InteractiveData();
            this.interactiveData.ClassClickedInClassDiagram.Register((string value) => { ClassNameTxt.text = value; });
            this.interactiveData.CurrentMethodOwningClass.Register((string value) => { classTxt.text = value; });
            this.interactiveData.CurrentMethod.Register((string value) => { methodTxt.text = value; });

            // Method button pagination
            SourceCodeMethodPagination = new MethodPagination(methodButtons);
            StartingMethodPagination = new MethodPagination(playBtns.Select(btn => btn.gameObject).ToList());
        }

        private void Start()
        {
            UpdateSpeed();
        }

        public void SetDiagramPath(string diagramPath)
        {
            diagramPathLabel.text = diagramPath;
            diagramRemoveBtn.interactable = true;
        }

        public void RemoveDiagram()
        {
            UIEditorManager.Instance.mainEditor.ClearDiagram();
        }
        public void SetSelectedAnimation(string name)
        {
            animationLabel.text = name;
            animationRemoveBtn.interactable = true;
        }

        public void InitializeAnim()
        {
            createdAnim = new Anim("");
            createdAnim.Initialize();
            generatePythonBtn.interactable = true;
        }

        public void StartAnimate()
        {
            scriptCode.text = string.Empty;
            isCreating = true;
            PanelInteractiveIntro.SetActive(true);
            PanelMethod.SetActive(false);
            PanelInteractive.SetActive(true);
            PanelInteractiveCompleted.SetActive(false);
            animationScreen.SetActive(true);
            mainScreen.SetActive(false);
        }

        public void EndAnimate()
        {
            isCreating = false;
            PanelInteractiveIntro.SetActive(false);
            PanelInteractive.SetActive(false);
            animationScreen.SetActive(false);
            saveBtn.interactable = false;
            mainScreen.SetActive(true);
            PanelInteractiveCompleted.SetActive(false);
        }

        public void SelectClass(String name)
        {

            // Save animation code
            SaveCurrentAnimation();

            // Unhighlight
            UnselectMethod();
            UnselectClass();

            // Set and highlight currently selected classs
            interactiveData.ClassClickedInClassDiagram.SetValue(name);
            Animation.Animation.Instance.HighlightClass(interactiveData.ClassClickedInClassDiagram.GetValue(), true);

            // Setup method buttons
            Class selectedClass = DiagramPool.Instance.ClassDiagram.FindClassByName(name).ParsedClass;
            PanelInteractiveIntro.SetActive(false);
            
            PanelMethod.SetActive(true);
            SourceCodeMethodPagination.FillItems(selectedClass.Methods.Select(method => method.Name).ToList());

            PanelInteractiveIntro.SetActive(false);
            PanelMethod.SetActive(true);
        }

        private void UnselectClass()
        {
            if (!string.IsNullOrEmpty(interactiveData.ClassClickedInClassDiagram.GetValue()))
            {
                Animation.Animation.Instance.HighlightClass(interactiveData.ClassClickedInClassDiagram.GetValue(), false);
            }

            if (!string.IsNullOrEmpty(interactiveData.CurrentMethodOwningClass.GetValue()))
            {
                Animation.Animation.Instance.HighlightClass(interactiveData.CurrentMethodOwningClass.GetValue(), false);
            }
        }

        public void SelectMethod(int buttonID)
        {
            string methodName = SourceCodeMethodPagination.GetSelectedItem(buttonID);

            interactiveData.CurrentMethodOwningClass.SetValue(interactiveData.ClassClickedInClassDiagram.GetValue());
            interactiveData.ClassClickedInClassDiagram.SetValue(string.Empty);
            interactiveData.CurrentMethod.SetValue(methodName);

            Animation.Animation.Instance.HighlightMethod
            (
                interactiveData.CurrentMethodOwningClass.GetValue(),
                interactiveData.CurrentMethod.GetValue(),
                true
            );
            sepInput.interactable = true;
            sepInput.text
                = createdAnim.GetMethodBody
                (
                    interactiveData.CurrentMethodOwningClass.GetValue(),
                    interactiveData.CurrentMethod.GetValue()
                );

            PanelInteractiveIntro.SetActive(true);
            PanelMethod.SetActive(false);
        }

        private void UnselectMethod()
        {
            if (!string.IsNullOrEmpty(interactiveData.CurrentMethod.GetValue()))
            {
                Animation.Animation.Instance.HighlightMethod(interactiveData.CurrentMethodOwningClass.GetValue(), interactiveData.CurrentMethod.GetValue(), false);
            }
        }

        //Save animation to file and memory
        private void SaveCurrentAnimation()
        {
            if
            (
                !string.IsNullOrEmpty(interactiveData.CurrentMethodOwningClass.GetValue())
                && !string.IsNullOrEmpty(interactiveData.CurrentMethod.GetValue())
            )
            {
                createdAnim.SetMethodCode
                (
                    interactiveData.CurrentMethodOwningClass.GetValue(),
                    interactiveData.CurrentMethod.GetValue(),
                    sepInput.text
                );
            }
        }
        public void SaveAnimation()
        {
            SaveCurrentAnimation();

            scriptCode.gameObject.SetActive(true);

            scriptCode.GetComponent<CodeHighlighter>().RemoveColors();
            scriptCode.gameObject.SetActive(false);
            fileLoader.SaveAnimation(createdAnim);
            EndAnimate();
        }

        public void OpenAnimation()
        {
            StartAnimate();
            createdAnim = AnimationData.Instance.selectedAnim;
        }

        public void ActivatePanelColors(bool show)
        {
            PanelColors.SetActive(show);
        }

        public void UpdateSpeed()
        {
            if (speedSlider && speedLabel)
            {
                AnimationData.Instance.AnimSpeed = speedSlider.value;
                speedLabel.text = speedSlider.value.ToString() + "s";
            }
        }

        public void PlayAnimation()
        {
            Debug.Assert(!AnimationData.Instance.selectedAnim.AnimationName.Equals(""));

            TooltipManager.Instance.HideTooltip();  
            PanelChooseAnimationStartMethod.SetActive(true);
            PanelSourceCodeAnimation.SetActive(false);

            isPlaying = true;
            panelAnimationPlay.SetActive(true);
            mainScreen.SetActive(false);
            foreach (Button button in playBtns)
            {
                button.gameObject.SetActive(false);
            }

            playIntroTexts.SetActive(true);
            if (Animation.Animation.Instance.standardPlayMode)
            {
                panelStepMode.SetActive(false);
                panelPlayMode.SetActive(true);
            }
            else
            {
                panelPlayMode.SetActive(false);
                panelStepMode.SetActive(true);
            }
        }

        public void ResetInteractiveSelection()
        {
            if (!string.IsNullOrEmpty(interactiveData.CurrentMethodOwningClass.GetValue()))
            {
                Animation.Animation.Instance.HighlightClass(interactiveData.CurrentMethodOwningClass.GetValue(), false);
            }

            if (interactiveData.CurrentMethod.GetValue() != null)
            {
                Animation.Animation.Instance.HighlightMethod
                (
                    interactiveData.CurrentMethodOwningClass.GetValue(),
                    interactiveData.CurrentMethod.GetValue(),
                    false
                );
            }

            interactiveData.Clear();
            PanelInteractiveIntro.SetActive(true);
            PanelMethod.SetActive(false);
        }

        public void ChangeMode()
        {
            Animation.Animation.Instance.UnhighlightAll();
            Animation.Animation.Instance.isPaused = false;
            if (Animation.Animation.Instance.standardPlayMode)
            {
                Animation.Animation.Instance.standardPlayMode = false;
                panelPlayMode.SetActive(false);
                panelStepMode.SetActive(true);
            }
            else
            {
                Animation.Animation.Instance.standardPlayMode = true;
                panelStepMode.SetActive(false);
                panelPlayMode.SetActive(true);
            }
        }

        public void SelectPlayClass(string name)
        {
            DiagramPool.Instance.ObjectDiagram.ResetDiagram();
            DiagramPool.Instance.ObjectDiagram.LoadDiagram();

            Animation.Animation.Instance.UnhighlightAll();
            Animation.Animation.Instance.HighlightClass(name, true);

            playIntroTexts.SetActive(false);
            Animation.Animation.Instance.startClassName = name;

            Class selectedClass = DiagramPool.Instance.ClassDiagram.FindClassByName(name).ParsedClass;
            animMethods = AnimationData.Instance.selectedAnim.GetMethodsByClassName(name);
            StartingMethodPagination.FillItems(animMethods.Select(method => method.Name).ToList());
        }

        public void SelectPlayMethod(int id)
        {
            string startMethodName = StartingMethodPagination.GetSelectedItem(id);
            string startClassName = Animation.Animation.Instance.startClassName;

            Animation.Animation.Instance.startMethodName = startMethodName;
            foreach (Button button in playBtns)
            {
                button.gameObject.SetActive(false);
            }

            playIntroTexts.SetActive(true);
            Debug.Log("Selected class: " + startClassName + "Selected Method: " + startMethodName);
            Animation.Animation.Instance.HighlightClass(startClassName, false);
        }

        public void UnshowAnimation()
        {
            Animation.Animation.Instance.UnhighlightAll();
        }

        public void EndPlay()
        {
            isPlaying = false;
            foreach (Button button in playBtns)
            {
                button.gameObject.SetActive(false);
            }

            Animation.Animation.Instance.startClassName = "";
            Animation.Animation.Instance.startMethodName = "";
        }

        public void HideGraphRelations()
        {
            if (hideRelToggle.isOn)
            {
                foreach (var interGraphRelation in DiagramPool.Instance.RelationsClassToObject)
                {
                    interGraphRelation.Show();
                }
            }
            else
            {
                foreach (var interGraphRelation in DiagramPool.Instance.RelationsClassToObject)
                {
                    interGraphRelation.Hide();
                }
            }
        }

        public static void SetAnimationButtonsActive(bool active)
        {
            GameObject.Find("AnimationSettings/Buttons/Edit").GetComponentInChildren<Button>().interactable = active; 
            GameObject.Find("AnimationSettings/Buttons/Play").GetComponentInChildren<Button>().interactable = active;
            GameObject.Find("GenerateToPythonButton").GetComponentInChildren<Button>().interactable = active;
            // generatePythonBtn.interactable = true; // TODO co je lepsie? takto by sme museli zmenit funciu zo static
        }
        public void AnimateSourceCodeAtMethodStart(EXEScopeMethod currentMethodScope)
        {
            PanelChooseAnimationStartMethod.SetActive(false);
            PanelSourceCodeAnimation.SetActive(true);

            PanelSourceCodeAnimation
                .GetComponent<PanelSourceCodeAnimation>()
                .SetMethodLabelText(currentMethodScope.MethodDefinition.OwningClass.Name, currentMethodScope.MethodDefinition.Name);

            VisitorCommandToString visitor = VisitorCommandToString.BorrowAVisitor();
            visitor.ActivateHighlighting();
            currentMethodScope.Accept(visitor);
            string sourceCode = visitor.GetCommandStringAndResetStateNow();
            PanelSourceCodeAnimation.GetComponent<PanelSourceCodeAnimation>().SetSourceCodeText(sourceCode);
        }
    }
}

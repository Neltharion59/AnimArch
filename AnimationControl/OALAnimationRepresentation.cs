using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace AnimationControl
{
    public class OALAnimationRepresentation
    {
        public CDClassPool ExecutionNameSpace { get; }

        public List<CDRelationship> RelationshipSpace { get; }
        public String DeclarationPartCode { get; set; }
        public String RelationShipDeclarationCode { get; set; }
        public String ExecutionPartCode { get; set; }

        private OALCodeConstructor CodeConstructor;
        public OALAnimationRepresentation()
        {
            this.ExecutionNameSpace = new CDClassPool();
            this.CodeConstructor = new OALCodeConstructor();
            this.DeclarationPartCode = "";
            this.RelationShipDeclarationCode = "";
            this.ExecutionPartCode = "";

            this.RelationshipSpace = new List<CDRelationship>();
        }

        public OALAnimationRepresentation ProduceCleanMirror()
        {
            OALAnimationRepresentation DeepClone = null;
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, this);
                ms.Position = 0;

                DeepClone = (OALAnimationRepresentation) formatter.Deserialize(ms);
            }

            foreach (CDClass Class in DeepClone.ExecutionNameSpace.ClassPool)
            {
                Class.Instances.Clear();
            }
            foreach (CDRelationship Relationship in DeepClone.RelationshipSpace)
            {
                Relationship.ClearRelationships();
            }

            return DeepClone;
        }

        public String TranslateToCode()
        {
            List<(String, String)> MockedAttributes = new List<(String, String)>();
            foreach (CDClass Class in ExecutionNameSpace.ClassPool)
            {
                foreach (CDAttribute Attribute in Class.Attributes)
                {
                    if (Attribute.IsMockedByCompiler)
                    {
                        String InstanceName = Class.Instances[0].ReferencingVariables[0];
                        String AttributeName = Attribute.Name;
                        MockedAttributes.Add((InstanceName, AttributeName));
                    }
                }
            }

            (List<String>, List<String>, List<String>) MethodTupple = this.ExecutionNameSpace.CreateDefinedMethodsTupple();
            String Code = CodeConstructor.ConstructFullCode(
                this.DeclarationPartCode,
                this.RelationShipDeclarationCode,
                this.ExecutionPartCode,
                MockedAttributes,
                MethodTupple.Item1,
                MethodTupple.Item2,
                MethodTupple.Item3
            );

            return Code;
        }

        public String TranslateToMainCode()
        {
            List<(String, String)> MockedAttributes = new List<(String, String)>();
            foreach (CDClass Class in ExecutionNameSpace.ClassPool)
            {
                foreach (CDAttribute Attribute in Class.Attributes)
                {
                    if (Attribute.IsMockedByCompiler)
                    {
                        String InstanceName = Class.Instances[0].ReferencingVariables[0];
                        String AttributeName = Attribute.Name;
                        MockedAttributes.Add((InstanceName, AttributeName));
                    }
                }
            }

            String Code = CodeConstructor.ConstructMainCode(
                this.DeclarationPartCode,
                this.RelationShipDeclarationCode,
                this.ExecutionPartCode,
                MockedAttributes
            );

            return Code;
        }
        public Boolean AddCallToAnimation(string CallerClassName, string CallerMethodName, string RelationshipName, string CalledClassName, string CalledMethodName)
        {
            if (
                !this.ExecutionNameSpace.ClassExists(CallerClassName)
                || !this.ExecutionNameSpace.ClassExists(CalledClassName)
            )
            {
                return false;
            }

            if (
                !this.ExecutionNameSpace.MethodExists(CallerClassName, CallerMethodName)
                || !this.ExecutionNameSpace.MethodExists(CalledClassName, CalledMethodName)
            )
            {
                return false;
            }

            CDRelationship CallRelationship = null;
            foreach (CDRelationship Relationship in this.RelationshipSpace)
            {
                if ((Relationship.FromClass == CallerClassName && Relationship.ToClass == CalledClassName)
                    || (Relationship.FromClass == CalledClassName && Relationship.ToClass == CallerClassName))
                {
                    CallRelationship = Relationship;
                    break;
                }
            }
            if (CallRelationship == null)
            {
                return false;
            }

            String CallerInstanceName = GetExistingClassInstanceName(CallerClassName);
            if (CallerInstanceName == null)
            {
                CallerInstanceName = GenerateNewInstanceName(CallerClassName);
                FindClassByName(CallerClassName).CreateClassInstance(CallerInstanceName);
                this.DeclarationPartCode = this.CodeConstructor.ConstructDeclarationPartCode(this.DeclarationPartCode, CallerInstanceName, CallerClassName);
            }
            

            String CalledInstanceName = GetExistingClassInstanceName(CalledClassName);
            if (CalledInstanceName == null)
            {
                CalledInstanceName = GenerateNewInstanceName(CalledClassName);
                FindClassByName(CalledClassName).CreateClassInstance(CalledInstanceName);
                this.DeclarationPartCode = this.CodeConstructor.ConstructDeclarationPartCode(this.DeclarationPartCode, CalledInstanceName, CalledClassName);
            }

            if (CallRelationship.CreateRelationship(CallerInstanceName, CalledInstanceName))
            {
                this.RelationShipDeclarationCode = this.CodeConstructor.ConstructRelationShipDeclarationPartCode(this.RelationShipDeclarationCode, CallerInstanceName, CalledInstanceName, CallRelationship.RelationshipName);
            }

            Boolean success = AddCall(CallerClassName, CallerMethodName, CallerInstanceName,
                CalledClassName, CalledMethodName, CalledInstanceName,
                CallRelationship.RelationshipName);

            return success;
        }
        private String GetExistingClassInstanceName(string ClassName)
        {
            CDClass Class = FindClassByName(ClassName);
            if (!Class.Instances.Any())
            {
                return null;
            }
            if (!Class.Instances.First().ReferencingVariables.Any())
            {
                return null;
            }
            String InstanceName = Class.Instances.First().ReferencingVariables.First();

            return InstanceName;
        }
        private String GenerateNewInstanceName(string ClassName)
        {
            CDClass Class = FindClassByName(ClassName);
            String NewInstanceName = "";
            
            String TempWord = "";
            foreach (char c in ClassName)
            {
                if (Char.IsUpper(c))
                {
                    if (TempWord != "")
                    {
                        if (!(NewInstanceName == ""))
                        {
                            NewInstanceName += "_";
                        }
                        NewInstanceName += TempWord;
                        TempWord = "";
                    }

                    TempWord += Char.ToLower(c);
                }
                else
                {
                    TempWord += c;
                }
            }
            if (TempWord != "")
            {
                NewInstanceName += TempWord;
            }

            return NewInstanceName;
        }
        private Boolean AddCall(string CallerClassName, string CallerMethodName, string CallerInstanceName, string CalledClassName, string CalledMethodName, string CalledInstanceName, String RelationshipName) {

            if(!(AnimationTuppleExists(CallerClassName, CallerMethodName, CallerInstanceName) && AnimationTuppleExists(CalledClassName, CalledMethodName, CalledInstanceName)))
            {
                return false;
            }

            CDMethod CallerMethod = FindMethodByName(CallerClassName, CallerMethodName);
            if (CallerMethod == null)
            {
                return false;
            }

            CallerMethod.OALCode = this.CodeConstructor.ConstructMethodDefinitionCode(CallerMethod.OALCode, CallerMethod.CallCountInAnimation, CalledInstanceName, CallerMethodName, CalledClassName, RelationshipName);
            CallerMethod.IncementCallCount();

            this.ExecutionPartCode = this.CodeConstructor.ConstructExecutionPartCode(this.ExecutionPartCode, CallerInstanceName, CallerMethodName);

            return true;
        }

        private Boolean AnimationTuppleExists(String ClassName, String MethodName, String InstanceName)
        {
            CDClass SearchedClass = FindClassByName(ClassName);

            if(SearchedClass == null)
            {
                return false;
            }

            Boolean InstanceNameExists = false;
            foreach (CDClassInstance Instance in SearchedClass.Instances)
            {
                foreach (String Name in Instance.ReferencingVariables)
                {
                    if (Name.Equals(InstanceName))
                    {
                        InstanceNameExists = true;
                    }
                }
            }
            if (!InstanceNameExists)
            {
                return false;
            }

            CDMethod SearchedMethod = FindMethodByName(ClassName, MethodName);

            if (SearchedMethod == null)
            {
                return false;
            }

            return true;
        }

        private CDClass FindClassByName(String ClassName)
        {
            CDClass SearchedClass = null;
            foreach (CDClass Class in this.ExecutionNameSpace.ClassPool)
            {
                if (Class.Name.Equals(ClassName))
                {
                    SearchedClass = Class;
                    break;
                }
            }

            return SearchedClass;
        }
        private CDMethod FindMethodByName(String ClassName, String MethodName)
        {
            CDClass SearchedClass = FindClassByName(ClassName);

            if (SearchedClass == null)
            {
                return null;
            }

            CDMethod SearchedMethod = null;
            foreach (CDMethod Method in SearchedClass.Methods)
            {
                if (Method.Name.Equals(MethodName))
                {
                    SearchedMethod = Method;
                    break;
                }
            }

            return SearchedMethod;
        }
    }
}
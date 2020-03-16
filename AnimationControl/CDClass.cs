using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{

    public class CDClass
    {
        public string Name { get; set; }
        public List<CDAttribute> Attributes { get; set; }
        public List<CDMethod> Methods { get; set; }
        public List<CDClassInstance> Instances { get; set; }
        private long ClassIDPrefix;
        private long InstanceIDSeed;

        public CDClass(long ClassIDPrefix, String Name)
        {
            this.ClassIDPrefix = ClassIDPrefix;
            this.InstanceIDSeed = 0;
            this.Name = Name;

            this.Attributes = new List<CDAttribute>();


            this.Methods = new List<CDMethod>();

            this.Instances = new List<CDClassInstance>();
        }

        public CDClassInstance CreateClassInstance(String InstanceName)
        {
            long NewInstanceID = ConstructNewInstanceUniqueID();
            this.InstanceIDSeed++;

            CDClassInstance Instance = new CDClassInstance(NewInstanceID, this.Attributes);
            Instance.AddReferencingVariable(InstanceName);
            this.Instances.Add(Instance);

            return Instance;
        }
        public CDClassInstance FindInstanceByName(String InstanceName)
        {
            CDClassInstance SearchedInstance = null;
            foreach (CDClassInstance Instance in this.Instances)
            {
                foreach (String VariableName in Instance.ReferencingVariables)
                {
                    if (VariableName == InstanceName)
                    {
                        SearchedInstance = Instance;
                        break;
                    }
                }
                if (SearchedInstance != null)
                {
                    break;
                }
            }
            return SearchedInstance;
        }
        private long ConstructNewInstanceUniqueID()
        {
            int DigitCount = this.InstanceIDSeed.ToString().Length;
            long NewInstanceID = ClassIDPrefix * PowerOf(10, DigitCount) + InstanceIDSeed;

            return NewInstanceID;
        }

        public Boolean AddMethod(CDMethod NewMethod)
        {
            Boolean Result = true;
            foreach (CDMethod Method in this.Methods)
            {
                if (Method.Name == NewMethod.Name)
                {
                    Result = false;
                    break;
                }
            }
            if (Result)
            {
                this.Methods.Add(NewMethod);
                this.AddMethodCallCounterAttribute(NewMethod.Name);
            }

            return Result;
        }

        public Boolean AddAttribute(CDAttribute NewAttribute)
        {
            Boolean Result = true;
            foreach (CDAttribute Attribute in this.Attributes)
            {
                if (Attribute.Name == NewAttribute.Name)
                {
                    Result = false;
                    break;
                }
            }
            if (Result)
            {
                this.Attributes.Add(NewAttribute);
            }

            return Result;
        }

        public Boolean MethodExists(String MethodName)
        {
            Boolean Result = false;

            foreach (CDMethod Method in this.Methods)
            {
                if (Method.Name.Equals(MethodName))
                {
                    Result = true;
                    break;
                }
            }

            return Result;
        }

        private void AddMethodCallCounterAttribute(String MethodName)
        {
            String MockAttributeName = "call_count_" + MethodName;
            CDAttribute Attribute = new CDAttribute(MockAttributeName, EXETypes.IntegerTypeName, true);
            this.Attributes.Add(Attribute);
        }

        private int PowerOf(int x, int power)
        {
            int result = 1;
            for (int i = 0; i < power; i++)
            {
                result *= x;
            }

            return result;
        }
    }
}

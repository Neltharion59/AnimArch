using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{
    public class EXECommandQueryCreate : EXECommandInterface
    {
        private String ReferencingVariableName { get; set; }
        private String ClassName { get; set; }

        public EXECommandQueryCreate(String ClassNamem, String ReferencingVariableName)
        {
            this.ReferencingVariableName = ReferencingVariableName;
            this.ClassName = ClassName;
        }

        public EXECommandQueryCreate(String ClassName)
        {
            this.ReferencingVariableName = "";
            this.ClassName = ClassName;
        }

        // SetUloh2
        public bool Execute(CDClassPool ExecutionSpace, CDRelationshipPool RelationshipSpace, EXEScope Scope)
        {
			//Create an instance of given class -> will affect ExecutionSpace.
            //If ReferencingVariableName is provided (is not ""), create a referencing variable pointing to this instance -> will affect scope
			CDClass Class = ExecutionSpace.getClassByName(ClassName);
			if(ReferencingVaiableName != "")
			{
				if(Class.FindInstanceByName(ReferencingVariableName) != null)
				{
					return false;
				}
				CDClassInstance instance = Class.CreateClassInstance(ReferencingVariableName);
            }
			else
			{
				long NewInstanceID = Class.ConstructNewInstanceUniqueID();
				CDClassInstance Instance = new CDClassInstance(NewInstanceID, Class.Attributes);
				Class.Instances.Add(Instance);
			}
			return true;
            throw new NotImplementedException();
        }

        //Ignore all methods below this comment
        public string GetCode()
        {
            throw new NotImplementedException();
        }

        public void Parse(EXEScope SuperScope)
        {
            throw new NotImplementedException();
        }

        public void PrintAST()
        {
            throw new NotImplementedException();
        }

        public string PrintSelf(bool IsTopLevel)
        {
            throw new NotImplementedException();
        }
    }
}

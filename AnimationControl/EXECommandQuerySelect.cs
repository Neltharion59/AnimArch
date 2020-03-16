using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{
    public class EXECommandQuerySelect
    {
        public const String CardinalityAny = "any";
        public const String CardinalityMany = "many";

        public String Cardinality { get; set; }
        public String ClassName { get; set; }

        public String VariableName { get; set; }

        public EXEASTNode WhereCondition { get; set; }

        public EXECommandQuerySelect()
        {
            this.ClassName = null;
            this.Cardinality = null;
            this.VariableName = null;
            this.WhereCondition = null;
        }

        // SetUloh2
        public bool Execute(CDClassPool ExecutionSpace, CDRelationshipPool RelationshipSpace, EXEScope Scope)
        {
			if(!ExecutionSpace.ClassExists(ClassName))
			{
				return false;
			}
			if(Cardinality == CardinalityAny)
			{
				bool exists = false;
				EXEReferencingVariable variable = Scope.FindReferencingVariableByName(VariableaName);
				if(variable != null && variable.ClassName != ClassName)
				{
					return false;
				}
				else if(variable != null)
				{
					CDClassInstance Instance = variable.RetrieveReferencedClassInstance(ExecutionSpace);
					Instance.RemoveReferencingVariables(VariableName);
					exists = true;
				}
				

				var random = new Random();
				CDClass Class = ExecutionSpace.getClassByName(ClassName);
				int index = random.Next(Class.Instances.Count);
				if(!exists)
				{
					variable = new EXEReferencingVariable(VariableName, ClassName, (Class.Instances[index]).UniqueID);
				}
				Scope.ReferencingVariables.Add(variable);
				(Class.Instances[index]).AddReferencingVariable(VariableName);				
				
				return true;
			}
			else
			{
				bool exists = false;
				EXEReferencingSetVariable variable = Scope.FindReferencingSetVariableByName(VariableaName);
				if(variable != null && variable.ClassName != ClassName)
				{
					return false;
				}
				
				for(EXEReferencingSetVariable setVar in Scope.SetReferencingVariables)
				{	
					if(setVar == variable)
					{
						variable = new EXEReferencingSetVariable(VariableName, ClassName);
						break;
					}
				}
				
				CDClass Class = ExecutionSpace.getClassByName(ClassName);
				for(CDClassInstance Instance in CDClass.Instances)
				{
					EXEReferencingVariable subVar = new EXEReferencingVariable("", ClassName, Instance.UniqueID);
					variable.AddReferencingVariable(subVar);
				}

				Scope.SetReferencingVariables.Add(variable);
				
				return true;
			}

			


            //Select instances of given class that match the criteria and assign them to variable with given name
            // ClassName tells us which class we are interested in
            // Cardinality tells us whether we want one random instance (matching the criteria) or all of them
            // "Many" - we create variable EXEReferencingSetVariable, "Any" - we create variable EXEReferencingVariable
            // Variable name tells us how to name the newly created referencing variable
            // Where condition tells us which instances to select from all instances of the class (just do EXEASTNode.Evaluate and return true if the result "true" and false for "false")
            // When making unit tests, do not use the "where" causule yet, because its evaluation is not yet implemented
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

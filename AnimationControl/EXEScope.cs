using System;
using System.Collections.Generic;
using System.Text;

namespace AnimationControl
{
    public class EXEScope : EXECommandInterface
    {
        public List<EXEPrimitiveVariable> PrimitiveVariables;
        public List<EXEReferencingVariable> ReferencingVariables;
        public List<EXEReferencingSetVariable> SetReferencingVariables;
        public EXEScope SuperScope;
        public List<EXECommandInterface> Commands;

        public String OALCode;

        public EXEScope()
        {
            this.PrimitiveVariables = new List<EXEPrimitiveVariable>();
            this.ReferencingVariables = new List<EXEReferencingVariable>();
            this.SetReferencingVariables = new List<EXEReferencingSetVariable>();
            this.SuperScope = null;
            this.Commands = new List<EXECommandInterface>();
        }

        // SetUloh1 - this method is done. Do the same with two similar methods below it
        public EXEPrimitiveVariable FindPrimitiveVariableByName(String Name)
        {
            EXEPrimitiveVariable Result = null;
            EXEScope CurrentScope = this;
            while (CurrentScope != null)
            {
                foreach (EXEPrimitiveVariable CurrentVariable in CurrentScope.PrimitiveVariables)
                {
                    if (CurrentVariable.Name == Name)
                    {
                        Result = CurrentVariable;
                        break;
                    }
                }

                if (Result != null)
                {
                    break;
                }

                CurrentScope = CurrentScope.SuperScope;
            }

            return Result;
        }
        public EXEReferencingVariable FindReferencingVariableByName(String Name)
        {
            throw new NotImplementedException();        
        }
        public EXEReferencingSetVariable FindSetReferencingVariableByName(String Name)
        {
            throw new NotImplementedException();
        }

        public Boolean EvaluateCondition()
        {
            return true;
        }

        public void AddCommand(EXECommandInterface Command)
        {
            this.Commands.Add(Command);
        }

        public String PrintSelf(Boolean IsTopLevel)
        {
            StringBuilder SelfPrintBuilder = new StringBuilder();
            String Indentation = IsTopLevel ? "" : "\t";


            SelfPrintBuilder.Append(Indentation);
            foreach (EXECommandInterface Command in this.Commands)
            {
                SelfPrintBuilder.Append(Indentation);
                SelfPrintBuilder.Append(Command.PrintSelf(false));
            }

            return SelfPrintBuilder.ToString();
        }

        //"Scope" param is ignored here
        public Boolean Execute(CDClassPool ExecutionSpace, CDRelationshipPool RelationshipSpace, EXEScope Scope)
        {
            Boolean Success = false;
            foreach(EXECommandInterface Command in this.Commands)
            {
                Success = Command.Execute(ExecutionSpace, RelationshipSpace, this);
                if (!Success)
                {
                    break;
                }
            }

            return Success;
        }

        public String GetCode()
        {
            return this.OALCode;
        }

        public void Parse(EXEScope SuperScope)
        {
            Console.WriteLine("ExeScope.Parse");
            OALParser Parser = new OALParser();
            Parser.DecomposeOALFragment(this);
            this.SuperScope = SuperScope;

            foreach (EXECommandInterface Command in this.Commands)
            {
                Command.Parse(this);
            }
        }

        public void PrintAST()
        {
            foreach (EXECommandInterface Command in this.Commands)
            {
                Command.PrintAST();
            }
        }
    }
}

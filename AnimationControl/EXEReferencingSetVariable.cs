using System;
using System.Collections.Generic;

namespace AnimationControl
{
    public class EXEReferencingSetVariable
    {
        public String Name { get; }
        public String ClassName { get; }
        private List<EXEReferencingVariable> ReferencingVariables;

        public EXEReferencingSetVariable(String Name, String ClassName)
        {
            this.Name = "";
            this.ClassName = "";
            this.ReferencingVariables = null;
        }

        public void AddReferencingVariable(EXEReferencingVariable NewReferencingVariable)
        {
            if (this.ReferencingVariables == null)
            {
                this.ReferencingVariables = new List<EXEReferencingVariable>();
            }

            this.ReferencingVariables.Add(NewReferencingVariable);
        }

        public List<EXEReferencingVariable> GetReferencingVariables()
        {
            return this.ReferencingVariables;
        }
    }
}

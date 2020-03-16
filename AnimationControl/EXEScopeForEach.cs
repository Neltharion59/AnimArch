using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{
    class EXEScopeForEach : EXEScope
    {
        public EXEReferencingVariable Iterator { get; set; }
        public EXEReferencingSetVariable Iterable { get; set; }

        public EXEScopeForEach()
        {
            this.Iterator = null;
            this.Iterable = null;
        }
        public Boolean Execute(CDClassPool ExecutionSpace, CDRelationshipPool RelationshipSpace, EXEScope Scope)
        {
            Boolean Success = false;
            
            this.ReferencingVariables.Add(this.Iterator);
            foreach (EXEReferencingVariable CurrentItem in this.Iterable.GetReferencingVariables())
            {
                this.Iterator.ReferencedInstanceId = CurrentItem.ReferencedInstanceId;

                Success = base.Execute(ExecutionSpace, RelationshipSpace, this);
                if (Success)
                {
                    break;
                }
            }

            return Success;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{
    class EXEScopeCondition : EXEScope
    {
        public EXEASTNode Condition { get; set; }
        private List<EXEScopeCondition> ElifScopes;
        public EXEScope ElseScope { get; set; }

        public EXEScopeCondition() : base()
        {
            this.Condition = null;
            this.ElifScopes = null;
            this.ElseScope = null;
        }

        public void AddElifScope(EXEScopeCondition ElifScope)
        {
            if (this.ElifScopes == null)
            {
                this.ElifScopes = new List<EXEScopeCondition>();
            }

            this.ElifScopes.Add(ElifScope);
        }

        // should evaluate to true only if base "if" is true
        public Boolean EvaluateCondition()
        {
            throw new NotImplementedException();
        }

        public Boolean Execute(CDClassPool ExecutionSpace, CDRelationshipPool RelationshipSpace, EXEScope Scope)
        {
            Boolean Result = false;
            Boolean AScopeWasExecuted = false;

            if (this.EvaluateCondition())
            {
                Result = base.Execute(ExecutionSpace, RelationshipSpace, this);
                AScopeWasExecuted = true;
            }

            if (AScopeWasExecuted)
            {
                return Result;
            }

            foreach(EXEScopeCondition CurrentElif in this.ElifScopes)
            {
                if (CurrentElif.EvaluateCondition())
                {
                    Result = CurrentElif.Execute(ExecutionSpace, RelationshipSpace, CurrentElif);
                    AScopeWasExecuted = true;
                    break;
                }
            }

            if (AScopeWasExecuted)
            {
                return Result;
            }

            Result = ElseScope.Execute(ExecutionSpace, RelationshipSpace, ElseScope);

            return Result;
        }
    }
}

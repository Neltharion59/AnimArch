using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{
    class EXEScopeLoopWhile : EXEScope
    {
        public EXEASTNode Condition;

        public EXEScopeLoopWhile(EXEASTNode Condition) : base()
        {
            this.Condition = Condition;
        }

        public Boolean EvaluateCondition()
        {
            throw new NotImplementedException();
        }
    }
}

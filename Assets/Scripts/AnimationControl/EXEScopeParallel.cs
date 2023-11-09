using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

namespace OALProgramControl
{
    public class EXEScopeParallel : EXEScope
    {
        public List<EXEScope> Threads { get;}
        public EXEScopeParallel(IEnumerable<EXEScope> threads) : base()
        {
            this.Threads = new List<EXEScope>();

            foreach (EXEScope thread in threads)
            {
                this.AddThread(thread);
            }
        }

        public void AddThread(EXEScope Thread)
        {
            Thread.SetSuperScope(this);
            this.Threads.Add(Thread);
        }
        protected override EXEExecutionResult Execute(OALProgram OALProgram)
        {
            this.CommandStack.Fork(this.Threads);

            return Success();
        }

        public override void Accept(Visitor v)
        {
            v.VisitExeScopeParallel(this);
        }

        protected override EXEScope CreateDuplicateScope()
        {
            return new EXEScopeParallel(Threads.Select(x => (EXEScope)x.CreateClone()));
        }
    }
}
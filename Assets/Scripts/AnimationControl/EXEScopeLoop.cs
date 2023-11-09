using System.Linq;

namespace OALProgramControl
{
    public abstract class EXEScopeLoop : EXEScope
    {
        protected abstract EXEExecutionResult HandleIterationStart(OALProgram OALProgram, out bool startNewIteration);
        protected override EXEExecutionResult Execute(OALProgram OALProgram)
        {
            bool startNewIteration;
            EXEExecutionResult iterationStartResult = HandleIterationStart(OALProgram, out startNewIteration);

            if (!HandleRepeatableASTEvaluation(iterationStartResult))
            {
                return iterationStartResult;
            }

            if (startNewIteration)
            {
                OALProgram.CommandStack.Enqueue(this);
                AddCommandsToStack
                (
                    this.Commands.Select(command => command.CreateClone()).ToList()
                );
                this.ClearVariables();
            }

            return Success();
        }

        public override void Accept(Visitor v)
        {
            v.VisitExeScopeLoop(this);
        }
    }
}
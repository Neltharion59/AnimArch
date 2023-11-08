namespace OALProgramControl
{
    public class EXECommandBreak : EXECommand
    {
        protected override EXEExecutionResult Execute(OALProgram OALProgram)
        {
            EXEScopeLoop currentLoop = this.SuperScope.GetCurrentLoopScope();

            if (currentLoop == null)
            {
                return Error("Tried to break out of something that is not a loop.", "XEC2019");
            }

            bool success = false;

            while (this.CommandStack.HasNext())
            {
                EXECommand peek = this.CommandStack.Next();

                if (peek == currentLoop)
                {
                    success = true;
                    break;
                }
            }

            if (!success)
            {
                return Error("Failed to find the current loop to break out from.", "XEC2020");
            }

            return Success();
        }
        public override void Accept(Visitor v) {
            v.VisitExeCommandBreak(this);
        }

        public override EXECommand CreateClone()
        {
            return new EXECommandBreak();
        }
    }
}

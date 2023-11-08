namespace OALProgramControl
{
    public class EXECommandContinue : EXECommand
    {
        protected override EXEExecutionResult Execute(OALProgram OALProgram)
        {
            EXEScopeLoop currentLoop = this.SuperScope.GetCurrentLoopScope();

            if (currentLoop == null)
            {
                return Error("Tried to continue in something that is not a loop.", "XEC2021");
            }

            bool success = false;

            while (this.CommandStack.HasNext())
            {
                EXECommand peek = this.CommandStack.Next();

                if (peek == currentLoop)
                {
                    this.CommandStack.Enqueue(peek);
                    success = true;
                    break;
                }
            }

            if (!success)
            {
                return Error("Failed to find the current loop to continue.", "XEC2022");
            }

            return Success();
        }
        public override void Accept(Visitor v) {
            v.VisitExeCommandContinue(this);
        }

        public override EXECommand CreateClone()
        {
            return new EXECommandContinue();
        }
    }
}

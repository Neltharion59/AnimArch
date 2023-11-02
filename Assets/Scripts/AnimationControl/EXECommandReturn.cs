
namespace OALProgramControl
{
    public class EXECommandReturn : EXECommand
    {
        private EXEASTNodeBase Expression { get; }

        public EXECommandReturn(EXEASTNodeBase Expression)
        {
            this.Expression = Expression;
        }

        protected override EXEExecutionResult Execute(OALProgram OALProgram)
        {
            EXEExecutionResult returnedExpressionEvaluationResult = null;

            if (Expression != null)
            {
                returnedExpressionEvaluationResult = Expression.Evaluate(this.SuperScope, OALProgram);

                if (!HandleRepeatableASTEvaluation(returnedExpressionEvaluationResult))
                {
                    return returnedExpressionEvaluationResult;
                }
            }

            EXEScopeMethod owningMethodScope = GetCurrentMethodScope();
            if (!owningMethodScope.CollectReturn(returnedExpressionEvaluationResult.ReturnedOutput, OALProgram))
            {
                return Error("Failed to deliver return value to owning scope.", "XEC2014");
            }

            return Success();
        }

        public override string ToCodeSimple()//------------------------------------------------
        {
            return this.Expression == null ? "return" : ("return " + this.Expression.ToCode());
        }
        public override void Accept(Visitor v)
        {
            v.VisitExeCommandReturn(this);
        }

        public override EXECommand CreateClone()
        {
            return new EXECommandReturn(Expression);
        }
    }
}

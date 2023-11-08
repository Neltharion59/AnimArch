using System;
using System.Collections.Generic;
using System.Linq;

namespace OALProgramControl
{
    public class EXECommandRemovingFromList : EXECommand
    {
        public EXEASTNodeBase Array { get; }
        public EXEASTNodeBase Item { get; }

        public EXECommandRemovingFromList(EXEASTNodeBase array, EXEASTNodeBase item)
        {
            this.Array = array;
            this.Item = item;
        }

        protected override EXEExecutionResult Execute(OALProgram OALProgram)
        {
            EXEExecutionResult itemEvaluationResult = this.Item.Evaluate(this.SuperScope, OALProgram);

            if (!HandleRepeatableASTEvaluation(itemEvaluationResult))
            {
                return itemEvaluationResult;
            }

            EXEExecutionResult arrayEvaluationResult = this.Array.Evaluate(this.SuperScope, OALProgram);

            if (!HandleRepeatableASTEvaluation(arrayEvaluationResult))
            {
                return arrayEvaluationResult;
            }

            EXEExecutionResult itemRemovalResult
                = arrayEvaluationResult.ReturnedOutput.RemoveElement(itemEvaluationResult.ReturnedOutput, OALProgram.ExecutionSpace);

            if (!HandleSingleShotASTEvaluation(itemRemovalResult))
            {
                return itemRemovalResult;
            }

            return Success();
        }

        public override void Accept(Visitor v)
        {
            v.VisitExeCommandRemovingFromList(this);
        }

        public override EXECommand CreateClone()
        {
            return new EXECommandRemovingFromList(this.Array.Clone() as EXEASTNodeAccessChain, this.Item.Clone());
        }
    }
}

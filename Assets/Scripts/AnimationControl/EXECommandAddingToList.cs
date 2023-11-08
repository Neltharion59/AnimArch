using System;
using System.Collections;
using System.Collections.Generic;

namespace OALProgramControl
{
    public class EXECommandAddingToList : EXECommand
    {
        public EXEASTNodeBase Array { get; }
        public EXEASTNodeBase AddedElement { get; }

        public EXECommandAddingToList(EXEASTNodeBase array, EXEASTNodeBase addedElement)
        {
            this.Array = array;
            this.AddedElement = addedElement;
        }

        protected override EXEExecutionResult Execute(OALProgram OALProgram)
        {
            EXEExecutionResult evaluationResultOfAddedElement = this.AddedElement.Evaluate(this.SuperScope, OALProgram);

            if (!HandleRepeatableASTEvaluation(evaluationResultOfAddedElement))
            {
                return evaluationResultOfAddedElement;
            }

            EXEExecutionResult evaluationResultOfArray = this.Array.Evaluate(this.SuperScope, OALProgram);

            if (!HandleRepeatableASTEvaluation(evaluationResultOfArray))
            {
                return evaluationResultOfArray;
            }

            EXEExecutionResult elementAppendmentResult
                = evaluationResultOfArray
                    .ReturnedOutput
                    .AppendElement(evaluationResultOfAddedElement.ReturnedOutput, OALProgram.ExecutionSpace);

            if (!HandleSingleShotASTEvaluation(elementAppendmentResult))
            {
                return elementAppendmentResult;
            }

            return Success();
        }

        public override void Accept(Visitor v)
        {
            v.VisitExeCommandAddingToList(this);
        }

        public override EXECommand CreateClone()
        {
            return new EXECommandAddingToList(this.Array.Clone(), this.AddedElement.Clone());
        }
        public CDClassInstance GetAssignmentTargetOwner()
        {
            return (this.Array as EXEASTNodeAccessChain)?.GetFinalValueOwner();
        }
        public CDClassInstance GetAppendedElementInstance()
        {
            return (this.AddedElement.EvaluationResult.ReturnedOutput as EXEValueReference)?.ClassInstance;
        }
    }
}

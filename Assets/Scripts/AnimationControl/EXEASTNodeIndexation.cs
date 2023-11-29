using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace OALProgramControl
{
    public class EXEASTNodeIndexation : EXEASTNodeBase
    {
        public EXEASTNodeBase List {get;}
        public EXEASTNodeBase Index {get;}

        public EXEASTNodeIndexation(EXEASTNodeBase list, EXEASTNodeBase index) : base()
        {
            this.List = list;
            this.Index = index;
        }

        public override EXEExecutionResult Evaluate(EXEScope currentScope, OALProgram currentProgramInstance, EXEASTNodeAccessChainContext valueContext = null)
        {
            if (this.EvaluationState == EEvaluationState.HasBeenEvaluated)
            {
                return this.EvaluationResult;
            }

            this.EvaluationState = EEvaluationState.IsBeingEvaluated;

            EXEExecutionResult operandExecutionResult;
            EXEExecutionResult operatorExecutionResult;

            operandExecutionResult = List.Evaluate(currentScope, currentProgramInstance);

            if (!operandExecutionResult.IsSuccess)
            {
                this.EvaluationState = EEvaluationState.HasBeenEvaluated;
                this.EvaluationResult = operandExecutionResult;
            }


                // Current operand evaluation finished and did not produce an error
                if (this.EvaluationResult == null)
                {
                    this.EvaluationResult = EXEExecutionResult.Success();
                    this.EvaluationResult.ReturnedOutput = operandExecutionResult.ReturnedOutput;
                    continue;
                }

                operatorExecutionResult = this.EvaluationResult.ReturnedOutput.ApplyOperator(this.Operation, operandExecutionResult.ReturnedOutput);

                if (!operatorExecutionResult.IsSuccess)
                {
                    this.EvaluationResult = operatorExecutionResult;
                    this.EvaluationState = EEvaluationState.HasBeenEvaluated;
                }

                if (!operatorExecutionResult.IsDone || !operatorExecutionResult.IsSuccess)
                {
                    // Either current operand evaluation did not finish or produced an error
                    return operatorExecutionResult;
                }

                this.EvaluationResult = EXEExecutionResult.Success();
                this.EvaluationResult.ReturnedOutput = operatorExecutionResult.ReturnedOutput;
            

            this.EvaluationState = EEvaluationState.HasBeenEvaluated;

            return this.EvaluationResult;
        }

        public override EXEASTNodeBase Clone()
        {
            return new EXEASTNodeIndexation(this.List.Clone(), this.Index.Clone());
        }

        public override void Accept(Visitor v)
        {
            v.VisitExeASTNodeIndexation(this);
        }
    }
}

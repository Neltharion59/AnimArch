using System;
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

            EXEExecutionResult executionResult;
            EXEValueArray evaluatedList;
            EXEValueInt evaluatedIndex;

            executionResult = Index.Evaluate(currentScope, currentProgramInstance, valueContext);
            if (!executionResult.IsSuccess)
            {
                this.EvaluationState = EEvaluationState.HasBeenEvaluated;
                this.EvaluationResult = executionResult;
            }
            evaluatedIndex = executionResult.ReturnedOutput as EXEValueInt;
            if (evaluatedIndex == null)
            {
                this.EvaluationState = EEvaluationState.HasBeenEvaluated;
                this.EvaluationResult = EXEExecutionResult.Error("Index used for indexing must be int!", "XEC1234");
            }
            if (evaluatedIndex.Value > UInt32.MaxValue)
            {
                this.EvaluationState = EEvaluationState.HasBeenEvaluated;
                this.EvaluationResult = EXEExecutionResult.Error("Index used for indexing must not be bigger than uint32 max!", "XEC1234");
            }
            if (evaluatedIndex.Value < 0)
            {
                this.EvaluationState = EEvaluationState.HasBeenEvaluated;
                this.EvaluationResult = EXEExecutionResult.Error("Index used for indexing must be bigger than 0!", "XEC1234");
            }


            executionResult = List.Evaluate(currentScope, currentProgramInstance, valueContext);
            if (!executionResult.IsSuccess)
            {
                this.EvaluationState = EEvaluationState.HasBeenEvaluated;
                this.EvaluationResult = executionResult;
            }
            evaluatedList = executionResult.ReturnedOutput as EXEValueArray;
            if (evaluatedList == null)
            {
                this.EvaluationState = EEvaluationState.HasBeenEvaluated;
                this.EvaluationResult = EXEExecutionResult.Error("List used for indexing to must be an array!", "XEC1234");
            }


            executionResult = evaluatedList.GetValueAt((UInt32)evaluatedIndex.Value);

            this.EvaluationState = EEvaluationState.HasBeenEvaluated;
            this.EvaluationResult = executionResult;

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

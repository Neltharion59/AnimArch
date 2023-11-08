using System;
using System.Collections.Generic;
using System.Linq;

namespace OALProgramControl
{
    public class EXECommandCreateList : EXECommand
    {
        public string ArrayType { get; }
        private string VariableType { get { return this.ArrayType + "[]"; } }
        public EXEASTNodeAccessChain AssignmentTarget { get; }
        public List<EXEASTNodeBase> Items { get; }

        public EXECommandCreateList(string arrayType, EXEASTNodeAccessChain assignmentTarget, List<EXEASTNodeBase> items)
        {
            this.ArrayType = arrayType;
            this.AssignmentTarget = assignmentTarget;
            this.Items = items;
        }

        protected override EXEExecutionResult Execute(OALProgram OALProgram)
        {
            EXEExecutionResult itemEvaluationResult = null;

            // Acquire the variable to assign the array to
            EXEExecutionResult evaluationResultOfAssignmentTarget
                = this.AssignmentTarget.Evaluate(this.SuperScope, OALProgram, new EXEASTNodeAccessChainContext() { CreateVariableIfItDoesNotExist = true, VariableCreationType = this.VariableType });

            if (!HandleRepeatableASTEvaluation(evaluationResultOfAssignmentTarget))
            {
                return evaluationResultOfAssignmentTarget;
            }

            // Prepare the items to populate the array with
            foreach (EXEASTNodeBase item in this.Items)
            {
                itemEvaluationResult = item.Evaluate(this.SuperScope, OALProgram);

                if (!HandleRepeatableASTEvaluation(itemEvaluationResult))
                {
                    return itemEvaluationResult;
                }
            }

            // Create the array
            EXEExecutionResult arrayCreationResult = EXEValueArray.CreateArray(this.VariableType);

            if (!HandleSingleShotASTEvaluation(arrayCreationResult))
            {
                return arrayCreationResult;
            }

            // Populate the array
            EXEValueArray array = arrayCreationResult.ReturnedOutput as EXEValueArray;
            array.InitializeEmptyArray();

            EXEExecutionResult itemAppendmentResult = null;
            foreach (EXEASTNodeBase item in this.Items)
            {
                itemAppendmentResult = array.AppendElement(item.EvaluationResult.ReturnedOutput, OALProgram.ExecutionSpace);
                if (!HandleSingleShotASTEvaluation(itemAppendmentResult))
                {
                    return itemAppendmentResult;
                }
            }

            // Perform the assignment
            EXEExecutionResult arrayAssignmentResult = evaluationResultOfAssignmentTarget.ReturnedOutput.AssignValueFrom(arrayCreationResult.ReturnedOutput);

            if (!HandleSingleShotASTEvaluation(arrayAssignmentResult))
            {
                return arrayAssignmentResult;
            }

            return Success();
        }

        public override void Accept(Visitor v)
        {
            v.VisitExeCommandCreateList(this);
        }

        public override EXECommand CreateClone()
        {
            return new EXECommandCreateList
            (
                this.ArrayType,
                this.AssignmentTarget.Clone() as EXEASTNodeAccessChain,
                this.Items
                    .Select(item => item.Clone())
                    .ToList()
            );
        }
    }
}

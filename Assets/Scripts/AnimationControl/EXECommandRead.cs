using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Visualization.UI;

namespace OALProgramControl
{
    public class EXECommandRead : EXECommand
    {
        public String AssignmentType { get; }
        public EXEASTNodeAccessChain AssignmentTarget { get; }
        public EXEASTNodeBase Prompt { get; }  // Must be String type

        public EXECommandRead(String assignmentType, EXEASTNodeAccessChain assignmentTarget, EXEASTNodeBase prompt)
        {
            this.AssignmentType = assignmentType ?? EXETypes.StringTypeName;
            this.AssignmentTarget = assignmentTarget;
            this.Prompt = prompt;
        }

        protected override EXEExecutionResult Execute(OALProgram OALProgram)
        {
            EXEExecutionResult assignmentTargetEvaluationResult
                = this.AssignmentTarget.Evaluate(this.SuperScope, OALProgram, new EXEASTNodeAccessChainContext() { CreateVariableIfItDoesNotExist = true, VariableCreationType = this.AssignmentType });

            if (!HandleRepeatableASTEvaluation(assignmentTargetEvaluationResult))
            {
                return assignmentTargetEvaluationResult;
            }

            EXEExecutionResult promptEvaluationResult = null;
            if (this.Prompt != null)
            {
                promptEvaluationResult  = this.Prompt.Evaluate(this.SuperScope, OALProgram);

                if (!HandleRepeatableASTEvaluation(promptEvaluationResult))
                {
                    return promptEvaluationResult;
                }
            }

            if (promptEvaluationResult.ReturnedOutput is not EXEValueString)
            {
                return Error(string.Format("Tried to read from console with prompt that is not string. Instead, it is \"{0}\".", promptEvaluationResult.ReturnedOutput.TypeName), "XEC2025");
            }

            string prompt = string.Empty;
            EXEValueString retOutput = promptEvaluationResult.ReturnedOutput as EXEValueString;
            if (retOutput != null) {
                VisitorCommandToString visitor = VisitorCommandToString.BorrowAVisitor();
                retOutput.Accept(visitor);
                prompt = visitor.GetCommandStringAndResetStateNow();
            }

            ConsolePanel.Instance.YieldOutput(prompt);

            return Success();
        }

        public EXEExecutionResult AssignReadValue(String Value, OALProgram OALProgram)
        {
            EPrimitiveType readValueType = EXETypes.DeterminePrimitiveType(Value);

            if (readValueType == EPrimitiveType.NotPrimitive)
            {
                return Error(ErrorMessage.InvalidValueForType(Value, this.AssignmentType), "XEC2026");
            }

            EXEValuePrimitive readValue = EXETypes.DeterminePrimitiveValue(Value);

            AssignmentTarget.EvaluationResult.ReturnedOutput.AssignValueFrom(readValue);

            return Success();
        }

        public override void Accept(Visitor v)
        {
            v.VisitExeCommandRead(this);
        }

        public override EXECommand CreateClone()
        {
            return new EXECommandRead(this.AssignmentType, this.AssignmentTarget.Clone() as EXEASTNodeAccessChain, this.Prompt.Clone());
        }
    }
}

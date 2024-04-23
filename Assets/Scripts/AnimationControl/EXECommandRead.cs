using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Visualization.UI;
using UnityEngine;

namespace OALProgramControl
{
    public class EXECommandRead : EXECommand
    {
        public String AssignmentType { get; }
        public EXEASTNodeAccessChain AssignmentTarget { get; }
        public EXEASTNodeBase Prompt { get; }  // Must be String type
        public string PromptText { get; private set; }

        public IStrategy Strategy = StrategyProduction.Instance;

        public EXECommandRead(String assignmentType, EXEASTNodeAccessChain assignmentTarget, EXEASTNodeBase prompt)
        {
            this.AssignmentType = assignmentType ?? EXETypes.StringTypeName;
            this.AssignmentTarget = assignmentTarget;
            this.Prompt = prompt;
        }

          public void SetStrategy(IStrategy Strategy){
            this.Strategy = Strategy;
        }

        protected override EXEExecutionResult Execute(OALProgram OALProgram)
        {
            Debug.LogError("commandRead najprv");

            EXEExecutionResult assignmentTargetEvaluationResult
                = this.AssignmentTarget.Evaluate(this.SuperScope, OALProgram, new EXEASTNodeAccessChainContext() { CreateVariableIfItDoesNotExist = true, VariableCreationType = this.AssignmentType });

            if (!HandleRepeatableASTEvaluation(assignmentTargetEvaluationResult))
            {
                Debug.LogError("commandRead HandleRepeatableASTEvaluation return");

                return assignmentTargetEvaluationResult;
            }

            //this.Strategy.Read(this);

            switch(this.Strategy.Read(this)){
                case 0:
                    return promptEvaluationResult;
                case 1:
                    return Error("XEC2025", string.Format("Tried to read from console with prompt that is not string. Instead, it is \"{0}\".", promptEvaluationResult.ReturnedOutput.TypeName));
                case 2:
                    return Success();
                default:
                    Debug.LogError("Something wrong happened in Strategy Read.");
                    return null;
            }

            
        }

        public EXEExecutionResult AssignReadValue(String Value, OALProgram OALProgram)
        {
            EPrimitiveType readValueType = EXETypes.DeterminePrimitiveType(Value);

            if (readValueType == EPrimitiveType.NotPrimitive)
            {
                return Error("XEC2026", ErrorMessage.InvalidValueForType(Value, this.AssignmentType));
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

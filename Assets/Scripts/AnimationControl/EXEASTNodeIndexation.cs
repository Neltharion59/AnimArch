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

            if (valueContext == null || valueContext.CurrentValue == null)
            {
                // We want to access a method of the current method owning object
                EXEVariable selfVariable = currentScope.FindVariable(EXETypes.SelfReferenceName);
                OwningObject = selfVariable.Value;
                valueContext = new EXEASTNodeAccessChainContext() { CurrentAccessChain = string.Empty };
            }
            else
            {
                // We want to access a method of an object given by the context
                OwningObject = valueContext.CurrentValue;
            }

            if (OwningObject.MethodExists(this.MethodName))
            {
                // Method exists
                Method = OwningObject.FindMethod(this.MethodName);
            }
            else
            {
                // Method does not exist, time to raise an error
                return EXEExecutionResult.Error(ErrorMessage.MethodNotFoundOnClass(this.MethodName, this.OwningObject.TypeName), "XEC2015");
            }

            if (this.Arguments.Count != this.Method.Parameters.Count)
            {
                return EXEExecutionResult.Error
                    (
                        ErrorMessage.InvalidParameterCount
                        (
                            this.Method.OwningClass.Name,
                            this.MethodName,
                            this.Method.Parameters.Count,
                            this.Arguments.Count
                        ),
                        "XEC2016"
                    );
            }

            // Now, let us evaluate the args and invoke the method
            EXEExecutionResult argumentExecutionResult;
            for (int i = 0; i < this.Arguments.Count; i++)
            {
                argumentExecutionResult = this.Arguments[i].Evaluate(currentScope, currentProgramInstance);

                if (!argumentExecutionResult.IsSuccess)
                {
                    this.EvaluationState = EEvaluationState.HasBeenEvaluated;
                    this.EvaluationResult = argumentExecutionResult;
                }

                if (!argumentExecutionResult.IsDone || !argumentExecutionResult.IsSuccess)
                {
                    // Either current argument evaluation did not finish or produced an error
                    return argumentExecutionResult;
                }

                // Current argument evaluation finished and did not produce an error

                // Check if the returned value matches the expected parameter type
                if (!EXETypes.CanBeAssignedTo(argumentExecutionResult.ReturnedOutput, this.Method.Parameters[i].Type, currentProgramInstance.ExecutionSpace))
                {
                    VisitorCommandToString visitor = VisitorCommandToString.BorrowAVisitor();
                    argumentExecutionResult.ReturnedOutput.Accept(visitor);
                    return EXEExecutionResult.Error
                    (
                        ErrorMessage.InvalidParameterValue
                        (
                            this.Method.OwningClass.Name,
                            this.MethodName,
                            this.Method.Parameters[i].Name,
                            this.Method.Parameters[i].Type, visitor.GetCommandStringAndResetStateNow()
                        ),
                        "XEC2017"
                    );
                }
            }

            // All arguments have been evaluated and without an error
            this.ExpectingReturn = true;
            this.EvaluationResult = EXEExecutionResult.Success();
            this.EvaluationResult.PendingCommand = new EXECommandCall(OwningObject, valueContext.CurrentAccessChain, this);
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

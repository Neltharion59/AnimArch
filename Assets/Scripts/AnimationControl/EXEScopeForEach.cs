using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OALProgramControl
{
    public class EXEScopeForEach : EXEScopeLoop
    {
        public String IteratorName { get; set; }
        public EXEASTNodeBase Iterable { get; set; }
        private int CurrentIterableIndex;

        public EXEScopeForEach(String iteratorName, EXEASTNodeBase iterable) : base()
        {
            this.IteratorName = iteratorName;
            this.Iterable = iterable;
            this.CurrentIterableIndex = 0;
        }

        // public override String ToCode(String Indent = "")
        // {
        //     return FormatCode(Indent, false);
        // }
        public override string ToFormattedCode(string Indent = "")
        {
            return FormatCode(Indent, IsActive);
        }
        private string FormatCode(String Indent, bool Highlight)
        {
            String Result
                =
                HighlightCodeIf
                (
                    Highlight,
                    Indent + "for each " + this.IteratorName + " in "
                    + this.Iterable.ToCode()
                    + "\n"
                );
            foreach (EXECommand Command in this.Commands)
            {
                Result += Command.ToFormattedCode(Indent + "\t");
            }
            Result += HighlightCodeIf(Highlight, Indent + "end for;\n");
            return Result;
        }

        protected override EXEScope CreateDuplicateScope()
        {
            return new EXEScopeForEach(IteratorName, Iterable.Clone());
        }

        protected override EXEExecutionResult HandleIterationStart(OALProgram OALProgram, out bool startNewIteration)
        {
            startNewIteration = false;

            EXEExecutionResult iterableEvaluationResult = this.Iterable.Evaluate(this.SuperScope, OALProgram);

            if (!HandleRepeatableASTEvaluation(iterableEvaluationResult))
            {
                return iterableEvaluationResult;
            }

            if (iterableEvaluationResult.ReturnedOutput is not EXEValueArray)
            {
                return Error(ErrorMessage.IsNotIterable(iterableEvaluationResult.ReturnedOutput.ToText()), "XEC2028");
            }

            EXEValueArray iterableValue = iterableEvaluationResult.ReturnedOutput as EXEValueArray;

            if (EXETypes.UnitializedName.Equals(iterableValue.ToText()))
            {
                return Error("Cannot iterate over uninitialized collection.", "XEC2027");
            }

            EXEVariable iteratorVariable = this.SuperScope.FindVariable(this.IteratorName);

            if (iteratorVariable != null)
            {
                if (!EXETypes.CanBeAssignedTo(iterableValue.ElementTypeName, iteratorVariable.Value.TypeName, OALProgram.ExecutionSpace))
                {
                    return Error
                    (
                        string.Format
                        (
                            "At iterable index {0}: Iterable elements are of types: {1}. {2}",
                            this.CurrentIterableIndex,
                            string.Join(", ", iterableValue.Elements.Select(element => element.TypeName)),
                            ErrorMessage.IterableAndIteratorTypeMismatch
                            (
                                this.Iterable.ToCode(),
                                iterableValue.ElementTypeName,
                                this.IteratorName,
                                iteratorVariable.Value.TypeName
                            )
                        ),
                        "XEC2029"
                    );
                }
            }
            else if (this.CurrentIterableIndex == 0)
            {
                iteratorVariable
                        = new EXEVariable
                        (
                            this.IteratorName,
                            EXETypes.DefaultValue(iterableValue.ElementTypeName, OALProgram.ExecutionSpace)
                        );

                EXEExecutionResult iteratorVariableCreationResult = this.SuperScope.AddVariable(iteratorVariable);

                if (!HandleSingleShotASTEvaluation(iteratorVariableCreationResult))
                {
                    return iteratorVariableCreationResult;
                }
            }
            else
            {
                return Error("Unexpected modification of iterator variable during foreach loop execution.", "XEC2034");
            };


            if (this.CurrentIterableIndex < iterableValue.Elements.Count)
            {
                EXEExecutionResult iteratorAssignmentResult
                    = iteratorVariable.Value.AssignValueFrom(iterableValue.GetElementAt(this.CurrentIterableIndex));

                if (!HandleSingleShotASTEvaluation(iteratorAssignmentResult))
                {
                    return iteratorAssignmentResult;
                }

                this.CurrentIterableIndex++;
                startNewIteration = true;
            }
            else
            {
                startNewIteration = false;
            }

            return Success();
        }
    }
}
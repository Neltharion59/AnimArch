using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OALProgramControl
{
    public class EXEScopeLoopWhile : EXEScopeLoop
    {
        public EXEASTNodeBase Condition;

        public EXEScopeLoopWhile(EXEASTNodeBase Condition) : base()
        {
            this.Condition = Condition;
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
            String Result = HighlightCodeIf(Highlight, Indent + "while (" + this.Condition.ToCode() + ")\n");
            foreach (EXECommand Command in this.Commands)
            {
                Result += Command.ToFormattedCode(Indent + "\t");
            }
            Result += HighlightCodeIf(Highlight, Indent + "end while;\n");
            return Result;
        }

        protected override EXEScope CreateDuplicateScope()
        {
            return new EXEScopeLoopWhile(Condition.Clone());
        }

        protected override EXEExecutionResult HandleIterationStart(OALProgram OALProgram, out bool startNewIteration)
        {
            EXEExecutionResult conditionEvaluationResult = this.Condition.Evaluate(this.SuperScope, OALProgram);

            if (!HandleRepeatableASTEvaluation(conditionEvaluationResult))
            {
                startNewIteration = false;
                return conditionEvaluationResult;
            }

            if (conditionEvaluationResult.ReturnedOutput is not EXEValueBool)
            {
                startNewIteration = false;
                return Error(ErrorMessage.InvalidValueForType(conditionEvaluationResult.ReturnedOutput.ToText(), EXETypes.BooleanTypeName), "XEC2028");
            }

            startNewIteration = (conditionEvaluationResult.ReturnedOutput as EXEValueBool).Value;

            return Success();
        }
    }
}

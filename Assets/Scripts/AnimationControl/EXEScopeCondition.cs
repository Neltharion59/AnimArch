using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OALProgramControl
{
    public class EXEScopeCondition : EXEScope
    {
        public EXEASTNodeBase Condition { get; set; }
        public List<EXEScopeCondition> ElifScopes { get; private set; }
        public EXEScope ElseScope { get; set; }
        private IEnumerable<EXEScopeCondition> AllConditionedScopes
        {
            get
            {
                yield return this;

                foreach (EXEScopeCondition elifScope in this.ElifScopes)
                {
                    yield return elifScope;
                }
            }
        }

        public EXEScopeCondition(EXEASTNodeBase Condition) : this(Condition, new List<EXEScopeCondition>(), null) {}
        public EXEScopeCondition(EXEASTNodeBase Condition, List<EXEScopeCondition> ElifScopes, EXEScope ElseScope) : base()
        {
            this.Condition = Condition;
            this.ElifScopes = ElifScopes;
            this.ElseScope = ElseScope;
        }

        public override void SetSuperScope(EXEScope SuperScope)
        {
            base.SetSuperScope(SuperScope);

            if (this.ElifScopes != null)
            {
                foreach (EXEScope ElifScope in this.ElifScopes)
                {
                    ElifScope.SetSuperScope(this.GetSuperScope());
                }
            }

            if (this.ElseScope != null)
            {
                this.ElseScope.SetSuperScope(this.GetSuperScope());
            }
        }

        public void AddElifScope(EXEScopeCondition ElifScope)
        {
            if (this.ElifScopes == null)
            {
                this.ElifScopes = new List<EXEScopeCondition>();
            }

            this.ElifScopes.Add(ElifScope);
            ElifScope.SetSuperScope(this.GetSuperScope());
        }

        protected override EXEExecutionResult Execute(OALProgram OALProgram)
        {
            foreach (EXEScopeCondition scope in this.AllConditionedScopes)
            {
                EXEExecutionResult conditionEvaluationResult = Condition.Evaluate(scope.SuperScope, OALProgram);

                if (!HandleRepeatableASTEvaluation(conditionEvaluationResult))
                {
                    return conditionEvaluationResult;
                }

                if (conditionEvaluationResult.ReturnedOutput is not EXEValueBool)
                {
                    return Error(ErrorMessage.InvalidValueForType(conditionEvaluationResult.ReturnedOutput.ToText(), EXETypes.BooleanTypeName), "XEC2027");
                }

                if ((conditionEvaluationResult.ReturnedOutput as EXEValueBool).Value)
                {
                    AddCommandsToStack(scope.Commands);
                    return Success();
                }
            }

            if (this.ElseScope != null)
            {
                AddCommandsToStack(this.ElseScope.Commands);
            }

            return Success();
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
            String Result = HighlightCodeIf(Highlight, Indent + "if (" + this.Condition.ToCode() + ")\n");
            foreach (EXECommand Command in this.Commands)
            {
                Result += Command.ToFormattedCode(Indent + "\t");
            }
            if (this.ElifScopes != null)
            {
                foreach (EXEScopeCondition Elif in this.ElifScopes)
                {
                    Result += HighlightCodeIf(Highlight, Indent + "elif (" + Elif.Condition.ToCode() + ")\n");
                    foreach (EXECommand Command in Elif.Commands)
                    {
                        Result += Command.ToFormattedCode(Indent + "\t");
                    }
                }
            }
            if (this.ElseScope != null)
            {
                Result += HighlightCodeIf(Highlight, Indent + "else\n");
                foreach (EXECommand Command in this.ElseScope.Commands)
                {
                    Result += Command.ToFormattedCode(Indent + "\t");
                }
            }
            Result += HighlightCodeIf(Highlight, Indent + "end if;\n");
            return Result;
        }

        protected override EXEScope CreateDuplicateScope()
        {
            return new EXEScopeCondition
            (
                Condition.Clone(),
                ElifScopes?.Select(x => (EXEScopeCondition)x.CreateClone()).ToList() ?? new List<EXEScopeCondition>(),
                ElseScope == null ? null : (EXEScope)ElseScope.CreateClone()
            );
        }
    }
}

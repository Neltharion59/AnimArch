using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace OALProgramControl
{
    public class EXEValueString : EXEValuePrimitive
    {
        public string Value {get; protected set; }
        public override string TypeName => EXETypes.StringTypeName;

        public EXEValueString(EXEValueString original)
        {
            CopyValues(original, this);
        }
        public EXEValueString(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("String value cannot be determined from null nor from an empty string.");
            }

            if (!EXETypes.IsValidStringValue(value))
            {
                throw new ArgumentException(string.Format("\"{0}\" is not a valid string value.", value));
            }

            this.Value = value.Substring(1, value.Length - 2);
        }
        public override EXEValueBase DeepClone()
        {
            return new EXEValueString(this);
        }
        public override void Accept(Visitor v)
        {
            throw new NotImplementedException();
        }
        protected override EXEExecutionResult AssignValueFromConcrete(EXEValueBase assignmentSource)
        {
            return assignmentSource.AssignValueTo(this);
        }
        public override EXEExecutionResult AssignValueTo(EXEValueString assignmentTarget)
        {
            if (!this.WasInitialized)
            {
                return UninitializedError();
            }

            CopyValues(this, assignmentTarget);
            this.WasInitialized = true;

            return EXEExecutionResult.Success();
        }
        private void CopyValues(EXEValueString source, EXEValueString target)
        {
            target.Value = source.Value;
        }
        public override EXEExecutionResult ApplyOperator(string operation)
        {
            if (!this.WasInitialized)
            {
                return UninitializedError();
            }

            EXEExecutionResult result = null;

            if ("cardinality".Equals(operation))
            {
                result = EXEExecutionResult.Success();
                result.ReturnedOutput = new EXEValueInt(this.Value.Length);
                return result;
            }

            return base.ApplyOperator(operation);
        }
        public override EXEExecutionResult ApplyOperator(string operation, EXEValueBase operand)
        {
            if (!this.WasInitialized || !operand.WasInitialized)
            {
                return base.ApplyOperator(operation, operand);
            }

            EXEExecutionResult result = null;

            if ("==".Equals(operation))
            {
                if (operand is not EXEValueString)
                {
                    return base.ApplyOperator(operation, operand);
                }

                result = EXEExecutionResult.Success();
                result.ReturnedOutput = new EXEValueBool(this.Value == (operand as EXEValueString).Value);
                return result;
            }
            else if ("!=".Equals(operation))
            {
                if (operand is not EXEValueString)
                {
                    return base.ApplyOperator(operation, operand);
                }

                result = EXEExecutionResult.Success();
                result.ReturnedOutput = new EXEValueBool(this.Value != (operand as EXEValueString).Value);
                return result;
            }
            else if ("+".Equals(operation))
            {
                if (operand is not EXEValueString)
                {
                    return base.ApplyOperator(operation, operand);
                }

                result = EXEExecutionResult.Success();
                result.ReturnedOutput = new EXEValueString(this.Value + (operand as EXEValueString).Value);
                return result;
            }
            else if ("*".Equals(operation))
            {
                if (operand is not EXEValueInt)
                {
                    return base.ApplyOperator(operation, operand);
                }

                result = EXEExecutionResult.Success();
                result.ReturnedOutput
                    = new EXEValueString
                    (
                        Enumerable
                            .Range(0, (int)(operand as EXEValueInt).Value)
                            .Aggregate("", (acc, x) => acc + x)
                    );
                return result;
            }

            return base.ApplyOperator(operation, operand);
        }

        public override string ToObjectDiagramText()
        {
            return string.Format("\"{0}\"", this.Value);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace OALProgramControl
{
    public class EXEValueArray : EXEValueBase
    {
        public string ElementTypeName { get; private set; }
        public override string TypeName => ElementTypeName + "[]";
        public List<EXEValueBase> Elements;

        public EXEValueArray(string type)
        {
            if (!EXETypes.IsValidArrayType(type))
            {
                throw new Exception(string.Format("\"{0}\" is not a valid array type.", type));
            }

            this.ElementTypeName = type.Substring(0, type.Length - 2);
            this.Elements = null;
        }
        private EXEValueArray(string type, List<EXEValueBase> elements) : this(type)
        {
            this.Elements = elements;
        }
        public static EXEExecutionResult CreateArray(string type)
        {
            if (!EXETypes.IsValidArrayType(type))
            {
                return EXEExecutionResult.Error(string.Format("Cannot create an array. \"{0}\" is not a valid array type.", type), "XEC2033");
            }

            EXEExecutionResult result = EXEExecutionResult.Success();

            EXEValueArray createdArray = new EXEValueArray(type);
            result.ReturnedOutput = createdArray;

            return result;
        }
        public void InitializeEmptyArray()
        {
            if (this.Elements != null)
            {
                throw new Exception("Tried to initialize non-empty array.");
            }

            this.Elements = new List<EXEValueBase>();
        }
        public override EXEValueBase DeepClone()
        {
            return new EXEValueArray(this.TypeName, this.Elements.Select(element => element.DeepClone()).ToList());
        }

        public override void Accept(Visitor v) {
            v.VisitExeValueArray(this);
        }

        public override EXEExecutionResult GetValueAt(UInt32 index)
        {
            UInt32 indexValue = index;
            if (indexValue < 0)
            {
                return EXEExecutionResult.Error("Index value cannot be lower than 0!", "XEC3004");
            }
            if (Elements == null)
            {
                return EXEExecutionResult.Error("Cannot get the value of an array that is null!", "XEC3005");
            }
            if (indexValue > Elements.Count)
            {
                return EXEExecutionResult.Error("Index " + indexValue + " is out of range (" + Elements.Count + ")!", "XEC3006");
            }
            
            EXEExecutionResult result = EXEExecutionResult.Success();
            result.ReturnedOutput = Elements[(int)indexValue];
            return result;
        }

        protected override EXEExecutionResult AssignValueFromConcrete(EXEValueBase assignmentSource)
        {
            return assignmentSource.AssignValueTo(this);
        }
        public override EXEExecutionResult AssignValueTo(EXEValueArray assignmentTarget)
        {
            if (!this.WasInitialized)
            {
                return UninitializedError();
            }

            if (!string.Equals(this.TypeName, assignmentTarget.TypeName))
            {
                return base.AssignValueTo(assignmentTarget);
            }

            CopyValues(this, assignmentTarget);

            return EXEExecutionResult.Success();
        }
        public override EXEExecutionResult AppendElement(EXEValueBase appendedElement, CDClassPool classPool)
        {
            if (!this.WasInitialized)
            {
                return UninitializedError();
            }

            if (!EXETypes.CanBeAssignedTo(appendedElement, this.ElementTypeName, classPool))
            {
                return base.AppendElement(appendedElement, classPool);
            }

            if (this.Elements == null)
            {
                return base.AppendElement(appendedElement, classPool);
            }

            this.Elements.Add(appendedElement);
            return EXEExecutionResult.Success();
        }
        public override EXEExecutionResult RemoveElement(EXEValueBase removedElement, CDClassPool classPool)
        {
            if (!this.WasInitialized)
            {
                return UninitializedError();
            }

            if (!string.Equals(this.ElementTypeName, removedElement.TypeName))
            {
                return base.RemoveElement(removedElement, classPool);
            }

            if (this.Elements == null)
            {
                return EXEExecutionResult.Error("Tried to remove an element from an uninitialized array.", "XEC2027");
            }

            this.Elements.RemoveAll(element => element.Equals(removedElement));

            return EXEExecutionResult.Success();
        }
        public EXEValueBase GetElementAt(int index)
        {
            if (index >= this.Elements.Count)
            {
                throw new IndexOutOfRangeException(string.Format("Tried to access index {0} in collection of size {1}.", index, this.Elements.Count));
            }

            return this.Elements[index];
        }
        private void CopyValues(EXEValueArray source, EXEValueArray target)
        {
            target.ElementTypeName = source.ElementTypeName;
            target.Elements = source.Elements?.Select(element => element.DeepClone()).ToList();
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
                result.ReturnedOutput = new EXEValueInt(this.Elements == null ? 0 : this.Elements.Count);
                return result;
            }
            else if ("empty".Equals(operation))
            {
                result = EXEExecutionResult.Success();
                result.ReturnedOutput = new EXEValueBool(this.Elements == null || !this.Elements.Any());
                return result;
            }
            else if ("not_empty".Equals(operation))
            {
                result = EXEExecutionResult.Success();
                result.ReturnedOutput = new EXEValueBool(this.Elements == null || this.Elements.Any());
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
                if (operand is not EXEValueArray)
                {
                    return base.ApplyOperator(operation, operand);
                }

                bool areEqual = false;
                EXEValueArray typedOperand = operand as EXEValueArray;

                if (this.Elements == null)
                {
                    areEqual = this.Elements == typedOperand.Elements;
                }
                else if (typedOperand.Elements == null)
                {
                    areEqual = false;
                }
                else if (this.Elements.Count != typedOperand.Elements.Count)
                {
                    areEqual = false;
                }
                else
                {
                    areEqual = true;

                    for (int i = 0; i < this.Elements.Count; i++)
                    {
                        if (!(this.Elements[i].IsEqualTo(typedOperand.Elements[i]).ReturnedOutput as EXEValueBool).Value)
                        {
                            areEqual = false;
                            break;
                        }
                    }
                }

                result = EXEExecutionResult.Success();
                result.ReturnedOutput = new EXEValueBool(areEqual);
                return result;
            }
            else if ("!=".Equals(operation))
            {
                bool areEqual = true;
                EXEValueArray typedOperand = operand as EXEValueArray;

                if (this.Elements == null)
                {
                    areEqual = this.Elements != typedOperand.Elements;
                }
                else if (typedOperand.Elements == null)
                {
                    areEqual = true;
                }
                else if (this.Elements.Count != typedOperand.Elements.Count)
                {
                    areEqual = true;
                }
                else
                {
                    areEqual = false;

                    for (int i = 0; i < this.Elements.Count; i++)
                    {
                        if ((this.Elements[i].IsEqualTo(typedOperand.Elements[i]).ReturnedOutput as EXEValueBool).Value)
                        {
                            areEqual = true;
                            break;
                        }
                    }
                }

                result = EXEExecutionResult.Success();
                result.ReturnedOutput = new EXEValueBool(areEqual);
                return result;
            }

            return base.ApplyOperator(operation, operand);
        }
        public override string ToObjectDiagramText()
        {
            return string.Format("[{0} ]", string.Join(", ", this.Elements.Select(element => element.ToObjectDiagramText())));
        }
    }
}
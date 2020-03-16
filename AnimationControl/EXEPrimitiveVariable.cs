using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{
    public class EXEPrimitiveVariable
    {
        public String Name { get; }
        public String Type { get; }
        public String Value { get; set; }

        public EXEPrimitiveVariable(String Name, String Value)
        {
            this.Name = Name;
            this.Value = Value;
            this.Type = EXETypes.DetermineVariableType(Name, Value);
        }

        public Boolean AssignValue(String name, String NewValue)
        {
            if (name == EXETypes.UniqueIDTypeName)
            {
                return false;
            }

            String NewValueType = EXETypes.DetermineVariableType(name, NewValue);
            if (NewValueType == this.Type)
            {
                this.Value = NewValue;
                return true;
            }

            if (NewValueType == EXETypes.IntegerTypeName && this.Type == EXETypes.RealTypeName && EXEExecutionGlobals.AllowPromotionOfIntegerToReal)
            {
                int NewValueInt = int.Parse(NewValue);
                double newValueDouble = NewValueInt;
                this.Value = newValueDouble.ToString();
                return true;
            }

            if (NewValueType == EXETypes.RealTypeName && this.Type == EXETypes.IntegerTypeName && EXEExecutionGlobals.AllowLossyAssignmentOfRealToInteger)
            {
                double newValueDouble = double.Parse(NewValue);
                int newValueInt = (int) newValueDouble;
                this.Value = newValueInt.ToString();
                return true;
            }

            return false;
        }
    }
}

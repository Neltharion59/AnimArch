using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{
    public class EXETypes
    {
        public static String IntegerTypeName = "integer";
        public const String RealTypeName = "real";
        public const String BooleanTypeName = "boolean";
        public const String StringTypeName = "string";
        public const String DateTypeName = "date";
        public const String UniqueIDTypeName = "unique_ID";

        public static String DetermineVariableType(String name, String value)
        {
            if (name == UniqueIDTypeName)
            {
                return UniqueIDTypeName;
            }

            if (long.TryParse(value, out _))
            {
                return IntegerTypeName;
            }

            if (double.TryParse(value, out _))
            {
                return RealTypeName;
            }

            if (value == "TRUE" || value == "FALSE")
            {
                return BooleanTypeName;
            }

            //TODO Date


            return StringTypeName;
        }
        public static Boolean IsValidValue(String Value, String Type)
        {
            Boolean Result = false;
            switch (Type)
            {
                case "integer":
                    Result = int.TryParse(Value, out _);
                    break;
                case "real":
                    Result = double.TryParse(Value, out _);
                    break;
                case "boolean":
                    Result = Boolean.TryParse(Value, out _);
                    break;
                case "unique_ID":
                    //Result = Name == UniqueIDTypeName;
                    Result = int.TryParse(Value, out _);
                    break;
                case "date":
                    //TODO (ak vobec)
                    break;
                default:
                    Result = true;
                    break;
            }

            return Result;
        }
    }
}

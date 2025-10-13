using OALProgramControl;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.AnimationControl.BuiltIn
{
    internal class BuiltInMethodStringParseInt : BuiltInMethodString
    {
        protected override EXEExecutionResult Evaluate(EXEValueString owningObject, List<EXEValueBase> parameters)
        {
            int parsedInt;
            if (!int.TryParse(owningObject.Value, out parsedInt))
            {
                parsedInt = 0;
            }

            EXEExecutionResult result = EXEExecutionResult.Success();
            result.ReturnedOutput = new EXEValueInt(parsedInt);
            return result;
        }
    }
}

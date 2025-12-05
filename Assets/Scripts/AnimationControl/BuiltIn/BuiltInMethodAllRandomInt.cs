using OALProgramControl;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.AnimationControl.BuiltIn
{
    public class BuiltInMethodAllRandomInt : BuiltInMethodBase
    {
        public override EXEExecutionResult Evaluate(EXEValueBase owningObject, List<EXEValueBase> parameters)
        {
            EXEValueInt minValue = parameters[0] as EXEValueInt;
            EXEValueInt maxValue = parameters[1] as EXEValueInt;

            long min = minValue.Value;
            long max = maxValue.Value;

            long generatedValue;
            try
            {
                generatedValue = LongRandom.Next(min, max);
            }
            catch (Exception e)
            {
                return EXEExecutionResult.Error("XEC2057", string.Format($"Failed to generate random int in range ({min},{max}) because {e.Message}."));
            }

            EXEExecutionResult result = EXEExecutionResult.Success();
            result.ReturnedOutput = new EXEValueInt(generatedValue);
            return result;
        }
    }
}
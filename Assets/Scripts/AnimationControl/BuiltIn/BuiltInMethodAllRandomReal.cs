using OALProgramControl;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.AnimationControl.BuiltIn
{
    public class BuiltInMethodAllRandomReal : BuiltInMethodBase
    {
        public override EXEExecutionResult Evaluate(EXEValueBase owningObject, List<EXEValueBase> parameters)
        {
            EXEValueReal minValue = parameters[0] as EXEValueReal;
            EXEValueReal maxValue = parameters[1] as EXEValueReal ;

            decimal min = minValue.Value;
            decimal max = maxValue.Value;

            decimal generatedValue;
            try
            {
                generatedValue = DecimalRandom.Next(min, max);
            }
            catch (Exception e)
            {
                return EXEExecutionResult.Error("XEC2056", string.Format($"Failed to generate random real in range ({min},{max}) because {e.Message}."));
            }

            EXEExecutionResult result = EXEExecutionResult.Success();
            result.ReturnedOutput = new EXEValueReal(generatedValue);
            return result;
        }
    }
}
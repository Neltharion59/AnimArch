using OALProgramControl;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.AnimationControl.BuiltIn
{
    public class BuiltInMethodAllRandomBool : BuiltInMethodBase
    {
        public override EXEExecutionResult Evaluate(EXEValueBase owningObject, List<EXEValueBase> parameters)
        {
            bool generatedValue;
            try
            {
                generatedValue = BoolRandom.Next();
            }
            catch (Exception e)
            {
                return EXEExecutionResult.Error("XEC2058", string.Format($"Failed to generate random bool because {e.Message}."));
            }

            EXEExecutionResult result = EXEExecutionResult.Success();
            result.ReturnedOutput = new EXEValueBool(generatedValue);
            return result;
        }
    }
}
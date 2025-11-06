using OALProgramControl;
using System;
using System.Collections.Generic;
using System.Linq;
using AnimArch.Extensions;
using System.IO;

namespace Assets.Scripts.AnimationControl.BuiltIn
{
    public class BuiltInMethodAllWriteToFile : BuiltInMethodBase
    {
        public override EXEExecutionResult Evaluate(EXEValueBase owningObject, List<EXEValueBase> parameters)
        {
            EXEValueString filePathValue = parameters[0] as EXEValueString;
            EXEValueString writtenTextValue = parameters[1] as EXEValueString;

            string filePath = filePathValue.Value;
            string writtenText = writtenTextValue.Value;

            try
            {
                File.WriteAllText(filePath, writtenText);
            }
            catch (Exception ex)
            {
                return EXEExecutionResult.Error("XEC2055", string.Format("Error writing '{0}' to file '{1}': '{2}'.", writtenText, filePath, ex.Message));
            }

            EXEExecutionResult result = EXEExecutionResult.Success();
            return result;
        }
    }
}
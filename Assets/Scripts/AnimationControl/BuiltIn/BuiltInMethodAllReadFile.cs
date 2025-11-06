using OALProgramControl;
using System;
using System.Collections.Generic;
using System.Linq;
using AnimArch.Extensions;
using System.IO;

namespace Assets.Scripts.AnimationControl.BuiltIn
{
    public class BuiltInMethodAllReadFile : BuiltInMethodBase
    {
        public override EXEExecutionResult Evaluate(EXEValueBase owningObject, List<EXEValueBase> parameters)
        {
            EXEValueString filePathValue = parameters[0] as EXEValueString;

            string filePath = filePathValue.Value;
            string readText = null;

            try
            {
                readText = File.ReadAllText(filePath);
            }
            catch (Exception ex)
            {
                return EXEExecutionResult.Error("XEC2054", string.Format("Error reading file '{0}': '{1}'.", filePath, ex.Message));
            }

            EXEExecutionResult result = EXEExecutionResult.Success();
            result.ReturnedOutput = new EXEValueString(string.Format("\"{0}\"", readText));
            return result;
        }
    }
}
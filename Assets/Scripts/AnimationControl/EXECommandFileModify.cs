using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace OALProgramControl
{
    public abstract class EXECommandFileModify : EXECommand
    {
        public EXEASTNodeBase StringToWrite { get; }
        public EXEASTNodeBase FileToWriteTo { get; }

        public abstract bool Append { get; }

        protected EXECommandFileModify(EXEASTNodeBase stringToWrite, EXEASTNodeBase fileToWriteTo)
        {
            this.StringToWrite = stringToWrite;
            this.FileToWriteTo = fileToWriteTo;
        }

        protected override EXEExecutionResult Execute(OALProgram OALProgram)
        {
            EXEExecutionResult StringToWriteEvaluation = StringToWrite.Evaluate(this.SuperScope, OALProgram);
            if (!HandleRepeatableASTEvaluation(StringToWriteEvaluation))
            {
                return StringToWriteEvaluation;
            }

            if (StringToWriteEvaluation.ReturnedOutput is not EXEValueString)
            {
                return EXEExecutionResult.Error("XEC2040", string.Format("Tried to write value '{0}' of type '{1}' to a file. The value needs to be a string.", StringToWriteEvaluation.ReturnedOutput.ToObjectDiagramText(), StringToWriteEvaluation.ReturnedOutput.TypeName));
            }

            EXEExecutionResult FileToWriteToEvaluation = FileToWriteTo.Evaluate(this.SuperScope, OALProgram);
            if (!HandleRepeatableASTEvaluation(FileToWriteToEvaluation))
            {
                return FileToWriteToEvaluation;
            }

            if (FileToWriteToEvaluation.ReturnedOutput is not EXEValueString)
            {
                return EXEExecutionResult.Error("XEC2041", string.Format("Tried to write value '{0}' of type '{1}' to a file. The value needs to be a string.", FileToWriteToEvaluation.ReturnedOutput.ToObjectDiagramText(), FileToWriteToEvaluation.ReturnedOutput.TypeName));
            }

            string stringToWrite = (StringToWriteEvaluation.ReturnedOutput as EXEValueString).Value;
            string fileToWriteTo = (FileToWriteToEvaluation.ReturnedOutput as EXEValueString).Value;

            try
            {
                using (StreamWriter writetext = new StreamWriter(fileToWriteTo, Append))
                {
                    writetext.Write(stringToWrite);
                }
            }
            catch (Exception e)
            {
                string errorMessage = string.Format("Failed to {0} to file '{1}', because of {2}:'{3}'", Append ? "Append" : "Write", fileToWriteTo, e.GetType().Name, e.Message);
                return EXEExecutionResult.Error("XEC2043", errorMessage);
            }

            return Success();
        }
    }
}
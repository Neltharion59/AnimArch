using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace OALProgramControl
{
    public class StrategyProduction : Singleton<StrategyProduction>, IStrategy
    {

        public List<string> Commands { get; }


        public void Read(EXECommandRead EXECommandRead, EXEExecutionResult promptEvaluationResult, EXEScope SuperScope, OALProgram OALProgram)
        {
            Debug.LogError("production read");
            // EXEExecutionResult promptEvaluationResult = null;
            // if (EXECommandRead.Prompt != null)
            // {
            //     promptEvaluationResult  = EXECommandRead.Prompt.Evaluate(SuperScope, OALProgram);

            //     if (!HandleRepeatableASTEvaluation(promptEvaluationResult))
            //     {
            //         // return 0;
            //         return promptEvaluationResult;
            //     }
            // }

            // if (promptEvaluationResult.ReturnedOutput is not EXEValueString)
            // {
            //     // return 1;
            //     return Error("XEC2025", string.Format("Tried to read from console with prompt that is not string. Instead, it is \"{0}\".", promptEvaluationResult.ReturnedOutput.TypeName));
            // }

            string prompt = string.Empty;
            EXEValueString retOutput = promptEvaluationResult.ReturnedOutput as EXEValueString;
            if (retOutput != null) {
                VisitorCommandToString visitor = VisitorCommandToString.BorrowAVisitor();
                retOutput.Accept(visitor);
                prompt = visitor.GetCommandStringAndResetStateNow();
            }

            EXECommandRead.PromptText = prompt;

            // return 2;
            // return EXECommandRead.Success();
        }      
        public void Write(EXECommandWrite EXECommandWrite)
        {
            Debug.LogError("production write");
            // EXECommandWrite.PromptText = result;  
        }

       
    }
}
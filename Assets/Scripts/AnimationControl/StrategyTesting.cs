using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace OALProgramControl
{
    public class StrategyTesting : Singleton<StrategyTesting>, IStrategy
    {
        public List<string> Commands { get; }

        public void Read(EXECommandRead EXECommandRead, EXEExecutionResult promptEvaluationResult, EXEScope SuperScope, OALProgram OALProgram)
        {    
            Debug.LogError("testing read");
        }     

        public void Write(EXECommandWrite EXECommandWrite)
        {
            Debug.LogError("testing write");
            Commands.Add(EXECommandWrite.PromptText);
        }
    }
}
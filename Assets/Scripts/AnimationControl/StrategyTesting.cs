using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace OALProgramControl
{
    public class StrategyTesting : IStrategy
    {
        public List<string> Commands { get; }

        public EXEExecutionResult Read(OALProgram OALProgram)
        {    
            return null;
        }     

        public void Write(EXECommandWrite EXECommandWrite, string result)
        {
            Debug.LogError("testing write");
            Commands.Add(result);
        }
    }
}
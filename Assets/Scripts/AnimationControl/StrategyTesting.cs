using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace OALProgramControl
{
    public class StrategyTesting : IStrategy
    {
        public List<string> Commands { get; }
        public IStrategy Strategy { get; set; }
        public StrategyTesting()
        {
            Strategy = this;
        }

        public void Read()
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
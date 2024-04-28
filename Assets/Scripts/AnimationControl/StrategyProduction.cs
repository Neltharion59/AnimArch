using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace OALProgramControl
{
    public class StrategyProduction : IStrategy
    {
        public List<string> Commands { get; }
        public IStrategy Strategy { get; set; }
        public StrategyProduction()
        {
            Strategy = this;
        }

        public void Read()
        {
            Debug.LogError("production read");
        }      
        public void Write(EXECommandWrite EXECommandWrite)
        {
            Debug.LogError("production write");
        }

    }
}
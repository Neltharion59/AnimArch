using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace OALProgramControl
{
    public class StrategyTesting : Singleton<StrategyTesting>, IStrategy
    {
        public List<string> Commands { get; }

        public int Read(EXECommandRead EXECommandRead)
        {    
            Debug.LogError("testing read");
            return null;
        }     

        public void Write(EXECommandWrite EXECommandWrite, string result)
        {
            Debug.LogError("testing write");
            Commands.Add(result);
        }
    }
}
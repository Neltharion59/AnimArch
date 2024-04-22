using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace OALProgramControl
{
    public interface IStrategy
    {
        public List<string> Commands { get; }

        EXEExecutionResult Read(OALProgram OALProgram);      
        void Write(EXECommandWrite EXECommandWrite, string result); 
    }
}
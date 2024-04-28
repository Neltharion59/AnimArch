using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace OALProgramControl
{
    public interface IStrategy
    {
        public List<string> Commands { get; }

        void Read(EXECommandRead EXECommandRead, EXEExecutionResult promptEvaluationResult, EXEScope SuperScope, OALProgram OALProgram);      
        void Write(EXECommandWrite EXECommandWrite); 
    }
}
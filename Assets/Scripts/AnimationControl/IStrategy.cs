using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace OALProgramControl
{
    public interface IStrategy
    {
        public List<string> Commands { get; }

        int Read(EXECommandRead EXECommandRead);      
        void Write(EXECommandWrite EXECommandWrite, string result); 
    }
}
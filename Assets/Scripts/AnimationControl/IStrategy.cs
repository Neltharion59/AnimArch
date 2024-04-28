using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace OALProgramControl
{
    public interface IStrategy
    {
        public List<string> Commands { get; }
        public abstract IStrategy Strategy {get; set;}

        void Read();      
        void Write(EXECommandWrite EXECommandWrite); 
    }
}
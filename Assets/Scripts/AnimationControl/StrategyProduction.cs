using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System;

namespace OALProgramControl
{
    public class StrategyProduction : IStrategy
    {
        public List<string> ConsoleHistory { get; }
        public String MockedInput { get; set; }
        public StrategyProduction()
        {
            this.ConsoleHistory = new List<string>();
            this.MockedInput = "";
        }

        public void Read(EXECommandRead CurrentCommand, OALProgram CurrentProgramInstance)
        {
        }      
        
        public void Write(string text)
        {
        }

    }
}
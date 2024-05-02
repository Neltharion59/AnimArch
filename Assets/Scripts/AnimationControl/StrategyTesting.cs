using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System;

namespace OALProgramControl
{
    public class StrategyTesting : IStrategy
    {
        public List<string> ConsoleHistory { get; }
        public String MockedInput { get; set; }
        public StrategyTesting()
        {
            this.ConsoleHistory = new List<string>();
            this.MockedInput = "";
        }

        public void Read(EXECommandRead CurrentCommand, OALProgram CurrentProgramInstance)
        {
            CurrentCommand.AssignReadValue(MockedInput, CurrentProgramInstance);
            ConsoleHistory.Add(MockedInput);
        }   

        public void Write(string text)
        {
            ConsoleHistory.Add(text);
        }

    }
}
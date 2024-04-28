using System.Linq;
using NUnit.Framework;
using OALProgramControl;
using Assets.Scripts.AnimationControl.OAL;
using System;
using System.Collections.Generic;

namespace Assets.UnitTests.AnimationControl
{
    public class EXECommandWriteTests : StandardTest
    {    
        [Test]
        public void HappyDay_01_Write_01() //TODOs
        {
            // Arrange
            EXEASTNodeAccessChain _assignmentTarget = new EXEASTNodeAccessChain();
            _assignmentTarget.AddElement(new EXEASTNodeLeaf("list"));
            EXEASTNodeBase _assignedValue = new EXEASTNodeLeaf("write(\"Zaciatok\");");

            List<EXEASTNodeBase> _args = new List<EXEASTNodeBase>{_assignedValue};
            EXECommandWrite _command = new EXECommandWrite(_args);
            OALProgram _OALProgram = new OALProgram();
           
            // Act
            _command.Strategy = new StrategyTesting(); 

            // Assert
            string _expectedOutput = "\"Zaciatok\"";
            Assert.AreEqual(_expectedOutput, _command.Strategy.Commands);
        }

        
    }
}
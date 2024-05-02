using NUnit.Framework;
using OALProgramControl;
using Assets.Scripts.AnimationControl.OAL;
using System.Collections.Generic;
using UnityEngine;
using Visualization.UI;
using System;

namespace Assets.UnitTests.AnimationControl
{
    public class EXECommandReadWriteTests : StandardTest
    {
        [Test]
        public void HappyDay_01_Read_01()
        {
            CommandTest Test = new CommandTest();

            // Arrange
            string _methodSourceCode = "placeholder=\"Zadaj text...\";\nx=string(read(placeholder));";

            OALProgram programInstance = new OALProgram();
            CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

            CDMethod owningMethod = new CDMethod(owningClass, "Method1", "");
            owningClass.AddMethod(owningMethod);
            List<String> _mockedInputs = new List<String> { "\"Ahoj\"" };

            // Act
            EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
            owningMethod.ExecutableCode = methodScope;
            programInstance.SuperScope = methodScope;

            //TODO kde ako potom podsuvat ten mocked input? Ma sa brat do uvahy aj to Zadaj text...?
            EXEExecutionResult _executionResult = PerformExecution(programInstance, _mockedInputs[0]);
            List<string> _expectedConsoleHistory = new List<string> { "\"Zadaj text...\"", "\"Ahoj\"" };

            // Assert
            Test.Declare(methodScope, _executionResult);
            Test.Variables
                .ExpectVariable("self", methodScope.OwningObject)
                .ExpectVariable("placeholder", new EXEValueString("\"Zadaj text...\""))
                .ExpectVariable("x", new EXEValueString("\"Ahoj\""));
            foreach (string text in _expectedConsoleHistory)
            {
                Test.ConsoleHistory.ExpectText(text);
            }

            Test.PerformAssertion();
        }

        [Test]
        public void HappyDay_01_Write_01() 
        {
            CommandTest Test = new CommandTest();

            // Arrange
            string _methodSourceCode = "write(\"Ahoj\");";

            OALProgram programInstance = new OALProgram();
            CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

            CDMethod owningMethod = new CDMethod(owningClass, "Method1", "");
            owningClass.AddMethod(owningMethod);
           
            // Act
            EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
            owningMethod.ExecutableCode = methodScope;
            programInstance.SuperScope = methodScope;

            EXEExecutionResult _executionResult = PerformExecution(programInstance);
            List<string> _expectedConsoleHistory = new List<string> { "\"Ahoj\"" };

            // Assert
            Test.Declare(methodScope, _executionResult);
            Test.Variables.ExpectVariable("self", methodScope.OwningObject);
            foreach (string text in _expectedConsoleHistory)
            {
                Test.ConsoleHistory.ExpectText(text);
            }

            Test.PerformAssertion();
        }

        [Test]
        public void HappyDay_01_Write_02() 
        {
            CommandTest Test = new CommandTest();

            // Arrange
            string _methodSourceCode = "write(123456789);";

            OALProgram programInstance = new OALProgram();
            CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

            CDMethod owningMethod = new CDMethod(owningClass, "Method1", "");
            owningClass.AddMethod(owningMethod);
           
            // Act
            EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
            owningMethod.ExecutableCode = methodScope;
            programInstance.SuperScope = methodScope;

            EXEExecutionResult _executionResult = PerformExecution(programInstance);
            List<string> _expectedConsoleHistory = new List<string> { "123456789" };

            // Assert
            Test.Declare(methodScope, _executionResult);
            Test.Variables.ExpectVariable("self", methodScope.OwningObject);
            foreach (string text in _expectedConsoleHistory)
            {
                Test.ConsoleHistory.ExpectText(text);
            }

            Test.PerformAssertion();
        }

        [Test]
        public void HappyDay_01_Write_03() 
        {
            CommandTest Test = new CommandTest();

            // Arrange
            string _methodSourceCode = "write(\"Čo je wrist?\");\nwrite(\"Zápästie,\nnie fist - päsť.\");\nwrite(-123);\nwrite(0,987);";

            OALProgram programInstance = new OALProgram();
            CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

            CDMethod owningMethod = new CDMethod(owningClass, "Method1", "");
            owningClass.AddMethod(owningMethod);
           
            // Act
            EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
            owningMethod.ExecutableCode = methodScope;
            programInstance.SuperScope = methodScope;

            EXEExecutionResult _executionResult = PerformExecution(programInstance);
            List<string> _expectedConsoleHistory = new List<string> { "\"Čo je wrist?\"", "\"Zápästie,\nnie fist - päsť.\"", "-123", "0, 987" };

            // Assert
            Test.Declare(methodScope, _executionResult);
            Test.Variables.ExpectVariable("self", methodScope.OwningObject);
            foreach (string text in _expectedConsoleHistory)
            {
                Test.ConsoleHistory.ExpectText(text);
            }

            Test.PerformAssertion();
        }

        [Test]
        public void HappyDay_01_ReadWrite_01()
        {
            CommandTest Test = new CommandTest();

            // Arrange
            string _methodSourceCode = "write(\"Ahoj.\");\nwrite(\"Ako sa máš?\");\nx=string(read(\"\"))\n";

            OALProgram programInstance = new OALProgram();
            CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

            CDMethod owningMethod = new CDMethod(owningClass, "Method1", "");
            owningClass.AddMethod(owningMethod);
            List<String> _mockedInputs = new List<String> { "\"Dobre.\"" };

            // Act
            EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
            owningMethod.ExecutableCode = methodScope;
            programInstance.SuperScope = methodScope;

            EXEExecutionResult _executionResult = PerformExecution(programInstance, _mockedInputs[0]);
            List<string> _expectedConsoleHistory = new List<string> { "\"Ahoj.\"", "\"Ako sa máš?\"", "\"Dobre.\"" };

            // Assert
            Test.Declare(methodScope, _executionResult);
            Test.Variables
                .ExpectVariable("self", methodScope.OwningObject)
                .ExpectVariable("x", new EXEValueString("\"Dobre.\""));
            foreach (string text in _expectedConsoleHistory)
            {
                Test.ConsoleHistory.ExpectText(text);
            }

            Test.PerformAssertion();
        }


    }
}
using System.Linq;
using NUnit.Framework;
using OALProgramControl;
using Assets.Scripts.AnimationControl.OAL;
using System;
using System.Collections.Generic;

namespace Assets.UnitTests.AnimationControl
{
    public class EXECommandStringMethodTests
    {
        [TestFixture]
        public class Join : StandardTest
        {
            [Test]
            public void HappyDay_01_MultipleElements_01_LetterDelimiter()
            {
                CommandTest Test = new CommandTest();

                // Arrange
                string _methodSourceCode = "create list x of string {\"a\", \"b\", \"c\"};\ny = \"o\";\nz = y.Join(x);";

                OALProgram programInstance = new OALProgram();
                CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

                CDMethod owningMethod = new CDMethod(owningClass, "Method1", "");
                owningClass.AddMethod(owningMethod);

                // Act
                EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
                owningMethod.ExecutableCode = methodScope;
                programInstance.SuperScope = methodScope;

                EXEExecutionResult _executionResult = PerformExecution(programInstance);

                // Assert
                Test.Declare(methodScope, _executionResult);

                Test.Variables
                        .ExpectVariable
                        (
                            "x",
                            new EXEValueArray("string[]")
                            {
                                Elements = new List<EXEValueBase>()
                                {
                                new EXEValueString("\"a\""),
                                new EXEValueString("\"b\""),
                                new EXEValueString("\"c\"")
                                }
                            }
                        )
                        .ExpectVariable("y", new EXEValueString("\"o\""))
                        .ExpectVariable("z", new EXEValueString("\"aoboc\""))
                        .ExpectVariable("self", methodScope.OwningObject);

                Test.PerformAssertion();
            }

            [Test]
            public void HappyDay_01_MultipleElements_02_CommaDelimiter()
            {
                CommandTest Test = new CommandTest();

                // Arrange
                string _methodSourceCode = "create list x of string {\"a\", \"b\", \"c\"};\ny = \",\";\nz = y.Join(x);";

                OALProgram programInstance = new OALProgram();
                CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

                CDMethod owningMethod = new CDMethod(owningClass, "Method1", "");
                owningClass.AddMethod(owningMethod);

                // Act
                EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
                owningMethod.ExecutableCode = methodScope;
                programInstance.SuperScope = methodScope;

                EXEExecutionResult _executionResult = PerformExecution(programInstance);

                // Assert
                Test.Declare(methodScope, _executionResult);

                Test.Variables
                        .ExpectVariable
                        (
                            "x",
                            new EXEValueArray("string[]")
                            {
                                Elements = new List<EXEValueBase>()
                                {
                                new EXEValueString("\"a\""),
                                new EXEValueString("\"b\""),
                                new EXEValueString("\"c\"")
                                }
                            }
                        )
                        .ExpectVariable("y", new EXEValueString("\",\""))
                        .ExpectVariable("z", new EXEValueString("\"a,b,c\""))
                        .ExpectVariable("self", methodScope.OwningObject);

                Test.PerformAssertion();
            }

            [Test]
            public void HappyDay_02_OneElement_01_LetterDelimiter()
            {
                CommandTest Test = new CommandTest();

                // Arrange
                string _methodSourceCode = "create list x of string {\"a\"};\ny = \"o\";\nz = y.Join(x);";

                OALProgram programInstance = new OALProgram();
                CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

                CDMethod owningMethod = new CDMethod(owningClass, "Method1", "");
                owningClass.AddMethod(owningMethod);

                // Act
                EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
                owningMethod.ExecutableCode = methodScope;
                programInstance.SuperScope = methodScope;

                EXEExecutionResult _executionResult = PerformExecution(programInstance);

                // Assert
                Test.Declare(methodScope, _executionResult);

                Test.Variables
                        .ExpectVariable
                        (
                            "x",
                            new EXEValueArray("string[]")
                            {
                                Elements = new List<EXEValueBase>()
                                {
                                new EXEValueString("\"a\"")
                                }
                            }
                        )
                        .ExpectVariable("y", new EXEValueString("\"o\""))
                        .ExpectVariable("z", new EXEValueString("\"a\""))
                        .ExpectVariable("self", methodScope.OwningObject);

                Test.PerformAssertion();
            }

            [Test]
            public void HappyDay_02_OneElement_02_CommaDelimiter()
            {
                CommandTest Test = new CommandTest();

                // Arrange
                string _methodSourceCode = "create list x of string {\"a\"};\ny = \",\";\nz = y.Join(x);";

                OALProgram programInstance = new OALProgram();
                CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

                CDMethod owningMethod = new CDMethod(owningClass, "Method1", "");
                owningClass.AddMethod(owningMethod);

                // Act
                EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
                owningMethod.ExecutableCode = methodScope;
                programInstance.SuperScope = methodScope;

                EXEExecutionResult _executionResult = PerformExecution(programInstance);

                // Assert
                Test.Declare(methodScope, _executionResult);

                Test.Variables
                        .ExpectVariable
                        (
                            "x",
                            new EXEValueArray("string[]")
                            {
                                Elements = new List<EXEValueBase>()
                                {
                                new EXEValueString("\"a\"")
                                }
                            }
                        )
                        .ExpectVariable("y", new EXEValueString("\",\""))
                        .ExpectVariable("z", new EXEValueString("\"a\""))
                        .ExpectVariable("self", methodScope.OwningObject);

                Test.PerformAssertion();
            }

            [Test]
            public void HappyDay_03_NoElements()
            {
                CommandTest Test = new CommandTest();

                // Arrange
                string _methodSourceCode = "create list x of string;\ny = \"o\";\nz = y.Join(x);";

                OALProgram programInstance = new OALProgram();
                CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

                CDMethod owningMethod = new CDMethod(owningClass, "Method1", "");
                owningClass.AddMethod(owningMethod);

                // Act
                EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
                owningMethod.ExecutableCode = methodScope;
                programInstance.SuperScope = methodScope;

                EXEExecutionResult _executionResult = PerformExecution(programInstance);

                // Assert
                Test.Declare(methodScope, _executionResult);

                Test.Variables
                        .ExpectVariable
                        (
                            "x",
                            new EXEValueArray("string[]")
                            {
                                Elements = new List<EXEValueBase>()
                            }
                        )
                        .ExpectVariable("y", new EXEValueString("\"o\""))
                        .ExpectVariable("z", new EXEValueString("\"\""))
                        .ExpectVariable("self", methodScope.OwningObject);

                Test.PerformAssertion();
            }
        }

        [TestFixture]
        public class Split : StandardTest
        {
            [Test]
            public void HappyDay_01_MultipleElements_01_SingleLetterDelimiter()
            {
                CommandTest Test = new CommandTest();

                // Arrange
                string _methodSourceCode = "x = \"aoboc\";\ny = \"o\";\nz = x.Split(y);";

                OALProgram programInstance = new OALProgram();
                CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

                CDMethod owningMethod = new CDMethod(owningClass, "Method1", "");
                owningClass.AddMethod(owningMethod);

                // Act
                EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
                owningMethod.ExecutableCode = methodScope;
                programInstance.SuperScope = methodScope;

                EXEExecutionResult _executionResult = PerformExecution(programInstance);

                // Assert
                Test.Declare(methodScope, _executionResult);

                Test.Variables
                        .ExpectVariable("x", new EXEValueString("\"aoboc\""))
                        .ExpectVariable("y", new EXEValueString("\"o\""))
                        .ExpectVariable
                        (
                            "z",
                            new EXEValueArray("string[]")
                            {
                                Elements = new List<EXEValueBase>()
                                {
                                new EXEValueString("\"a\""),
                                new EXEValueString("\"b\""),
                                new EXEValueString("\"c\"")
                                }
                            }
                        )
                        .ExpectVariable("self", methodScope.OwningObject);

                Test.PerformAssertion();
            }

            [Test]
            public void HappyDay_01_MultipleElements_02_MultiLetterDelimiter()
            {
                CommandTest Test = new CommandTest();

                // Arrange
                string _methodSourceCode = "x = \"amnobmnoc\";\ny = \"mno\";\nz = x.Split(y);";

                OALProgram programInstance = new OALProgram();
                CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

                CDMethod owningMethod = new CDMethod(owningClass, "Method1", "");
                owningClass.AddMethod(owningMethod);

                // Act
                EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
                owningMethod.ExecutableCode = methodScope;
                programInstance.SuperScope = methodScope;

                EXEExecutionResult _executionResult = PerformExecution(programInstance);

                // Assert
                Test.Declare(methodScope, _executionResult);

                Test.Variables
                        .ExpectVariable("x", new EXEValueString("\"amnobmnoc\""))
                        .ExpectVariable("y", new EXEValueString("\"mno\""))
                        .ExpectVariable
                        (
                            "z",
                            new EXEValueArray("string[]")
                            {
                                Elements = new List<EXEValueBase>()
                                {
                                new EXEValueString("\"a\""),
                                new EXEValueString("\"b\""),
                                new EXEValueString("\"c\"")
                                }
                            }
                        )
                        .ExpectVariable("self", methodScope.OwningObject);

                Test.PerformAssertion();
            }

            [Test]
            public void HappyDay_01_MultipleElements_03_CommaDelimiter()
            {
                CommandTest Test = new CommandTest();

                // Arrange
                string _methodSourceCode = "x = \"a,b,c\";\ny = \",\";\nz = x.Split(y);";

                OALProgram programInstance = new OALProgram();
                CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

                CDMethod owningMethod = new CDMethod(owningClass, "Method1", "");
                owningClass.AddMethod(owningMethod);

                // Act
                EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
                owningMethod.ExecutableCode = methodScope;
                programInstance.SuperScope = methodScope;

                EXEExecutionResult _executionResult = PerformExecution(programInstance);

                // Assert
                Test.Declare(methodScope, _executionResult);

                Test.Variables
                        .ExpectVariable("x", new EXEValueString("\"a,b,c\""))
                        .ExpectVariable("y", new EXEValueString("\",\""))
                        .ExpectVariable
                        (
                            "z",
                            new EXEValueArray("string[]")
                            {
                                Elements = new List<EXEValueBase>()
                                {
                                new EXEValueString("\"a\""),
                                new EXEValueString("\"b\""),
                                new EXEValueString("\"c\"")
                                }
                            }
                        )
                        .ExpectVariable("self", methodScope.OwningObject);

                Test.PerformAssertion();
            }

            [Test]
            public void HappyDay_01_MultipleElements_04_NewLineDelimiter()
            {
                CommandTest Test = new CommandTest();

                // Arrange
                string _methodSourceCode = "x = \"a\nb\nc\";\ny = \"\n\";\nz = x.Split(y);";

                OALProgram programInstance = new OALProgram();
                CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

                CDMethod owningMethod = new CDMethod(owningClass, "Method1", "");
                owningClass.AddMethod(owningMethod);

                // Act
                EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
                owningMethod.ExecutableCode = methodScope;
                programInstance.SuperScope = methodScope;

                EXEExecutionResult _executionResult = PerformExecution(programInstance);

                // Assert
                Test.Declare(methodScope, _executionResult);

                Test.Variables
                        .ExpectVariable("x", new EXEValueString("\"a\nb\nc\""))
                        .ExpectVariable("y", new EXEValueString("\"\n\""))
                        .ExpectVariable
                        (
                            "z",
                            new EXEValueArray("string[]")
                            {
                                Elements = new List<EXEValueBase>()
                                {
                                new EXEValueString("\"a\""),
                                new EXEValueString("\"b\""),
                                new EXEValueString("\"c\"")
                                }
                            }
                        )
                        .ExpectVariable("self", methodScope.OwningObject);

                Test.PerformAssertion();
            }

            [Test]
            public void HappyDay_02_SingleElement()
            {
                CommandTest Test = new CommandTest();

                // Arrange
                string _methodSourceCode = "x = \"abc\";\ny = \",\";\nz = x.Split(y);";

                OALProgram programInstance = new OALProgram();
                CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

                CDMethod owningMethod = new CDMethod(owningClass, "Method1", "");
                owningClass.AddMethod(owningMethod);

                // Act
                EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
                owningMethod.ExecutableCode = methodScope;
                programInstance.SuperScope = methodScope;

                EXEExecutionResult _executionResult = PerformExecution(programInstance);

                // Assert
                Test.Declare(methodScope, _executionResult);

                Test.Variables
                        .ExpectVariable("x", new EXEValueString("\"abc\""))
                        .ExpectVariable("y", new EXEValueString("\",\""))
                        .ExpectVariable
                        (
                            "z",
                            new EXEValueArray("string[]")
                            {
                                Elements = new List<EXEValueBase>()
                                {
                                new EXEValueString("\"abc\"")
                                }
                            }
                        )
                        .ExpectVariable("self", methodScope.OwningObject);

                Test.PerformAssertion();
            }

            [Test]
            public void HappyDay_03_EmptyString()
            {
                CommandTest Test = new CommandTest();

                // Arrange
                string _methodSourceCode = "x = \"\";\ny = \",\";\nz = x.Split(y);";

                OALProgram programInstance = new OALProgram();
                CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

                CDMethod owningMethod = new CDMethod(owningClass, "Method1", "");
                owningClass.AddMethod(owningMethod);

                // Act
                EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
                owningMethod.ExecutableCode = methodScope;
                programInstance.SuperScope = methodScope;

                EXEExecutionResult _executionResult = PerformExecution(programInstance);

                // Assert
                Test.Declare(methodScope, _executionResult);

                Test.Variables
                        .ExpectVariable("x", new EXEValueString("\"\""))
                        .ExpectVariable("y", new EXEValueString("\",\""))
                        .ExpectVariable
                        (
                            "z",
                            new EXEValueArray("string[]")
                            {
                                Elements = new List<EXEValueBase>()
                                {
                                new EXEValueString("\"\"")
                                }
                            }
                        )
                        .ExpectVariable("self", methodScope.OwningObject);

                Test.PerformAssertion();
            }
        }

        [TestFixture]
        public class FirstIndexOf : StandardTest
        {
            [Test]
            public void HappyDay_01_MultipleOccurences_01_SingleCharacterSubstring()
            {
                CommandTest Test = new CommandTest();

                // Arrange
                string _methodSourceCode = "x = \"aoboc\";\ny = \"o\";\nz = x.FirstIndexOf(y);";

                OALProgram programInstance = new OALProgram();
                CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

                CDMethod owningMethod = new CDMethod(owningClass, "Method1", "");
                owningClass.AddMethod(owningMethod);

                // Act
                EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
                owningMethod.ExecutableCode = methodScope;
                programInstance.SuperScope = methodScope;

                EXEExecutionResult _executionResult = PerformExecution(programInstance);

                // Assert
                Test.Declare(methodScope, _executionResult);

                Test.Variables
                        .ExpectVariable("x", new EXEValueString("\"aoboc\""))
                        .ExpectVariable("y", new EXEValueString("\"o\""))
                        .ExpectVariable("z", new EXEValueInt("1"))
                        .ExpectVariable("self", methodScope.OwningObject);

                Test.PerformAssertion();
            }

            [Test]
            public void HappyDay_01_MultipleOccurences_02_MultiCharacterSubstring()
            {
                CommandTest Test = new CommandTest();

                // Arrange
                string _methodSourceCode = "x = \"aoooboooc\";\ny = \"ooo\";\nz = x.FirstIndexOf(y);";

                OALProgram programInstance = new OALProgram();
                CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

                CDMethod owningMethod = new CDMethod(owningClass, "Method1", "");
                owningClass.AddMethod(owningMethod);

                // Act
                EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
                owningMethod.ExecutableCode = methodScope;
                programInstance.SuperScope = methodScope;

                EXEExecutionResult _executionResult = PerformExecution(programInstance);

                // Assert
                Test.Declare(methodScope, _executionResult);

                Test.Variables
                        .ExpectVariable("x", new EXEValueString("\"aoooboooc\""))
                        .ExpectVariable("y", new EXEValueString("\"ooo\""))
                        .ExpectVariable("z", new EXEValueInt("1"))
                        .ExpectVariable("self", methodScope.OwningObject);

                Test.PerformAssertion();
            }

            [Test]
            public void HappyDay_02_SingleOccurence_01_SingleCharacterSubstring()
            {
                CommandTest Test = new CommandTest();

                // Arrange
                string _methodSourceCode = "x = \"abco\";\ny = \"o\";\nz = x.FirstIndexOf(y);";

                OALProgram programInstance = new OALProgram();
                CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

                CDMethod owningMethod = new CDMethod(owningClass, "Method1", "");
                owningClass.AddMethod(owningMethod);

                // Act
                EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
                owningMethod.ExecutableCode = methodScope;
                programInstance.SuperScope = methodScope;

                EXEExecutionResult _executionResult = PerformExecution(programInstance);

                // Assert
                Test.Declare(methodScope, _executionResult);

                Test.Variables
                        .ExpectVariable("x", new EXEValueString("\"abco\""))
                        .ExpectVariable("y", new EXEValueString("\"o\""))
                        .ExpectVariable("z", new EXEValueInt("3"))
                        .ExpectVariable("self", methodScope.OwningObject);

                Test.PerformAssertion();
            }

            [Test]
            public void HappyDay_02_SingleOccurence_02_MultiCharacterSubstring()
            {
                CommandTest Test = new CommandTest();

                // Arrange
                string _methodSourceCode = "x = \"abcooo\";\ny = \"ooo\";\nz = x.FirstIndexOf(y);";

                OALProgram programInstance = new OALProgram();
                CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

                CDMethod owningMethod = new CDMethod(owningClass, "Method1", "");
                owningClass.AddMethod(owningMethod);

                // Act
                EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
                owningMethod.ExecutableCode = methodScope;
                programInstance.SuperScope = methodScope;

                EXEExecutionResult _executionResult = PerformExecution(programInstance);

                // Assert
                Test.Declare(methodScope, _executionResult);

                Test.Variables
                        .ExpectVariable("x", new EXEValueString("\"abcooo\""))
                        .ExpectVariable("y", new EXEValueString("\"ooo\""))
                        .ExpectVariable("z", new EXEValueInt("3"))
                        .ExpectVariable("self", methodScope.OwningObject);

                Test.PerformAssertion();
            }

            [Test]
            public void HappyDay_03_NoOccurence_02_MultiCharacterSubstring()
            {
                CommandTest Test = new CommandTest();

                // Arrange
                string _methodSourceCode = "x = \"abc\";\ny = \"o\";\nz = x.FirstIndexOf(y);";

                OALProgram programInstance = new OALProgram();
                CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

                CDMethod owningMethod = new CDMethod(owningClass, "Method1", "");
                owningClass.AddMethod(owningMethod);

                // Act
                EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
                owningMethod.ExecutableCode = methodScope;
                programInstance.SuperScope = methodScope;

                EXEExecutionResult _executionResult = PerformExecution(programInstance);

                // Assert
                Test.Declare(methodScope, _executionResult);

                Test.Variables
                        .ExpectVariable("x", new EXEValueString("\"abc\""))
                        .ExpectVariable("y", new EXEValueString("\"o\""))
                        .ExpectVariable("z", new EXEValueInt("-1"))
                        .ExpectVariable("self", methodScope.OwningObject);

                Test.PerformAssertion();
            }
        }

        [TestFixture]
        public class AllIndexesOf : StandardTest
        {
            [Test]
            public void HappyDay_01_MultipleElements_01_SingleLetterDelimiter()
            {
                CommandTest Test = new CommandTest();

                // Arrange
                string _methodSourceCode = "x = \"aoboc\";\ny = \"o\";\nz = x.AllIndexesOf(y);";

                OALProgram programInstance = new OALProgram();
                CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

                CDMethod owningMethod = new CDMethod(owningClass, "Method1", "");
                owningClass.AddMethod(owningMethod);

                // Act
                EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
                owningMethod.ExecutableCode = methodScope;
                programInstance.SuperScope = methodScope;

                EXEExecutionResult _executionResult = PerformExecution(programInstance);

                // Assert
                Test.Declare(methodScope, _executionResult);

                Test.Variables
                        .ExpectVariable("x", new EXEValueString("\"aoboc\""))
                        .ExpectVariable("y", new EXEValueString("\"o\""))
                        .ExpectVariable
                        (
                            "z",
                            new EXEValueArray("integer[]")
                            {
                                Elements = new List<EXEValueBase>()
                                {
                                    new EXEValueInt(1),
                                    new EXEValueInt(3)
                                }
                            }
                        )
                        .ExpectVariable("self", methodScope.OwningObject);

                Test.PerformAssertion();
            }

            [Test]
            public void HappyDay_01_MultipleElements_02_MultipleLetterDelimiter()
            {
                CommandTest Test = new CommandTest();

                // Arrange
                string _methodSourceCode = "x = \"aoobooc\";\ny = \"oo\";\nz = x.AllIndexesOf(y);";

                OALProgram programInstance = new OALProgram();
                CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

                CDMethod owningMethod = new CDMethod(owningClass, "Method1", "");
                owningClass.AddMethod(owningMethod);

                // Act
                EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
                owningMethod.ExecutableCode = methodScope;
                programInstance.SuperScope = methodScope;

                EXEExecutionResult _executionResult = PerformExecution(programInstance);

                // Assert
                Test.Declare(methodScope, _executionResult);

                Test.Variables
                        .ExpectVariable("x", new EXEValueString("\"aoobooc\""))
                        .ExpectVariable("y", new EXEValueString("\"oo\""))
                        .ExpectVariable
                        (
                            "z",
                            new EXEValueArray("integer[]")
                            {
                                Elements = new List<EXEValueBase>()
                                {
                                    new EXEValueInt(1),
                                    new EXEValueInt(4)
                                }
                            }
                        )
                        .ExpectVariable("self", methodScope.OwningObject);

                Test.PerformAssertion();
            }

            [Test]
            public void HappyDay_02_SingleElement_01_SingleLetterDelimiter()
            {
                CommandTest Test = new CommandTest();

                // Arrange
                string _methodSourceCode = "x = \"aboc\";\ny = \"o\";\nz = x.AllIndexesOf(y);";

                OALProgram programInstance = new OALProgram();
                CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

                CDMethod owningMethod = new CDMethod(owningClass, "Method1", "");
                owningClass.AddMethod(owningMethod);

                // Act
                EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
                owningMethod.ExecutableCode = methodScope;
                programInstance.SuperScope = methodScope;

                EXEExecutionResult _executionResult = PerformExecution(programInstance);

                // Assert
                Test.Declare(methodScope, _executionResult);

                Test.Variables
                        .ExpectVariable("x", new EXEValueString("\"aboc\""))
                        .ExpectVariable("y", new EXEValueString("\"o\""))
                        .ExpectVariable
                        (
                            "z",
                            new EXEValueArray("integer[]")
                            {
                                Elements = new List<EXEValueBase>()
                                {
                                    new EXEValueInt(2)
                                }
                            }
                        )
                        .ExpectVariable("self", methodScope.OwningObject);

                Test.PerformAssertion();
            }

            [Test]
            public void HappyDay_02_SingleElement_02_MultipleLetterDelimiter()
            {
                CommandTest Test = new CommandTest();

                // Arrange
                string _methodSourceCode = "x = \"abooc\";\ny = \"oo\";\nz = x.AllIndexesOf(y);";

                OALProgram programInstance = new OALProgram();
                CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

                CDMethod owningMethod = new CDMethod(owningClass, "Method1", "");
                owningClass.AddMethod(owningMethod);

                // Act
                EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
                owningMethod.ExecutableCode = methodScope;
                programInstance.SuperScope = methodScope;

                EXEExecutionResult _executionResult = PerformExecution(programInstance);

                // Assert
                Test.Declare(methodScope, _executionResult);

                Test.Variables
                        .ExpectVariable("x", new EXEValueString("\"abooc\""))
                        .ExpectVariable("y", new EXEValueString("\"oo\""))
                        .ExpectVariable
                        (
                            "z",
                            new EXEValueArray("integer[]")
                            {
                                Elements = new List<EXEValueBase>()
                                {
                                    new EXEValueInt(2)
                                }
                            }
                        )
                        .ExpectVariable("self", methodScope.OwningObject);

                Test.PerformAssertion();
            }

            [Test]
            public void HappyDay_03_SelfOverlappingDelimiter()
            {
                CommandTest Test = new CommandTest();

                // Arrange
                string _methodSourceCode = "x = \"abooooc\";\ny = \"oo\";\nz = x.AllIndexesOf(y);";

                OALProgram programInstance = new OALProgram();
                CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

                CDMethod owningMethod = new CDMethod(owningClass, "Method1", "");
                owningClass.AddMethod(owningMethod);

                // Act
                EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
                owningMethod.ExecutableCode = methodScope;
                programInstance.SuperScope = methodScope;

                EXEExecutionResult _executionResult = PerformExecution(programInstance);

                // Assert
                Test.Declare(methodScope, _executionResult);

                Test.Variables
                        .ExpectVariable("x", new EXEValueString("\"abooooc\""))
                        .ExpectVariable("y", new EXEValueString("\"oo\""))
                        .ExpectVariable
                        (
                            "z",
                            new EXEValueArray("integer[]")
                            {
                                Elements = new List<EXEValueBase>()
                                {
                                    new EXEValueInt(2),
                                    new EXEValueInt(3),
                                    new EXEValueInt(4)
                                }
                            }
                        )
                        .ExpectVariable("self", methodScope.OwningObject);

                Test.PerformAssertion();
            }
        }

        [TestFixture]
        public class Length : StandardTest
        {
            [Test]
            public void HappyDay_01_LongString()
            {
                CommandTest Test = new CommandTest();

                // Arrange
                string _methodSourceCode = "x = \"abcd\";\ny = x.Length();";

                OALProgram programInstance = new OALProgram();
                CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

                CDMethod owningMethod = new CDMethod(owningClass, "Method1", "");
                owningClass.AddMethod(owningMethod);

                // Act
                EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
                owningMethod.ExecutableCode = methodScope;
                programInstance.SuperScope = methodScope;

                EXEExecutionResult _executionResult = PerformExecution(programInstance);

                // Assert
                Test.Declare(methodScope, _executionResult);

                Test.Variables
                        .ExpectVariable("x", new EXEValueString("\"abcd\""))
                        .ExpectVariable("y", new EXEValueInt("4"))
                        .ExpectVariable("self", methodScope.OwningObject);

                Test.PerformAssertion();
            }

            [Test]
            public void HappyDay_02_SingleCharacterString()
            {
                CommandTest Test = new CommandTest();

                // Arrange
                string _methodSourceCode = "x = \"a\";\ny = x.Length();";

                OALProgram programInstance = new OALProgram();
                CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

                CDMethod owningMethod = new CDMethod(owningClass, "Method1", "");
                owningClass.AddMethod(owningMethod);

                // Act
                EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
                owningMethod.ExecutableCode = methodScope;
                programInstance.SuperScope = methodScope;

                EXEExecutionResult _executionResult = PerformExecution(programInstance);

                // Assert
                Test.Declare(methodScope, _executionResult);

                Test.Variables
                        .ExpectVariable("x", new EXEValueString("\"a\""))
                        .ExpectVariable("y", new EXEValueInt("1"))
                        .ExpectVariable("self", methodScope.OwningObject);

                Test.PerformAssertion();
            }

            [Test]
            public void HappyDay_03_EmptyString()
            {
                CommandTest Test = new CommandTest();

                // Arrange
                string _methodSourceCode = "x = \"\";\ny = x.Length();";

                OALProgram programInstance = new OALProgram();
                CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

                CDMethod owningMethod = new CDMethod(owningClass, "Method1", "");
                owningClass.AddMethod(owningMethod);

                // Act
                EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
                owningMethod.ExecutableCode = methodScope;
                programInstance.SuperScope = methodScope;

                EXEExecutionResult _executionResult = PerformExecution(programInstance);

                // Assert
                Test.Declare(methodScope, _executionResult);

                Test.Variables
                        .ExpectVariable("x", new EXEValueString("\"\""))
                        .ExpectVariable("y", new EXEValueInt("0"))
                        .ExpectVariable("self", methodScope.OwningObject);

                Test.PerformAssertion();
            }
        }

        [TestFixture]
        public class SubstringFrom : StandardTest
        {
            [Test]
            public void HappyDay_01_FromStart()
            {
                CommandTest Test = new CommandTest();

                // Arrange
                string _methodSourceCode = "x = \"abc\";\ny = 0;\nz = x.SubstringFrom(y);";

                OALProgram programInstance = new OALProgram();
                CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

                CDMethod owningMethod = new CDMethod(owningClass, "Method1", "");
                owningClass.AddMethod(owningMethod);

                // Act
                EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
                owningMethod.ExecutableCode = methodScope;
                programInstance.SuperScope = methodScope;

                EXEExecutionResult _executionResult = PerformExecution(programInstance);

                // Assert
                Test.Declare(methodScope, _executionResult);

                Test.Variables
                        .ExpectVariable("x", new EXEValueString("\"abc\""))
                        .ExpectVariable("y", new EXEValueInt("0"))
                        .ExpectVariable("z", new EXEValueString("\"abc\""))
                        .ExpectVariable("self", methodScope.OwningObject);

                Test.PerformAssertion();
            }

            [Test]
            public void HappyDay_02_FromSecond()
            {
                CommandTest Test = new CommandTest();

                // Arrange
                string _methodSourceCode = "x = \"abc\";\ny = 1;\nz = x.SubstringFrom(y);";

                OALProgram programInstance = new OALProgram();
                CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

                CDMethod owningMethod = new CDMethod(owningClass, "Method1", "");
                owningClass.AddMethod(owningMethod);

                // Act
                EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
                owningMethod.ExecutableCode = methodScope;
                programInstance.SuperScope = methodScope;

                EXEExecutionResult _executionResult = PerformExecution(programInstance);

                // Assert
                Test.Declare(methodScope, _executionResult);

                Test.Variables
                        .ExpectVariable("x", new EXEValueString("\"abc\""))
                        .ExpectVariable("y", new EXEValueInt("1"))
                        .ExpectVariable("z", new EXEValueString("\"bc\""))
                        .ExpectVariable("self", methodScope.OwningObject);

                Test.PerformAssertion();
            }

            [Test]
            public void HappyDay_03_FromThird()
            {
                CommandTest Test = new CommandTest();

                // Arrange
                string _methodSourceCode = "x = \"abc\";\ny = 2;\nz = x.SubstringFrom(y);";

                OALProgram programInstance = new OALProgram();
                CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

                CDMethod owningMethod = new CDMethod(owningClass, "Method1", "");
                owningClass.AddMethod(owningMethod);

                // Act
                EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
                owningMethod.ExecutableCode = methodScope;
                programInstance.SuperScope = methodScope;

                EXEExecutionResult _executionResult = PerformExecution(programInstance);

                // Assert
                Test.Declare(methodScope, _executionResult);

                Test.Variables
                        .ExpectVariable("x", new EXEValueString("\"abc\""))
                        .ExpectVariable("y", new EXEValueInt("2"))
                        .ExpectVariable("z", new EXEValueString("\"c\""))
                        .ExpectVariable("self", methodScope.OwningObject);

                Test.PerformAssertion();
            }

            [Test]
            public void HappyDay_04_FromOutsideRange()
            {
                CommandTest Test = new CommandTest();

                // Arrange
                string _methodSourceCode = "x = \"abc\";\ny = 3;\nz = x.SubstringFrom(y);";

                OALProgram programInstance = new OALProgram();
                CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

                CDMethod owningMethod = new CDMethod(owningClass, "Method1", "");
                owningClass.AddMethod(owningMethod);

                // Act
                EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
                owningMethod.ExecutableCode = methodScope;
                programInstance.SuperScope = methodScope;

                EXEExecutionResult _executionResult = PerformExecution(programInstance);

                // Assert
                Test.Declare(methodScope, _executionResult);

                Test.Variables
                        .ExpectVariable("x", new EXEValueString("\"abc\""))
                        .ExpectVariable("y", new EXEValueInt("3"))
                        .ExpectVariable("z", new EXEValueString("\"\""))
                        .ExpectVariable("self", methodScope.OwningObject);

                Test.PerformAssertion();
            }
        }


        [TestFixture]
        public class Substring : StandardTest
        {
            [Test]
            public void HappyDay_01_FromStart()
            {
                CommandTest Test = new CommandTest();

                // Arrange
                string _methodSourceCode = "x = \"abcd\";\ny = 0;\nz = 3;\na = x.Substring(y, z);";

                OALProgram programInstance = new OALProgram();
                CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

                CDMethod owningMethod = new CDMethod(owningClass, "Method1", "");
                owningClass.AddMethod(owningMethod);

                // Act
                EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
                owningMethod.ExecutableCode = methodScope;
                programInstance.SuperScope = methodScope;

                EXEExecutionResult _executionResult = PerformExecution(programInstance);

                // Assert
                Test.Declare(methodScope, _executionResult);

                Test.Variables
                        .ExpectVariable("x", new EXEValueString("\"abcd\""))
                        .ExpectVariable("y", new EXEValueInt("0"))
                        .ExpectVariable("z", new EXEValueInt("3"))
                        .ExpectVariable("a", new EXEValueString("\"abc\""))
                        .ExpectVariable("self", methodScope.OwningObject);

                Test.PerformAssertion();
            }

            [Test]
            public void HappyDay_02_ToEnd()
            {
                CommandTest Test = new CommandTest();

                // Arrange
                string _methodSourceCode = "x = \"abcd\";\ny = 1;\nz = 3;\na = x.Substring(y, z);";

                OALProgram programInstance = new OALProgram();
                CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

                CDMethod owningMethod = new CDMethod(owningClass, "Method1", "");
                owningClass.AddMethod(owningMethod);

                // Act
                EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
                owningMethod.ExecutableCode = methodScope;
                programInstance.SuperScope = methodScope;

                EXEExecutionResult _executionResult = PerformExecution(programInstance);

                // Assert
                Test.Declare(methodScope, _executionResult);

                Test.Variables
                        .ExpectVariable("x", new EXEValueString("\"abcd\""))
                        .ExpectVariable("y", new EXEValueInt("1"))
                        .ExpectVariable("z", new EXEValueInt("3"))
                        .ExpectVariable("a", new EXEValueString("\"bcd\""))
                        .ExpectVariable("self", methodScope.OwningObject);

                Test.PerformAssertion();
            }

            [Test]
            public void HappyDay_03_Middle()
            {
                CommandTest Test = new CommandTest();

                // Arrange
                string _methodSourceCode = "x = \"abcd\";\ny = 1;\nz = 2;\na = x.Substring(y, z);";

                OALProgram programInstance = new OALProgram();
                CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

                CDMethod owningMethod = new CDMethod(owningClass, "Method1", "");
                owningClass.AddMethod(owningMethod);

                // Act
                EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
                owningMethod.ExecutableCode = methodScope;
                programInstance.SuperScope = methodScope;

                EXEExecutionResult _executionResult = PerformExecution(programInstance);

                // Assert
                Test.Declare(methodScope, _executionResult);

                Test.Variables
                        .ExpectVariable("x", new EXEValueString("\"abcd\""))
                        .ExpectVariable("y", new EXEValueInt("1"))
                        .ExpectVariable("z", new EXEValueInt("2"))
                        .ExpectVariable("a", new EXEValueString("\"bc\""))
                        .ExpectVariable("self", methodScope.OwningObject);

                Test.PerformAssertion();
            }

            [Test]
            public void HappyDay_04_Length0()
            {
                CommandTest Test = new CommandTest();

                // Arrange
                string _methodSourceCode = "x = \"abcd\";\ny = 1;\nz = 0;\na = x.Substring(y, z);";

                OALProgram programInstance = new OALProgram();
                CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

                CDMethod owningMethod = new CDMethod(owningClass, "Method1", "");
                owningClass.AddMethod(owningMethod);

                // Act
                EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
                owningMethod.ExecutableCode = methodScope;
                programInstance.SuperScope = methodScope;

                EXEExecutionResult _executionResult = PerformExecution(programInstance);

                // Assert
                Test.Declare(methodScope, _executionResult);

                Test.Variables
                        .ExpectVariable("x", new EXEValueString("\"abcd\""))
                        .ExpectVariable("y", new EXEValueInt("1"))
                        .ExpectVariable("z", new EXEValueInt("0"))
                        .ExpectVariable("a", new EXEValueString("\"\""))
                        .ExpectVariable("self", methodScope.OwningObject);

                Test.PerformAssertion();
            }
        }

        [TestFixture]
        public class Contains : StandardTest
        {
            [Test]
            public void HappyDay_01_MultipleOccurences_01_SingleCharacterSubstring()
            {
                CommandTest Test = new CommandTest();

                // Arrange
                string _methodSourceCode = "x = \"aoboc\";\ny = \"o\";\nz = x.Contains(y);";

                OALProgram programInstance = new OALProgram();
                CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

                CDMethod owningMethod = new CDMethod(owningClass, "Method1", "");
                owningClass.AddMethod(owningMethod);

                // Act
                EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
                owningMethod.ExecutableCode = methodScope;
                programInstance.SuperScope = methodScope;

                EXEExecutionResult _executionResult = PerformExecution(programInstance);

                // Assert
                Test.Declare(methodScope, _executionResult);

                Test.Variables
                        .ExpectVariable("x", new EXEValueString("\"aoboc\""))
                        .ExpectVariable("y", new EXEValueString("\"o\""))
                        .ExpectVariable("z", new EXEValueBool(true))
                        .ExpectVariable("self", methodScope.OwningObject);

                Test.PerformAssertion();
            }

            [Test]
            public void HappyDay_01_MultipleOccurences_02_MultiCharacterSubstring()
            {
                CommandTest Test = new CommandTest();

                // Arrange
                string _methodSourceCode = "x = \"aoooboooc\";\ny = \"ooo\";\nz = x.Contains(y);";

                OALProgram programInstance = new OALProgram();
                CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

                CDMethod owningMethod = new CDMethod(owningClass, "Method1", "");
                owningClass.AddMethod(owningMethod);

                // Act
                EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
                owningMethod.ExecutableCode = methodScope;
                programInstance.SuperScope = methodScope;

                EXEExecutionResult _executionResult = PerformExecution(programInstance);

                // Assert
                Test.Declare(methodScope, _executionResult);

                Test.Variables
                        .ExpectVariable("x", new EXEValueString("\"aoooboooc\""))
                        .ExpectVariable("y", new EXEValueString("\"ooo\""))
                        .ExpectVariable("z", new EXEValueBool(true))
                        .ExpectVariable("self", methodScope.OwningObject);

                Test.PerformAssertion();
            }

            [Test]
            public void HappyDay_02_SingleOccurence_01_SingleCharacterSubstring()
            {
                CommandTest Test = new CommandTest();

                // Arrange
                string _methodSourceCode = "x = \"abco\";\ny = \"o\";\nz = x.Contains(y);";

                OALProgram programInstance = new OALProgram();
                CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

                CDMethod owningMethod = new CDMethod(owningClass, "Method1", "");
                owningClass.AddMethod(owningMethod);

                // Act
                EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
                owningMethod.ExecutableCode = methodScope;
                programInstance.SuperScope = methodScope;

                EXEExecutionResult _executionResult = PerformExecution(programInstance);

                // Assert
                Test.Declare(methodScope, _executionResult);

                Test.Variables
                        .ExpectVariable("x", new EXEValueString("\"abco\""))
                        .ExpectVariable("y", new EXEValueString("\"o\""))
                        .ExpectVariable("z", new EXEValueBool(true))
                        .ExpectVariable("self", methodScope.OwningObject);

                Test.PerformAssertion();
            }

            [Test]
            public void HappyDay_02_SingleOccurence_02_MultiCharacterSubstring()
            {
                CommandTest Test = new CommandTest();

                // Arrange
                string _methodSourceCode = "x = \"abcooo\";\ny = \"ooo\";\nz = x.Contains(y);";

                OALProgram programInstance = new OALProgram();
                CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

                CDMethod owningMethod = new CDMethod(owningClass, "Method1", "");
                owningClass.AddMethod(owningMethod);

                // Act
                EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
                owningMethod.ExecutableCode = methodScope;
                programInstance.SuperScope = methodScope;

                EXEExecutionResult _executionResult = PerformExecution(programInstance);

                // Assert
                Test.Declare(methodScope, _executionResult);

                Test.Variables
                        .ExpectVariable("x", new EXEValueString("\"abcooo\""))
                        .ExpectVariable("y", new EXEValueString("\"ooo\""))
                        .ExpectVariable("z", new EXEValueBool(true))
                        .ExpectVariable("self", methodScope.OwningObject);

                Test.PerformAssertion();
            }

            [Test]
            public void HappyDay_03_NoOccurence_02_MultiCharacterSubstring()
            {
                CommandTest Test = new CommandTest();

                // Arrange
                string _methodSourceCode = "x = \"abc\";\ny = \"o\";\nz = x.Contains(y);";

                OALProgram programInstance = new OALProgram();
                CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

                CDMethod owningMethod = new CDMethod(owningClass, "Method1", "");
                owningClass.AddMethod(owningMethod);

                // Act
                EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
                owningMethod.ExecutableCode = methodScope;
                programInstance.SuperScope = methodScope;

                EXEExecutionResult _executionResult = PerformExecution(programInstance);

                // Assert
                Test.Declare(methodScope, _executionResult);

                Test.Variables
                        .ExpectVariable("x", new EXEValueString("\"abc\""))
                        .ExpectVariable("y", new EXEValueString("\"o\""))
                        .ExpectVariable("z", new EXEValueBool(false))
                        .ExpectVariable("self", methodScope.OwningObject);

                Test.PerformAssertion();
            }
        }

        [TestFixture]
        public class Replace : StandardTest
        {
            [Test]
            public void HappyDay_01_MultipleOccurences_01_SingleToSingle()
            {
                CommandTest Test = new CommandTest();

                // Arrange
                string _methodSourceCode = "x = \"aoboc\";\ny = \"o\";\nz = \"m\";\na = x.Replace(y, z);";

                OALProgram programInstance = new OALProgram();
                CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

                CDMethod owningMethod = new CDMethod(owningClass, "Method1", EXETypes.StringTypeName);
                owningClass.AddMethod(owningMethod);

                // Act
                EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
                owningMethod.ExecutableCode = methodScope;
                programInstance.SuperScope = methodScope;

                EXEExecutionResult _executionResult = PerformExecution(programInstance);

                // Assert
                Test.Declare(methodScope, _executionResult);

                Test.Variables
                        .ExpectVariable("x", new EXEValueString("\"aoboc\""))
                        .ExpectVariable("y", new EXEValueString("\"o\""))
                        .ExpectVariable("z", new EXEValueString("\"m\""))
                        .ExpectVariable("a", new EXEValueString("\"ambmc\""))
                        .ExpectVariable("self", methodScope.OwningObject);

                Test.PerformAssertion();
            }

            [Test]
            public void HappyDay_01_MultipleOccurences_02_SingleToMulti()
            {
                CommandTest Test = new CommandTest();

                // Arrange
                string _methodSourceCode = "x = \"aoboc\";\ny = \"o\";\nz = \"mno\";\na = x.Replace(y, z);";

                OALProgram programInstance = new OALProgram();
                CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

                CDMethod owningMethod = new CDMethod(owningClass, "Method1", EXETypes.StringTypeName);
                owningClass.AddMethod(owningMethod);

                // Act
                EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
                owningMethod.ExecutableCode = methodScope;
                programInstance.SuperScope = methodScope;

                EXEExecutionResult _executionResult = PerformExecution(programInstance);

                // Assert
                Test.Declare(methodScope, _executionResult);

                Test.Variables
                        .ExpectVariable("x", new EXEValueString("\"aoboc\""))
                        .ExpectVariable("y", new EXEValueString("\"o\""))
                        .ExpectVariable("z", new EXEValueString("\"mno\""))
                        .ExpectVariable("a", new EXEValueString("\"amnobmnoc\""))
                        .ExpectVariable("self", methodScope.OwningObject);

                Test.PerformAssertion();
            }

            [Test]
            public void HappyDay_01_MultipleOccurences_03_MultiToSingle()
            {
                CommandTest Test = new CommandTest();

                // Arrange
                string _methodSourceCode = "x = \"aoooboooc\";\ny = \"ooo\";\nz = \"m\";\na = x.Replace(y, z);";

                OALProgram programInstance = new OALProgram();
                CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

                CDMethod owningMethod = new CDMethod(owningClass, "Method1", EXETypes.StringTypeName);
                owningClass.AddMethod(owningMethod);

                // Act
                EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
                owningMethod.ExecutableCode = methodScope;
                programInstance.SuperScope = methodScope;

                EXEExecutionResult _executionResult = PerformExecution(programInstance);

                // Assert
                Test.Declare(methodScope, _executionResult);

                Test.Variables
                        .ExpectVariable("x", new EXEValueString("\"aoooboooc\""))
                        .ExpectVariable("y", new EXEValueString("\"ooo\""))
                        .ExpectVariable("z", new EXEValueString("\"m\""))
                        .ExpectVariable("a", new EXEValueString("\"ambmc\""))
                        .ExpectVariable("self", methodScope.OwningObject);

                Test.PerformAssertion();
            }

            [Test]
            public void HappyDay_01_MultipleOccurences_04_MultiToMulti()
            {
                CommandTest Test = new CommandTest();

                // Arrange
                string _methodSourceCode = "x = \"aoooboooc\";\ny = \"ooo\";\nz = \"mno\";\na = x.Replace(y, z);";

                OALProgram programInstance = new OALProgram();
                CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

                CDMethod owningMethod = new CDMethod(owningClass, "Method1", EXETypes.StringTypeName);
                owningClass.AddMethod(owningMethod);

                // Act
                EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
                owningMethod.ExecutableCode = methodScope;
                programInstance.SuperScope = methodScope;

                EXEExecutionResult _executionResult = PerformExecution(programInstance);

                // Assert
                Test.Declare(methodScope, _executionResult);

                Test.Variables
                        .ExpectVariable("x", new EXEValueString("\"aoooboooc\""))
                        .ExpectVariable("y", new EXEValueString("\"ooo\""))
                        .ExpectVariable("z", new EXEValueString("\"mno\""))
                        .ExpectVariable("a", new EXEValueString("\"amnobmnoc\""))
                        .ExpectVariable("self", methodScope.OwningObject);

                Test.PerformAssertion();
            }

            [Test]
            public void HappyDay_02_SingleOccurence_01_SingleToSingle()
            {
                CommandTest Test = new CommandTest();

                // Arrange
                string _methodSourceCode = "x = \"aobc\";\ny = \"o\";\nz = \"m\";\na = x.Replace(y, z);";

                OALProgram programInstance = new OALProgram();
                CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

                CDMethod owningMethod = new CDMethod(owningClass, "Method1", EXETypes.StringTypeName);
                owningClass.AddMethod(owningMethod);

                // Act
                EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
                owningMethod.ExecutableCode = methodScope;
                programInstance.SuperScope = methodScope;

                EXEExecutionResult _executionResult = PerformExecution(programInstance);

                // Assert
                Test.Declare(methodScope, _executionResult);

                Test.Variables
                        .ExpectVariable("x", new EXEValueString("\"aobc\""))
                        .ExpectVariable("y", new EXEValueString("\"o\""))
                        .ExpectVariable("z", new EXEValueString("\"m\""))
                        .ExpectVariable("a", new EXEValueString("\"ambc\""))
                        .ExpectVariable("self", methodScope.OwningObject);

                Test.PerformAssertion();
            }

            [Test]
            public void HappyDay_02_SingleOccurence_02_SingleToMulti()
            {
                CommandTest Test = new CommandTest();

                // Arrange
                string _methodSourceCode = "x = \"aobc\";\ny = \"o\";\nz = \"mno\";\na = x.Replace(y, z);";

                OALProgram programInstance = new OALProgram();
                CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

                CDMethod owningMethod = new CDMethod(owningClass, "Method1", EXETypes.StringTypeName);
                owningClass.AddMethod(owningMethod);

                // Act
                EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
                owningMethod.ExecutableCode = methodScope;
                programInstance.SuperScope = methodScope;

                EXEExecutionResult _executionResult = PerformExecution(programInstance);

                // Assert
                Test.Declare(methodScope, _executionResult);

                Test.Variables
                        .ExpectVariable("x", new EXEValueString("\"aobc\""))
                        .ExpectVariable("y", new EXEValueString("\"o\""))
                        .ExpectVariable("z", new EXEValueString("\"mno\""))
                        .ExpectVariable("a", new EXEValueString("\"amnobc\""))
                        .ExpectVariable("self", methodScope.OwningObject);

                Test.PerformAssertion();
            }

            [Test]
            public void HappyDay_02_SingleOccurence_03_MultiToSingle()
            {
                CommandTest Test = new CommandTest();

                // Arrange
                string _methodSourceCode = "x = \"aooobc\";\ny = \"ooo\";\nz = \"m\";\na = x.Replace(y, z);";

                OALProgram programInstance = new OALProgram();
                CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

                CDMethod owningMethod = new CDMethod(owningClass, "Method1", EXETypes.StringTypeName);
                owningClass.AddMethod(owningMethod);

                // Act
                EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
                owningMethod.ExecutableCode = methodScope;
                programInstance.SuperScope = methodScope;

                EXEExecutionResult _executionResult = PerformExecution(programInstance);

                // Assert
                Test.Declare(methodScope, _executionResult);

                Test.Variables
                        .ExpectVariable("x", new EXEValueString("\"aooobc\""))
                        .ExpectVariable("y", new EXEValueString("\"ooo\""))
                        .ExpectVariable("z", new EXEValueString("\"m\""))
                        .ExpectVariable("a", new EXEValueString("\"ambc\""))
                        .ExpectVariable("self", methodScope.OwningObject);

                Test.PerformAssertion();
            }

            [Test]
            public void HappyDay_02_SingleOccurence_04_MultiToMulti()
            {
                CommandTest Test = new CommandTest();

                // Arrange
                string _methodSourceCode = "x = \"aboooc\";\ny = \"ooo\";\nz = \"mno\";\na = x.Replace(y, z);";

                OALProgram programInstance = new OALProgram();
                CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

                CDMethod owningMethod = new CDMethod(owningClass, "Method1", EXETypes.StringTypeName);
                owningClass.AddMethod(owningMethod);

                // Act
                EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
                owningMethod.ExecutableCode = methodScope;
                programInstance.SuperScope = methodScope;

                EXEExecutionResult _executionResult = PerformExecution(programInstance);

                // Assert
                Test.Declare(methodScope, _executionResult);

                Test.Variables
                        .ExpectVariable("x", new EXEValueString("\"aboooc\""))
                        .ExpectVariable("y", new EXEValueString("\"ooo\""))
                        .ExpectVariable("z", new EXEValueString("\"mno\""))
                        .ExpectVariable("a", new EXEValueString("\"abmnoc\""))
                        .ExpectVariable("self", methodScope.OwningObject);

                Test.PerformAssertion();
            }

            [Test]
            public void HappyDay_03_NoOccurence()
            {
                CommandTest Test = new CommandTest();

                // Arrange
                string _methodSourceCode = "x = \"abc\";\ny = \"o\";\nz = \"m\";\na = x.Replace(y, z);";

                OALProgram programInstance = new OALProgram();
                CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

                CDMethod owningMethod = new CDMethod(owningClass, "Method1", EXETypes.StringTypeName);
                owningClass.AddMethod(owningMethod);

                // Act
                EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
                owningMethod.ExecutableCode = methodScope;
                programInstance.SuperScope = methodScope;

                EXEExecutionResult _executionResult = PerformExecution(programInstance);

                // Assert
                Test.Declare(methodScope, _executionResult);

                Test.Variables
                        .ExpectVariable("x", new EXEValueString("\"abc\""))
                        .ExpectVariable("y", new EXEValueString("\"o\""))
                        .ExpectVariable("z", new EXEValueString("\"m\""))
                        .ExpectVariable("a", new EXEValueString("\"abc\""))
                        .ExpectVariable("self", methodScope.OwningObject);

                Test.PerformAssertion();
            }

            [Test]
            public void HappyDay_04_NewLine()
            {
                CommandTest Test = new CommandTest();

                // Arrange
                string _methodSourceCode = "x = \"abc\nabc\";\ny = \"\n\";\nz = \"\";\na = x.Replace(y, z);";

                OALProgram programInstance = new OALProgram();
                CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

                CDMethod owningMethod = new CDMethod(owningClass, "Method1", EXETypes.StringTypeName);
                owningClass.AddMethod(owningMethod);

                // Act
                EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
                owningMethod.ExecutableCode = methodScope;
                programInstance.SuperScope = methodScope;

                EXEExecutionResult _executionResult = PerformExecution(programInstance);

                // Assert
                Test.Declare(methodScope, _executionResult);

                Test.Variables
                        .ExpectVariable("x", new EXEValueString("\"abc\nabc\""))
                        .ExpectVariable("y", new EXEValueString("\"\n\""))
                        .ExpectVariable("z", new EXEValueString("\"\""))
                        .ExpectVariable("a", new EXEValueString("\"abcabc\""))
                        .ExpectVariable("self", methodScope.OwningObject);

                Test.PerformAssertion();
            }
        }

        [TestFixture]
        public class ParseInt : StandardTest
        {
            [Test]
            // Valid Int
            [TestCase("-974111", -974111)]
            [TestCase("-7", -7)]
            [TestCase("0", 0)]
            [TestCase("7", 7)]
            [TestCase("974111", 974111)]
            // Invalid Int
            [TestCase("abcd", 0)]
            [TestCase("./.*", 0)]
            [TestCase("-.-.-.-.", 0)]
            [TestCase("", 0)]
            [TestCase("\n", 0)]
            [TestCase("78.0", 0)]
            [TestCase("true", 0)]
            [TestCase("[]", 0)]
            public void HappyDay(string x, int y)
            {
                CommandTest Test = new CommandTest();

                // Arrange
                string _methodSourceCode = $"x = \"{x}\";\ny = x.ParseInt();";

                OALProgram programInstance = new OALProgram();
                CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

                CDMethod owningMethod = new CDMethod(owningClass, "Method1", "");
                owningClass.AddMethod(owningMethod);

                // Act
                EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
                owningMethod.ExecutableCode = methodScope;
                programInstance.SuperScope = methodScope;

                EXEExecutionResult _executionResult = PerformExecution(programInstance);

                // Assert
                Test.Declare(methodScope, _executionResult);

                Test.Variables
                        .ExpectVariable("x", new EXEValueString($"\"{x}\""))
                        .ExpectVariable("y", new EXEValueInt(y))
                        .ExpectVariable("self", methodScope.OwningObject);

                Test.PerformAssertion();
            }
        }
    }
}
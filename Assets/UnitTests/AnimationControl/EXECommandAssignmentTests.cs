using System.Linq;
using NUnit.Framework;
using OALProgramControl;
using Assets.Scripts.AnimationControl.OAL;
using System;
using System.Collections.Generic;

namespace Assets.UnitTests.AnimationControl
{
    public class EXECommandAssignmentTests : StandardTest
    {
        [Test]
        public void HappyDay_01_AssignToLocalVariable_01_Literal_01_Int()
        {
            CommandTest Test = new CommandTest();

            // Arrange
            string _methodSourceCode = "x = 5;";

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
                    .ExpectVariable("x", new EXEValueInt(5))
                    .ExpectVariable("self", new EXEValueReference(methodScope.OwningObject));

            Test.PerformAssertion();
        }

        [Test]
        public void HappyDay_01_AssignToLocalVariable_01_Literal_02_Real()
        {
            CommandTest Test = new CommandTest();

            // Arrange
            string _methodSourceCode = "x = 5.05;";

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
                    .ExpectVariable("x", new EXEValueReal("5.05"))
                    .ExpectVariable("self", new EXEValueReference(methodScope.OwningObject));

            Test.PerformAssertion();
        }

        [Test]
        public void HappyDay_01_AssignToLocalVariable_01_Literal_03_String()
        {
            CommandTest Test = new CommandTest();

            // Arrange
            string _methodSourceCode = "x = \"Hello world\";";

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
                    .ExpectVariable("x", new EXEValueString("\"Hello world\""))
                    .ExpectVariable("self", new EXEValueReference(methodScope.OwningObject));

            Test.PerformAssertion();
        }

        [Test]
        public void HappyDay_01_AssignToLocalVariable_01_Literal_04_Bool_01_True()
        {
            CommandTest Test = new CommandTest();

            // Arrange
            string _methodSourceCode = "x = TRUE;";

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
                    .ExpectVariable("x", new EXEValueBool(true))
                    .ExpectVariable("self", new EXEValueReference(methodScope.OwningObject));

            Test.PerformAssertion();
        }

        [Test]
        public void HappyDay_01_AssignToLocalVariable_01_Literal_04_Bool_02_False()
        {
            CommandTest Test = new CommandTest();

            // Arrange
            string _methodSourceCode = "x = FALSE;";

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
                    .ExpectVariable("x", new EXEValueBool(false))
                    .ExpectVariable("self", new EXEValueReference(methodScope.OwningObject));

            Test.PerformAssertion();
        }

        [Test]
        public void HappyDay_01_AssignToLocalVariable_02_FromAnotherVariable_01_Int()
        {
            CommandTest Test = new CommandTest();

            // Arrange
            string _methodSourceCode = "y = 5;\nx = y;";

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
                    .ExpectVariable("x", new EXEValueInt(5))
                    .ExpectVariable("y", new EXEValueInt(5))
                    .ExpectVariable("self", new EXEValueReference(methodScope.OwningObject));

            Test.PerformAssertion();
        }

        [Test]
        public void HappyDay_01_AssignToLocalVariable_02_FromAnotherVariable_02_Real()
        {
            CommandTest Test = new CommandTest();

            // Arrange
            string _methodSourceCode = "y = 5.05;\nx = y;";

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
                    .ExpectVariable("x", new EXEValueReal(5.05m))
                    .ExpectVariable("y", new EXEValueReal(5.05m))
                    .ExpectVariable("self", new EXEValueReference(methodScope.OwningObject));

            Test.PerformAssertion();
        }

        [Test]
        public void HappyDay_01_AssignToLocalVariable_02_FromAnotherVariable_03_String()
        {
            CommandTest Test = new CommandTest();

            // Arrange
            string _methodSourceCode = "y = \"Hello world\";\nx = y;";

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
                    .ExpectVariable("x", new EXEValueString("\"Hello world\""))
                    .ExpectVariable("y", new EXEValueString("\"Hello world\""))
                    .ExpectVariable("self", new EXEValueReference(methodScope.OwningObject));

            Test.PerformAssertion();
        }

        [Test]
        public void HappyDay_01_AssignToLocalVariable_02_FromAnotherVariable_04_Bool_01_True()
        {
            CommandTest Test = new CommandTest();

            // Arrange
            string _methodSourceCode = "y = TRUE;\nx = y;";

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
                    .ExpectVariable("x", new EXEValueBool(true))
                    .ExpectVariable("y", new EXEValueBool(true))
                    .ExpectVariable("self", new EXEValueReference(methodScope.OwningObject));

            Test.PerformAssertion();
        }

        [Test]
        public void HappyDay_01_AssignToLocalVariable_02_FromAnotherVariable_04_Bool_02_False()
        {
            CommandTest Test = new CommandTest();

            // Arrange
            string _methodSourceCode = "y = FALSE;\n x = y;";

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
                    .ExpectVariable("x", new EXEValueBool(false))
                    .ExpectVariable("y", new EXEValueBool(false))
                    .ExpectVariable("self", new EXEValueReference(methodScope.OwningObject));

            Test.PerformAssertion();
        }

        [Test]
        public void HappyDay_01_AssignToLocalVariable_02_FromAnotherVariable_05_DirectSubtype()
        {
            CommandTest Test = new CommandTest();

            // Arrange
            string _methodSourceCode = "x = self;\ncreate object instance y of SubClass1;\nx = y;";

            OALProgram programInstance = new OALProgram();
            CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("SuperClass1");

            CDMethod owningMethod = new CDMethod(owningClass, "Method1", "");
            owningClass.AddMethod(owningMethod);

            CDClass subClass = programInstance.ExecutionSpace.SpawnClass("SubClass1");
            subClass.SuperClass = owningClass;

            // Act
            EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
            owningMethod.ExecutableCode = methodScope;
            programInstance.SuperScope = methodScope;

            EXEExecutionResult _executionResult = PerformExecution(programInstance);

            // Assert
            Test.Declare(methodScope, _executionResult);

            Test.Variables
                    .ExpectVariable("self", new EXEValueReference(methodScope.OwningObject))
                    .ExpectVariable("x", new EXEValueReference(subClass.Instances.First()))
                    .ExpectVariable("y", new EXEValueReference(subClass.Instances.First()));

            Test.PerformAssertion();
        }

        [Test]
        public void HappyDay_01_AssignToLocalVariable_03_FromMethod_01_Trivial()
        {
            CommandTest Test = new CommandTest();

            // Arrange
            string _methodSourceCode1 = "create object instance inst2 of Class2;\nx = inst2.Method2();";
            string _methodSourceCode2 = "return 5";

            OALProgram programInstance = new OALProgram();
            CDClass owningClass1 = programInstance.ExecutionSpace.SpawnClass("Class1");
            CDClass owningClass2 = programInstance.ExecutionSpace.SpawnClass("Class2");

            CDMethod owningMethod1 = new CDMethod(owningClass1, "Method1", "");
            owningClass1.AddMethod(owningMethod1);

            CDMethod owningMethod2 = new CDMethod(owningClass2, "Method2", "integer");
            owningClass2.AddMethod(owningMethod2);

            // Act
            EXEScopeMethod methodScope1 = OALParserBridge.Parse(_methodSourceCode1);
            owningMethod1.ExecutableCode = methodScope1;

            EXEScopeMethod methodScope2 = OALParserBridge.Parse(_methodSourceCode2);
            owningMethod2.ExecutableCode = methodScope2;

            programInstance.SuperScope = methodScope1;

            EXEExecutionResult _executionResult = PerformExecution(programInstance);

            // Assert
            Test.Declare(methodScope1, _executionResult);

            Test.Variables
                    .ExpectVariable("x", new EXEValueInt(5))
                    .ExpectVariable("inst2", new EXEValueReference())
                    .ExpectVariable("self", new EXEValueReference(methodScope1.OwningObject));

            Test.PerformAssertion();
        }

        [Test]
        public void HappyDay_01_AssignToLocalVariable_03_FromMethod_02_Multiple()
        {
            CommandTest Test = new CommandTest();

            // Arrange
            string _methodSourceCode1 = "create object instance inst2 of Class2;\nx = inst2.Method2();";
            string _methodSourceCode2 = "create object instance inst3 of Class3;\nreturn inst3.Method3();";
            string _methodSourceCode3 = "return 5";

            OALProgram programInstance = new OALProgram();
            CDClass owningClass1 = programInstance.ExecutionSpace.SpawnClass("Class1");
            CDClass owningClass2 = programInstance.ExecutionSpace.SpawnClass("Class2");
            CDClass owningClass3 = programInstance.ExecutionSpace.SpawnClass("Class3");

            CDMethod owningMethod1 = new CDMethod(owningClass1, "Method1", "");
            owningClass1.AddMethod(owningMethod1);

            CDMethod owningMethod2 = new CDMethod(owningClass2, "Method2", "integer");
            owningClass2.AddMethod(owningMethod2);

            CDMethod owningMethod3 = new CDMethod(owningClass3, "Method3", "integer");
            owningClass3.AddMethod(owningMethod3);

            // Act
            EXEScopeMethod methodScope1 = OALParserBridge.Parse(_methodSourceCode1);
            owningMethod1.ExecutableCode = methodScope1;

            EXEScopeMethod methodScope2 = OALParserBridge.Parse(_methodSourceCode2);
            owningMethod2.ExecutableCode = methodScope2;

            EXEScopeMethod methodScope3 = OALParserBridge.Parse(_methodSourceCode3);
            owningMethod3.ExecutableCode = methodScope3;

            programInstance.SuperScope = methodScope1;

            EXEExecutionResult _executionResult = PerformExecution(programInstance);

            // Assert
            Test.Declare(methodScope1, _executionResult);

            Test.Variables
                    .ExpectVariable("x", new EXEValueInt(5))
                    .ExpectVariable("inst2", new EXEValueReference())
                    .ExpectVariable("self", new EXEValueReference(methodScope1.OwningObject));

            Test.PerformAssertion();
        }

        [Test]
        public void HappyDay_01_AssignToLocalVariable_03_FromMethod_03_Many()
        {
            CommandTest Test = new CommandTest();

            // Arrange
            string _methodSourceCode1 = "create object instance inst2 of Class2;\nx = inst2.Method2();";
            string _methodSourceCode2 = "create object instance inst3 of Class3;\nreturn inst3.Method3();";
            string _methodSourceCode3 = "create object instance inst4 of Class4;\nreturn inst4.Method4();";
            string _methodSourceCode4 = "create object instance inst5 of Class5;\nreturn inst5.Method5();";
            string _methodSourceCode5 = "return 5";

            OALProgram programInstance = new OALProgram();
            CDClass owningClass1 = programInstance.ExecutionSpace.SpawnClass("Class1");
            CDClass owningClass2 = programInstance.ExecutionSpace.SpawnClass("Class2");
            CDClass owningClass3 = programInstance.ExecutionSpace.SpawnClass("Class3");
            CDClass owningClass4 = programInstance.ExecutionSpace.SpawnClass("Class4");
            CDClass owningClass5 = programInstance.ExecutionSpace.SpawnClass("Class5");

            CDMethod owningMethod1 = new CDMethod(owningClass1, "Method1", "");
            owningClass1.AddMethod(owningMethod1);

            CDMethod owningMethod2 = new CDMethod(owningClass2, "Method2", "integer");
            owningClass2.AddMethod(owningMethod2);

            CDMethod owningMethod3 = new CDMethod(owningClass3, "Method3", "integer");
            owningClass3.AddMethod(owningMethod3);

            CDMethod owningMethod4 = new CDMethod(owningClass4, "Method4", "integer");
            owningClass4.AddMethod(owningMethod4);

            CDMethod owningMethod5 = new CDMethod(owningClass5, "Method5", "integer");
            owningClass5.AddMethod(owningMethod5);

            // Act
            EXEScopeMethod methodScope1 = OALParserBridge.Parse(_methodSourceCode1);
            owningMethod1.ExecutableCode = methodScope1;

            EXEScopeMethod methodScope2 = OALParserBridge.Parse(_methodSourceCode2);
            owningMethod2.ExecutableCode = methodScope2;

            EXEScopeMethod methodScope3 = OALParserBridge.Parse(_methodSourceCode3);
            owningMethod3.ExecutableCode = methodScope3;

            EXEScopeMethod methodScope4 = OALParserBridge.Parse(_methodSourceCode4);
            owningMethod4.ExecutableCode = methodScope4;

            EXEScopeMethod methodScope5 = OALParserBridge.Parse(_methodSourceCode5);
            owningMethod5.ExecutableCode = methodScope5;

            programInstance.SuperScope = methodScope1;

            EXEExecutionResult _executionResult = PerformExecution(programInstance);

            // Assert
            Test.Declare(methodScope1, _executionResult);

            Test.Variables
                    .ExpectVariable("x", new EXEValueInt(5))
                    .ExpectVariable("inst2", new EXEValueReference())
                    .ExpectVariable("self", new EXEValueReference(methodScope1.OwningObject));

            Test.PerformAssertion();
        }

        [Test]
        public void HappyDay_01_AssignToLocalVariable_03_FromMethod_04_Expression()
        {
            CommandTest Test = new CommandTest();

            // Arrange
            string _methodSourceCode1 = "create object instance inst2 of Class2;\nx = inst2.Method2();";
            string _methodSourceCode2 = "x = 5;\nreturn x * x + 5;";

            OALProgram programInstance = new OALProgram();
            CDClass owningClass1 = programInstance.ExecutionSpace.SpawnClass("Class1");
            CDClass owningClass2 = programInstance.ExecutionSpace.SpawnClass("Class2");

            CDMethod owningMethod1 = new CDMethod(owningClass1, "Method1", "");
            owningClass1.AddMethod(owningMethod1);

            CDMethod owningMethod2 = new CDMethod(owningClass2, "Method2", "integer");
            owningClass2.AddMethod(owningMethod2);

            // Act
            EXEScopeMethod methodScope1 = OALParserBridge.Parse(_methodSourceCode1);
            owningMethod1.ExecutableCode = methodScope1;

            EXEScopeMethod methodScope2 = OALParserBridge.Parse(_methodSourceCode2);
            owningMethod2.ExecutableCode = methodScope2;

            programInstance.SuperScope = methodScope1;

            EXEExecutionResult _executionResult = PerformExecution(programInstance);

            // Assert
            Test.Declare(methodScope1, _executionResult);

            Test.Variables
                    .ExpectVariable("x", new EXEValueInt(30))
                    .ExpectVariable("inst2", new EXEValueReference())
                    .ExpectVariable("self", new EXEValueReference(methodScope1.OwningObject));

            Test.PerformAssertion();
        }

        [Test]
        public void HappyDay_02_AssignToAttribute_01_ImplicitReference_01_FromAnotherVariable_02_DirectSubtype()
        {
            CommandTest Test = new CommandTest();

            // Arrange
            string _methodSourceCode = "create object instance y of SubClass1;\nAttribute1 = y;";

            OALProgram programInstance = new OALProgram();
            CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("OwningClass");

            CDAttribute attribute = new CDAttribute("Attribute1", "SuperClass1");
            if (!owningClass.AddAttribute(attribute))
            {
                throw new Exception("Failed to add attribute.");
            }

            CDMethod owningMethod = new CDMethod(owningClass, "Method1", "");
            owningClass.AddMethod(owningMethod);

            CDClass superClass = programInstance.ExecutionSpace.SpawnClass("SuperClass1");
            CDClass subClass = programInstance.ExecutionSpace.SpawnClass("SubClass1");
            subClass.SuperClass = superClass;

            // Act
            EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
            owningMethod.ExecutableCode = methodScope;
            programInstance.SuperScope = methodScope;

            EXEExecutionResult _executionResult = PerformExecution(programInstance);

            // Assert
            Test.Declare(methodScope, _executionResult);

            Test.Variables
                    .ExpectVariable("self", new EXEValueReference(methodScope.OwningObject))
                    .ExpectVariable("y", new EXEValueReference(subClass.Instances.First()));

            Test.PerformAssertion();
        }

        [Test]
        public void HappyDay_02_AssignToAttribute_02_ExplicitReference_01_FromAnotherVariable_02_DirectSubtype()
        {
            CommandTest Test = new CommandTest();

            // Arrange
            string _methodSourceCode = "create object instance y of SubClass1;\nself.Attribute1 = y;";

            OALProgram programInstance = new OALProgram();
            CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("OwningClass");

            CDAttribute attribute = new CDAttribute("Attribute1", "SuperClass1");
            if (!owningClass.AddAttribute(attribute))
            {
                throw new Exception("Failed to add attribute.");
            }

            CDMethod owningMethod = new CDMethod(owningClass, "Method1", "");
            owningClass.AddMethod(owningMethod);

            CDClass superClass = programInstance.ExecutionSpace.SpawnClass("SuperClass1");
            CDClass subClass = programInstance.ExecutionSpace.SpawnClass("SubClass1");
            subClass.SuperClass = superClass;

            // Act
            EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
            owningMethod.ExecutableCode = methodScope;
            programInstance.SuperScope = methodScope;

            EXEExecutionResult _executionResult = PerformExecution(programInstance);

            // Assert
            Test.Declare(methodScope, _executionResult);

            Test.Variables
                    .ExpectVariable("self", new EXEValueReference(methodScope.OwningObject))
                    .ExpectVariable("y", new EXEValueReference(subClass.Instances.First()));

            Test.PerformAssertion();
        }

        [Test]
        public void HappyDay_02_AssignToAttribute_03_MultiChain_01_Int()
        {
            CommandTest Test = new CommandTest();

            // Arrange
            string _methodSourceCode = "create object instance inst2 of Class2;\ncreate object instance inst2.att2 of Class3;\ninst2.att2.att3 = 5;";

            OALProgram programInstance = new OALProgram();
            CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

            CDMethod owningMethod = new CDMethod(owningClass, "Method1", "");
            owningClass.AddMethod(owningMethod);

            CDClass class2 = programInstance.ExecutionSpace.SpawnClass("Class2");
            CDAttribute attribute2 = new CDAttribute("att2", "Class3");
            class2.AddAttribute(attribute2);

            CDClass class3 = programInstance.ExecutionSpace.SpawnClass("Class3");
            CDAttribute attribute3 = new CDAttribute("att3", "integer");
            class3.AddAttribute(attribute3);

            // Act
            EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
            owningMethod.ExecutableCode = methodScope;
            programInstance.SuperScope = methodScope;

            EXEExecutionResult _executionResult = PerformExecution(programInstance);

            // Assert
            Test.Declare(methodScope, _executionResult);

            Test.Variables
                    .ExpectVariable("inst2", new EXEValueReference(class2.Instances.First()))
                    .ExpectVariable("self", new EXEValueReference(methodScope.OwningObject));

            Test.PerformAssertion();
        }

        [Test]
        public void SadDay_01_CallEmptyMethod()
        {
            CommandTest Test = new CommandTest();

            // Arrange
            string _methodSourceCode1 = "create object instance inst2 of Class2;\nx = inst2.Method2();";
            string _methodSourceCode2 = "";

            OALProgram programInstance = new OALProgram();
            CDClass owningClass1 = programInstance.ExecutionSpace.SpawnClass("Class1");
            CDClass owningClass2 = programInstance.ExecutionSpace.SpawnClass("Class2");

            CDMethod owningMethod1 = new CDMethod(owningClass1, "Method1", "");
            owningClass1.AddMethod(owningMethod1);

            CDMethod owningMethod2 = new CDMethod(owningClass2, "Method2", "");
            owningClass2.AddMethod(owningMethod2);

            // Act
            EXEScopeMethod methodScope1 = OALParserBridge.Parse(_methodSourceCode1);
            owningMethod1.ExecutableCode = methodScope1;

            EXEScopeMethod methodScope2 = OALParserBridge.Parse(_methodSourceCode2);
            owningMethod2.ExecutableCode = methodScope2;

            programInstance.SuperScope = methodScope1;

            EXEExecutionResult _executionResult = PerformExecution(programInstance);

            // Assert
            Test.Declare(methodScope1, _executionResult);

            Test.Variables
                    .ExpectVariable("inst2", new EXEValueReference());

            Test.PerformAssertion();
        }

        [Test]
        public void ThursdayDay_01_indexationEvaluate()
        {
            CommandTest Test = new CommandTest();

            // Arrange
            string _methodSourceCode = 
            @"
            create object instance inst0 of SubClass1;
            create object instance inst1 of SubClass1;
            create list Objects of SubClass1;
            add inst0 to Objects;
            add inst1 to Objects;
            x = Objects[1];
            ";

            OALProgram programInstance = new OALProgram();
            CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

            CDMethod owningMethod = new CDMethod(owningClass, "Method1", "");
            owningClass.AddMethod(owningMethod);

            CDClass subClass = programInstance.ExecutionSpace.SpawnClass("SubClass1");
            subClass.SuperClass = owningClass;

            // Act
            EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
            owningMethod.ExecutableCode = methodScope;
            programInstance.SuperScope = methodScope;

            EXEExecutionResult _executionResult = PerformExecution(programInstance);

            CDClassInstance instance0 = subClass.Instances[0];
            CDClassInstance instance1 = subClass.Instances[1];

            EXEValueReference instance0Reference = new EXEValueReference(instance0);
            EXEValueReference instance1Reference = new EXEValueReference(instance1);

            EXEValueArray array = new EXEValueArray("Class1[]");
            array.InitializeEmptyArray();
            array.AppendElement(instance0Reference, subClass.OwningClassPool);
            array.AppendElement(instance1Reference, subClass.OwningClassPool);

            // Assert
            Test.Declare(methodScope, _executionResult);

            Test.Variables
                    .ExpectVariable("inst0", instance0Reference)
                    .ExpectVariable("inst1", instance1Reference)
                    .ExpectVariable("x", instance1Reference)
                    .ExpectVariable("Objects", array)
                    .ExpectVariable("self", new EXEValueReference(methodScope.OwningObject));

            Test.PerformAssertion();
        }

        [Test]
        public void WednesdayDay_02_indexationAsAccessChainEvaluate()
        {
            CommandTest Test = new CommandTest();

            // Arrange
            string _methodSourceCode = 
            @"
            create object instance inst0 of SubClass1;
            create object instance inst1 of SubClass1;
            inst0.att1 = 2;
            inst1.att1 = 3;
            create list Objects of SubClass1;
            add inst0 to Objects;
            add inst1 to Objects;
            x = Objects[1].att1;
            ";

            OALProgram programInstance = new OALProgram();
            CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

            CDMethod owningMethod = new CDMethod(owningClass, "Method1", "");
            owningClass.AddMethod(owningMethod);

            CDClass subClass = programInstance.ExecutionSpace.SpawnClass("SubClass1");
            subClass.SuperClass = owningClass;

            CDAttribute attribute = new CDAttribute("att1", "integer");
            subClass.AddAttribute(attribute);

            // Act
            EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
            owningMethod.ExecutableCode = methodScope;
            programInstance.SuperScope = methodScope;

            EXEExecutionResult _executionResult = PerformExecution(programInstance);

            CDClassInstance instance0 = subClass.Instances[0];
            CDClassInstance instance1 = subClass.Instances[1];

            EXEValueReference instance0Reference = new EXEValueReference(instance0);
            EXEValueReference instance1Reference = new EXEValueReference(instance1);

            EXEValueArray array = new EXEValueArray("Class1[]");
            array.InitializeEmptyArray();
            array.AppendElement(instance0Reference, subClass.OwningClassPool);
            array.AppendElement(instance1Reference, subClass.OwningClassPool);

            // Assert
            Test.Declare(methodScope, _executionResult);

            Test.Variables
                    .ExpectVariable("inst0", instance0Reference)
                    .ExpectVariable("inst1", instance1Reference)
                    .ExpectVariable("x", new EXEValueInt(3))
                    .ExpectVariable("Objects", array)
                    .ExpectVariable("self", new EXEValueReference(methodScope.OwningObject));

            Test.PerformAssertion();
        }
        
    }
}
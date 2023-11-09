using NUnit.Framework;
using OALProgramControl;
using Assets.Scripts.AnimationControl.OAL;
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
                    .ExpectVariable("x", new EXEValueInt(5));

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
                    .ExpectVariable("x", new EXEValueReal("5.05"));

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
                    .ExpectVariable("x", new EXEValueString("\"Hello world\""));

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
                    .ExpectVariable("x", new EXEValueBool(true));

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
                    .ExpectVariable("x", new EXEValueBool(false));

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
                    .ExpectVariable("y", new EXEValueInt(5));

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
                    .ExpectVariable("y", new EXEValueReal(5.05m));

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
                    .ExpectVariable("y", new EXEValueString("\"Hello world\""));

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
                    .ExpectVariable("y", new EXEValueBool(true));

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
                    .ExpectVariable("y", new EXEValueBool(false));

            Test.PerformAssertion();
        }

        [Test]
        public void HappyDay_01_AssignToLocalVariable_02_FromMethod_01_Trivial()
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
                    .ExpectVariable("inst2", new EXEValueReference());

            Test.PerformAssertion();
        }

        [Test]
        public void HappyDay_01_AssignToLocalVariable_02_FromMethod_02_Multiple()
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
                    .ExpectVariable("inst2", new EXEValueReference());

            Test.PerformAssertion();
        }

        [Test]
        public void HappyDay_01_AssignToLocalVariable_02_FromMethod_03_Many()
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
                    .ExpectVariable("inst2", new EXEValueReference());

            Test.PerformAssertion();
        }

        [Test]
        public void HappyDay_01_AssignToLocalVariable_02_FromMethod_04_Expression()
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
                    .ExpectVariable("inst2", new EXEValueReference());

            Test.PerformAssertion();
        }
    }
}
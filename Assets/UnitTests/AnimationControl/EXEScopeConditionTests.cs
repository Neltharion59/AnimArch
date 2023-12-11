using Assets.Scripts.AnimationControl.OAL;
using NUnit.Framework;
using OALProgramControl;

namespace Assets.UnitTests.AnimationControl
{
    public class EXEScopeConditionTests : StandardTest
    {
        [Test]
        public void HappyDay_01_DoesNotHaveElse_01_ConditionTrue()
        {
            CommandTest Test = new CommandTest();

            // Arrange
            string _methodSourceCode = "x = 1;\n if (TRUE)\nx = 5;\nend if;";

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
        public void HappyDay_01_DoesNotHaveElse_02_ConditionFalse()
        {
            CommandTest Test = new CommandTest();

            // Arrange
            string _methodSourceCode = "x = 1;\n if (FALSE)\nx = 5;\nend if;";

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
                    .ExpectVariable("x", new EXEValueInt(1))
                    .ExpectVariable("self", new EXEValueReference(methodScope.OwningObject));

            Test.PerformAssertion();
        }

        [Test]
        public void HappyDay_02_HasElse_01_ConditionTrue()
        {
            CommandTest Test = new CommandTest();

            // Arrange
            string _methodSourceCode = "x = 1;\nif (TRUE)\nx = 5;\nelse\nx = 6;\nend if;";

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
        public void HappyDay_02_HasElse_02_ConditionFalse()
        {
            CommandTest Test = new CommandTest();

            // Arrange
            string _methodSourceCode = "x = 1;\nif (FALSE)\nx = 5;\nelse\nx = 6;\nend if;";

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
                    .ExpectVariable("x", new EXEValueInt(6))
                    .ExpectVariable("self", new EXEValueReference(methodScope.OwningObject));

            Test.PerformAssertion();
        }

        [Test]
        public void HappyDay_03_HasElif_01_FirstConditionTrue()
        {
            CommandTest Test = new CommandTest();

            // Arrange
            string _methodSourceCode = "x = 1;\nif (TRUE)\nx = 5;\nelif (FALSE)\nx = 6;\nend if;";

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
        public void HappyDay_03_HasElif_02_SecondConditionTrue()
        {
            CommandTest Test = new CommandTest();

            // Arrange
            string _methodSourceCode = "x = 1;\nif (FALSE)\nx = 5;\nelif (TRUE)\nx = 6;\nend if;";

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
                    .ExpectVariable("x", new EXEValueInt(6))
                    .ExpectVariable("self", new EXEValueReference(methodScope.OwningObject));

            Test.PerformAssertion();
        }
    }
}

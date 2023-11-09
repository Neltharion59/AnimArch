using OALProgramControl;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace Assets.UnitTests.AnimationControl
{
    public class CommandTest
    {
        public readonly VariableAsserter Variables;
        public readonly ObjectInstanceAsserter ObjectInstances;
        private EXEExecutionResult ActualExecutionResult;

        public CommandTest()
        {
            this.Variables = new VariableAsserter();
            this.ObjectInstances = new ObjectInstanceAsserter();
        }

        public void Declare(EXEScope actualScope, EXEExecutionResult executionResult)
        {
            this.Variables.Declare(actualScope);
            this.ObjectInstances.Declare();
            this.ActualExecutionResult = executionResult;
        }

        public void PerformAssertion()
        {
            Assert.IsTrue(this.ActualExecutionResult.IsSuccess, "There was an execution error.\n" + this.ActualExecutionResult.ToString());
            Assert.IsTrue(this.ActualExecutionResult.IsDone, "The execution did not finish.");

            this.Variables.PerformAssertion();
            this.ObjectInstances.PerformAssertion();
        }
    }
}

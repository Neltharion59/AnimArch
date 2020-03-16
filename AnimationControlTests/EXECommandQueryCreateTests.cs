using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnimationControl;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimationControl.Tests
{
    [TestClass]
    public class EXECommandQueryCreateTests
    {
        [TestMethod]
        public void Execute_Normal_01()
        {
            CDClassPool ExecutionSpace = new CDClassPool();
            CDClass DesiredClass = ExecutionSpace.SpawnClass("Observer");
            ExecutionSpace.SpawnClass("Subject");
            ExecutionSpace.SpawnClass("Form");

            CDRelationshipPool RelationshipSpace = null;

            EXEScope Scope = new EXEScope();

            EXECommandQueryCreate CreateCommand = new EXECommandQueryCreate("Observer", "o");
            Boolean ExecutionSuccess = CreateCommand.Execute(ExecutionSpace, RelationshipSpace, Scope);

            Boolean Success = ExecutionSuccess && Scope.ReferencingVariables[0].Name == "o" && DesiredClass.Instances.Count == 1;

            Assert.IsTrue(Success);
        }

        [TestMethod]
        public void Execute_Normal_02()
        {
            CDClassPool ExecutionSpace = new CDClassPool();
            CDClass DesiredClass = ExecutionSpace.SpawnClass("Observer");
            ExecutionSpace.SpawnClass("Subject");

            CDRelationshipPool RelationshipSpace = null;

            EXEScope Scope = new EXEScope();

            EXECommandQueryCreate CreateCommand = new EXECommandQueryCreate("Observer", "o");
            Boolean ExecutionSuccess = CreateCommand.Execute(ExecutionSpace, RelationshipSpace, Scope);

            Boolean Success = ExecutionSuccess && Scope.ReferencingVariables[0].Name == "o" && DesiredClass.Instances.Count == 1;

            Assert.IsTrue(Success);
        }

        [TestMethod]
        public void Execute_Bad_01()
        {
            CDClassPool ExecutionSpace = new CDClassPool();
            CDClass DesiredClass = ExecutionSpace.SpawnClass("Observer");
            ExecutionSpace.SpawnClass("Subject");

            CDRelationshipPool RelationshipSpace = null;

            EXEScope Scope = new EXEScope();

            EXECommandQueryCreate CreateCommand = new EXECommandQueryCreate("Object", "o");
            Boolean ExecutionSuccess = CreateCommand.Execute(ExecutionSpace, RelationshipSpace, Scope);

            Boolean Success = ExecutionSuccess && Scope.ReferencingVariables.Count == 0 && DesiredClass.Instances.Count == 0;

            Assert.IsTrue(Success);
        }
    }
}
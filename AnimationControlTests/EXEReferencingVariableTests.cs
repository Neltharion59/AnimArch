using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnimationControl;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimationControl.Tests
{
    [TestClass]
    public class EXEReferencingVariableTests
    {
        [TestMethod]
        public void RetrieveReferencedClassInstance_Normal_01()
        {
            CDClassPool ExecutionSpace = new CDClassPool();
            CDClass UserClass = ExecutionSpace.SpawnClass("User");
            UserClass.AddAttribute(new CDAttribute("Nick", EXETypes.StringTypeName));
            UserClass.AddAttribute(new CDAttribute("Email", EXETypes.StringTypeName));
            UserClass.AddAttribute(new CDAttribute("Age", EXETypes.IntegerTypeName));

            CDClassInstance Inst1 = UserClass.CreateClassInstance("x");
            Inst1.SetAttribute("Nick", "Alex");
            CDClassInstance Inst2 = UserClass.CreateClassInstance("y");
            Inst2.SetAttribute("Nick", "Miso");
            CDClassInstance Inst3 = UserClass.CreateClassInstance("z");
            Inst3.SetAttribute("Nick", "Mato");
            CDClassInstance Inst4 = UserClass.CreateClassInstance("w");
            Inst4.SetAttribute("Nick", "Samo");

            CDRelationshipPool RelationshipSpace = null;

            EXEScope Scope = new EXEScope();

            EXEReferencingVariable ReferencingVariable = new EXEReferencingVariable("user1", "User", Inst2.UniqueID);
            CDClassInstance ReferencedInstance = ReferencingVariable.RetrieveReferencedClassInstance(ExecutionSpace);

            Dictionary<string, string> ExpectedOutput = Inst2.State;
            Dictionary<string, string> ActualOutput = ReferencedInstance.State;

            CollectionAssert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void RetrieveReferencedClassInstance_Bad_01()
        {
            CDClassPool ExecutionSpace = new CDClassPool();
            CDClass UserClass = ExecutionSpace.SpawnClass("User");
            UserClass.AddAttribute(new CDAttribute("Nick", EXETypes.StringTypeName));
            UserClass.AddAttribute(new CDAttribute("Email", EXETypes.StringTypeName));
            UserClass.AddAttribute(new CDAttribute("Age", EXETypes.IntegerTypeName));

            CDClassInstance Inst1 = UserClass.CreateClassInstance("x");
            Inst1.SetAttribute("Nick", "Alex");
            CDClassInstance Inst2 = UserClass.CreateClassInstance("y");
            Inst2.SetAttribute("Nick", "Miso");
            CDClassInstance Inst3 = UserClass.CreateClassInstance("z");
            Inst3.SetAttribute("Nick", "Mato");
            CDClassInstance Inst4 = UserClass.CreateClassInstance("w");
            Inst4.SetAttribute("Nick", "Samo");

            CDRelationshipPool RelationshipSpace = null;

            EXEScope Scope = new EXEScope();

            EXEReferencingVariable ReferencingVariable = new EXEReferencingVariable("user1", "User", Inst4.UniqueID + 1);
            CDClassInstance ReferencedInstance = ReferencingVariable.RetrieveReferencedClassInstance(ExecutionSpace);

            Assert.IsNull(ReferencedInstance);
        }
        [TestMethod]
        public void RetrieveReferencedClassInstance_Bad_02()
        {
            CDClassPool ExecutionSpace = new CDClassPool();
            CDClass UserClass = ExecutionSpace.SpawnClass("User");
            UserClass.AddAttribute(new CDAttribute("Nick", EXETypes.StringTypeName));
            UserClass.AddAttribute(new CDAttribute("Email", EXETypes.StringTypeName));
            UserClass.AddAttribute(new CDAttribute("Age", EXETypes.IntegerTypeName));

            CDClassInstance Inst1 = UserClass.CreateClassInstance("x");
            Inst1.SetAttribute("Nick", "Alex");
            CDClassInstance Inst2 = UserClass.CreateClassInstance("y");
            Inst2.SetAttribute("Nick", "Miso");
            CDClassInstance Inst3 = UserClass.CreateClassInstance("z");
            Inst3.SetAttribute("Nick", "Mato");
            CDClassInstance Inst4 = UserClass.CreateClassInstance("w");
            Inst4.SetAttribute("Nick", "Samo");

            CDRelationshipPool RelationshipSpace = null;

            EXEScope Scope = new EXEScope();

            EXEReferencingVariable ReferencingVariable = new EXEReferencingVariable("user1", "SomeOtherClass", Inst4.UniqueID );
            CDClassInstance ReferencedInstance = ReferencingVariable.RetrieveReferencedClassInstance(ExecutionSpace);

            Assert.IsNull(ReferencedInstance);
        }
    }
}
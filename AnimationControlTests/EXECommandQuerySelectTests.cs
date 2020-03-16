using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnimationControl;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimationControl.Tests
{
    [TestClass]
    public class EXECommandQuerySelectTests
    {
        [TestMethod]
        public void Execute_Normal_Any_01()
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

            EXECommandQuerySelect SelectCommand = new EXECommandQuerySelect();
            SelectCommand.Cardinality = EXECommandQuerySelect.CardinalityAny;
            SelectCommand.ClassName = "User";
            SelectCommand.VariableName = "selected_user";

            Boolean ExecutionSuccess = SelectCommand.Execute(ExecutionSpace, RelationshipSpace, Scope);
            Boolean VariableCreationSuccess = Scope.ReferencingVariables[0].Name == "selected_user";
            Boolean CorrectId = Scope.ReferencingVariables[0].ReferencedInstanceId == Inst1.UniqueID ||
                                Scope.ReferencingVariables[0].ReferencedInstanceId == Inst2.UniqueID ||
                                Scope.ReferencingVariables[0].ReferencedInstanceId == Inst3.UniqueID ||
                                Scope.ReferencingVariables[0].ReferencedInstanceId == Inst4.UniqueID;
            Boolean Success = ExecutionSuccess && VariableCreationSuccess && CorrectId;

            Assert.IsTrue(Success);
        }
        [TestMethod]
        public void Execute_Normal_Many_01()
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

            EXECommandQuerySelect SelectCommand = new EXECommandQuerySelect();
            SelectCommand.Cardinality = EXECommandQuerySelect.CardinalityMany;
            SelectCommand.ClassName = "User";
            SelectCommand.VariableName = "selected_user";

            Boolean ExecutionSuccess = SelectCommand.Execute(ExecutionSpace, RelationshipSpace, Scope);
            Boolean VariableCreationSuccess = Scope.SetReferencingVariables[0].Name == "selected_user";

            List<long> CreatedIds = new List<long>(new long[] { Inst1.UniqueID, Inst2.UniqueID, Inst3.UniqueID, Inst4.UniqueID});
            List<long> SelectedIds = new List<long>();
            foreach (EXEReferencingVariable TempRefVar in Scope.SetReferencingVariables[0].GetReferencingVariables())
            {
                SelectedIds.Add(TempRefVar.ReferencedInstanceId);
            }

            Boolean CorrectIds = CompareLists(CreatedIds, SelectedIds);
            Boolean Success = ExecutionSuccess && VariableCreationSuccess && CorrectIds;

            Assert.IsTrue(Success);
        }

        private Boolean CompareLists(List<long> List1, List<long> List2)
        {
            List1.Sort();
            List2.Sort();

            return List1.Equals(List2);
        }
    }
}


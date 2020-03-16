using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnimationControl;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimationControl.Tests
{
    [TestClass]
    public class EXEReferenceEvaluatorTests
    {
        //SetUloh1
        // The test scenarios are a bit complicated here, because we first must prepare classes, their instances and referencing variables,
        // before we call methods to set a and get attribute values
        // Add some more tests. You are the one who implements these methods, so you know which situations need to be checked

        [TestMethod]
        public void EvaluateAttributeValue_Normal_01()
        {
            CDClassPool ExecutionSpace = new CDClassPool();
            
            CDClass UserAccountClass = ExecutionSpace.SpawnClass("UserAccount");
            UserAccountClass.AddAttribute(new CDAttribute("Nick", EXETypes.StringTypeName));

            CDClassInstance ClassInstance = UserAccountClass.CreateClassInstance("x");
            ClassInstance.SetAttribute("Nick", "\"Jano245\"");

            EXEScope Scope = new EXEScope();
            Scope.ReferencingVariables.Add(new EXEReferencingVariable("new_user", "UserAccount", ClassInstance.UniqueID));

            EXEReferenceEvaluator Evaluator = new EXEReferenceEvaluator();
            String ActualOutput = Evaluator.EvaluateAttributeValue("new_user", "Nick", Scope, ExecutionSpace);

            String ExpectedOutput = "\"Jano245\"";

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }

        [TestMethod]
        public void EvaluateAttributeValue_Normal_02()
        {
            CDClassPool ExecutionSpace = new CDClassPool();

            CDClass GameClass = ExecutionSpace.SpawnClass("Game");
            GameClass.AddAttribute(new CDAttribute("Score", EXETypes.IntegerTypeName));
            GameClass.AddAttribute(new CDAttribute("Name", EXETypes.StringTypeName));

            CDClass UserAccountClass = ExecutionSpace.SpawnClass("UserAccount");
            UserAccountClass.AddAttribute(new CDAttribute("Nick", EXETypes.StringTypeName));
            UserAccountClass.AddAttribute(new CDAttribute("FirstName", EXETypes.StringTypeName));
            UserAccountClass.AddAttribute(new CDAttribute("LastName", EXETypes.StringTypeName));
            UserAccountClass.AddAttribute(new CDAttribute("Email", EXETypes.StringTypeName));

            CDClassInstance ClassInstance1 = UserAccountClass.CreateClassInstance("x");
            ClassInstance1.SetAttribute("Nick", "\"Jano245\"");
            ClassInstance1.SetAttribute("FirstName", "\"Jano\"");
            ClassInstance1.SetAttribute("LastName", "\"Podnozka\"");
            ClassInstance1.SetAttribute("Email", "\"jano.podnozka17@gmail.com\"");

            CDClassInstance ClassInstance2 = UserAccountClass.CreateClassInstance("y");
            ClassInstance2.SetAttribute("Nick", "\"sexica2521\"");
            ClassInstance2.SetAttribute("FirstName", "\"Maria\"");
            ClassInstance2.SetAttribute("LastName", "\"Cirova\"");
            ClassInstance2.SetAttribute("Email", "\"majka.cajka@azet.sk\"");

            CDClassInstance ClassInstance3 = UserAccountClass.CreateClassInstance("z");
            ClassInstance3.SetAttribute("Nick", "\"MedievalCollectibles\"");
            ClassInstance3.SetAttribute("FirstName", "\"Anne\"");
            ClassInstance3.SetAttribute("LastName", "\"Vazziereth\"");
            ClassInstance3.SetAttribute("Email", "\"medieval.collectibles@gmail.com\"");

            EXEScope Scope = new EXEScope();
            Scope.ReferencingVariables.Add(new EXEReferencingVariable("new_user1", "UserAccount", ClassInstance1.UniqueID));
            Scope.ReferencingVariables.Add(new EXEReferencingVariable("new_user2", "UserAccount", ClassInstance2.UniqueID));
            Scope.ReferencingVariables.Add(new EXEReferencingVariable("new_user3", "UserAccount", ClassInstance3.UniqueID));

            EXEReferenceEvaluator Evaluator = new EXEReferenceEvaluator();
            String ActualOutput = Evaluator.EvaluateAttributeValue("new_user2", "LastName", Scope, ExecutionSpace);

            String ExpectedOutput = "\"Cirova\"";

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }

        [TestMethod]
        public void SetAttributeValue_Normal_01()
        {
            CDClassPool ExecutionSpace = new CDClassPool();

            CDClass UserAccountClass = ExecutionSpace.SpawnClass("UserAccount");
            UserAccountClass.AddAttribute(new CDAttribute("Nick", EXETypes.StringTypeName));

            CDClassInstance ClassInstance = UserAccountClass.CreateClassInstance("x");
            ClassInstance.SetAttribute("Nick", "\"Jano245\"");

            EXEScope Scope = new EXEScope();
            Scope.ReferencingVariables.Add(new EXEReferencingVariable("new_user", "UserAccount", ClassInstance.UniqueID));

            EXEReferenceEvaluator Evaluator = new EXEReferenceEvaluator();
            Evaluator.SetAttributeValue("new_user", "Nick", Scope, ExecutionSpace, "\"Jano69\"");

            String ActualOutput = ClassInstance.GetAttribute("Nick");

            String ExpectedOutput = "\"Jano69\"";

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void SetAttributeValue_Normal_02()
        {
            CDClassPool ExecutionSpace = new CDClassPool();

            CDClass GameClass = ExecutionSpace.SpawnClass("Game");
            GameClass.AddAttribute(new CDAttribute("Score", EXETypes.IntegerTypeName));
            GameClass.AddAttribute(new CDAttribute("Name", EXETypes.StringTypeName));

            CDClass UserAccountClass = ExecutionSpace.SpawnClass("UserAccount");
            UserAccountClass.AddAttribute(new CDAttribute("Nick", EXETypes.StringTypeName));
            UserAccountClass.AddAttribute(new CDAttribute("FirstName", EXETypes.StringTypeName));
            UserAccountClass.AddAttribute(new CDAttribute("LastName", EXETypes.StringTypeName));
            UserAccountClass.AddAttribute(new CDAttribute("Email", EXETypes.StringTypeName));

            CDClassInstance ClassInstance1 = UserAccountClass.CreateClassInstance("x");
            ClassInstance1.SetAttribute("Nick", "\"Jano245\"");
            ClassInstance1.SetAttribute("FirstName", "\"Jano\"");
            ClassInstance1.SetAttribute("LastName", "\"Podnozka\"");
            ClassInstance1.SetAttribute("Email", "\"jano.podnozka17@gmail.com\"");

            CDClassInstance ClassInstance2 = UserAccountClass.CreateClassInstance("y");
            ClassInstance2.SetAttribute("Nick", "\"sexica2521\"");
            ClassInstance2.SetAttribute("FirstName", "\"Maria\"");
            ClassInstance2.SetAttribute("LastName", "\"Cirova\"");
            ClassInstance2.SetAttribute("Email", "\"majka.cajka@azet.sk\"");

            CDClassInstance ClassInstance3 = UserAccountClass.CreateClassInstance("z");
            ClassInstance3.SetAttribute("Nick", "\"MedievalCollectibles\"");
            ClassInstance3.SetAttribute("FirstName", "\"Anne\"");
            ClassInstance3.SetAttribute("LastName", "\"Vazziereth\"");
            ClassInstance3.SetAttribute("Email", "\"medieval.collectibles@gmail.com\"");

            EXEScope Scope = new EXEScope();
            Scope.ReferencingVariables.Add(new EXEReferencingVariable("new_user1", "UserAccount", ClassInstance1.UniqueID));
            Scope.ReferencingVariables.Add(new EXEReferencingVariable("new_user2", "UserAccount", ClassInstance2.UniqueID));
            Scope.ReferencingVariables.Add(new EXEReferencingVariable("new_user3", "UserAccount", ClassInstance3.UniqueID));

            EXEReferenceEvaluator Evaluator = new EXEReferenceEvaluator();
            Evaluator.SetAttributeValue("new_user1", "FirstName", Scope, ExecutionSpace, "\"Ivan\"");

            String ActualOutput = ClassInstance1.GetAttribute("FírstName");

            String ExpectedOutput = "\"Ivan\"";

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
    }
}
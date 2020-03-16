using AnimationControl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AnimationControlTests
{
    [TestClass]
    public class OALCodeBuilderTests
    {
        [TestMethod]
        public void AddCall_Normal01_1Call_1Seq_CorrectNewSeqFlag()
        {
            OALCodeBuilder CodeBuilder = new OALCodeBuilder();

            Boolean ExpectedOutput = true;

            Boolean ActualOutput = CodeBuilder.AddCall("Observer", "init", "R1", "Subject", "Register", true);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }

        [TestMethod]
        public void AddCall_Normal02_2Calls_1Seq_CorrectNewSeqFlag()
        {
            OALCodeBuilder CodeBuilder = new OALCodeBuilder();

            Boolean ExpectedOutput = true;

            CodeBuilder.AddCall("Observer", "init", "R1", "Subject", "Register", true);
            Boolean ActualOutput = CodeBuilder.AddCall("Subject", "Register", "R2", "Bussiness", "Update", false);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }

        [TestMethod]
        public void AddCall_Normal03_2Calls_2Seq_CorrectNewSeqFlag()
        {
            OALCodeBuilder CodeBuilder = new OALCodeBuilder();

            Boolean ExpectedOutput = true;

            CodeBuilder.AddCall("Observer", "init", "R1", "Subject", "Register", true);
            Boolean ActualOutput = CodeBuilder.AddCall("Bussiness", "Update", "R2", "Bussiness", "Update", true);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }

        [TestMethod]
        public void AddCall_Bad01_2Calls_2Seq_InCorrectNewSeqFlag()
        {
            OALCodeBuilder CodeBuilder = new OALCodeBuilder();

            Boolean ExpectedOutput = false;

            CodeBuilder.AddCall("Observer", "init", "R1", "Subject", "Register", true);
            Boolean ActualOutput = CodeBuilder.AddCall("Bussiness", "Update", "R2", "Bussiness", "Update", false);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }

        [TestMethod]
        public void ToCode_Normal01_1Call_CorrectNewSeqFlag()
        {
            OALCodeBuilder CodeBuilder = new OALCodeBuilder();

            String ExpectedOutput = 
            "create object instance observer of Observer;\n" +
            "create object instance subject of Subject;\n" +
            "relate observer to subject across Observer->Subject[R1];\n" +
            "observer.init();\n" +
            "subject.register();\n";

            CodeBuilder.AddCall("Observer", "init", "R1", "Subject", "Register", true);

            Assert.AreEqual(ExpectedOutput, CodeBuilder.ToCode());
        }
        [TestMethod]
        public void ToCode_Normal02_1Call_InCorrectNewSeqFlag()
        {
            OALCodeBuilder CodeBuilder = new OALCodeBuilder();

            String ExpectedOutput =
            "create object instance observer of Observer;\n" +
            "create object instance subject of Subject;\n" +
            "relate observer to subject across Observer->Subject[R1];\n" +
            "observer.init()[R1];\n" +
            "subject.register();\n";

            CodeBuilder.AddCall("Observer", "init", "R1", "Subject", "register", false);

            Assert.AreEqual(ExpectedOutput, CodeBuilder.ToCode());
        }
        [TestMethod]
        public void ToCode_Normal03_3Calls_1Seq_CorrectSeqFlag()
        {
            OALCodeBuilder CodeBuilder = new OALCodeBuilder();

            String ExpectedOutput =
            "create object instance class1 of Class1;\n" +
            "create object instance class2 of Class2;\n" +
            "create object instance class2 of Class2;\n" +
            "relate class1 to class2 across Class1->Class2[R2];\n" +
            "relate class2 to class3 across Class2->Class3[R4];\n" +
            "relate class3 to class4 across Class3->Class4[R15];\n" +
            "class1.method1()[R2];\n" +
            "class2.method2()[R4];\n" +
            "class3.method3()[R15];\n" +
            "class4.method4()[];\n";

            CodeBuilder.AddCall("Class1", "method1", "R2", "Class2", "method2", true);
            CodeBuilder.AddCall("Class2", "method2", "R4", "Class3", "method3", false);
            CodeBuilder.AddCall("Class3", "method3", "R15", "Class4", "method4", false);

            Assert.AreEqual(ExpectedOutput, CodeBuilder.ToCode());
        }
        [TestMethod]
        public void ToCode_Normal04_3Calls_2Seq_CorrectSeqFlag()
        {
            OALCodeBuilder CodeBuilder = new OALCodeBuilder();

            String ExpectedOutput =
            "create object instance class1 of Class1;\n" +
            "create object instance class2 of Class2;\n" +
            "create object instance class2 of Class2;\n" +
            "relate class1 to class2 across Class1->Class2[R2];\n" +
            "relate class2 to class3 across Class2->Class3[R4];\n" +
            "relate class5 to class4 across Class5->Class4[R17];\n" +
            "class1.method1()[R2];\n" +
            "class2.method2()[R4];\n" +
            "class3.method3()[];\n" +
            "class5.method5()[R17];\n" +
            "class4.method4()[];\n";

            CodeBuilder.AddCall("Class1", "method1", "R2", "Class2", "method2", true);
            CodeBuilder.AddCall("Class2", "method2", "R4", "Class3", "method3", false);
            CodeBuilder.AddCall("Class5", "method5", "R17", "Class4", "method4", true);

            Assert.AreEqual(ExpectedOutput, CodeBuilder.ToCode());
        }
    }
}

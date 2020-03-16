using AnimationControl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AnimationControlTests
{
    [TestClass]
    public class OALCommandParserTests
    {
        [TestMethod]
        public void IdentifyFirstTopLevelOperator_NormalArithmetic1()
        {
            OALCommandParser OCP = new OALCommandParser();

            (String, int) ExpectedOutput = ("-", 9);

            String Input = "(6 + 12) - (3 * (7 + 2) + (6))";
            (String, int) ActualOutput = OCP.IdentifyFirstTopLevelOperator(Input);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void IdentifyFirstTopLevelOperator_NormalArithmetic2()
        {
            OALCommandParser OCP = new OALCommandParser();

            (String, int) ExpectedOutput = ("*", 13);

            String Input = "(15000 + 47) * 9";
            (String, int) ActualOutput = OCP.IdentifyFirstTopLevelOperator(Input);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void IdentifyFirstTopLevelOperator_NormalString1()
        {
            OALCommandParser OCP = new OALCommandParser();

            (String, int) ExpectedOutput = ("+", 8);

            String Input = "\"D(+) \" + \"si...\"";
            (String, int) ActualOutput = OCP.IdentifyFirstTopLevelOperator(Input);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void IdentifyFirstTopLevelOperator_Case1State1InBracketsArithmetic()
        {
            OALCommandParser OCP = new OALCommandParser();

            (String, int) ExpectedOutput = ("", -1);

            String Input = "(5 + 6 * 7 - (6 * 9 + 2))";
            (String, int) ActualOutput = OCP.IdentifyFirstTopLevelOperator(Input);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void IdentifyFirstTopLevelOperator_Case1State2InBracketsArithmetic()
        {
            OALCommandParser OCP = new OALCommandParser();

            (String, int) ExpectedOutput = ("+", 2);

            String Input = "5 + 6 * 7 - (6 * 9 + 2)";
            (String, int) ActualOutput = OCP.IdentifyFirstTopLevelOperator(Input);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }

        [TestMethod]
        public void IdentifyFirstTopLevelOperator_Case1State3InBracketsArithmetic()
        {
            OALCommandParser OCP = new OALCommandParser();

            (String, int) ExpectedOutput = ("-", 6);

            String Input = "6 * 7 - (6 * 9 + 2)";
            (String, int) ActualOutput = OCP.IdentifyFirstTopLevelOperator(Input);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void IdentifyFirstTopLevelOperator_Case1State4InBracketsArithmetic()
        {
            OALCommandParser OCP = new OALCommandParser();

            (String, int) ExpectedOutput = ("*", 2);

            String Input = "6 * 7";
            (String, int) ActualOutput = OCP.IdentifyFirstTopLevelOperator(Input);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void IdentifyFirstTopLevelOperator_Case1State5InBracketsArithmetic()
        {
            OALCommandParser OCP = new OALCommandParser();

            (String, int) ExpectedOutput = ("+", 6);

            String Input = "6 * 9 + 2";
            (String, int) ActualOutput = OCP.IdentifyFirstTopLevelOperator(Input);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void IdentifyFirstTopLevelOperator_Case1State6InBracketsArithmetic()
        {
            OALCommandParser OCP = new OALCommandParser();

            (String, int) ExpectedOutput = ("*", 2);

            String Input = "6 * 9";
            (String, int) ActualOutput = OCP.IdentifyFirstTopLevelOperator(Input);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }

        [TestMethod]
        public void IsControlCommand_Normal1()
        {
            OALCommandParser OCP = new OALCommandParser();

            Boolean ExpectedOutput = true;

            String Input = "break";
            Boolean ActualOutput = OCP.IsControlCommand(Input);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void IsControlCommand_Normal2()
        {
            OALCommandParser OCP = new OALCommandParser();

            Boolean ExpectedOutput = true;

            String Input = "continue";
            Boolean ActualOutput = OCP.IsControlCommand(Input);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void IsControlCommand_Normal3()
        {
            OALCommandParser OCP = new OALCommandParser();

            Boolean ExpectedOutput = true;

            String Input = "return";
            Boolean ActualOutput = OCP.IsControlCommand(Input);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void IsControlCommand_Malformed1()
        {
            OALCommandParser OCP = new OALCommandParser();

            Boolean ExpectedOutput = false;

            String Input = "retur";
            Boolean ActualOutput = OCP.IsControlCommand(Input);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void IsControlCommand_Malformed2()
        {
            OALCommandParser OCP = new OALCommandParser();

            Boolean ExpectedOutput = false;

            String Input = "returned";
            Boolean ActualOutput = OCP.IsControlCommand(Input);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void IsControlCommand_Malformed3()
        {
            OALCommandParser OCP = new OALCommandParser();

            Boolean ExpectedOutput = false;

            String Input = "";
            Boolean ActualOutput = OCP.IsControlCommand(Input);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void IsControlCommand_Malformed4()
        {
            OALCommandParser OCP = new OALCommandParser();

            Boolean ExpectedOutput = false;

            String Input = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            Boolean ActualOutput = OCP.IsControlCommand(Input);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void IsControlCommand_Wrong1()
        {
            OALCommandParser OCP = new OALCommandParser();

            Boolean ExpectedOutput = false;

            String Input = "create";
            Boolean ActualOutput = OCP.IsControlCommand(Input);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void IsControlCommand_Wrong2()
        {
            OALCommandParser OCP = new OALCommandParser();

            Boolean ExpectedOutput = false;

            String Input = "while";
            Boolean ActualOutput = OCP.IsControlCommand(Input);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void IsControlCommand_Wrong3()
        {
            OALCommandParser OCP = new OALCommandParser();

            Boolean ExpectedOutput = false;

            String Input = "select";
            Boolean ActualOutput = OCP.IsControlCommand(Input);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void IsControlCommand_Wrong4()
        {
            OALCommandParser OCP = new OALCommandParser();

            Boolean ExpectedOutput = false;

            String Input = "if";
            Boolean ActualOutput = OCP.IsControlCommand(Input);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
    }
}

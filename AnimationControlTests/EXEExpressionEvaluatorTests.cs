using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnimationControl;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimationControl.Tests
{
    [TestClass]
    public class EXEExpressionEvaluatorTests
    {
        // SetUloh1 Create more unit tests for method EXEExpressionEvaluator.Evaluate()
        // Keep the naming convention as you see it here. Check the bottom 2 tests - they are a bit different (Assert.IsNull)
        //Prepare unit tests for each primitive type a and each operator -> experiment with edge values (negative, 0, null, empty string and so on)
        [TestMethod]
        public void Evaluate_Normal_Plus_Integer_01()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("+", new List<String>(new String[] { "1", "1"}));

            String ExpectedOutput = "2";

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Evaluate_Normal_Plus_Integer_02()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("+", new List<String>(new String[] { "0", "5", "12", "6"}));

            String ExpectedOutput = "23";

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Evaluate_Normal_Plus_Integer_03()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("+", new List<String>(new String[] { "-5", "-2"}));

            String ExpectedOutput = "-7";

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Evaluate_Normal_Plus_Integer_04()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("+", new List<String>(new String[] { "-5", "9" }));

            String ExpectedOutput = "4";

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Evaluate_Normal_Plus_Integer_05()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("+", new List<String>(new String[] { "-5", "9", "-6", "0", "1", "2", "-1" }));

            String ExpectedOutput = "0";

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Evaluate_Normal_Minus_Integer_01()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("-", new List<String>(new String[] { "12", "4" }));

            String ExpectedOutput = "8";

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Evaluate_Normal_Minus_Integer_02()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("-", new List<String>(new String[] { "5", "17" }));

            String ExpectedOutput = "-12";

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Evaluate_Normal_Minus_Integer_03()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("-", new List<String>(new String[] { "3", "-22" }));

            String ExpectedOutput = "25";

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Evaluate_Normal_Minus_Integer_04()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("-", new List<String>(new String[] { "-7", "-5" }));

            String ExpectedOutput = "-2";

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Evaluate_Normal_Minus_Integer_05()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("-", new List<String>(new String[] { "100", "5", "0", "10", "-5", "0" }));

            String ExpectedOutput = "90";

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Evaluate_Normal_Multiply_Integer_01()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("*", new List<String>(new String[] { "6", "7" }));

            String ExpectedOutput = "42";

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Evaluate_Normal_Multiply_Integer_02()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("*", new List<String>(new String[] { "10", "5", "1", "10", "-5", "1" }));

            String ExpectedOutput = "-2500";

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Evaluate_Normal_Multiply_Integer_03()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("*", new List<String>(new String[] { "1", "-1", "2", "2", "-2", "2", "1", "-1" }));

            String ExpectedOutput = "-16";

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Evaluate_Normal_Multiply_Integer_04()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("*", new List<String>(new String[] { "1", "-1", "2", "2", "-2", "2", "1", "0", "-1" }));

            String ExpectedOutput = "0";

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Evaluate_Normal_Divide_Integer_01()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("*", new List<String>(new String[] { "36", "4"}));

            String ExpectedOutput = "9";

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Evaluate_Bad_Divide_01_IntegerBool()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("*", new List<String>(new String[] { "78", "true" }));

            Assert.IsNull(ActualOutput);
        }
        [TestMethod]
        public void Evaluate_Bad_Plus_01_FloatString()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("+", new List<String>(new String[] { "2.87", "\"2.13\"" }));

            Assert.IsNull(ActualOutput);
        }
    }
}
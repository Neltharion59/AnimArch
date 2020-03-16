using AnimationControl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace AnimationControlTests
{
    [TestClass]
    public class EXEQueryCheckerTests
    {
        /// <summary>
        /// Unit tests for IsQuery method
        /// </summary>
        [TestMethod]
        public void IsQuery_CreateNormal()
        {
            EXEQueryChecker OEC = new EXEQueryChecker();

            Boolean ExpectedOutput = true;

            String Input = "create object instance o of Observer";
            Boolean ActualOutput = OEC.IsQuery(Input);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void IsQuery_CreateNoWhitespace()
        {
            EXEQueryChecker OEC = new EXEQueryChecker();

            Boolean ExpectedOutput = false;

            String Input = "createobject instance o of Observer";
            Boolean ActualOutput = OEC.IsQuery(Input);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void IsQuery_CreateMalformed()
        {
            EXEQueryChecker OEC = new EXEQueryChecker();

            Boolean ExpectedOutput = true;

            String Input = "create object instance o of instance o Observer";
            Boolean ActualOutput = OEC.IsQuery(Input);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void IsQuery_RelateNormal()
        {
            EXEQueryChecker OEC = new EXEQueryChecker();

            Boolean ExpectedOutput = true;

            String Input = "relate student1 to ucitel3 across R1";
            Boolean ActualOutput = OEC.IsQuery(Input);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void IsQuery_RelateNoWhiteSpace()
        {
            EXEQueryChecker OEC = new EXEQueryChecker();

            Boolean ExpectedOutput = false;

            String Input = "relatestudent1 to ucitel3 across R1";
            Boolean ActualOutput = OEC.IsQuery(Input);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void IsQuery_RelateMalformed()
        {
            EXEQueryChecker OEC = new EXEQueryChecker();

            Boolean ExpectedOutput = true;

            String Input = "relate student1 to ucitel3 and ucitel22 via R1";
            Boolean ActualOutput = OEC.IsQuery(Input);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void IsQuery_SelectNormal()
        {
            EXEQueryChecker OEC = new EXEQueryChecker();

            Boolean ExpectedOutput = true;

            String Input = "select any x related by self->class[R1]";
            Boolean ActualOutput = OEC.IsQuery(Input);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void IsQuery_SelectNoWhiteSpace()
        {
            EXEQueryChecker OEC = new EXEQueryChecker();

            Boolean ExpectedOutput = false;

            String Input = "selectany x related by self->Car[R1]";
            Boolean ActualOutput = OEC.IsQuery(Input);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void IsQuery_SelectMalformed()
        {
            EXEQueryChecker OEC = new EXEQueryChecker();

            Boolean ExpectedOutput = true;

            String Input = "select any x related to Car by R1";
            Boolean ActualOutput = OEC.IsQuery(Input);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void IsQuery_MiscUnrelatedString()
        {
            EXEQueryChecker OEC = new EXEQueryChecker();

            Boolean ExpectedOutput = false;

            String Input = "vcera som neveceral";
            Boolean ActualOutput = OEC.IsQuery(Input);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void IsQuery_MiscKeywordMisuse()
        {
            EXEQueryChecker OEC = new EXEQueryChecker();

            Boolean ExpectedOutput = true;

            String Input = "create = 22.4";
            Boolean ActualOutput = OEC.IsQuery(Input);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void IsQuery_MiscEmptyString()
        {
            EXEQueryChecker OEC = new EXEQueryChecker();

            Boolean ExpectedOutput = false;

            String Input = "";
            Boolean ActualOutput = OEC.IsQuery(Input);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void IsQuery_MiscNull()
        {
            EXEQueryChecker OEC = new EXEQueryChecker();

            Boolean ExpectedOutput = false;

            String Input = null;
            Boolean ActualOutput = OEC.IsQuery(Input);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }

        /// <summary>
        /// Unit tests for ConstructASTCreate method
        /// </summary>
        ///
        [TestMethod]
        public void ConstructASTCreate_Normal()
        {
            EXEQueryChecker OEC = new EXEQueryChecker();

            List<String> ExpectedOutput = new List<String>(new String[] { "create", "observer2", "Observer"});

            String Input = "create object instance observer2 of Observer";
            EXEASTNodeComposite ResultObject = OEC.ConstructASTCreate(Input);
            List<String> ActualOutput = new List<String>(new String[] { ResultObject.Operation, ResultObject.Operands[0].GetNodeValue(), ResultObject.Operands[1].GetNodeValue()});

            CollectionAssert.AreEqual(ExpectedOutput, ActualOutput);
        }
        /// <summary>
        /// Unit tests for ConstructASTRelate method
        /// </summary>
        ///
        [TestMethod]
        public void ConstructASTRelate_Normal1()
        {
            EXEQueryChecker OEC = new EXEQueryChecker();

            List<String> ExpectedOutput = new List<String>(new String[] { "relate", "a", "b", "R1" });

            String Input = "relate a to b across R1";
            EXEASTNodeComposite ResultObject = OEC.ConstructASTRelate(Input);
            List<String> ActualOutput = new List<String>(new String[] { ResultObject.Operation, ResultObject.Operands[0].GetNodeValue(), ResultObject.Operands[1].GetNodeValue(), ResultObject.Operands[2].GetNodeValue() });

            CollectionAssert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void ConstructASTRelate_Normal2()
        {
            EXEQueryChecker OEC = new EXEQueryChecker();

            List<String> ExpectedOutput = new List<String>(new String[] { "relate", "observer2", "subject13", "R52" });

            String Input = "relate observer2 to subject13 across R52";
            EXEASTNodeComposite ResultObject = OEC.ConstructASTRelate(Input);
            List<String> ActualOutput = new List<String>(new String[] { ResultObject.Operation, ResultObject.Operands[0].GetNodeValue(), ResultObject.Operands[1].GetNodeValue(), ResultObject.Operands[2].GetNodeValue() });

            CollectionAssert.AreEqual(ExpectedOutput, ActualOutput);
        }
        /// <summary>
        /// Unit tests for ConstructASTSelectAnyRelated method
        /// </summary>
        ///
        [TestMethod]
        public void ConstructASTSelect_Normal1()
        {
            EXEQueryChecker OEC = new EXEQueryChecker();

            List<String> ExpectedOutput = new List<String>(new String[] { "select any related by", "subject", "self", "Observer", "R1" });

            String Input = "select any subject related by self->Observer[R1]";
            EXEASTNodeComposite ResultObject = OEC.ConstructASTSelectAnyRelated(Input);
            List<String> ActualOutput = new List<String>(new String[] { ResultObject.Operation, ResultObject.Operands[0].GetNodeValue(), ResultObject.Operands[1].GetNodeValue(), ResultObject.Operands[2].GetNodeValue(), ResultObject.Operands[3].GetNodeValue() });

            CollectionAssert.AreEqual(ExpectedOutput, ActualOutput);
        }
    }
}

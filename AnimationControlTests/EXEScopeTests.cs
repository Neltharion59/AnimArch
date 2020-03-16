using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnimationControl;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimationControl.Tests
{
    [TestClass]
    public class EXEScopeTests
    {
        // SetUloh1 Create similar tests(all 4 of them) for the methods you implemented in this class (both of them)
        // Write these tests into this class and keep the naming convention and code structure
        // If you notice test case that isn here, you can write even more tests

        // Test circle of scopes
        [TestMethod]
        public void FindPrimitiveVariableByNameTest_Normal_01()
        {
            EXEScope Scope = new EXEScope();
            Scope.PrimitiveVariables.Add(new EXEPrimitiveVariable("x", "15"));
            Scope.PrimitiveVariables.Add(new EXEPrimitiveVariable("y", "6"));
            Scope.PrimitiveVariables.Add(new EXEPrimitiveVariable("z", "-4"));
            Scope.PrimitiveVariables.Add(new EXEPrimitiveVariable("a", "\"ahoj\""));
            Scope.PrimitiveVariables.Add(new EXEPrimitiveVariable("b", "\"nazdar\""));
            Scope.PrimitiveVariables.Add(new EXEPrimitiveVariable("c", "\"caaaaaau\""));

            EXEPrimitiveVariable SeekedVariable =  Scope.FindPrimitiveVariableByName("a");
            (String, String) ActualOutput = (SeekedVariable.Name, SeekedVariable.Value);

            (String, String) ExpectedOutput = ("a", "\"ahoj\"");

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void FindPrimitiveVariableByNameTest_Normal_02()
        {
            EXEScope Scope = new EXEScope();
            Scope.PrimitiveVariables.Add(new EXEPrimitiveVariable("x", "15"));
            Scope.PrimitiveVariables.Add(new EXEPrimitiveVariable("y", "6"));
            Scope.PrimitiveVariables.Add(new EXEPrimitiveVariable("z", "-4"));

            EXEScope ScopeIn1 = new EXEScope();
            ScopeIn1.PrimitiveVariables.Add(new EXEPrimitiveVariable("a", "\"ahoj\""));
            ScopeIn1.PrimitiveVariables.Add(new EXEPrimitiveVariable("b", "\"nazdar\""));
            ScopeIn1.PrimitiveVariables.Add(new EXEPrimitiveVariable("c", "\"caaaaaau\""));
            Scope.SuperScope = ScopeIn1;

            EXEScope ScopeIn2 = new EXEScope();
            ScopeIn2.PrimitiveVariables.Add(new EXEPrimitiveVariable("nejake_cislo", "59.59"));
            ScopeIn1.SuperScope = ScopeIn2;

            EXEPrimitiveVariable SeekedVariable = Scope.FindPrimitiveVariableByName("nejake_cislo");
            (String, String) ActualOutput = (SeekedVariable.Name, SeekedVariable.Value);

            (String, String) ExpectedOutput = ("nejake_cislo", "59.59");

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }

        [TestMethod]
        public void FindPrimitiveVariableByNameTest_Bad_01()
        {
            EXEScope Scope = new EXEScope();
            Scope.PrimitiveVariables.Add(new EXEPrimitiveVariable("x", "15"));
            Scope.PrimitiveVariables.Add(new EXEPrimitiveVariable("y", "6"));
            Scope.PrimitiveVariables.Add(new EXEPrimitiveVariable("z", "-4"));
            Scope.PrimitiveVariables.Add(new EXEPrimitiveVariable("a", "\"ahoj\""));
            Scope.PrimitiveVariables.Add(new EXEPrimitiveVariable("b", "\"nazdar\""));
            Scope.PrimitiveVariables.Add(new EXEPrimitiveVariable("c", "\"caaaaaau\""));

            EXEPrimitiveVariable SeekedVariable = Scope.FindPrimitiveVariableByName("neznama_premenna");

            Assert.IsNull(SeekedVariable);
        }

        [TestMethod]
        public void FindPrimitiveVariableByNameTest_Bad_02()
        {
            EXEScope Scope = new EXEScope();
            Scope.PrimitiveVariables.Add(new EXEPrimitiveVariable("x", "15"));
            Scope.PrimitiveVariables.Add(new EXEPrimitiveVariable("y", "6"));
            Scope.PrimitiveVariables.Add(new EXEPrimitiveVariable("z", "-4"));

            EXEScope ScopeIn1 = new EXEScope();
            ScopeIn1.PrimitiveVariables.Add(new EXEPrimitiveVariable("a", "\"ahoj\""));
            ScopeIn1.PrimitiveVariables.Add(new EXEPrimitiveVariable("b", "\"nazdar\""));
            ScopeIn1.PrimitiveVariables.Add(new EXEPrimitiveVariable("c", "\"caaaaaau\""));
            Scope.SuperScope = ScopeIn1;

            EXEScope ScopeIn2 = new EXEScope();
            ScopeIn2.PrimitiveVariables.Add(new EXEPrimitiveVariable("nejaky_float", "59.59"));
            ScopeIn1.SuperScope = ScopeIn2;

            EXEPrimitiveVariable SeekedVariable = Scope.FindPrimitiveVariableByName("nejaky_integer");

            Assert.IsNull(SeekedVariable);
        }

    }
}
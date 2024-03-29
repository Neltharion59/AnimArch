﻿
using Assets.Scripts.AnimationControl.OAL;
using NUnit.Framework;
using OALProgramControl;

namespace Assets.UnitTests.AnimationControl
{
    public class ParserTests : StandardTest
    {
        [Test]
        public void EXECommandAssignment_01_ThreeAttributesInRow()
        {
            // Arrange
            string _input = "x.y.z = 5;";

            // Act
            EXEScopeMethod _parsedCommands = OALParserBridge.Parse(_input);
            var _actualOutput = ToCode(_parsedCommands);

            // Assert
            string _expectedOutput = "x.y.z = 5;\n";
            Assert.AreEqual(_expectedOutput, _actualOutput);
        }
    }
}
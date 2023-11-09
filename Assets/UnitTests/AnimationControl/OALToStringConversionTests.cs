using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.AnimationControl.OAL;
using NUnit.Framework;
using OALProgramControl;
using UnityEngine;
using UnityEngine.TestTools;

public class OALToStringConversionTests
{
    [Test]
    public void OALToStringConversionTestsSimplePasses()
    {
        string _methodSourceCode = "ntrollfactory_1.CreateMage();\r\n";
        string expectedCode = "<b><color=green>ntrollfactory_1.CreateMage();</color></b>\n";
        EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
        foreach (EXECommand command in methodScope.Commands) {
            command.IsActive = true;
        }

        EXECommand.visitor.ActivateHighlighting();
        methodScope.Accept(EXECommand.visitor);

        Assert.AreEqual(expectedCode, EXECommand.visitor.GetCommandStringAndResetStateNow());
    }
}

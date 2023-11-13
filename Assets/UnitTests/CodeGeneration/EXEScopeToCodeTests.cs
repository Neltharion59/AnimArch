using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.AnimationControl.OAL;
using NUnit.Framework;
using OALProgramControl;
using UnityEngine;
using UnityEngine.TestTools;

public class EXEScopeToCodeTests
{
    [Test]
    public void EXEScopeForEach_ToCodeConversionTest()
    {//.Perform()element
        // Arrange
        string _methodSourceCode = "for each element in list\r\n\telement.Perform();\r\nend for;";
        EXEScopeMethod _scope = OALParserBridge.Parse(_methodSourceCode);
        _scope.IsActive = true;
        foreach (EXECommand _command in _scope.Commands) {
            _command.IsActive = true;
        }

        // Act
        VisitorCommandToString visitor = VisitorCommandToString.BorrowAVisitor();
        visitor.DeactivateSimpleFormatting();
        _scope.Accept(visitor);
        string _actualUnformattedOutput = visitor.GetCommandStringAndResetStateNow();

        visitor = VisitorCommandToString.BorrowAVisitor();
        _scope.Accept(visitor);
        string _actualFormattedOutput = visitor.GetCommandStringAndResetStateNow();

        visitor = VisitorCommandToString.BorrowAVisitor();
        visitor.DeactivateSimpleFormatting();
        visitor.ActivateHighlighting();
        _scope.Accept(visitor);
        string _actualHighlightedOutput = visitor.GetCommandStringAndResetStateNow();

        visitor = VisitorCommandToString.BorrowAVisitor();
        visitor.ActivateHighlighting();
        _scope.Accept(visitor);
        string _actualHighlightedAndFormattedOutput = visitor.GetCommandStringAndResetStateNow();
    
        // Assert
        string _expectedUnformattedOutput             = "for each element in list\nelement.Perform()end for";
        string _expectedFormattedOutput               = "for each element in list\n\telement.Perform();\nend for;\n";
        string _expectedHighlightedOutput             = "<b><color=green>for each element in list\n</color></b>element.Perform()<b><color=green>end for</color></b>";
        string _expectedHighlightedAndFormattedOutput = "<b><color=green>for each element in list\n</color></b>\telement.Perform();\n<b><color=green>end for;\n</color></b>";
    
        Assert.AreEqual(_expectedUnformattedOutput, _actualUnformattedOutput);
        Assert.AreEqual(_expectedFormattedOutput, _actualFormattedOutput);
        Assert.AreEqual(_expectedHighlightedOutput, _actualHighlightedOutput);
        Assert.AreEqual(_expectedHighlightedAndFormattedOutput, _actualHighlightedAndFormattedOutput);
    }
}

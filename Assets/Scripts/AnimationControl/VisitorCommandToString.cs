using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OALProgramControl;
using UnityEngine;

public class VisitorCommandToString : Visitor
{
    private readonly StringBuilder commandString;
    private bool simpleFormatting;

    private int indentationLevel;

    public string GetCommandStringAndResetStateNow() {
        string result = commandString.ToString();
        ResetState();
        return result;
    }

    public VisitorCommandToString()
    {
        commandString = new StringBuilder();
        ResetState();
    }

    private void ResetState() {
        simpleFormatting = true;
        indentationLevel = 0;
        commandString.Clear();
    }

    public void ActivateSimpleFormatting() {
        simpleFormatting = true;
        indentationLevel = 0;
    }

    public void DeactivateSimpleFormatting() {
        simpleFormatting = false;
    }

    private void IncreaseIndentation() {
        indentationLevel++;
    }

    private void DecreaseIndentation() {
        indentationLevel--;
    }

    private void AddIndentation() {
        if (simpleFormatting) {
            commandString.Append(indentationLevel*4*' ');
        }
    }

    private void AddEOL() {
        if (simpleFormatting) {
            commandString.Append(";\n");
        }
    }

    public override void VisitExeCommand(EXECommand command)
    {
        AddIndentation();
        commandString.Append("Command");
        AddEOL();
    }

    public override void VisitExeCommandBreak(EXECommandBreak command)
    {
        AddIndentation();
        commandString.Append("break");
        AddEOL();
    }

    public override void VisitExeCommandCall(EXECommandCall command)
    {
        AddIndentation();
        commandString.Append(command.MethodAccessChainS + "." + command.MethodCall.ToCode());
        AddEOL();
    }

    public override void VisitExeCommandContinue(EXECommandContinue command)
    {
        AddIndentation();
        commandString.Append("continue");
        AddEOL();
    }

    public override void VisitExeCommandAddingToList(EXECommandAddingToList command)
    {
        AddIndentation();
        commandString.Append("add " + command.AddedElement.ToCode() + " to " + command.Array.ToCode());
        AddEOL();
    }

    public override void VisitExeCommandAssignment(EXECommandAssignment command)
    {
        AddIndentation();
        commandString.Append(command.AssignmentTarget.ToCode() + " = " + command.AssignedExpression.ToCode());
        AddEOL();
    }

    public override void VisitExeCommandCreateList(EXECommandCreateList command)
    {
        AddIndentation();
        commandString.Append("create list " + command.AssignmentTarget.ToCode()
               + " of " + command.ArrayType + " { " + string.Join(", ", command.Items.Select(item => item.ToCode())) + " }");
        AddEOL();
    }

    public override void VisitExeCommandMulti(EXECommandMulti command)
    {
        //nie je implemetacia v command multi
    }

    public override void VisitExeCommandQueryCreate(EXECommandQueryCreate command)
    {
        AddIndentation();
        commandString.Append("create object instance " + (command.AssignmentTarget?.ToCode() ?? string.Empty) + " of " + command.ClassName);
        AddEOL();
    }

    public override void VisitExeCommandQueryDelete(EXECommandQueryDelete command)
    {
        AddIndentation();
        commandString.Append("delete object instance " + command.DeletedVariable.ToCode());
        AddEOL();
    }

    public override void VisitExeCommandRead(EXECommandRead command)
    {
        AddIndentation();
        commandString.Append(
               command.AssignmentTarget.ToCode()
                   + " = "
                   + command.AssignmentType
                  + (command.Prompt?.ToCode() ?? string.Empty)
                   + (EXETypes.StringTypeName.Equals(command.AssignmentType) ? ")" : "))"));
        AddEOL();
    }

    public override void VisitExeCommandRemovingFromList(EXECommandRemovingFromList command)
    {
        AddIndentation();
        commandString.Append("remove " + command.Item.ToCode() + " from " + command.Array.ToCode());
        AddEOL();
    }

    public override void VisitExeCommandReturn(EXECommandReturn command)
    {
        AddIndentation();
        commandString.Append(command.Expression == null ? "return" : ("return " + command.Expression.ToCode()));
        AddEOL();
    }

    public override void VisitExeCommandWrite(EXECommandWrite command)
    {
        AddIndentation();
        commandString.Append("write(" + string.Join(", ", command.Arguments.Select(argument => argument.ToCode())) + ")");
        AddEOL();
    }

    public override void VisitExeScope(EXEScope scope)
    {
        
    }

    public override void VisitExeScopeLoop(EXEScopeLoop scope)
    {
    }

    public override void VisitExeScopeMethod(EXEScopeMethod scope)
    {
    }

    public override void VisitExeScopeForEach(EXEScopeForEach scope)
    {
    }

    public override void VisitExeScopeParallel(EXEScopeParallel scope)
    {
    }

    public override void VisitExeScopeCondition(EXEScopeCondition scope)
    {
    }

    public override void VisitExeScopeLoopWhile(EXEScopeLoopWhile scope)
    {
    }
}

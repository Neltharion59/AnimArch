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
    private bool highlighting;

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
        highlighting = false;
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

    public void ActivateHighlighting() {
        highlighting = true;
    }

    public void DeactivateHighlighting() {
        highlighting = false;
    }

    private void IncreaseIndentation() {
        indentationLevel++;
    }

    private void DecreaseIndentation() {
        indentationLevel--;
    }

    private void HighlightBegin(EXECommand command) {
        if (highlighting && command.IsActive) {
            commandString.Append("<b><color=green>");
        }

    }

    private void HighlightEnd(EXECommand command) {
        if (highlighting && command.IsActive) {
            commandString.Append("</color></b>");
        }
    }

    private void WriteIndentation() {
        if (simpleFormatting) {
            commandString.Append(string.Concat(Enumerable.Repeat("\t", indentationLevel)));
        }
    }

    private void AddEOL() {
        if (simpleFormatting) {
            commandString.Append(";\n");
        }
    }

    public override void VisitExeCommand(EXECommand command)
    {
        HighlightBegin(command);
        WriteIndentation();
        commandString.Append("Command");
        AddEOL();
        HighlightEnd(command);
    }

    public override void VisitExeCommandBreak(EXECommandBreak command)
    {   
        HighlightBegin(command);
        WriteIndentation();
        commandString.Append("break");
        AddEOL();
        HighlightEnd(command);
    }

    public override void VisitExeCommandCall(EXECommandCall command)
    {
        HighlightBegin(command);
        WriteIndentation();
        commandString.Append(command.MethodAccessChainS + "." + command.MethodCall.ToCode());
        AddEOL();
        HighlightEnd(command);
    }

    public override void VisitExeCommandContinue(EXECommandContinue command)
    {
        HighlightBegin(command);
        WriteIndentation();
        commandString.Append("continue");
        AddEOL();
        HighlightEnd(command);
    }

    public override void VisitExeCommandAddingToList(EXECommandAddingToList command)
    {
        HighlightBegin(command);
        WriteIndentation();
        commandString.Append("add " + command.AddedElement.ToCode() + " to " + command.Array.ToCode());
        AddEOL();
        HighlightEnd(command);
    }

    public override void VisitExeCommandAssignment(EXECommandAssignment command)
    {
        HighlightBegin(command);
        WriteIndentation();
        commandString.Append(command.AssignmentTarget.ToCode() + " = " + command.AssignedExpression.ToCode());
        AddEOL();
        HighlightEnd(command);
    }

    public override void VisitExeCommandCreateList(EXECommandCreateList command)
    {
        HighlightBegin(command);
        WriteIndentation();
        commandString.Append("create list " + command.AssignmentTarget.ToCode()
               + " of " + command.ArrayType + " { " + string.Join(", ", command.Items.Select(item => item.ToCode())) + " }");
        AddEOL();
        HighlightEnd(command);
    }

    public override void VisitExeCommandMulti(EXECommandMulti command)
    {
        VisitExeCommand(command);
    }

    public override void VisitExeCommandQueryCreate(EXECommandQueryCreate command)
    {
        HighlightBegin(command);
        WriteIndentation();
        commandString.Append("create object instance " + (command.AssignmentTarget?.ToCode() ?? string.Empty) + " of " + command.ClassName);
        AddEOL();
        HighlightEnd(command);
    }

    public override void VisitExeCommandQueryDelete(EXECommandQueryDelete command)
    {
        HighlightBegin(command);
        WriteIndentation();
        commandString.Append("delete object instance " + command.DeletedVariable.ToCode());
        AddEOL();
        HighlightEnd(command);
    }

    public override void VisitExeCommandRead(EXECommandRead command)
    {
        HighlightBegin(command);
        WriteIndentation();
        commandString.Append(
               command.AssignmentTarget.ToCode()
                   + " = "
                   + command.AssignmentType
                  + (command.Prompt?.ToCode() ?? string.Empty)
                   + (EXETypes.StringTypeName.Equals(command.AssignmentType) ? ")" : "))"));
        AddEOL();
        HighlightEnd(command);
    }

    public override void VisitExeCommandRemovingFromList(EXECommandRemovingFromList command)
    {
        HighlightBegin(command);
        WriteIndentation();
        commandString.Append("remove " + command.Item.ToCode() + " from " + command.Array.ToCode());
        AddEOL();
        HighlightEnd(command);
    }

    public override void VisitExeCommandReturn(EXECommandReturn command)
    {
        HighlightBegin(command);
        WriteIndentation();
        commandString.Append(command.Expression == null ? "return" : ("return " + command.Expression.ToCode()));
        AddEOL();
        HighlightEnd(command);
    }

    public override void VisitExeCommandWrite(EXECommandWrite command)
    {
        HighlightBegin(command);
        WriteIndentation();
        commandString.Append("write(" + string.Join(", ", command.Arguments.Select(argument => argument.ToCode())) + ")");
        AddEOL();
        HighlightEnd(command);
    }

    public override void VisitExeScope(EXEScope scope)
    {
        foreach (EXECommand Command in scope.Commands)
        {
            Command.Accept(this);
        }
    }

    public override void VisitExeScopeLoop(EXEScopeLoop scope)
    {
        
        foreach (EXECommand Command in scope.Commands)
        {
            Command.Accept(this);
        }
    }

    public override void VisitExeScopeMethod(EXEScopeMethod scope)
    {
        foreach (EXECommand Command in scope.Commands)
        {
            Command.Accept(this);
        }
    }

    public override void VisitExeScopeForEach(EXEScopeForEach scope)
    { 
        HighlightBegin(scope);
        WriteIndentation();
        commandString.Append("for each " + scope.IteratorName + " in "
                    + scope.Iterable.ToCode()
                    + "\n");

            ActivateHighlighting();
            IncreaseIndentation();
            foreach (EXECommand Command in scope.Commands)
            {
                Command.Accept(this);
            }
            DeactivateHighlighting();
            DecreaseIndentation();

            HighlightBegin(scope);
            WriteIndentation();
            commandString.Append("end for");
            AddEOL();
    }

    public override void VisitExeScopeParallel(EXEScopeParallel scope)
    {
        HighlightBegin(scope);
        WriteIndentation();
        commandString.Append("par\n");
        HighlightEnd(scope);


        if (scope.Threads != null)
        {
            foreach (EXEScope Thread in scope.Threads)
            {
                
                HighlightBegin(scope);
                WriteIndentation();
                commandString.Append("\tthread\n");
                HighlightEnd(scope);


                ActivateHighlighting();
                IncreaseIndentation();
                IncreaseIndentation();
                foreach (EXECommand Command in Thread.Commands)
                {
                    Command.Accept(this);
                }
                DecreaseIndentation();
                DecreaseIndentation();
                DeactivateHighlighting();

                HighlightBegin(scope);
                WriteIndentation();
                commandString.Append("\tend thread");
                AddEOL();
                HighlightEnd(scope);
            }
        }

        HighlightBegin(scope);
        WriteIndentation();
        commandString.Append("end par");
        AddEOL();
        HighlightEnd(scope);
    }

    public override void VisitExeScopeCondition(EXEScopeCondition scope)
    {
        HighlightBegin(scope);
        WriteIndentation();
        commandString.Append("if (" + scope.Condition.ToCode() + ")\n");
        HighlightEnd(scope);

        ActivateHighlighting();
        IncreaseIndentation();
        foreach (EXECommand Command in scope.Commands)
        {
            Command.Accept(this);
        }
        DecreaseIndentation();
        DeactivateHighlighting();

        if (scope.ElifScopes != null)
        {
            foreach (EXEScopeCondition Elif in scope.ElifScopes)
            {
                HighlightBegin(scope);
                WriteIndentation();
                commandString.Append("elif ("+ Elif.Condition.ToCode() + ")\n");
                HighlightEnd(scope);

                ActivateHighlighting();
                IncreaseIndentation();
                foreach (EXECommand Command in Elif.Commands)
                {
                    Command.Accept(this);
                }
                DecreaseIndentation();
                DeactivateHighlighting(); 
            }
        }
            if (scope.ElseScope != null)
            {
                HighlightBegin(scope);
                WriteIndentation();
                commandString.Append("else\n");
                HighlightEnd(scope);

                ActivateHighlighting();
                IncreaseIndentation();
                foreach (EXECommand Command in scope.ElseScope.Commands)
                {
                    Command.Accept(this);
                }
                DecreaseIndentation();
                DeactivateHighlighting(); 
            }

            HighlightBegin(scope);
            WriteIndentation();
            commandString.Append("end if");
            AddEOL();
            HighlightEnd(scope);

    }

    public override void VisitExeScopeLoopWhile(EXEScopeLoopWhile scope)
    {
        HighlightBegin(scope);
        WriteIndentation();
        commandString.Append("while (" + scope.Condition.ToCode() + ")\n");
        HighlightEnd(scope);

        ActivateHighlighting();
        IncreaseIndentation();
        foreach (EXECommand Command in scope.Commands)
        {
            Command.Accept(this);
        }
        DecreaseIndentation();
        DeactivateHighlighting();

        HighlightBegin(scope);
        WriteIndentation();
        commandString.Append("end while");
        AddEOL();
        HighlightEnd(scope);

    }
}

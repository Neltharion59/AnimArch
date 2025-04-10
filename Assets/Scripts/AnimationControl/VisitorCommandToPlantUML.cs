using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Msagl.Drawing;
using OALProgramControl;
using UnityEngine;

public class VisitorCommandToPlantUML : Visitor
{
    private readonly StringBuilder commandString;
    private bool simpleFormatting;

    private int indentationLevel;

    public Stack<string> classNames;


    public VisitorCommandToPlantUML()
    {
        commandString = new StringBuilder();
        classNames = new Stack<string>();
        ResetState();
    }

    public string GetCommandString() {
        string result = commandString.ToString();
        return result;
    }

    public void AppendToCommandString(string keyword) {
        commandString.Append(keyword);
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

    private void WriteIndentation() {
        if (simpleFormatting) {
            commandString.Append(string.Concat(Enumerable.Repeat("\t", indentationLevel)));
        }
    }

    private void AddEOL() {
        if (simpleFormatting) {
            commandString.Append("\n");
        }
    }

    private void HandleBasicEXECommand(EXECommand command, Func<VisitorCommandToPlantUML, bool> addCommandSimpleString) {
        WriteIndentation();
        addCommandSimpleString(this);
        AddEOL();
    }

    public override void VisitExeCommand(EXECommand command)
    {
        HandleBasicEXECommand(command, (visitor) => {
            return false;
        });
    }

    public override void VisitExeCommandBreak(EXECommandBreak command)
    {   
        HandleBasicEXECommand(command, (visitor) => {
            return false;
        });
    }

    public override void VisitExeCommandCall(EXECommandCall command)
    {
        HandleBasicEXECommand(command, (visitor) => {
            string callerClass = classNames.Peek();
            string nextClass = command.MethodAccessChainS;
            if (nextClass == "self"){
                nextClass = callerClass;
            }
            visitor.commandString.Append(callerClass + " -> " + nextClass  + " : ");
            classNames.Push(nextClass);

            command.MethodCall.Accept(visitor);
            visitor.commandString.Append("\nactivate " + nextClass);

            return false;
        });
    }

    public override void VisitExeCommandContinue(EXECommandContinue command)
    {
        HandleBasicEXECommand(command, (visitor) => {
            return false;
        });
    }

    public override void VisitExeCommandListOperation(EXECommandListOperation command)
    {
        HandleBasicEXECommand(command, (visitor) => {
            return false;
        });
    }

    public override void VisitExeCommandAddingToList(EXECommandAddingToList command)
    {
        HandleBasicEXECommand(command, (visitor) => {
            command.AddedElement.Accept(visitor);
            command.Array.Accept(visitor);
            return false;
        });
    }

    public override void VisitExeCommandAssignment(EXECommandAssignment command)
    {
        HandleBasicEXECommand(command, (visitor) => {
            command.AssignmentTarget.Accept(visitor);
            command.AssignedExpression.Accept(visitor);
            return false;
        });
    }

    public override void VisitExeCommandCreateList(EXECommandCreateList command)
    {
        HandleBasicEXECommand(command, (visitor) => {
            command.AssignmentTarget.Accept(visitor);
            foreach (var item in command.Items)
            {
                item.Accept(this);
            }
            return false;
        });
    }

    public override void VisitExeCommandFileAppend(EXECommandFileAppend command)
    {
        HandleBasicEXECommand(command, (visitor) => {
            command.StringToWrite.Accept(visitor);
            command.FileToWriteTo.Accept(visitor);
            return false;
        });
    }

    public override void VisitExeCommandFileExists(EXECommandFileExists command)
    {
        HandleBasicEXECommand(command, (visitor) => {
            command.AssignmentTarget.Accept(visitor);
            command.FileToCheck.Accept(visitor);
            return false;
        });
    }

    public override void VisitExeCommandFileRead(EXECommandFileRead command)
    {
        HandleBasicEXECommand(command, (visitor) => {
            command.AssignmentTarget.Accept(visitor);
            command.FileToReadFrom.Accept(visitor);
            return false;
        });
    }

    public override void VisitExeCommandFileWrite(EXECommandFileWrite command)
    {
        HandleBasicEXECommand(command, (visitor) => {
            command.StringToWrite.Accept(visitor);
            command.FileToWriteTo.Accept(visitor);
            return false;
        });
    }

    public override void VisitExeCommandQueryCreate(EXECommandQueryCreate command)
    {
        HandleBasicEXECommand(command, (visitor) => {
            if (command.AssignmentTarget != null)
            {
                command.AssignmentTarget.Accept(visitor);
            }
            return false;
        });
    }

    public override void VisitExeCommandQueryDelete(EXECommandQueryDelete command)
    {
        HandleBasicEXECommand(command, (visitor) => {
            command.DeletedVariable.Accept(visitor);
            return false;
        });
    }

    public override void VisitExeCommandRead(EXECommandRead command)
    {
        HandleBasicEXECommand(command, (visitor) => {
            command.AssignmentTarget.Accept(visitor);

            if (command.Prompt != null)
            {
                command.Prompt.Accept(visitor);
            }
            return false;
        });
    }

    public override void VisitExeCommandRemovingFromList(EXECommandRemovingFromList command)
    {
        HandleBasicEXECommand(command, (visitor) => {
            command.Item.Accept(visitor);
            command.Array.Accept(visitor);
            return false;
        });
    }

    public override void VisitExeCommandReturn(EXECommandReturn command)
    {
        HandleBasicEXECommand(command, (visitor) => {
            if (command.Expression != null)
            {
                command.Expression.Accept(visitor);
            }
            visitor.commandString.Append("\nreturn done");
            classNames.Pop();
            
            return false;
        });
    }


    public override void VisitExeCommandWait(EXECommandWait command)
    {
        HandleBasicEXECommand(command, (visitor) => {
            command.WaitTime.Accept(visitor);

            return false;
        });
    }

    public override void VisitExeCommandWrite(EXECommandWrite command)
    {
        HandleBasicEXECommand(command, (visitor) => {
            foreach (var arg in command.Arguments)
            {
                arg.Accept(this);
            }
            return false;
        });
    }

    public override void VisitExeScopeNull(EXEScopeNull scope)
    {
        HandleBasicEXECommand(scope, (visitor) => {
            return false;
        });
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
    }

    public override void VisitExeScopeForEach(EXEScopeForEach scope)
    { 
        WriteIndentation();
        scope.Iterable.Accept(this);

        IncreaseIndentation();

        DecreaseIndentation();

        WriteIndentation();
        AddEOL();
    }

    public override void VisitExeScopeParallel(EXEScopeParallel scope)
    {
        WriteIndentation();

        if (scope.Threads != null)
        {
            foreach (EXEScope Thread in scope.Threads)
            {
                WriteIndentation();

                IncreaseIndentation();
                IncreaseIndentation();
                foreach (EXECommand Command in Thread.Commands)
                {
                    Command.Accept(this);
                }
                DecreaseIndentation();
                DecreaseIndentation();

                WriteIndentation();
                AddEOL();
            }
        }

        WriteIndentation();
        AddEOL();
    }

    public override void VisitExeScopeCondition(EXEScopeCondition scope)
    {
        WriteIndentation();
        scope.Condition.Accept(this);

        IncreaseIndentation();
        foreach (EXECommand Command in scope.Commands)
        {
            Command.Accept(this);
        }
        DecreaseIndentation();

        if (scope.ElifScopes != null)
        {
            foreach (EXEScopeCondition Elif in scope.ElifScopes)
            {
                WriteIndentation();
                Elif.Condition.Accept(this);

                IncreaseIndentation();
                foreach (EXECommand Command in Elif.Commands)
                {
                    Command.Accept(this);
                }
                DecreaseIndentation();
            }
        }
            if (scope.ElseScope != null)
            {
                WriteIndentation();

                IncreaseIndentation();
                foreach (EXECommand Command in scope.ElseScope.Commands)
                {
                    Command.Accept(this);
                }
                DecreaseIndentation();
            }

            WriteIndentation();
            AddEOL();

    }

    public override void VisitExeScopeLoopWhile(EXEScopeLoopWhile scope)
    {
        WriteIndentation();
        scope.Condition.Accept(this);

        IncreaseIndentation();
        foreach (EXECommand Command in scope.Commands)
        {
            Command.Accept(this);
        }
        DecreaseIndentation();

        WriteIndentation();
        AddEOL();

    }

    public override void VisitExeASTNodeAccesChain(EXEASTNodeAccessChain node)
    {
        foreach (var element in node.GetElements()) {
            element.NodeValue.Accept(this);
        }
    }

    public override void VisitExeASTNodeComposite(EXEASTNodeComposite node)
    {
        if (node.Operands.Count == 1)
        {
            node.Operands.First().Accept(this);
        }
        else
        {
            foreach (var operand in node.Operands) {
                operand.Accept(this);
            }
        }
    }

    public override void VisitExeASTNodeLeaf(EXEASTNodeLeaf node)
    {
    }

    public override void VisitExeASTNodeMethodCall(EXEASTNodeMethodCall node)
    {
        commandString.Append(node.MethodName + "(");
        bool first = true;
        foreach (var arg in node.Arguments) {
            if (first)
            {
                first = false;
            }
            else
            {
                commandString.Append(", ");
            }
            arg.Accept(this);
        }
        commandString.Append(")");
    }

    public override void VisitExeValueArray(EXEValueArray value)
    {
        if (value.Elements != null)
        {
            foreach (var element in value.Elements) {
                element.Accept(this);
            }
        }
    }

    public override void VisitExeValueBool(EXEValueBool value)
    {
    }

    public override void VisitExeValueInt(EXEValueInt value)
    {
    }

    public override void VisitExeValueReal(EXEValueReal value)
    {
    }

    public override void VisitExeValueReference(EXEValueReference value)
    {
    }

    public override void VisitExeValueString(EXEValueString value)
    {
    }

    public override void VisitExeASTNodeIndexation(EXEASTNodeIndexation node)
    {
        node.List.Accept(this);
        node.Index.Accept(this);
    }
}

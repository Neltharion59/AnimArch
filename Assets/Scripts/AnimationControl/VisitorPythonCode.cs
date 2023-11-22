using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Microsoft.Msagl.Drawing;
using OALProgramControl;
using UnityEngine;

public class VisitorPythonCode : Visitor
{
    private readonly StringBuilder commandString;

    private int indentationLevel;

    private bool available;
    private static readonly LinkedList<VisitorPythonCode> visitors = new LinkedList<VisitorPythonCode>();

    public static VisitorPythonCode BorrowAVisitor() {
        foreach (VisitorPythonCode v in visitors) {
            if (v.isVisitorAvailable()) {
                return v.BorrowVisitor();
            }
        }
        VisitorPythonCode newVisitor = new VisitorPythonCode();
        visitors.AddLast(newVisitor);
        return newVisitor.BorrowVisitor();
    }

    private VisitorPythonCode()
    {
        commandString = new StringBuilder();
        available = true;
        ResetState();
    }

    private bool isVisitorAvailable() {
        return available;
    }

    private VisitorPythonCode BorrowVisitor() {
        if (!available) {throw new Exception("Borrowing a visitor that is not available!");}
        available = false;
        return this;
    }

    public string GetCommandStringAndResetStateNow() {
        string result = commandString.ToString();
        available = true;
        ResetState();
        return result;
    }

    private void ResetState() {
        indentationLevel = 0;
        commandString.Clear();
    }

    private void IncreaseIndentation() {
        indentationLevel++;
    }

    private void DecreaseIndentation() {
        indentationLevel--;
    }
    public void SetIndentation(int level) {
        indentationLevel = level;
    }

    private void WriteIndentation() {
        commandString.Append(string.Concat(Enumerable.Repeat("\t", indentationLevel)));
    }

    private void AddEOL() {
        commandString.Append("\n");
    }

    private void HandleBasicEXECommand(EXECommand command, Func<VisitorPythonCode, bool> addCommandSimpleString) {
        WriteIndentation();
        addCommandSimpleString(this);
        AddEOL();
    }

    public override void VisitExeCommand(EXECommand command)
    {
        if (!typeof(EXECommand).GetTypeInfo().IsAbstract)
        {
            throw new Exception("We expect EXECommand to be abstract.");
        }

        command.Accept(this);
    }

    public override void VisitExeCommandBreak(EXECommandBreak command)
    {   
        
        HandleBasicEXECommand(command, (visitor) => {
            visitor.commandString.Append("break");
            return false;
        });
        
    }

    public override void VisitExeCommandCall(EXECommandCall command)
    {
        
        HandleBasicEXECommand(command, (visitor) => {
            visitor.commandString.Append(command.MethodAccessChainS + ".");
            command.MethodCall.Accept(visitor);
            return false;
        });
        
    }

    public override void VisitExeCommandContinue(EXECommandContinue command)
    {
        
        HandleBasicEXECommand(command, (visitor) => {
            visitor.commandString.Append("continue");
            return false;
        });
        
    }

    public override void VisitExeCommandAddingToList(EXECommandAddingToList command)
    {
        
        HandleBasicEXECommand(command, (visitor) => {
            command.Array.Accept(visitor);
            visitor.commandString.Append(".append(");
            command.AddedElement.Accept(visitor);
            visitor.commandString.Append(")");
            
            return false;
        });
        
    }

    public override void VisitExeCommandAssignment(EXECommandAssignment command)
    {
        
        HandleBasicEXECommand(command, (visitor) => {
            command.AssignmentTarget.Accept(visitor);
            visitor.commandString.Append(" = ");
            command.AssignedExpression.Accept(visitor);
            return false;
        });
        
    }

    public override void VisitExeCommandCreateList(EXECommandCreateList command)
    {
        
        HandleBasicEXECommand(command, (visitor) => {
            command.AssignmentTarget.Accept(visitor);
            visitor.commandString.Append(" = [");
            bool first = true;
            foreach (var item in command.Items)
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    commandString.Append(", ");
                }
                item.Accept(this);
            }
            visitor.commandString.Append("]");
            return false;
        });
        
    }

    public override void VisitExeCommandMulti(EXECommandMulti command)
    {
        throw new Exception("Tried to convert EXECommandMulti to Python.");
    }

    public override void VisitExeCommandQueryCreate(EXECommandQueryCreate command)
    {
        
        HandleBasicEXECommand(command, (visitor) => {
            if (command.AssignmentTarget != null)
            {
                command.AssignmentTarget.Accept(visitor);
                visitor.commandString.Append(" = ");
            }

            visitor.commandString.Append(command.ClassName + "()");
            return false;
        });
        
    }

    public override void VisitExeCommandQueryDelete(EXECommandQueryDelete command)
    {
        
        HandleBasicEXECommand(command, (visitor) => {
            visitor.commandString.Append("del ");
            command.DeletedVariable.Accept(visitor);
            return false;
        });
        
    }

    public override void VisitExeCommandRead(EXECommandRead command)
    {
        
        HandleBasicEXECommand(command, (visitor) => {
            command.AssignmentTarget.Accept(visitor);
            visitor.commandString.Append(" = ");
            if (!EXETypes.StringTypeName.Equals(command.AssignmentType)) {
                if (command.AssignmentType.Equals("int")) {
                    visitor.commandString.Append("int(");
                }
                else if (command.AssignmentType.Equals("real")) {
                    visitor.commandString.Append("float(");
                }
                else if (command.AssignmentType.Equals("bool")) {
                    visitor.commandString.Append("boolean(");
                }
            }
            visitor.commandString.Append("input(");
            if (command.Prompt != null)
            {
                command.Prompt.Accept(visitor);
            }
            visitor.commandString.Append(")");
            if (!EXETypes.StringTypeName.Equals(command.AssignmentType)) {
                visitor.commandString.Append(")");
            }
            return false;
        });
        
    }

    public override void VisitExeCommandRemovingFromList(EXECommandRemovingFromList command)
    {
        HandleBasicEXECommand(command, (visitor) => {
            command.Array.Accept(visitor);
            visitor.commandString.Append(" = [x for x in ");
            command.Array.Accept(visitor);
            visitor.commandString.Append(" if x != ");
            command.Item.Accept(visitor);
            visitor.commandString.Append("]");
            
            return false;
        });
        
    }

    public override void VisitExeCommandReturn(EXECommandReturn command)
    {
        HandleBasicEXECommand(command, (visitor) => {
            visitor.commandString.Append("return");
            if (command.Expression != null)
            {
                visitor.commandString.Append(" ");
                command.Expression.Accept(visitor);
            }
            return false;
        });
        
    }

    public override void VisitExeCommandWrite(EXECommandWrite command)
    {
        
        HandleBasicEXECommand(command, (visitor) => {
            visitor.commandString.Append("print(");
            bool first = true;
            foreach (var arg in command.Arguments)
            {
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
            visitor.commandString.Append(")");
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
        foreach (EXECommand Command in scope.Commands)
        {
            Command.Accept(this);
        }
    }

    public override void VisitExeScopeForEach(EXEScopeForEach scope)
    { 
        WriteIndentation();
        commandString.Append("for " + scope.IteratorName + " in ");
        scope.Iterable.Accept(this);
        commandString.Append(":");
        AddEOL();

        IncreaseIndentation();
        foreach (EXECommand Command in scope.Commands)
        {
            Command.Accept(this);
        }
        DecreaseIndentation();
    }

    public override void VisitExeScopeParallel(EXEScopeParallel scope)
    {
        throw new Exception("Tried to visit EXEScopeParallel.");
        /*
        WriteIndentation();
        commandString.Append("par\n");


        if (scope.Threads != null)
        {
            foreach (EXEScope Thread in scope.Threads)
            {
                
                WriteIndentation();
                commandString.Append("\tthread\n");


                IncreaseIndentation();
                IncreaseIndentation();
                foreach (EXECommand Command in Thread.Commands)
                {
                    Command.Accept(this);
                }
                DecreaseIndentation();
                DecreaseIndentation();

                WriteIndentation();
                commandString.Append("\tend thread");
                AddEOL();
            }
        }

        WriteIndentation();
        commandString.Append("end par");
        AddEOL();
        */
    }

    public override void VisitExeScopeCondition(EXEScopeCondition scope)
    {
        WriteIndentation();
        commandString.Append("if ");
        scope.Condition.Accept(this);
        commandString.Append(":");
        AddEOL();

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
                commandString.Append("elif ");
                Elif.Condition.Accept(this);
                commandString.Append(":");
                AddEOL();

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
                commandString.Append("else:");
                AddEOL();

                IncreaseIndentation();
                foreach (EXECommand Command in scope.ElseScope.Commands)
                {
                    Command.Accept(this);
                }
                DecreaseIndentation();
            }
    }

    public override void VisitExeScopeLoopWhile(EXEScopeLoopWhile scope)
    {
        WriteIndentation();
        commandString.Append("while ");
        scope.Condition.Accept(this);
        commandString.Append(":");
        AddEOL();

        IncreaseIndentation();
        foreach (EXECommand Command in scope.Commands)
        {
            Command.Accept(this);
        }
        DecreaseIndentation();
    }

    public override void VisitExeASTNodeAccesChain(EXEASTNodeAccessChain node)
    {
        bool first = true;
        foreach (var element in node.GetElements()) {
            if (first)
            {
                first = false;
            }
            else
            {
                commandString.Append(".");
            }
            element.NodeValue.Accept(this);
        }
    }

    public override void VisitExeASTNodeComposite(EXEASTNodeComposite node)
    {
        if (node.Operands.Count == 1)
        {
            if (node.Operation.ToLower().Equals("not") || node.Operation.ToLower().Equals("empty")) {
                commandString.Append("not ");
                node.Operands.First().Accept(this);
            }
            else if (node.Operation.ToLower().Equals("cardinality")) {
                commandString.Append("cardinality(");
                node.Operands.First().Accept(this);
                commandString.Append(")");
            }
            if (node.Operation.ToLower().Equals("not_empty")) {
                node.Operands.First().Accept(this);
            }
            else {
                commandString.Append(node.Operation + " ");
                node.Operands.First().Accept(this);
            }
        }
        else
        {
            bool first = true;
            foreach (var operand in node.Operands) {
                if (first)
                {
                    first = false;
                }
                else
                {
                    commandString.AppendFormat(" {0} ", node.Operation.ToLower());
                }
                operand.Accept(this);
            }
        }
    }

    public override void VisitExeASTNodeLeaf(EXEASTNodeLeaf node)
    {
        if (node.Value.Equals("FALSE")) {
            commandString.Append("False");
        }
        else if (node.Value.Equals("TRUE")) {
            commandString.Append("True");
        }
        else if (node.Value.Equals("UNDEFINED")) {
            commandString.Append("None");
        }
        else {
            commandString.Append(node.Value);
        }
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
        if (value.Elements == null)
        {
            commandString.Append("[]");
        }
        else
        {
            commandString.Append("[");
            bool first = true;
            foreach (var element in value.Elements) {
                if (first)
                {
                    first = false;
                }
                else
                {
                    commandString.Append(", ");
                }
                element.Accept(this);
            }
            commandString.Append("]");
        }
    }

    public override void VisitExeValueBool(EXEValueBool value)
    {
        commandString.Append(value.Value ? "True" : "False");
    }

    public override void VisitExeValueInt(EXEValueInt value)
    {
        
        commandString.Append(value.Value.ToString());
        
    }

    public override void VisitExeValueReal(EXEValueReal value)
    {
        
        commandString.Append(value.Value.ToString());
        
    }

    public override void VisitExeValueReference(EXEValueReference value)
    {
        throw new Exception("Tried to visit EXEValueReference.");
    }

    public override void VisitExeValueString(EXEValueString value)
    {
        
        commandString.Append("\"" + value.Value + "\"");
        
    }
}


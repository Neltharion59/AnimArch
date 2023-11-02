using System.Collections;
using System.Collections.Generic;
using OALProgramControl;
using UnityEngine;

public class VisitorCommandToString : Visitor
{
    private string commandString;

    public override void VisitExeCommand(EXECommand c)
    {
        commandString += "Command";
    }

    public override void VisitExeCommandBreak(EXECommandBreak c)
    {
        commandString += "break";
    }

    public override void VisitExeCommandCall(EXECommandCall c)
    {
        commandString += c.GetMethodAccessChainS() + "." + c.MethodCall.ToCode();
    }

    public override void VisitExeCommandContinue(EXECommandContinue c)
    {
        commandString += "continue";
    }

    public string GetCommandString() {
        return commandString;
    }

    public override void VisitExeCommandAddingToList(EXECommandAddingToList c)
    {
        //return "add " + this.AddedElement.ToCode() + " to " + this.Array.ToCode();
        throw new System.NotImplementedException();
    }

    public override void VisitExeCommandAssignment(EXECommandAssignment c)
    {
        //return this.AssignmentTarget.ToCode() + " = " + this.AssignedExpression.ToCode();
        throw new System.NotImplementedException();
    }

    public override void VisitExeCommandCreateList(EXECommandCreateList c)
    {
        //return "create list " + this.AssignmentTarget.ToCode()
          //      + " of " + this.ArrayType + " { " + string.Join(", ", this.Items.Select(item => item.ToCode())) + " }";
        throw new System.NotImplementedException();
    }

    public override void VisitExeCommandMulti(EXECommandMulti c)
    {
        //nie je implemetacia v command multi
        throw new System.NotImplementedException();
    }

    public override void VisitExeCommandQueryCreate(EXECommandQueryCreate c)
    {
        //return "create object instance " + (this.AssignmentTarget?.ToCode() ?? string.Empty) + " of " + this.ClassName;
        throw new System.NotImplementedException();
    }

    public override void VisitExeCommandQueryDelete(EXECommandQueryDelete c)
    {
        //return "delete object instance " + this.DeletedVariable.ToCode();
        throw new System.NotImplementedException();
    }

    public override void VisitExeCommandRead(EXECommandRead c)
    {
        //return
        //        this.AssignmentTarget.ToCode()
        //            + " = "
        //            + this.AssignmentType
        //           + (this.Prompt?.ToCode() ?? string.Empty)
         //           + (EXETypes.StringTypeName.Equals(this.AssignmentType) ? ")" : "))");
        throw new System.NotImplementedException();
    }

    public override void VisitExeCommandRemovingFromList(EXECommandRemovingFromList c)
    {
        //return "remove " + this.Item.ToCode() + " from " + this.Array.ToCode();
        throw new System.NotImplementedException();
    }

    public override void VisitExeCommandReturn(EXECommandReturn c)
    {
        //return this.Expression == null ? "return" : ("return " + this.Expression.ToCode());
        throw new System.NotImplementedException();
    }

    public override void VisitExeCommandWrite(EXECommandWrite c)
    {
        //String Result = "write(" + string.Join(", ", this.Arguments.Select(argument => argument.ToCode())) + ")";
        throw new System.NotImplementedException();
    }
}

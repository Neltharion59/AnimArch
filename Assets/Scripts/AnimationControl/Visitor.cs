using System.Collections;
using System.Collections.Generic;
using OALProgramControl;
using UnityEngine;

public interface IVisitable {
    void Accept(Visitor v);
}

public abstract class Visitor
{
    public abstract void VisitExeCommandCall(EXECommandCall command);
    public abstract void VisitExeCommandBreak(EXECommandBreak command);
    public abstract void VisitExeCommandContinue(EXECommandContinue command);
    public abstract void VisitExeCommandAddingToList(EXECommandAddingToList command);
    public abstract void VisitExeCommandAssignment(EXECommandAssignment command);
    public abstract void VisitExeCommandCreateList(EXECommandCreateList command);
    public abstract void VisitExeCommandMulti(EXECommandMulti command);
    public abstract void VisitExeCommandQueryCreate(EXECommandQueryCreate command);
    public abstract void VisitExeCommandQueryDelete(EXECommandQueryDelete command);
    public abstract void VisitExeCommandRead(EXECommandRead command);
    public abstract void VisitExeCommandRemovingFromList(EXECommandRemovingFromList command);
    public abstract void VisitExeCommandReturn(EXECommandReturn command);
    public abstract void VisitExeCommandWrite(EXECommandWrite command);
    public abstract void VisitExeCommand(EXECommand command);

    public abstract void VisitExeScope(EXEScope scope);
    public abstract void VisitExeScopeLoop(EXEScopeLoop scope);
    public abstract void VisitExeScopeMethod(EXEScopeMethod scope);
    public abstract void VisitExeScopeForEach(EXEScopeForEach scope);
    public abstract void VisitExeScopeParallel(EXEScopeParallel scope);
    public abstract void VisitExeScopeCondition(EXEScopeCondition scope);
    public abstract void VisitExeScopeLoopWhile(EXEScopeLoopWhile scope);
}

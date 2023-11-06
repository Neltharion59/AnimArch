using System.Collections;
using System.Collections.Generic;
using OALProgramControl;
using UnityEngine;

public abstract class Visitor
{

    public abstract void VisitExeCommandCall(EXECommandCall c);
    public abstract void VisitExeCommandBreak(EXECommandBreak c);
    public abstract void VisitExeCommandContinue(EXECommandContinue c);
    public abstract void VisitExeCommandAddingToList(EXECommandAddingToList c);
    public abstract void VisitExeCommandAssignment(EXECommandAssignment c);
    public abstract void VisitExeCommandCreateList(EXECommandCreateList c);
    public abstract void VisitExeCommandMulti(EXECommandMulti c);
    public abstract void VisitExeCommandQueryCreate(EXECommandQueryCreate c);
    public abstract void VisitExeCommandQueryDelete(EXECommandQueryDelete c);
    public abstract void VisitExeCommandRead(EXECommandRead c);
    public abstract void VisitExeCommandRemovingFromList(EXECommandRemovingFromList c);
    public abstract void VisitExeCommandReturn(EXECommandReturn c);
    public abstract void VisitExeCommandWrite(EXECommandWrite c);
    public abstract void VisitExeCommand(EXECommand c);
}

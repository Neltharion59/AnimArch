using OALProgramControl;
using System;
using UnityEditor;
using UnityEngine;

namespace Assets.UnitTests.AnimationControl
{
    public abstract class StandardTest
    {
        private const int LIMIT = 200;

        protected EXEExecutionResult PerformExecution(OALProgram programInstance)
        {
            EXEScopeMethod executedMethod = programInstance.SuperScope as EXEScopeMethod;

            // Object owning the executed method
            CDClassInstance owningObject = executedMethod.MethodDefinition.OwningClass.CreateClassInstance();
            executedMethod.OwningObject = owningObject;
            executedMethod.AddVariable(new EXEVariable(EXETypes.SelfReferenceName, new EXEValueReference(owningObject)));

            EXEExecutionResult _executionResult = EXEExecutionResult.Success();

            int i = 0;
            while (_executionResult.IsSuccess && programInstance.CommandStack.HasNext())
            {
                EXECommand currentCommand = programInstance.CommandStack.Next();
              
                VisitorCommandToString visitor = VisitorCommandToString.BorrowAVisitor();
                currentCommand.Accept(visitor);
                Debug.Log(i.ToString() + visitor.GetCommandStringAndResetStateNow());
              
                _executionResult = currentCommand.PerformExecution(programInstance);

                if (!_executionResult.IsSuccess)
                {
                    break;
                }

                Debug.Log(_executionResult.ToString());
                i++;

                if (i > LIMIT)
                {
                    _executionResult = EXEExecutionResult.Error("Command execution limit crossed.", "XEC2040");
                    break;
                };
            }

            return _executionResult;
        }
    }
}
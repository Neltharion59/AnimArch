using OALProgramControl;
using System;
using UnityEditor;
using UnityEngine;

namespace Assets.UnitTests.AnimationControl
{
    public abstract class StandardTest
    {
        private const int LIMIT = 10000;

        protected EXEExecutionResult PerformExecution(OALProgram programInstance)
        {
            EXEScopeMethod executedMethod = programInstance.SuperScope as EXEScopeMethod;

            // Object owning the executed method
            executedMethod.OwningObject = executedMethod.MethodDefinition.OwningClass.CreateClassInstance();

            EXEExecutionResult _executionResult = EXEExecutionResult.Success();

            int i = 0;
            while (_executionResult.IsSuccess && programInstance.CommandStack.HasNext())
            {
                EXECommand currentCommand = programInstance.CommandStack.Next();
                Debug.Log(i.ToString() + currentCommand.ToCode());
                _executionResult = currentCommand.PerformExecution(programInstance);

                if (!_executionResult.IsSuccess)
                {
                    break;
                }

                Debug.Log(i.ToString() + _executionResult.ToString());
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
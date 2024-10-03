﻿using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace OALProgramControl
{
    public abstract class EXECommand
    {
        public bool IsActive { get; set; } = false;
        public static EXEScopeNull NullScope = EXEScopeNull.GetInstance();
        public EXEScopeBase SuperScope { get; set; } = NullScope; 
        public EXEExecutionStack CommandStack { get; set; } = null;
        
        public virtual IEnumerable<EXEScopeBase> ScopesToTop()
        {
            EXEScopeBase currentScope = this.SuperScope;
            yield return currentScope;

            foreach (EXEScopeBase scope in currentScope.ScopesToTop())
            {
                yield return scope;
            }
        }
        public EXEScopeMethod GetCurrentMethodScope()
        {
            return ScopesToTop()
                .FirstOrDefault(scope => scope is EXEScopeMethod)
                as EXEScopeMethod;
        }
        public EXEScopeLoop GetCurrentLoopScope()
        {
            return ScopesToTop()
                .FirstOrDefault(scope => scope is EXEScopeLoop)
                as EXEScopeLoop;
        }
        public EXEExecutionResult PerformExecution(OALProgram OALProgram)
        {
            EXEExecutionResult Result = Execute(OALProgram);

            return Result;
        }
        protected abstract EXEExecutionResult Execute(OALProgram OALProgram);
        public virtual bool SubmitReturn(EXEValueBase returnedValue, OALProgram programInstance)
        {
            return false;
        }
        protected EXEExecutionResult Success()
        {
            return EXEExecutionResult.Success(this);
        }
        protected EXEExecutionResult Error(string errorCode, string errorMessage)
        {
            return EXEExecutionResult.Error(errorCode, errorMessage, this);
        }
        public EXEScopeBase GetSuperScope()
        {
            return this.SuperScope;
        }
        public virtual void SetSuperScope(EXEScopeBase SuperScope)
        {
            if (this == SuperScope)
            {
                return;
            }

            this.SuperScope = SuperScope;

            if (this.CommandStack == null)
            {
                this.CommandStack = SuperScope?.CommandStack;
            }
        }
        public EXEScopeBase GetTopLevelScope()
        {
            EXEScopeBase CurrentScope = this.SuperScope;

            if (CurrentScope is EXEScopeNull)
            {
                return this as EXEScopeBase;
            }

            return CurrentScope.GetTopLevelScope();
        }
        public virtual Boolean IsComposite()
        {
            return false;
        }
        public abstract EXECommand CreateClone();
        public virtual void Accept(Visitor v) {
            v.VisitExeCommand(this);
        }

        public virtual void ToggleActiveRecursiveBottomUp(bool active)
        {
            this.IsActive = active;
            this.SuperScope.ToggleActiveRecursiveBottomUp(active);
        }
        /**Use this when evaluating AST nodes and Execute might need to be called again.**/
        protected bool HandleRepeatableASTEvaluation(EXEExecutionResult executionResult)
        {
            if (!executionResult.IsDone)
            {
                // It's a stack-like structure, so we enqueue the current command first, then the pending command.
                this.CommandStack.Enqueue(this);
                executionResult.PendingCommand.SetSuperScope(this.SuperScope);
                executionResult.PendingCommand.CommandStack = this.CommandStack;
                this.CommandStack.Enqueue(executionResult.PendingCommand);
                return false;
            }

            if (!executionResult.IsSuccess)
            {
                executionResult.OwningCommand = this;
                return false;
            }

            return true;
        }
        /**Use this when performing an action and Execute cannot be called again.**/
        protected bool HandleSingleShotASTEvaluation(EXEExecutionResult executionResult)
        {
            executionResult.OwningCommand = this;

            if (!executionResult.IsDone)
            {
                executionResult.IsSuccess = false;
                executionResult.ErrorMessage = "Unexpected request to re-evaluate during command execution";
                executionResult.ErrorCode = "XEC2024";
                return false;
            }

            return executionResult.IsSuccess;
        }
    }
}

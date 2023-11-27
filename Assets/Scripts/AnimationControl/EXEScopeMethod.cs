using System;
using System.Linq;
using System.Collections.Generic;

namespace OALProgramControl
{
    public class EXEScopeMethod : EXEScope
    {
        public CDMethod MethodDefinition;
        public EXEASTNodeMethodCall MethodCallOrigin;
        public CDClassInstance OwningObject;

        public EXEScopeMethod() : this(null)
        {
        }
        public EXEScopeMethod(CDMethod methodDefinition) : base()
        {
            this.MethodDefinition = methodDefinition;
            this.MethodCallOrigin = null;
            this.OwningObject = null;
        }
        public override bool CollectReturn(EXEValueBase returnedValue, OALProgram programInstance)
        {
            // Check compatibility of types
            if (!EXETypes.CanBeAssignedTo(returnedValue, this.MethodDefinition.ReturnType, programInstance.ExecutionSpace))
            {
                return false;
            }

            // This actually performs the assignment
            this.MethodCallOrigin.EvaluationResult = Success();
            this.MethodCallOrigin.EvaluationResult.ReturnedOutput = returnedValue;
            this.MethodCallOrigin.ReturnCollected = true;
            return true;
        }
        protected override EXEExecutionResult Execute(OALProgram OALProgram)
        {
            AddCommandsToStack(this.Commands);
            return Success();
        }

        public override void Accept(Visitor v)
        {
            v.VisitExeScopeMethod(this);
        }

        protected override EXEScope CreateDuplicateScope()
        {
            return new EXEScopeMethod(this.MethodDefinition);
        }
        public override EXEExecutionResult AddVariable(EXEVariable variable)
        {
            if (this.MethodDefinition.Parameters.Select(parameter => parameter.Name).Contains(variable.Name))
            {
                return Error(string.Format("Cannot create variable \"{0}\" as it is already name of a parameter of the current method.", variable.Name), "XEC2030");
            }

            if (this.MethodDefinition.OwningClass.Attributes.Select(attribute => attribute.Name).Contains(variable.Name))
            {
                return Error(string.Format("Cannot create variable \"{0}\" as it is already name of an attribute of the current class.", variable.Name), "XEC2031");
            }

            if (this.MethodDefinition.OwningClass.Methods.Select(method => method.Name).Contains(variable.Name))
            {
                return Error(string.Format("Cannot create variable \"{0}\" as it is already name of a method of the current class.", variable.Name), "XEC2032");
            }

            return base.AddVariable(variable);
        }
        public EXEExecutionResult AddParameterVariable(EXEVariable variable)
        {
            if (this.MethodDefinition.OwningClass.Attributes.Select(attribute => attribute.Name).Contains(variable.Name))
            {
                return Error(string.Format("Cannot create variable \"{0}\" as it is already name of an attribute of the current class.", variable.Name), "XEC2031");
            }

            if (this.MethodDefinition.OwningClass.Methods.Select(method => method.Name).Contains(variable.Name))
            {
                return Error(string.Format("Cannot create variable \"{0}\" as it is already name of a method of the current class.", variable.Name), "XEC2032");
            }

            return base.AddVariable(variable);
        }
        public EXEExecutionResult InitializeVariables(OALProgram programInstance)
        {
            return InitializeVariables
            (
                this.MethodDefinition
                    .Parameters
                    .Select(parameter => new EXEVariable(parameter.Name, EXETypes.DefaultValue(parameter.Type, programInstance.ExecutionSpace)))
            );
        }
        public EXEExecutionResult InitializeVariables(IEnumerable<EXEVariable> argumentVariables)
        {
            EXEExecutionResult variableCreationSuccess
                = AddVariable(new EXEVariable(EXETypes.SelfReferenceName, new EXEValueReference(this.OwningObject)));

            if (!HandleSingleShotASTEvaluation(variableCreationSuccess))
            {
                return variableCreationSuccess;
            }

            foreach (EXEVariable variable in argumentVariables)
            {
                variableCreationSuccess = AddParameterVariable(variable);

                if (!HandleSingleShotASTEvaluation(variableCreationSuccess))
                {
                    return variableCreationSuccess;
                }
            }

            return Success();
        }
    }
}
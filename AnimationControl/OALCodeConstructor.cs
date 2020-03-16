using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{
    public class OALCodeConstructor
    {
        private static string ObjectDeclarationComment = "//Object Declarations\n";
        private static string RelationshipDeclarationComment = "//Relationship Declarations\n";
        private static string AnimationExecutionPartComment = "//AnimationExecutionPart\n";
        private static string MethodCommentPrefix = "//Execution of method ";
        private static string MethodCommentInfix = ".";
        private static string MethodCommentSufix = "()\n";

        public String ConstructFullCode(String ObjectDeclaration, String RelationshipDeclaration, String AnimationExecution, List<(String, String)> MockedAttributes, List<String> ClassNames, List<String> MethodNames, List<String> MethodExecution)
        {
            StringBuilder FullCodeBuilder = new StringBuilder();
            
            FullCodeBuilder.Append(ObjectDeclarationComment);
            FullCodeBuilder.Append(ObjectDeclaration);
            FullCodeBuilder.Append(RelationshipDeclarationComment);
            FullCodeBuilder.Append(RelationshipDeclaration);

            foreach ((String, String) InstanceAttributeTupple in MockedAttributes)
            {
                FullCodeBuilder.Append(InstanceAttributeTupple.Item1);
                FullCodeBuilder.Append(".");
                FullCodeBuilder.Append(InstanceAttributeTupple.Item2);
                FullCodeBuilder.Append("=");
                FullCodeBuilder.Append("0");
                FullCodeBuilder.Append(";\n");
            }

            FullCodeBuilder.Append(AnimationExecutionPartComment);
            FullCodeBuilder.Append(AnimationExecution);

            for (int i = 0; i < ClassNames.Count; i++)
            {
                FullCodeBuilder.Append(MethodCommentPrefix);
                FullCodeBuilder.Append(ClassNames[i]);
                FullCodeBuilder.Append(MethodCommentInfix);
                FullCodeBuilder.Append(MethodNames[i]);
                FullCodeBuilder.Append(MethodCommentSufix);
                FullCodeBuilder.Append(MethodExecution[i]);
            }

            return FullCodeBuilder.ToString();
        }

        public String ConstructMainCode(String ObjectDeclaration, String RelationshipDeclaration, String AnimationExecution, List<(String, String)> MockedAttributes)
        {
            StringBuilder FullCodeBuilder = new StringBuilder();

            FullCodeBuilder.Append(ObjectDeclarationComment);
            FullCodeBuilder.Append(ObjectDeclaration);
            FullCodeBuilder.Append(RelationshipDeclarationComment);
            FullCodeBuilder.Append(RelationshipDeclaration);

            foreach ((String, String) InstanceAttributeTupple in MockedAttributes)
            {
                FullCodeBuilder.Append(InstanceAttributeTupple.Item1);
                FullCodeBuilder.Append(".");
                FullCodeBuilder.Append(InstanceAttributeTupple.Item2);
                FullCodeBuilder.Append("=");
                FullCodeBuilder.Append("0");
                FullCodeBuilder.Append(";\n");
            }

            FullCodeBuilder.Append(AnimationExecutionPartComment);
            FullCodeBuilder.Append(AnimationExecution);

            return FullCodeBuilder.ToString();
        }
        private String ConstructObjectDeclarationCode(String InstanceName, String ClassName)
        {
            String ObjectDeclarationCode = "create object instance " + InstanceName + " of " + ClassName + ";\n";
            return ObjectDeclarationCode;
        }
        public String ConstructDeclarationPartCode(String PreceedingCode, String InstanceName, String ClassName)
        {
            String DeclarationPartCode = PreceedingCode + ConstructObjectDeclarationCode(InstanceName, ClassName);
            return DeclarationPartCode;
        }

        private String ConstructRelationShipDeclarationCode(String FirstInstanceName, String SecondInstanceName, String RelationshipName)
        {
            String RelationshipDeclarationCode = "relate " + FirstInstanceName + " to " + SecondInstanceName + " across " + RelationshipName + ";\n";
            return RelationshipDeclarationCode;
        }
        public String ConstructRelationShipDeclarationPartCode(String PreceedingCode, String FirstInstanceName, String SecondInstanceName, String RelationshipName)
        {
            String RelationshipDeclarationCodePart = PreceedingCode + ConstructRelationShipDeclarationCode(FirstInstanceName, SecondInstanceName, RelationshipName);
            return RelationshipDeclarationCodePart;
        }

        public String ConstructExecutionPartCode(String PreceedingCode, String InstanceName, String MethodName)
        {
            String ExecutionPartCode = PreceedingCode + ConstructMethodCallCode(InstanceName, MethodName);
            return ExecutionPartCode;
        }
        private String ConstructMethodCallCode(String InstanceName, String MethodName)
        {
            String CallCode = InstanceName + "." + MethodName + "();\n";
            return CallCode;
        }

        public String ConstructMethodDefinitionCode(String PreceedingCode, int MethodCallOrder, String InstanceName, String MethodName, String InstanceClass, String RelationshipName)
        {
            String MethodCounterName = "call_count_" + MethodName;
            String MethodCounterAccess = "self." + MethodCounterName;

            String NewMethodDefinition = PreceedingCode;
            String FirstLine = "if (" + MethodCounterAccess + " == " + MethodCallOrder.ToString() + ")\n";

            String SecondLine = "\tselect any " + InstanceName + " related by self->" + InstanceClass + "[" + RelationshipName + "];\n";
            String ThirdLine = "\t" + ConstructMethodCallCode(InstanceName, MethodName);
            String FourthLine = "\t" + MethodCounterAccess + " = " + MethodCounterAccess + " + 1;\n";
            String FifthLine = "\treturn;\n";
            String SixthLine = "end if;\n";

            NewMethodDefinition += FirstLine + SecondLine + ThirdLine + FourthLine + FifthLine + SixthLine;
            return NewMethodDefinition;
        }
    }
}

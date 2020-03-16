using System;

namespace AnimationControl
{
    public class EXEASTNodeLeaf : EXEASTNode
    {
        public Boolean IsVariableReference { get; set; }
        public Boolean IsClassReference { get; set; }
        public Boolean IsRelationshipReference { get; set; }
        public Boolean UnknownType { get; set; }
        public String Value { get; set; }

        public EXEASTNodeLeaf(String Value)
        {
            this.Value = Value;

            this.IsVariableReference = false;
            this.IsClassReference = false;
            this.IsRelationshipReference = false;

            this.UnknownType = true;
        }
        public EXEASTNodeLeaf(String Value, Boolean IsVariableReference, Boolean IsClassReference, Boolean IsRelationshipReference)
        {
            this.Value = Value;

            this.IsVariableReference = IsVariableReference;
            this.IsClassReference = IsClassReference;
            this.IsRelationshipReference = IsRelationshipReference;

            this.UnknownType = false;
        }

        public String GetNodeValue()
        {
            return this.Value;
        }
        public String Evaluate(EXEScope Scope)
        {
            throw new NotImplementedException();
        }

        //https://stackoverflow.com/questions/1649027/how-do-i-print-out-a-tree-structure
        public void PrintPretty(string indent, bool last)
        {
            Console.Write(indent);
            if (last)
            {
                Console.Write("\\-");
                indent += "  ";
            }
            else
            {
                Console.Write("|-");
                indent += "| ";
            }
            Console.WriteLine(this.Value);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{
    public class EXEASTNodeComposite : EXEASTNode
    {
        public String Operation { get; set; }
        public List<EXEASTNode> Operands { get; }

        public EXEASTNodeComposite(String Operation)
        {
            this.Operation = Operation;
            this.Operands = new List<EXEASTNode>();
        }

        public void AddOperand(EXEASTNode Operand)
        {
            if (Operand == null)
            {
                return;
            }

            this.Operands.Add(Operand);
        }

        public String GetNodeValue()
        {
            return this.Operation;
        }
        public String Evaluate(EXEScope Scope)
        {
            throw new NotImplementedException();
        }

        public void PrintPretty(string indent, bool last)
        {
            Console.Write(indent);
            if (last)
            {
                Console.Write("\\+");
                indent += "  ";
            }
            else
            {
                Console.Write("|+");
                indent += "| ";
            }
            Console.WriteLine(this.Operation);

            for (int i = 0; i < this.Operands.Count; i++)
                this.Operands[i].PrintPretty(indent, i == this.Operands.Count - 1);
        }
    }
}

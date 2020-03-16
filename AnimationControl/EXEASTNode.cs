using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{
    public interface EXEASTNode
    {
        String GetNodeValue();
        String Evaluate(EXEScope Scope);
        void PrintPretty(string indent, bool last);
    }
}

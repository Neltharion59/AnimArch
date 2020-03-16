using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{
    public interface EXECommandInterface
    {
        String PrintSelf(Boolean IsTopLevel);

        void PrintAST();
        void Parse(EXEScope SuperScope);
        String GetCode();
        // Pass "Scope == null" if executing EXEScope
        Boolean Execute(CDClassPool ExecutionSpace, CDRelationshipPool RelationshipSpace, EXEScope Scope);
    }
}

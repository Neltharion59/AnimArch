using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{
    public class EXECommand : EXECommandInterface
    {
        public String OALCode { get; set; }
        public String CommandType { get; set; }
        public EXEASTNode AST { get; set; }

        public EXECommand(String OALCode)
        {
            this.OALCode = OALCode;
        }

        public String GetCode()
        {
            return this.OALCode;
        }
        public String PrintSelf(Boolean IsTopLevel)
        {
            return this.OALCode;
        }
        public void PrintAST()
        {
            this.AST.PrintPretty("", false);
        }
        public Boolean Execute(CDClassPool ExecutionSpace, CDRelationshipPool RelationshipSpace, EXEScope Scope)
        {
            throw new NotImplementedException();
        }

        public void Parse(EXEScope SuperScope)
        {
            Console.WriteLine("EXECommand.Parse");

            OALCommandParser Parser = new OALCommandParser();
            this.AST = Parser.ConstructAST(this.OALCode);
        }
    }
}

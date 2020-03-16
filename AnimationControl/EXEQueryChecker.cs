using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{

    public class EXEQueryChecker
    {
        public static List<String> QueryTypeStartKeyWord = new List<String>(new String[] { "create", "relate", "select", "unrelate"});
        
        // Checks whether given string is an OAL query. To be a query, its first word must be one of those in the list above.
        // This does not check whether the query is valid, so the rest may be malformed.
        public Boolean IsQuery(String Command)
        {
            Boolean Result = false;

            if (Command == null)
            {
                return Result;
            }

            String[] Tokens = EXEParseUtil.SqueezeWhiteSpace(Command).Split(' ');
            if (QueryTypeStartKeyWord.Contains(Tokens[0]))
            {
                Result = true;
            }
            return Result;
        }
        public EXEASTNodeComposite ConstructQueryAST(String Query)
        {
            Console.WriteLine("QueryChecker: " + Query);

            EXEASTNodeComposite AST = null;
            if (!IsQuery(Query))
            {
                return AST;
            }

            String[] Tokens = EXEParseUtil.SqueezeWhiteSpace(Query).Split(' ');

            switch (Tokens[0])
            {
                case "create":
                    AST = ConstructASTCreate(Query);
                    break;
                case "relate":
                    AST = ConstructASTRelate(Query);
                    break;
                case "select":
                    AST = ConstructASTSelectAnyRelated(Query);
                    break;
                case "unrelate":
                    //TODO
                    break;
                default:
                    break;
            }
            
            return AST;
        }
        public EXEASTNodeComposite ConstructASTCreate(String Query)
        {
            EXEASTNodeComposite AST = null;

            String[] Tokens = EXEParseUtil.SqueezeWhiteSpace(Query).Split(' ');

            if (Tokens.Length != 6)
            {
                return AST;
            }

            if (!"create".Equals(Tokens[0]))
            {
                return AST;
            }

            if (!"object".Equals(Tokens[1]))
            {
                return AST;
            }

            if (!"instance".Equals(Tokens[2]))
            {
                return AST;
            }

            if (!OALCommandParser.IsValidName(Tokens[3]))
            {
                return AST;
            }

            if (!"of".Equals(Tokens[4]))
            {
                return AST;
            }

            if (!OALCommandParser.IsValidName(Tokens[5]))
            {
                return AST;
            }

            AST = new EXEASTNodeComposite("create");
            AST.AddOperand(new EXEASTNodeLeaf(Tokens[3], true, false, false));
            AST.AddOperand(new EXEASTNodeLeaf(Tokens[5], false, true, false));

            return AST;
        }
        public EXEASTNodeComposite ConstructASTRelate(String Query)
        {
            
            EXEASTNodeComposite AST = null;

            String[] Tokens = EXEParseUtil.SqueezeWhiteSpace(Query).Split(' ');

            if (Tokens.Length != 6)
            {
                return AST;
            }

            if (!"relate".Equals(Tokens[0]))
            {
                return AST;
            }

            if (!OALCommandParser.IsValidName(Tokens[1]))
            {
                return AST;
            }

            if (!"to".Equals(Tokens[2]))
            {
                return AST;
            }

            if (!OALCommandParser.IsValidName(Tokens[3]))
            {
                return AST;
            }

            if (!"across".Equals(Tokens[4]))
            {
                return AST;
            }

            if (!OALCommandParser.IsValidRelationshipName(Tokens[5]))
            {
                return AST;
            }

            AST = new EXEASTNodeComposite("relate");
            AST.AddOperand(new EXEASTNodeLeaf(Tokens[1], true, false, false));
            AST.AddOperand(new EXEASTNodeLeaf(Tokens[3], true, false, false));
            AST.AddOperand(new EXEASTNodeLeaf(Tokens[5], false, false, true));

            return AST;
        }
        public EXEASTNodeComposite ConstructASTSelectAnyRelated(String Query)
        {
            Console.WriteLine("related query parsing");

            EXEASTNodeComposite AST = null;

            String[] Tokens = EXEParseUtil.SqueezeWhiteSpace(Query).Split(' ');

            int i = 0;
            foreach (String Token in Tokens)
            {
                Console.WriteLine(i++ + ": " + Token + "EOL");
            }

            if (Tokens.Length != 6)
            {
                return AST;
            }

            if (!"select".Equals(Tokens[0]))
            {
                return AST;
            }

            if (!"any".Equals(Tokens[1]))
            {
                return AST;
            }

            if (!OALCommandParser.IsValidName(Tokens[2]))
            {
                return AST;
            }

            if (!"related".Equals(Tokens[3]))
            {
                return AST;
            }

            if (!"by".Equals(Tokens[4]))
            {
                return AST;
            }

            Console.WriteLine("passed tokens");

            String RelationshipReference = Tokens[5];
            int IndexOfArrow = RelationshipReference.IndexOf("->");
            int IndexOfOpeningBracket = RelationshipReference.IndexOf("[");
            int IndexOfClosingBracket = RelationshipReference.IndexOf("]");

            Console.WriteLine( IndexOfArrow + ", " + IndexOfOpeningBracket);

            String ReferingInstanceName = RelationshipReference.Substring(0, IndexOfArrow);
            String ReferedClassName = RelationshipReference.Substring(IndexOfArrow + 2, IndexOfOpeningBracket - IndexOfArrow - 2);
            String RelationshipName = RelationshipReference.Substring(IndexOfOpeningBracket + 1, IndexOfClosingBracket - IndexOfOpeningBracket - 1);

            Console.WriteLine("retrieved tricky parts");

            AST = new EXEASTNodeComposite("select any related by");
            AST.AddOperand(new EXEASTNodeLeaf(Tokens[2], true, false, false));
            AST.AddOperand(new EXEASTNodeLeaf(ReferingInstanceName, true, false, false));
            AST.AddOperand(new EXEASTNodeLeaf(ReferedClassName, false, true, false));
            AST.AddOperand(new EXEASTNodeLeaf(RelationshipName, false, false, true));

            Console.WriteLine("created AST");

            return AST;
        }
    }
}

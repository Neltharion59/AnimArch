using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{
    public class OALParser
    {
        private static Boolean Debug = false;

        public EXEScope DecomposeOALFragment(String OALFragment)
        {
            EXEScope ActionScope = new EXEScope();
            ActionScope.OALCode = OALFragment;
            DecomposeOALFragment(ActionScope);

            return ActionScope;
        }
        public void DecomposeOALFragment(EXEScope ActionScope)
        {

            String OALFragment = ActionScope.OALCode;
            ActionScope.OALCode = OALFragment;

            //Filter out comments
            String CommentFreeCode = this.FilterOutComments(OALFragment);

            //Split into commands (need to merge if/while/for each/elif/else into one)

            Stack<EXEScope> ScopeStack = new Stack<EXEScope>();
            ScopeStack.Push(ActionScope);
            String[] TokensBySemicolon = CommentFreeCode.Split(';');
            foreach (String SemicolonToken in TokensBySemicolon)
            {

                String WhitespaceClearedToken = EXEParseUtil.RemoveWhitespace(SemicolonToken);
                int WhitespaceClearedTokenLenght = WhitespaceClearedToken.Length;
                // Ignore empty line
                if (WhitespaceClearedToken.Length == 0)
                {
                    DebugPrint("EMPTY TOKEN\n");
                    continue;
                }

                //Console.WriteLine(WhitespaceClearedToken);

                // Start new scopes
                if (WhitespaceClearedTokenLenght >= 2 && "if".Equals(WhitespaceClearedToken.Substring(0, 2)))
                {
                    EXEScope Scope = new EXEScopeCondition();
                    throw new NotImplementedException();
                    /* String FirstCommand = ExtractControlStructureInit(Scope, SemicolonToken);
                     if (Scope.IsMyEnding(EXEParseUtil.RemoveWhitespace(FirstCommand)))
                     {
                         ScopeStack.Peek().AddCommand(Scope);
                     }
                     else
                     {
                         Scope.AddCommand(new EXECommand(FirstCommand));
                         ScopeStack.Push(Scope);
                     }
                     //TODO extract condition
                     DebugPrint("STARTING IF" + WhitespaceClearedToken + "\n");
                     continue;*/
                }
                if (WhitespaceClearedTokenLenght >= 5 && "while".Equals(WhitespaceClearedToken.Substring(0, 5)))
                {
                    throw new NotImplementedException();
                    /*EXEScope Scope = new EXEScopeLoopWhile();
                    String FirstCommand = ExtractControlStructureInit(Scope, SemicolonToken);
                    if (Scope.IsMyEnding(EXEParseUtil.RemoveWhitespace(FirstCommand)))
                    {
                        ScopeStack.Peek().AddCommand(Scope);
                    }
                    else
                    {
                        Scope.AddCommand(new EXECommand(FirstCommand));
                        ScopeStack.Push(Scope);
                    }
                    //TODO extract condition
                    DebugPrint("STARTING WHILE" + WhitespaceClearedToken + "\n");
                    continue;*/
                }
                if (WhitespaceClearedTokenLenght >= 7 && "foreach".Equals(WhitespaceClearedToken.Substring(0, 7)))
                {
                    throw new NotImplementedException();
                    /*EXEScope Scope = new EXEScope(EXEScope.ScopeTypeNameForeach);
                    String FirstCommand = ExtractControlStructureInit(Scope, SemicolonToken);
                    if (Scope.IsMyEnding(EXEParseUtil.RemoveWhitespace(FirstCommand)))
                    {
                        ScopeStack.Peek().AddCommand(Scope);
                    }
                    else
                    {
                        Scope.AddCommand(new EXECommand(FirstCommand));
                        ScopeStack.Push(Scope);
                    }
                    //TODO extract iterator/iterable initialization
                    DebugPrint("STARTING FOREACH" + WhitespaceClearedToken + "\n");
                    continue;*/
                }
                /*if ("elif".Equals(WhitespaceClearedToken.Substring(0, 4)))
                {
                    ScopeStack.Push(new EXEScope(EXEScope.ScopeTypeNameElif));
                    //TODO extract condition
                    continue;
                }*/
                /*if ("else".Equals(WhitespaceClearedToken.Substring(0, 4)))
                {
                    ScopeStack.Push(new EXEScope(EXEScope.ScopeTypeNameElse));
                    //TODO extract condition
                    continue;
                }*/

                //End current scope
                throw new NotImplementedException();
               /* if (WhitespaceClearedTokenLenght >= 5 && ("endif".Equals(WhitespaceClearedToken.Substring(0, 5))))
                {
                    if (ScopeStack.Peek().ScopeType == EXEScope.ScopeTypeNameIf)
                    {
                        EXEScope CurrentScope = ScopeStack.Pop();
                        ScopeStack.Peek().AddCommand(CurrentScope);

                        DebugPrint("ENDING IF" + WhitespaceClearedToken + "\n");
                    }
                    else
                    {
                        //TODO - error here
                        DebugPrint("ENDING IF WHEN WE ARE NOT IN IF\n");
                    }
                    continue;
                }
                if (WhitespaceClearedTokenLenght >= 8 && "endwhile".Equals(WhitespaceClearedToken.Substring(0, 8)))
                {
                    if (ScopeStack.Peek().ScopeType == EXEScope.ScopeTypeNameWhile)
                    {
                        EXEScope CurrentScope = ScopeStack.Pop();
                        ScopeStack.Peek().AddCommand(CurrentScope);

                        DebugPrint("STARTING WHILE" + WhitespaceClearedToken + "\n");
                    }
                    else
                    {
                        //TODO - error here
                        DebugPrint("ENDING while WHEN WE ARE NOT IN while\n");
                    }
                    continue;
                }
                if (WhitespaceClearedTokenLenght >= 6 && "endfor".Equals(WhitespaceClearedToken.Substring(0, 6)))
                {
                    if (ScopeStack.Peek().ScopeType == EXEScope.ScopeTypeNameForeach)
                    {
                        EXEScope CurrentScope = ScopeStack.Pop();
                        ScopeStack.Peek().AddCommand(CurrentScope);

                        DebugPrint("STARTING FOREACH" + WhitespaceClearedToken + "\n");
                    }
                    else
                    {
                        //TODO - error here
                        DebugPrint("ENDING foreach WHEN WE ARE NOT IN foreach\n");
                    }
                    continue;
                }*/

                //If we are here, we have non-empty command that is not a control structure
                EXECommand CurrentCommand = new EXECommand(SemicolonToken);
                ScopeStack.Peek().AddCommand(CurrentCommand);

                DebugPrint("BASIC COMMAND:" + WhitespaceClearedToken + "\n");
            }

            EXEScope FinalScope = ScopeStack.Pop();
            /*if (ScopeStack.Count == 0)
            {
                return FinalScope;
            }
            else
            {
                //TODO - this means error - some scopes are not properly terminated
                return null;
            }*/
        }
        public String FilterOutComments(String Code)
        {
            Boolean CommentSection = false;
            StringBuilder FilteredCodeBuilder = new StringBuilder();
            int CurrentSlashCount = 0;
            char LastAppendedChar = '\0';
            foreach (char c in Code)
            {
                // If we are currently in comment section, skip the char. Just test if it's a newline, meaning end of the comment section.
                if (CommentSection)
                {
                    if (c == '\n')
                    {
                        CommentSection = false;
                        if (LastAppendedChar != '\n')
                        {
                            FilteredCodeBuilder.Append('\n');
                            LastAppendedChar = '\n';
                        }
                    }
                }
                else
                {
                    if (c == '/')
                    {
                        ++CurrentSlashCount;
                        if (CurrentSlashCount >= 2)
                        {
                            CommentSection = true;
                        }
                    }
                    else if (c == '\n' && LastAppendedChar == '\n')
                    {
                        continue;
                    }
                    else
                    {
                        FilteredCodeBuilder.Append(c);
                        LastAppendedChar = c;
                    }
                }
            }
            return FilteredCodeBuilder.ToString();
        }


        // Return String of code of first command following the control statement
        // can be   -> command
        //          -> bunch'a nested statements
        //          -> endif, .....
        private String ExtractControlStructureInit(EXEScope Scope, String InitOALCode)
        {
            String WhitespaceClearedToken = EXEParseUtil.RemoveWhitespace(InitOALCode);
            int WhitespaceClearedTokenLenght = WhitespaceClearedToken.Length;

            String ConditionOALCode = "";
            int ConditionEndIndex = -1;
            if (WhitespaceClearedTokenLenght >= 2 && "if".Equals(WhitespaceClearedToken.Substring(0, 2)))
            {
                Boolean InCondition = false;
                Boolean InString = false;
                int ParentnessLevel = 0;
                int IfStart = InitOALCode.IndexOf("if");
                for (int i = IfStart; i < InitOALCode.Length; i++)
                {
                    if (!InCondition)
                    {
                        InCondition = (InitOALCode[i] == '(');
                        continue;
                    }
                    if (InitOALCode[i] == '"')
                    {
                        InString = !InString;
                    }
                    if(!InString)
                    {
                        if (InitOALCode[i] == '(')
                        {
                            ParentnessLevel++;
                            continue;
                        }
                        else if (InitOALCode[i] == ')')
                        {
                            if (ParentnessLevel == 0)
                            {
                                ConditionEndIndex = i;
                                break;
                            }
                            ParentnessLevel--;
                            
                            continue;
                        }
                    }
                    Console.WriteLine(i.ToString() + ": " + InitOALCode[i]);
                    ConditionOALCode += InitOALCode[i];
                }

                //Scope.ConditionOAL = ConditionOALCode;

                String FirstCommand = InitOALCode.Substring(ConditionEndIndex + 1);
                return FirstCommand;
            }
            if (WhitespaceClearedTokenLenght >= 5 && "while".Equals(WhitespaceClearedToken.Substring(0, 5)))
            {
                Boolean InCondition = false;
                Boolean InString = false;
                int ParentnessLevel = 0;
                int IfStart = InitOALCode.IndexOf("while");
                for (int i = IfStart; i < InitOALCode.Length; i++)
                {
                    if (!InCondition)
                    {
                        InCondition = InitOALCode[i] == '(';
                        continue;
                    }
                    if (InitOALCode[i] == '"')
                    {
                        InString = !InString;
                    }
                    if (!InString)
                    {
                        if (InitOALCode[i] == '(')
                        {
                            ParentnessLevel++;
                            continue;
                        }
                        else if (InitOALCode[i] == ')')
                        {
                            if(ParentnessLevel == 0)
                            {
                                ConditionEndIndex = i;
                                break;
                            }
                            ParentnessLevel--;
                            continue;
                        }
                    }
                    ConditionOALCode += InitOALCode[i];
                }

                //Scope.ConditionOAL = ConditionOALCode;
                String FirstCommand = InitOALCode.Substring(ConditionEndIndex + 1);
                return FirstCommand;
            }
            if (WhitespaceClearedTokenLenght >= 7 && "foreach".Equals(WhitespaceClearedToken.Substring(0, 7)))
            {
                String SqueezedOalCode = EXEParseUtil.SqueezeWhiteSpace(InitOALCode);
                int IndexOfIteratorName = "for each ".Length;
                int IteratorNameLength = SqueezedOalCode.IndexOf(" in ") - IndexOfIteratorName;
                int IndexOfIterableName = SqueezedOalCode.IndexOf(" in ") + " in ".Length;
                int IterableNameLength = 0;
                for (int i = IndexOfIterableName; !Char.IsWhiteSpace(SqueezedOalCode[i]); i++)
                {
                    IterableNameLength++;
                }

                String IteratorName = SqueezedOalCode.Substring(IndexOfIteratorName, IteratorNameLength);
                String IterableName = SqueezedOalCode.Substring(IndexOfIterableName, IterableNameLength);

                //Scope.IteratorName = IteratorName;
                //Scope.IterableName = IterableName;

                ConditionEndIndex = IndexOfIterableName + IterableNameLength;
                String FirstCommand = InitOALCode.Substring(ConditionEndIndex + 1);
                return FirstCommand;
            }

            return null;
        }

        private void DebugPrint(String String)
        {
            if (Debug)
            {
                Console.WriteLine(String);
            }
        }
    }
}
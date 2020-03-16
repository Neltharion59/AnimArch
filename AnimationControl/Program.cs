using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{
    public class Program
    {
        static void Main(string[] args)
        {
            /*
            OALAnimationRepresentation Animation = new OALAnimationRepresentation();

            CDClass ObserverClass = Animation.ExecutionNameSpace.SpawnClass("Observer", new List<String>(), new List<String>(new String[] { "update", "init", "addComment" }));
            CDClass SubjectClass = Animation.ExecutionNameSpace.SpawnClass("Subject", new List<String>(), new List<String>(new String[] { "attach", "update", "notifyObservers" }));
            CDRelationship OSRel = new CDRelationship("Observer", "Subject");
            String OSRelName = OSRel.RelationshipName;
            Animation.RelationshipSpace.Add(OSRel);
            Boolean success;

            success = Animation.AddCallToAnimation("Observer", "init", OSRelName, "Subject", "attach");
           // Console.WriteLine(success);
            success = Animation.AddCallToAnimation("Observer", "init", OSRelName, "Subject", "attach");
           // Console.WriteLine(success);
            success = Animation.AddCallToAnimation("Observer", "init", OSRelName, "Subject", "attach");
           // Console.WriteLine(success);

            success = Animation.AddCallToAnimation("Observer", "addComment", OSRelName, "Subject", "update");
            //Console.WriteLine(success);
            success = Animation.AddCallToAnimation("Observer", "addComment", OSRelName, "Subject", "notifyObservers");
           // Console.WriteLine(success);
            
            success = Animation.AddCallToAnimation("Subject", "notifyObservers", OSRelName, "Observer", "update");
           // Console.WriteLine(success);
            success = Animation.AddCallToAnimation("Subject", "notifyObservers", OSRelName, "Observer", "update");
            //Console.WriteLine(success);
            success = Animation.AddCallToAnimation("Subject", "notifyObservers", OSRelName, "Observer", "update");
           // Console.WriteLine(success);

            //Console.WriteLine("\n\n\n!!!!!!!!!!!!!!!!!!!!!!!");
            String Code = Animation.TranslateToMainCode();
            OALParser Parser = new OALParser();
            OALCommandParser cp = new OALCommandParser();

            EXEScope SuperScope = Parser.DecomposeOALFragment(Code);
            SuperScope.Parse(null);*/
            OALAnimationRepresentation Animation = null;
            OALCommandParser cp = new OALCommandParser();

            System.IO.File.AppendAllText(@".\ExceptionLog.txt", DateTime.Now.ToString(@"MM\/dd\/yyyy h\:mm tt") + " Starting new session" + Environment.NewLine);


            while (true)
            {
                Console.Write(">> ");
                String command = Console.ReadLine();

                System.IO.File.AppendAllText(@".\ExceptionLog.txt", DateTime.Now.ToString(@"MM\/dd\/yyyy h\:mm tt") + " >> " + command + Environment.NewLine);



                if ("exit".Equals(command))
                {
                    break;
                }

                if ("create animation".Equals(command))
                {
                    Animation = new OALAnimationRepresentation();
                    Console.WriteLine("Animation created");
                    continue;
                }

                if ("delete animation".Equals(command))
                {
                    Animation = null;
                    Console.WriteLine("Animation deleted");
                    continue;
                }

                if ("to code".Equals(command))
                {
                    try
                    {

                        Console.WriteLine(Animation.TranslateToCode());
                    }
                    catch (Exception e)
                    {
                        String ExceptionText1 = "Got this exception while trying to translate to code";
                        String ExceptionText2 = e.ToString();

                        System.IO.File.AppendAllText(@".\ExceptionLog.txt", ExceptionText1 + Environment.NewLine + ExceptionText2 + Environment.NewLine);

                        Console.WriteLine("Exception occured and was logged");
                    }
                    continue;
                }

                if ("show classes".Equals(command))
                {
                    try
                    {
                        foreach (CDClass Class in Animation.ExecutionNameSpace.ClassPool)
                        {
                            Console.WriteLine(Class.Name);
                        }
                    }
                    catch (Exception e)
                    {
                        String ExceptionText1 = "Got this exception while trying to show all classes";
                        String ExceptionText2 = e.ToString();

                        System.IO.File.AppendAllText(@".\ExceptionLog.txt", ExceptionText1 + Environment.NewLine + ExceptionText2 + Environment.NewLine);

                        Console.WriteLine("Exception occured and was logged");
                    }
                    continue;
                }

                if ("show relations".Equals(command))
                {
                    try
                    {
                        foreach (CDRelationship Relation in Animation.RelationshipSpace)
                        {
                            Console.WriteLine(Relation.RelationshipName + ": " + Relation.FromClass + " - " + Relation.ToClass);
                        }
                    }
                    catch (Exception e)
                    {
                        String ExceptionText1 = "Got this exception while trying to show all relations";
                        String ExceptionText2 = e.ToString();

                        System.IO.File.AppendAllText(@".\ExceptionLog.txt", ExceptionText1 + Environment.NewLine + ExceptionText2 + Environment.NewLine);

                        Console.WriteLine("Exception occured and was logged");
                    }
                    continue;
                }

                if ( "show methods".Equals(command))
                {
                    String Expression = command.Substring(12);

                    try
                    {
                        foreach (CDClass Class in Animation.ExecutionNameSpace.ClassPool)
                        {
                            foreach (CDMethod Method in Class.Methods)
                            {
                                Console.WriteLine(Class.Name + "::" + Method.Name);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        String ExceptionText1 = "Got this exception while trying to show methods of class" + Expression;
                        String ExceptionText2 = e.ToString();

                        System.IO.File.AppendAllText(@".\ExceptionLog.txt", ExceptionText1 + Environment.NewLine + ExceptionText2 + Environment.NewLine);

                        Console.WriteLine("Exception occured and was logged");
                    }
                    continue;
                }

                if (command.Length > 10 && "add class ".Equals(command.Substring(0, 10)))
                {
                    String Expression = command.Substring(10);

                    try
                    {
                        Animation.ExecutionNameSpace.SpawnClass(Expression);
                        Console.WriteLine("Class Created");
                    }
                    catch (Exception e)
                    {
                        String ExceptionText1 = "Got this exception while trying to create class";
                        String ExceptionText2 = e.ToString();

                        System.IO.File.AppendAllText(@".\ExceptionLog.txt", ExceptionText1 + Environment.NewLine + ExceptionText2 + Environment.NewLine);

                        Console.WriteLine("Exception occured and was logged");
                    }
                    continue;
                }

                if (command.Length > 11 && "add method ".Equals(command.Substring(0, 11)))
                {
                    String Expression = command.Substring(11);
                    String[] Tokens = Expression.Split(' ');

                    if (Tokens.Length != 2)
                    {
                        Console.WriteLine("You need to enter both class and method name!");
                        continue;
                    }

                    String Class = Tokens[0];
                    String Method = Tokens[1];

                    try
                    {
                        CDClass ClassObject = Animation.ExecutionNameSpace.getClassByName(Class);
                        ClassObject.AddMethod(new CDMethod(Method, "void"));
                        Console.WriteLine("Method Created");
                    }
                    catch (Exception e)
                    {
                        String ExceptionText1 = "Got this exception while trying to create class";
                        String ExceptionText2 = e.ToString();

                        System.IO.File.AppendAllText(@".\ExceptionLog.txt", ExceptionText1 + Environment.NewLine + ExceptionText2 + Environment.NewLine);

                        Console.WriteLine("Exception occured and was logged");
                    }
                    continue;
                }

                if (command.Length > 7 && "relate ".Equals(command.Substring(0, 7)))
                {
                    String Expression = command.Substring(7);
                    String[] Tokens = Expression.Split(' ');
                    String Class1 = Tokens[0];
                    String Class2 = Tokens[1];

                    try
                    {
                        /* CDRelationship rel = new CDRelationship(Class1, Class2);
                         Animation.RelationshipSpace.Add(rel);
                         Console.WriteLine("Created relationship " + rel.RelationshipName + " between " + Class1 + " and " + Class2);*/

                        throw new NotImplementedException();
                    }
                    catch (Exception e)
                    {
                        String ExceptionText1 = "Got this exception while trying to create relationship between " + Class1 + " and " + Class2;
                        String ExceptionText2 = e.ToString();

                        System.IO.File.AppendAllText(@".\ExceptionLog.txt", ExceptionText1 + Environment.NewLine + ExceptionText2 + Environment.NewLine);

                        Console.WriteLine("Exception occured and was logged");
                    }
                    continue;
                }

                if (command.Length > 9 && "add call ".Equals(command.Substring(0, 9)))
                {
                    String Expression = command.Substring(9);
                    

                    try
                    {
                        String[] Tokens = Expression.Split(' ');
                        String CallerClass = Tokens[0];
                        String CallerMethod = Tokens[1];
                        String Relation = Tokens[2];
                        String CalledClass = Tokens[3];
                        String CalledMethod = Tokens[4];

                        Boolean success = Animation.AddCallToAnimation(CallerClass, CallerMethod, Relation, CalledClass, CalledMethod);
                        if (!success)
                        {
                            Console.WriteLine("Failed to add call");
                        }
                        else
                        {
                            Console.WriteLine("Call added successfully");
                        }
                    }
                    catch (Exception e)
                    {
                        String ExceptionText1 = "Got this exception while trying to add call" + command;
                        String ExceptionText2 = e.ToString();

                        System.IO.File.AppendAllText(@".\ExceptionLog.txt", ExceptionText1 + Environment.NewLine + ExceptionText2 + Environment.NewLine);

                        Console.WriteLine("Exception occured and was logged");
                    }
                    continue;
                }

                if (command.Length > 6 && "parse ".Equals(command.Substring(0, 6)))
                {
                    String Expression = command.Substring(6);

                    try
                    {
                        EXEASTNode AST = cp.ConstructAST(Expression);
                        AST.PrintPretty("", false);
                    }
                    catch (Exception e)
                    {
                        String ExceptionText1 = "Got this exception while trying to parse expression \"" + Expression + "\"";
                        String ExceptionText2 = e.ToString();
                        String ExceptionText3 = e.StackTrace;

                        System.IO.File.AppendAllText(@".\ExceptionLog.txt", ExceptionText1 + Environment.NewLine + ExceptionText2 + Environment.NewLine + ExceptionText3 + Environment.NewLine);

                        Console.WriteLine("Exception occured and was logged");
                    }
                    continue;
                }
            }


            System.IO.File.AppendAllText(@".\ExceptionLog.txt", "------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
        }
    }
}
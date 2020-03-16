using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{
    class PseudoDemo
    {
        static void Main(string[] args)
        {
            // Create new object, that will represent the program, and its execution space
            // This object is empty - it knows no classes or instances
            OALAnimationRepresentation Animation = new OALAnimationRepresentation();
            // Create new class in the execution space
            CDClass ObserverClass = Animation.ExecutionNameSpace.SpawnClass(
                "Observer",             // Name this class "Observer" (as in Observer pattern)
                new List<String>(),     // Pass the list of attributes of this class
                new List<String>(new String[] { "update", "init", "addComment" })   //Pass the list of methods of this class
            );
            // Create another class in the execution space
            CDClass SubjectClass = Animation.ExecutionNameSpace.SpawnClass(
                "Subject",                                  // Name this class "Subject" (as in Observer pattern)
                new List<String>(new String[] { "state" }), // Pass the list of attributes of this class
                new List<String>(new String[] { "attach", "update", "notifyObservers" })   //Pass the list of methods of this class
            );
            // Create a relationship between classes "Observer" and "Subject"
            CDRelationship OSRel = new CDRelationship("Observer", "Subject");
            // Add this relationship to execution space, to create association between those classes
            Animation.RelationshipSpace.Add(OSRel);

            // Add a few steps to animation. Each step is a an action, when a method of one class calls in its body
            // method of another class. There is also need of specifying the edge connecting these classes,
            // because two classes can be connected with multiple edges
            Animation.AddCallToAnimation( // Add step to animation
                "Observer", // The calling class will be "Observer"
                "init",     // The calling class's method will be "init"
                OSRel.RelationshipName, // The call will be transmitted along edge with this name
                "Subject", // The called class will be "Subject"
                "attach"  // The called class's method will be "attach"
            ); 
            Animation.AddCallToAnimation("Observer", "init", OSRel.RelationshipName, "Subject", "attach"); // Add animation step
            Animation.AddCallToAnimation("Observer", "init", OSRel.RelationshipName, "Subject", "attach"); // Add animation step
            Animation.AddCallToAnimation("Observer", "addComment", OSRel.RelationshipName, "Subject", "update"); // Add animation step
            Animation.AddCallToAnimation("Observer", "addComment", OSRel.RelationshipName, "Subject", "notifyObservers"); // Add animation step
            Animation.AddCallToAnimation("Subject", "notifyObservers", OSRel.RelationshipName, "Observer", "update"); // Add animation step
            Animation.AddCallToAnimation("Subject", "notifyObservers", OSRel.RelationshipName, "Observer", "update"); // Add animation step
            Animation.AddCallToAnimation("Subject", "notifyObservers", OSRel.RelationshipName, "Observer", "update"); // Add animation step

            // Now that the animation has defined classes and steps, it can be transformed to OAL code.
            String Code = Animation.TranslateToCode(); // Transform the animation to OAL code
            OALParser Parser = new OALParser();        // Prepare parser for the code
            EXEScope SuperScope = Parser.DecomposeOALFragment(Code); // Break the code into commands, encapsulating in execution-ready object
            SuperScope.Parse(null); // Parse each command into execution-ready objects

            SuperScope.Execute(Animation, null); // Start the execution of the animation


            Console.ReadLine();
        }
    }
}

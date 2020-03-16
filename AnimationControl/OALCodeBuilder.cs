using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{
    public class OALCodeBuilder
    {
        private List<List<MethodCall>> CallQueue { get; set; }

        public OALCodeBuilder()
        {
            this.CallQueue = new List<List<MethodCall>>();
        }
        public Boolean AddCall(String CallerClass, String CallerMethod, String RelationshipName, String CalledClass, String CalledMethod, Boolean StartNewSequence)
        {
            Boolean Success;

            MethodCall NewCall = new MethodCall
            {
                CallerClass = CallerClass,
                CallerMethod = CallerMethod,
                RelationshipName = RelationshipName,
                CalledClass = CalledClass,
                CalledMethod = CalledMethod
            };

            if (StartNewSequence)
            {
                List<MethodCall> NewSequence = new List<MethodCall>();
                NewSequence.Add(NewCall);
                this.CallQueue.Add(NewSequence);
                Success = true;
            }
            else if (!StartNewSequence && !CallQueue.Any())
            {
                List<MethodCall> NewSequence = new List<MethodCall>();
                NewSequence.Add(NewCall);
                this.CallQueue.Add(NewSequence);
                Success = true;
            }
            else
            {
                //Perform check if CalledClass of previous call is the same as Caller class in the new call
                //if yes, we append the new call to the last element in CallQueue. Return true
                //if no, we do not append it. Return false

                throw new NotImplementedException();
            }

            return Success;
        }

        public String ToCode()
        {
            throw new NotImplementedException();
        }
    }
}

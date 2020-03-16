using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{
    class OALCodeTranslator
    {
        private static OALCodeTranslator Instance = null;

        public static OALCodeTranslator GetInstance()
        {
            if (Instance == null)
            {
                Instance = new OALCodeTranslator();
            }
            return Instance;
        }

        public OALAnimationRepresentation GenerateAnimation(String Code)
        {
            //TODO - This will need to be upgraded to accept instructions other than "call(...)"
            String[] CallTokens = Code.Split(';');
            OALAnimationRepresentation Animation = new OALAnimationRepresentation();

            //TODO Put Classes here?
            //Classes and relationships should be accessible globally somewhere to be put into animation
            OALCall Call;
            foreach (String CallToken in CallTokens)
            {
                Call = new OALCall(CallToken);
                Animation.AddCallToAnimation(
                    Call.CallerClassName,
                    Call.CallerMethodName,
                    Call.RelationshipName,
                    Call.CalledClassName,
                    Call.CalledMethodName);
            }

            return Animation;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{
    class OALCall
    {
        public String CallerClassName { get; set; }
        public String CallerMethodName { get; set; }
        public String RelationshipName { get; set; }
        public String CalledClassName { get; set; }
        public String CalledMethodName { get; set; }

        public OALCall(String CallerClassName, String CallerMethodName, String RelationshipName, String CalledClassName, String CalledMethodName)
        {
            this.CallerClassName = CallerClassName;
            this.CallerMethodName = CallerMethodName;
            this.RelationshipName = RelationshipName;
            this.CalledClassName = CalledClassName;
            this.CalledMethodName = CalledMethodName;
        }

        public OALCall(String CallCode)
        {
            String SanitizedCallCode = EXEParseUtil.SqueezeWhiteSpace(CallCode);
            //Remove "call(" and ")" chars
            SanitizedCallCode = SanitizedCallCode.Substring(5, SanitizedCallCode.Length - 5 - 1);
            String[] Args = SanitizedCallCode.Split(',');

            this.CallerClassName = Args[0];
            this.CallerMethodName = Args[1];
            this.RelationshipName = Args[2];
            this.CalledClassName = Args[3];
            this.CalledMethodName = Args[4];
        }
        public String ToGUIOALCode()
        {
            String Code = "call("
                + this.CallerClassName
                + this.CallerMethodName
                + this.RelationshipName
                + this.CalledClassName
                + this.CalledMethodName
                + ");";
            return Code;
        }
    }
}

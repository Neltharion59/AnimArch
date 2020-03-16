using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{
    class MethodCall
    {
        public String CallerClass {get; set;}
        public String CalledClass { get; set; }
        public String CallerMethod { get; set; }
        public String CalledMethod { get; set; }
        public String RelationshipName { get; set; }
        public Boolean NewSequence { get; set; }
    }
}

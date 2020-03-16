using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{
    public class CDRelationshipPool
    {
        private long RelationshipIdSeed;
        public List<CDRelationship> RelationshipPool { get; }

        public CDRelationshipPool()
        {
            this.RelationshipIdSeed = 1;
            this.RelationshipPool = new List<CDRelationship>();
        }

        public CDRelationship SpawnRelationship(String FromClass, String ToClass)
        {
            CDRelationship NewRelationship = new CDRelationship(FromClass, ToClass, "R" + this.RelationshipIdSeed);
            this.RelationshipPool.Add(NewRelationship);
            this.RelationshipIdSeed++;

            return NewRelationship;
        }
    }
}

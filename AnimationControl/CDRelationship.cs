using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{
    public class CDRelationship
    {
        public String FromClass { get; set; }
        public String ToClass { get; set; }
        public String RelationshipName { get; }

        private List<(String, String)> InstanceRelationships { get;}

        public CDRelationship(String FromClass, String ToClass, String Name)
        {

            this.FromClass = FromClass;
            this.ToClass = ToClass;
            this.RelationshipName = Name;
            this.InstanceRelationships = new List<(String, String)>();
            
        }

        public Boolean RelationshipExists(String Instance1, String Instance2)
        {
            Boolean Result = false;
            foreach ((String, String) Tupple in this.InstanceRelationships)
            {
                if (Tupple == (Instance1, Instance2) || Tupple == (Instance2, Instance1))
                {
                    Result = true;
                    break;
                }
            }
            return Result;
        }
        public Boolean CreateRelationship(String InstanceName1, String InstanceName2)
        {
            if (RelationshipExists(InstanceName1, InstanceName2))
            {
                return false;
            }

            this.InstanceRelationships.Add((InstanceName1, InstanceName2));

            return true;
        }

        public void ClearRelationships()
        {
            this.InstanceRelationships.Clear();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{
    public class CDClassPool
    {
        private long ClassIDPrefix;
        public List<CDClass> ClassPool { get; }

        public CDClassPool()
        {
            this.ClassIDPrefix = 1000;
            this.ClassPool = new List<CDClass>();
        }

        // SetUloh2
        public CDClassInstance GetClassInstanceById(String ClassName, long Id)
        {
			ClassPool.ForEach(delegate(CDClass Class)
			{
				if(Class.ClassIDPrefix == Id)
				{
					return Class;
				}
			});
			return null;
            throw new NotImplementedException();
        }
        public CDClass SpawnClass(String Name)
        {
            long NewClassID = this.GenerateClassID();
            CDClass NewClass = new CDClass(NewClassID, Name);
            this.ClassPool.Add(NewClass);
            return NewClass;
        }

        public Boolean ClassesExist(List<String> ClassNames)
        {
            Boolean Result = true;

            foreach (String ClassName in ClassNames)
            {
                if (!ClassExists(ClassName))
                {
                    Result = false;
                }
            }

            return Result;
        }

        public Boolean ClassExists(String ClassName)
        {
            Boolean Result = false;

            foreach (CDClass Class in this.ClassPool)
            {
                if (Class.Name.Equals(ClassName))
                {
                    Result = true;
                    break;
                }
            }

            return Result;
        }

        public CDClass getClassByName(String ClassName)
        {
            CDClass Result = null;

            foreach (CDClass Class in this.ClassPool)
            {
                if (Class.Name.Equals(ClassName))
                {
                    Result = Class;
                    break;
                }
            }

            return Result;
        }

        public Boolean MethodExists(String ClassName, String MethodName)
        {
            if (!ClassExists(ClassName))
            {
                return false;
            }

            CDClass SearchedClass = null;

            foreach (CDClass Class in this.ClassPool)
            {
                if (Class.Name.Equals(ClassName))
                {
                    SearchedClass = Class;
                    break;
                }
            }

            if (SearchedClass == null)
            {
                return false;
            }

            return SearchedClass.MethodExists(MethodName);
        }

        public (List<String>, List<String>, List<String>) CreateDefinedMethodsTupple()
        {
            (List<String>, List<String>, List<String>) Tupples = (new List<String>(), new List<String>(), new List<String>());
            foreach (CDClass Class in this.ClassPool)
            {
                foreach (CDMethod Method in Class.Methods)
                {
                    Tupples.Item1.Add(Class.Name);
                    Tupples.Item2.Add(Method.Name);
                    Tupples.Item3.Add(Method.OALCode);
                }
            }
            return Tupples;
        }

        private long GenerateClassID()
        {
            long NewClassID = this.ClassIDPrefix;
            this.ClassIDPrefix++;

            return NewClassID;
        }
    }
}

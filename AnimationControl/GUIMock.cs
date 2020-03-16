using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{
    class GUIMock
    {
        private static GUIMock Instance = null;

        public static GUIMock GetInstance()
        {
            if (Instance == null)
            {
                Instance = new GUIMock();
            }
            return Instance;
        }

        public void UpdateTempCodeBuilder(String Code)
        {
            Console.WriteLine();
        }
    }
}

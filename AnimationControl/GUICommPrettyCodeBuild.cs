using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{
    class GUICommPrettyCodeBuild
    {
        private static GUICommPrettyCodeBuild Instance = null;
        private String UserCode { get; set;}
        private List<String> TempArgs { get; set; }

        public static GUICommPrettyCodeBuild GetInstance()
        {
            if (Instance == null)
            {
                Instance = new GUICommPrettyCodeBuild();
            }

            return Instance;
        }

        private GUICommPrettyCodeBuild()
        {
            this.UserCode = "";
            this.TempArgs = new List<string>();
        }

        public String AddArgsToTempCallCode(String Arg1, String Arg2)
        {
            this.TempArgs.Add(Arg1);
            return this.AddArgToTempCallCode(Arg2);
        }
        public String AddArgToTempCallCode(String Arg)
        {
            TempArgs.Add(Arg);
            String Result = "call(";
            for (int i = 0; i < this.TempArgs.Count; i++)
            {
                if (i > 0 && i < this.TempArgs.Count - 1)
                {
                    Result += ", ";
                }
                Result += this.TempArgs[i];
            }
            if (this.TempArgs.Count == 5)
            {
                Result += ");\n";
                this.UserCode += Result;
            }

            GUIMock.GetInstance().UpdateTempCodeBuilder(Result);

            return Result;
        }

        public String AddCall(String CallerClassName, String CallerMethodName, String RelationshipName, String CalledClassName, String CalledMethodName)
        {
            OALCall Call = new OALCall(CallerClassName, CallerMethodName, RelationshipName, CalledClassName, CalledMethodName);
            this.UserCode += Call.ToString();

            return this.UserCode;
        }

        public String OverwriteCode(String Code)
        {
            this.UserCode = Code;
            return this.UserCode;
        }

        public void ClearCode()
        {
            this.UserCode = "";
        }
    }
}

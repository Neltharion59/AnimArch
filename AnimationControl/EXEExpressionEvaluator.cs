using System;
using System.Collections.Generic;

namespace AnimationControl
{
    public class EXEExpressionEvaluator
    {

        // SetUloh1
        // Here you get operator and operands, and you need to return the result. Check the unit tests to see what this is about
        public String Evaluate(String Operator, List<String> Operands)
        {
            if (!this.CanBeEvaluated(Operator, Operands))
            {
                return null;
            }


            //EXETypes.DetermineVariableType(null, "5");
            //EXETypes.StringTypeName;
            throw new NotImplementedException();
        }
        //What's the difference between "FAIL" and "false"?
        public Boolean CanBeEvaluated(String Operator, List<String> Params)
        {
            String Result = this.IsValid(Operator, Params.ToArray());

            Boolean CanBeEvaluated = "true".Equals(Result) ? true : false;

            return CanBeEvaluated;
        }
        // Potentially refactor this
        private String IsValid(string oper, string[] param)
        {
            int p1;
            double p2;
            bool b1 = int.TryParse(param[0], out p1);
            bool b2 = double.TryParse(param[0], out p2);

            if (b1 || b2)
            {
                if (param.Length < 2)
                {
                    return "FAIL";
                }
                if (oper == ">" || oper == "<" || oper == "<=" || oper == ">=" || oper == "+" ||
                   oper == "-" || oper == "/" || oper == "*" || oper == "==" || oper == "!=")
                {
                    foreach (string paramx in param)
                    {
                        if (!int.TryParse(paramx, out p1) && !double.TryParse(paramx, out p2))
                        {
                            return "FAIL";
                        }
                    }
                    double res;
                    switch (oper)
                    {
                        case ">":
                            double.TryParse(param[0], out res);
                            for (int i = 1; i < param.Length; i++)
                            {
                                double.TryParse(param[i], out p2);
                                if (!(res > p2))
                                {
                                    return "false";
                                }
                                res = p2;
                            }
                            return "true";
                        case "<":
                            double.TryParse(param[0], out res);
                            for (int i = 1; i < param.Length; i++)
                            {
                                double.TryParse(param[i], out p2);
                                if (!(res < p2))
                                {
                                    return "false";
                                }
                                res = p2;
                            }
                            return "true";
                        case "<=":
                            double.TryParse(param[0], out res);
                            for (int i = 1; i < param.Length; i++)
                            {
                                double.TryParse(param[i], out p2);
                                if (!(res <= p2))
                                {
                                    return "false";
                                }
                                res = p2;
                            }
                            return "true";
                        case "==":
                            double.TryParse(param[0], out res);
                            for (int i = 1; i < param.Length; i++)
                            {
                                double.TryParse(param[i], out p2);
                                if (!(res == p2))
                                {
                                    return "false";
                                }
                                res = p2;
                            }
                            return "true";
                        case "!=":
                            double.TryParse(param[0], out res);
                            for (int i = 1; i < param.Length; i++)
                            {
                                double.TryParse(param[i], out p2);
                                if (!(res != p2))
                                {
                                    return "false";
                                }
                                res = p2;
                            }
                            return "true";
                        case ">=":
                            double.TryParse(param[0], out res);
                            for (int i = 1; i < param.Length; i++)
                            {
                                double.TryParse(param[i], out p2);
                                if (!(res >= p2))
                                {
                                    return "false";
                                }
                                res = p2;
                            }
                            return "true";
                        case "+":
                            double.TryParse(param[0], out res);

                            for (int i = 1; i < param.Length; i++)
                            {
                                double.TryParse(param[i], out p2);
                                res += p2;
                            }
                            return res.ToString();
                        case "-":
                            double.TryParse(param[0], out res);
                            for (int i = 1; i < param.Length; i++)
                            {
                                double.TryParse(param[i], out p2);
                                res -= p2;
                            }
                            return res.ToString();
                        case "/":
                            double.TryParse(param[0], out res);
                            for (int i = 1; i < param.Length; i++)
                            {
                                double.TryParse(param[i], out p2);
                                res /= p2;
                            }
                            return res.ToString();
                        case "*":
                            double.TryParse(param[0], out res);
                            for (int i = 1; i < param.Length; i++)
                            {
                                double.TryParse(param[i], out p2);
                                res *= p2;
                            }
                            return res.ToString();

                        default:
                            return "FAIL";

                    }
                }
                else if (oper == "%")
                {
                    foreach (string paramx in param)
                    {
                        if (!int.TryParse(paramx, out p1))
                        {
                            return "FAIL";
                        }
                    }
                    int res;
                    int.TryParse(param[0], out res);
                    for (int i = 1; i < param.Length; i++)
                    {
                        int.TryParse(param[i], out p1);
                        res %= p1;
                    }
                    return res.ToString();
                }
                return "FAIL2";
            }
            else
            {
                foreach (string paramx in param)
                {
                    if (int.TryParse(paramx, out p1) || double.TryParse(paramx, out p2))
                    {
                        return "FAIL";
                    }
                }
            }

            b1 = param[0][0] == '\"';
            b2 = param[0][param[0].Length - 1] == '\"';

            if (b1 && b2)
            {
                if (param.Length < 2)
                {
                    return "FAIL";
                }
                if (oper == ">" || oper == "<" || oper == "<=" || oper == ">=" || oper == "==" || oper == "!=")
                {
                    foreach (string paramx in param)
                    {
                        if (!(paramx[paramx.Length - 1] == '\"') || !(paramx[0] == '\"'))
                        {
                            return "FAIL";
                        }
                    }
                    switch (oper)
                    {
                        case "<":
                            for (int i = 1; i < param.Length; i++)
                            {
                                if (!((param[i - 1]).Length < (param[i]).Length))
                                {
                                    return "false";
                                }
                            }
                            return "true";
                        case ">":
                            for (int i = 1; i < param.Length; i++)
                            {
                                if (!((param[i - 1]).Length > (param[i]).Length))
                                {
                                    return "false";
                                }
                            }
                            return "true";
                        case "<=":
                            for (int i = 1; i < param.Length; i++)
                            {
                                if (!((param[i - 1]).Length <= (param[i]).Length))
                                {
                                    return "false";
                                }
                            }
                            return "true";
                        case ">=":
                            for (int i = 1; i < param.Length; i++)
                            {
                                if (!((param[i - 1]).Length >= (param[i]).Length))
                                {
                                    return "false";
                                }
                            }
                            return "true";
                        case "==":
                            for (int i = 1; i < param.Length; i++)
                            {
                                if (!(param[i - 1] == param[i]))
                                {
                                    return "false";
                                }
                            }
                            return "true";
                        case "!=":
                            for (int i = 1; i < param.Length; i++)
                            {
                                if (!(param[i - 1] != param[i]))
                                {
                                    return "false";
                                }
                            }
                            return "true";
                        default:
                            return "FAIL";
                    }
                }
                return "FAIL";
            }
            else
            {
                foreach (string paramx in param)
                {
                    if (paramx[paramx.Length - 1] == '\"' || paramx[0] == '\"')
                    {
                        return "FAIL";
                    }
                }
            }


            bool.TryParse(param[0], out b1);

            if (bool.TryParse((param[0]).ToLower(), out b1))
            {
                if (param.Length < 2)
                {
                    return "FAIL";
                }

                if (oper == "==" || oper == "!=" || oper.ToLower() == "and" || oper.ToLower() == "or" || oper.ToLower() == "xor" || oper.ToLower() == "xnor")
                {
                    foreach (string paramx in param)
                    {
                        if (!bool.TryParse(paramx.ToLower(), out b1))
                        {
                            return "FAIL";
                        }
                    }
                    bool res;
                    bool.TryParse(param[0], out res);
                    switch (oper)
                    {
                        case "==":
                            for (int i = 1; i < param.Length; i++)
                            {
                                bool.TryParse(param[i].ToLower(), out b1);
                                if (res != b1)
                                {
                                    return "false";
                                }
                            }
                            return "true";
                        case "!=":
                            for (int i = 1; i < param.Length; i++)
                            {
                                bool.TryParse(param[i].ToLower(), out b1);
                                if (res == b1)
                                {
                                    return "false";
                                }
                            }
                            return "true";
                        case "and":
                            for (int i = 1; i < param.Length; i++)
                            {
                                bool.TryParse(param[i].ToLower(), out b1);
                                res = res && b1;
                            }
                            return res.ToString();
                        case "or":
                            for (int i = 1; i < param.Length; i++)
                            {
                                bool.TryParse(param[i].ToLower(), out b1);
                                res = res || b1;
                            }
                            return res.ToString();
                        case "xor":
                            for (int i = 1; i < param.Length; i++)
                            {
                                bool.TryParse(param[i].ToLower(), out b1);
                                res = (res && !b1) || (!res && b1);
                            }
                            return res.ToString();
                        case "xnor":
                            for (int i = 1; i < param.Length; i++)
                            {
                                bool.TryParse(param[i].ToLower(), out b1);
                                res = !((res && !b1) || (!res && b1));
                            }
                            return res.ToString();
                        default:
                            return "FAIL";
                    }
                }
                else if (oper.ToLower() == "not")
                {
                    if (param.Length != 1)
                    {
                        return "FAIL";
                    }
                    bool.TryParse(param[0].ToLower(), out b1);
                    return (!b1).ToString();
                }
            }

            //if (DateTime.TryParseExact(param1, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture,System.Globalization.DateTimeStyles.NoCurrentDateDefault  , out DateTime datetime) && 
            //   DateTime.TryParseExact(param2, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture,System.Globalization.DateTimeStyles.NoCurrentDateDefault  , out datetime))
            //{
            //	return true;
            //}		
            return "FAIL";
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Calc.Annotations;

namespace Calc.Model
{
    class Calculator
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public string Display { get; set; }
        public string FullExpression { get; set; }
        public string FirstArgument { get; set; }
        public string SecondArgument { get; set; }
        public string Operation { get; set; }
        public string Result { get; set; }

        public Calculator()
        {
            FirstArgument = string.Empty;
            SecondArgument = string.Empty;
            Operation = string.Empty;
        }

        public Calculator(string display)
        {
            Display = display;
        }

        public bool CanPutDot()
        {
            return !Display.Contains(".");
        }

        public void CalculateResult()
        {
            try
            {
                switch (Operation)
                {
                    case ("+"):
                        Result = (Convert.ToDouble(FirstArgument) + Convert.ToDouble(SecondArgument)).ToString();
                        break;

                    case ("-"):
                        Result = (Convert.ToDouble(FirstArgument) - Convert.ToDouble(SecondArgument)).ToString();
                        break;

                    case ("*"):
                        Result = (Convert.ToDouble(FirstArgument) * Convert.ToDouble(SecondArgument)).ToString();
                        break;

                    case ("/"):
                        if (Convert.ToDouble(SecondArgument) == 0)
                        {
                            throw new ArgumentException("Division with 0");
                        }
                        Result = (Convert.ToDouble(FirstArgument) / Convert.ToDouble(SecondArgument)).ToString();
                        break;
                    case ("sqrt"):
                        if (Convert.ToDouble(FirstArgument) < 0)
                        {
                            throw new ArgumentException("Sqrt of value < 0");
                        }
                        Result = (Math.Sqrt(Convert.ToDouble(FirstArgument))).ToString();
                        break;
                    case ("%"):
                        if (Convert.ToDouble(SecondArgument) < 0)
                        {
                            throw new ArgumentException("Cannot have percent of negative value");
                        }
                        Result = (Convert.ToDouble(FirstArgument) * Convert.ToDouble(SecondArgument) / Double.Parse("100")).ToString();
                        break;

                }
            }
            catch (Exception e)
            {
                log.Debug(e.Message);
                Result = "Err";
                throw;
            }
        }
    }
}

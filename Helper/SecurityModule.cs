using System;

namespace CountryWeb.Helper
{
    public class SecurityModule
    {
        #region Recaptcha

        // private static readonly string[] _operationStr = new string[] { " + ", " - " };
                private static readonly string[] _operationStr = new string[] { " + " };


        private static int Calculation (int num1, int num2, int operation)
        {
            switch (operation)
            {
                case 1:
                    return num1 - num2;
                default:
                case 0:
                    return num1 + num2;
            }
        }

        public static string[] CreateRecaptchaString ()
        {
            Random generator = new Random ();

            int num1 = generator.Next (30) + 1;
            int num2 = generator.Next (30) + 1;
            int operation = generator.Next (_operationStr.Length);

            //session["captcha"] = Calculation(num1, num2, operation);

            string quesion = num1 + _operationStr[operation] + num2 + " = ?";
            string answer = Calculation (num1, num2, operation).ToString ();
            string[] s = { quesion, answer };

            return s;
        }

        #endregion
    }
}
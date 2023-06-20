using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxioMath.Utils
{

    using System;
    using System.Text.RegularExpressions;

    public class LatexConverter
    {

        public static string SimplifyParenthesis(string expression)
        {
            string simplifiedExpression = expression;

            // Elimina paréntesis redundantes de la forma "((expr))"
            while (Regex.IsMatch(simplifiedExpression, @"\(\(([^()]+)\)\)"))
            {
                simplifiedExpression = Regex.Replace(simplifiedExpression, @"\(\(([^()]+)\)\)", "($1)");
            }

            // Elimina paréntesis redundantes de la forma "(expr)"
            simplifiedExpression = Regex.Replace(simplifiedExpression, @"\(([^()]+)\)", "$1");

            return simplifiedExpression;
        }
        public static string ConvertToLatex(string expression)
        {
            expression = SimplifyParenthesis(expression);
            // Reemplaza los operadores y los exponentes con la notación LaTeX correspondiente
            string latexExpression = Regex.Replace(expression, @"(\*|\+|-|\/|\^)", match =>
            {
                switch (match.Value)
                {
                    case "*":
                        return @"\cdot ";
                    case "+":
                        return "+";
                    case "-":
                        return "-";
                    case "/":
                        return @"\frac";
                    case "^":
                        return "^";
                    default:
                        return match.Value;
                }
            });

            // Encierra la expresión en los delimitadores de ecuación LaTeX
            latexExpression = @"\(" + latexExpression + @"\)";

            return latexExpression;
        }

    }

}

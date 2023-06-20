using AxioMath.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxioMath.Solvers
{
    public class ExpressionBuilder
    {
        IExpression result;
        string nextConstantName = "a";
        private int constantIndex = 0;


        public ExpressionBuilder Constant(double realNumber)
        {
            result = new RealConstant<RealNumber>(GetConstantName(), new RealNumber(realNumber));
            return this;
        }
        public ExpressionBuilder Variable(string name)
        {
            result = new Variable<RealNumber>(name);
            return this;
        }

        public IExpression Generate()
        {
            var res = result;
            result = null;
            return res;
        }

        public ExpressionBuilder Sum(params double[] realTerms)
        {
            var parameters = new List<IExpression>();
            parameters.Add(result);
            parameters.AddRange(realTerms.Select(x => new RealConstant<RealNumber>(GetConstantName(), new RealNumber(x))));
            result = new Sum(parameters.ToArray());
            return this;
        }

        public ExpressionBuilder CreateMultiply(params double[] realFactors)
        {
            result = new Multiply(realFactors.Select(x => new RealConstant<RealNumber>(GetConstantName(), new RealNumber(x))).ToArray());
            return this;
        }
        public ExpressionBuilder Multiply(params double[] realFactors)
        {
            var parameters = new List<IExpression>();
            parameters.Add(result);
            parameters.AddRange(realFactors.Select(x => new RealConstant<RealNumber>(GetConstantName(), new RealNumber(x))));
            result = new Multiply(parameters.ToArray());
            return this;
        }
        public ExpressionBuilder Multiply(string variable)
        {
            result = new Multiply(result, new Variable<RealNumber>(variable));
            return this;
        }

        public ExpressionBuilder Divide(double realDenominator)
        {
            result = new Divide(result, new RealConstant<RealNumber>(GetConstantName(), new RealNumber(realDenominator)));
            return this;
        }


        private string GetConstantName()
        {

            string currentConstantName = nextConstantName;

            UpdateNextConstantName();

            return currentConstantName;
        }

        private void UpdateNextConstantName()
        {
            // Incrementar el índice
            constantIndex++;

            // Obtener la letra correspondiente al índice
            char letter = GetLetterFromIndex(constantIndex);

            // Si se excedió el rango de letras, reiniciar el índice y concatenar el número
            if (letter == '\0')
            {
                constantIndex = 1;
                nextConstantName = "a1";
            }
            else
            {
                nextConstantName = letter.ToString();
            }
        }
        private char GetLetterFromIndex(int index)
        {
            // Convertir el índice a un código ASCII de letra minúscula (97=a)
            int letterCode = 96 + (index % 26);

            // Si se excedió el rango de letras, devolver '\0'
            if (letterCode > 122)
            {
                return '\0';
            }

            return (char)letterCode;
        }
    }
}

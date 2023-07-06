using AxioMath.Core;
using System.Xml.Linq;

namespace AxioMath.Solvers
{
    public class ComplexExpressionBuilder
    {

        //TODO
        //El complex value debe ser expression

        //Expression<ComplexValue> result;
        ////string nextConstantName = "a";
        ////private int constantIndex = 0;


        //public ComplexExpressionBuilder Constant(double realNumber)
        //{
        //    result = new ComplexValue(realNumber);
        //    return this;
        //}
        //public ComplexExpressionBuilder Variable(string name)
        //{
        //    result = new ComplexValue(name);
        //    return this;
        //}

        //public Expression<ComplexValue> Generate()
        //{
        //    var res = result;
        //    result = null;
        //    return res;
        //}

        //public ComplexExpressionBuilder CreateSum(double left, double right)
        //{

        //    result = new AdditionExpression<ComplexValue>(new ComplexValue(left),
        //       new ComplexValue(right));
        //    return this;
        //}

        //public ComplexExpressionBuilder CreateSum(Expression<ComplexValue> left, Expression<ComplexValue> right)
        //{

        //    result = new AdditionExpression<ComplexValue>(left, right);
        //    return this;
        //}

        //public ComplexExpressionBuilder Sum(double right)
        //{
        //    result = new AdditionExpression<ComplexValue>(result,new ComplexValue(right));
        //    return this;
        //}

        //public ComplexExpressionBuilder CreateMultiply(Expression<ComplexValue> left, Expression<ComplexValue> right)
        //{
        //    result = new MultiplicationExpression<ComplexValue>(left, right);
        //    return this;
        //}
        //public ComplexExpressionBuilder CreateMultiply(double left, double right)
        //{
        //    result = new MultiplicationExpression<ComplexValue>(new ComplexValue(left),
        //         new ComplexValue(right));
        //    return this;
        //}
        //public ComplexExpressionBuilder Multiply(double right)
        //{

        //    result = new MultiplicationExpression<ComplexValue>(result,
        //        new ComplexValue(right));
        //    return this;

        //}
        //public ComplexExpressionBuilder Multiply(string variable)
        //{
        //    result = new MultiplicationExpression<ComplexValue>(result,
        //         new ComplexValue(variable));
        //    return this;
        //}

        //public ComplexExpressionBuilder Divide(double realDenominator)
        //{
        //    result = new Divide(result, new RealConstant<RealNumber>(GetConstantName(), new RealNumber(realDenominator)));
        //    return this;
        //}


        //private string GetConstantName()
        //{

        //    string currentConstantName = nextConstantName;

        //    UpdateNextConstantName();

        //    return currentConstantName;
        //}

        //private void UpdateNextConstantName()
        //{
        //    // Incrementar el índice
        //    constantIndex++;

        //    // Obtener la letra correspondiente al índice
        //    char letter = GetLetterFromIndex(constantIndex);

        //    // Si se excedió el rango de letras, reiniciar el índice y concatenar el número
        //    if (letter == '\0')
        //    {
        //        constantIndex = 1;
        //        nextConstantName = "a1";
        //    }
        //    else
        //    {
        //        nextConstantName = letter.ToString();
        //    }
        //}
        //private char GetLetterFromIndex(int index)
        //{
        //    // Convertir el índice a un código ASCII de letra minúscula (97=a)
        //    int letterCode = 96 + (index % 26);

        //    // Si se excedió el rango de letras, devolver '\0'
        //    if (letterCode > 122)
        //    {
        //        return '\0';
        //    }

        //    return (char)letterCode;
        //}
    }
}

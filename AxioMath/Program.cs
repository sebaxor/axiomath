using AxioMath.Core.Complex;
using AxioMath.Core.Expression.Definition;
using AxioMath.Core.Expression.Implementation;
using AxioMath.Core.Operations;
using AxioMath.Solvers;

IAlgebra<ComplexValue> algebra = new ComplexAlgebra();




var suma = new SumExpression<ComplexValue>(new ComplexValue(2), new ComplexValue(5));
var producto = new MultiplicationExpression<ComplexValue>(new ComplexValue(10), new ComplexValue(15));
var expr5 = new SumExpression<ComplexValue>(suma, producto);


ComplexExpressionBuilder builder = new ComplexExpressionBuilder();

// Evaluación de la expresión
Expression<ComplexValue> result = expr5.Accept(algebra);

Console.WriteLine(expr5);  // Resultado: 20
Console.WriteLine(result);  // Resultado: 20


//var expression = builder.CreateSum(builder.Variable("x").Sum(5).Generate(), builder.CreateSum(10, 15).Generate()).Generate();

//Expression<ComplexValue> result2 = expression.Accept(algebra);

//Console.WriteLine(expression);  // Resultado: 20
//Console.WriteLine(result2);  // Resultado: 20






Console.ReadLine();

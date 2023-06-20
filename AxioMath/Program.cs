
using AxioMath.Core;
using AxioMath.Solvers;

IAlgebra<ComplexValue> algebra = new ComplexAlgebra();




var suma = new AdditionExpression<ComplexValue>(new ComplexValue("x"), new ComplexValue(5));
var producto = new MultiplicationExpression<ComplexValue>(new ComplexValue(10), new ComplexValue(15));
var expr5 = new AdditionExpression<ComplexValue>(suma, producto);


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

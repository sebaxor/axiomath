
using AxioMath.Core;




IAlgebra<Expression<ComplexValue>> algebra = new ComplexExpressionsAlgebra();




var suma = new AdditionExpression<Expression<ComplexValue>>(new NumberExpression<Expression<ComplexValue>>(new NumberExpression<ComplexValue>(new ComplexValue("x")))
    , new NumberExpression<Expression<ComplexValue>>(new NumberExpression<ComplexValue>(new ComplexValue(5))));

var producto = new MultiplicationExpression<Expression<ComplexValue>>(new NumberExpression<Expression<ComplexValue>>(new NumberExpression<ComplexValue>(new ComplexValue(10)))
    , new NumberExpression<Expression<ComplexValue>>(new NumberExpression<ComplexValue>(new ComplexValue(15))));
var expr5 = new AdditionExpression<Expression<ComplexValue>>(suma, producto);

// Evaluación de la expresión
Expression<ComplexValue> result = expr5.Accept(algebra);
Console.WriteLine(result);  // Resultado: 20






Console.ReadLine();

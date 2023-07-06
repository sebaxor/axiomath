
using AxioMath.Core;
using AxioMath.Solvers;

var a = new RealNumber(10);
var b = new RationalNumber(-31,3);
var c = new IntegerNumber(2);
var d = new RealNumber(65);
var e = new RealNumber(12);
var f = new RationalNumber(20,7);
var g = new RealNumber(100);


var sum = new Sum(g, a, b);
var product = new Multiply(sum, d);
var sum2 = new Sum(sum, product);
var expon = new Exponentiate(sum2, c);

Console.WriteLine(expon.ToString());
//TODO 
var result = Solver.Solve(expon);
Console.WriteLine(result.ToString());
Console.ReadLine();

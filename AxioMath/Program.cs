
using AxioMath.Core;
using AxioMath.Solvers;

var a = new RealNumber(15);
var b = new RealNumber(34);
var x = new Variable<RealNumber>("x");

var sum = new Sum(x, a, b);

Console.WriteLine(sum.ToString());

var result = ExpressionSolver.Solve(sum);
Console.WriteLine(result.ToString());
Console.ReadLine();


using AxioMath.Core;
using AxioMath.Geometry;
using AxioMath.Solvers;
using AxioMath.Utils;
using System.Reflection.Metadata;

var builder = new ExpressionBuilder();
var term1 = builder.Constant(3).Multiply(2).Divide(6).Sum(20).Divide(2).Generate();
var term2 = builder.Variable("x").Sum(5).Divide(2).Generate();
var term1 = builder.Constant(3).Multiply(2).Divide(6).Sum(20).Divide(2).Generate();

Console.WriteLine(expression);
Console.WriteLine(expression.Evaluate());


var resolv = ExpressionSolver.Solve(expression);
Console.WriteLine("Solucion");
Console.WriteLine(resolv.Evaluate());



//Console.WriteLine(LatexConverter.ConvertToLatex(cocient.Evaluate()));


//var space = new Space(2);
//var pointA = new Point(space,"A", 0,0); //Be point A
//var pointB = new Point(space,"B",1,1);
//var rectL = new Rect(space, "l", pointA, pointB);

//Console.WriteLine(space.Points.Count);
//Console.WriteLine(space.GeometricPlaces.Count);
Console.ReadLine();

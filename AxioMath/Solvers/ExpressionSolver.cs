using AxioMath.Core;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxioMath.Solvers
{
    public class ExpressionSolver
    {



        private static IExpression SolveInternal(IExpression expression1, IExpression expression2,
            Func<INumber, INumber, IExpression> operation, Func<IExpression, IExpression, IExpression> builder)
        {

            IExpression? expr1 = expression1 as INumber;
            IExpression? expr2 = expression2 as INumber;

            if (expr1 != null && expr2 != null)
                return Sum(expr1 as INumber, expr2 as INumber);


            if (expr1 == null && (expression1 is Sum || expression1 is Multiply || expression1 is Exponentiate || expression1 is Divide))
            {
                expr1 = Solve(expression1);
            }
            if (expr2 == null && (expression2 is Sum || expression1 is Multiply || expression1 is Exponentiate || expression1 is Divide))
            {
                expr2 = Solve(expression2);
            }

            if (expr1 is INumber t1Const && expr2 is INumber t2Const)
            {
                return operation.Invoke(t1Const, t2Const);
            }

            return builder(expr1 ?? expression1, expr2 ?? expression2);
        }

        public static IExpression Solve(IExpression expression)
        {
            if (expression is Sum sum)
            {
                if (sum.Term1 is IVariable && sum.Term2 is IVariable)
                    return sum;
                return SolveInternal(sum.Term1, sum.Term2, ExpressionSolver.Sum, (e1, e2) => new Sum(e1, e2));
            }
            else if (expression is Multiply mult)
            {
                if (mult.Factor1 is IVariable && mult.Factor2 is IVariable)
                    return mult;

                return SolveInternal(mult.Factor1, mult.Factor2, ExpressionSolver.Multiply, (e1, e2) => new Multiply(e1, e2));

            }
            else if (expression is Divide div)
            {
                if (div.Numerator is IVariable && div.Denominator is IVariable)
                    return div;

                return SolveInternal(div.Numerator, div.Denominator, ExpressionSolver.Divide, (e1, e2) => new Divide(e1, e2));

            }
            else if (expression is Exponentiate expo)
            {
                if (expo.Base is IVariable && expo.Exponent is IVariable)
                    return expo;

                return SolveInternal(expo.Base, expo.Exponent, ExpressionSolver.Exponentiate, (e1, e2) => new Exponentiate(e1, e2));
            }
            else
            {
                return expression;
            }
        }


        private static IExpression Sum(INumber term1, INumber term2)
        {
            return new RealNumber(term1.RealPart + term2.RealPart);
        }

        private static IExpression Multiply(INumber factor1, INumber factor2)
        {
            return new RealNumber(factor1.RealPart * factor2.RealPart);
        }

        private static IExpression Exponentiate(INumber baseExp, INumber exponent)
        {
            return new RealNumber(Math.Pow(baseExp.RealPart, exponent.RealPart));
        }


        private static IExpression Divide(INumber numerator, INumber denominator)
        {
            return new RealNumber(numerator.RealPart / denominator.RealPart);
        }


    }
}

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

      

        public static IExpression Solve(IExpression expression)
        {
            if (expression is Sum sum)
            {
                if (sum.Term1 is IVariable && sum.Term2 is IVariable)
                    return sum;

                IExpression term1 = sum.Term1 as IConstant;
                IExpression term2 = sum.Term2 as IConstant;

                if (term1 != null && term2 != null)
                    return Sum(term1 as IConstant, term2 as IConstant);


                if (term1 == null && (sum.Term1 is Sum || sum.Term1 is Multiply || sum.Term1 is Exponentiate || sum.Term1 is Divide))
                {
                    term1 = Solve(sum.Term1);
                }
                if (term2 == null && (sum.Term2 is Sum || sum.Term2 is Multiply || sum.Term2 is Exponentiate || sum.Term2 is Divide))
                {
                    term2 = Solve(sum.Term2);
                }

                if (term1 is IConstant t1Const && term2 is IConstant t2Const)
                {
                    return Sum(t1Const, t2Const);
                }

                return new Sum(term1??sum.Term1, term2 ?? sum.Term2);
            }
            else if (expression is Multiply mult)
            {

                if (mult.Factor1 is IVariable && mult.Factor2 is IVariable)
                    return mult;

                IExpression term1 = mult.Factor1 as IConstant;
                IExpression term2 = mult.Factor2 as IConstant;

                if (term1 != null && term2 != null)
                    return Multiply(term1 as IConstant, term2 as IConstant);


                if (term1 == null && (mult.Factor1 is Sum || mult.Factor1 is Multiply || mult.Factor1 is Exponentiate || mult.Factor1 is Divide))
                {
                    term1 = Solve(mult.Factor1);
                }
                if (term2 == null && (mult.Factor2 is Sum || mult.Factor2 is Multiply || mult.Factor2 is Exponentiate || mult.Factor2 is Divide))
                {
                    term2 = Solve(mult.Factor2);
                }

                if (term1 is IConstant t1Const && term2 is IConstant t2Const)
                {
                    return Multiply(t1Const, t2Const);
                }

                return new Multiply(term1 ?? mult.Factor1, term2 ?? mult.Factor2);
            }
            else if (expression is Divide div)
            {
                if (div.Numerator is IVariable && div.Denominator is IVariable)
                    return div;

                IExpression term1 = div.Numerator as IConstant;
                IExpression term2 = div.Denominator as IConstant;

                if (term1 != null && term2 != null)
                    return Divide(term1 as IConstant, term2 as IConstant);


                if (term1 == null && (div.Numerator is Sum || div.Numerator is Multiply || div.Numerator is Exponentiate || div.Numerator is Divide))
                {
                    term1 = Solve(div.Numerator);
                }
                if (term2 == null && (div.Denominator is Sum || div.Denominator is Multiply || div.Denominator is Exponentiate || div.Denominator is Divide))
                {
                    term2 = Solve(div.Denominator);
                }

                if (term1 is IConstant t1Const && term2 is IConstant t2Const)
                {
                    return Divide(t1Const, t2Const);
                }

                return new Divide(term1 ?? div.Numerator, term2 ?? div.Denominator);
            }
            else if (expression is Exponentiate expo)
            {
                if (expo.Base is IVariable && expo.Exponent is IVariable)
                    return expo;

                IExpression term1 = expo.Base as IConstant;
                IExpression term2 = expo.Exponent as IConstant;

                if (term1 != null && term2 != null)
                    return Exponentiate(term1 as IConstant, term2 as IConstant);


                if (term1 == null && (expo.Base is Sum || expo.Base is Multiply || expo.Base is Exponentiate || expo.Base is Divide))
                {
                    term1 = Solve(expo.Base);
                }
                if (term2 == null && (expo.Exponent is Sum || expo.Exponent is Multiply || expo.Exponent is Exponentiate || expo.Exponent is Divide))
                {
                    term2 = Solve(expo.Exponent);
                }

                if (term1 is IConstant t1Const && term2 is IConstant t2Const)
                {
                    return Exponentiate(t1Const, t2Const);
                }

                return new Exponentiate(term1 ?? expo.Base, term2 ?? expo.Exponent);
            }
            else
            {
                return expression;
            }
        }


        private static IExpression Sum(IConstant term1, IConstant term2)
        {
            return new RealConstant<RealNumber>("result", new RealNumber(term1.Value.RealPart + term2.Value.RealPart));
        }

        private static IExpression Multiply(IConstant factor1, IConstant factor2)
        {
            return new RealConstant<RealNumber>("result", new RealNumber(factor1.Value.RealPart * factor2.Value.RealPart));
        }

        private static IExpression Exponentiate(IConstant baseExp, IConstant exponent)
        {
            return new RealConstant<RealNumber>("result", new RealNumber(Math.Pow(baseExp.Value.RealPart, exponent.Value.RealPart)));
        }


        private static IExpression Divide(IConstant numerator, IConstant denominator)
        {
            return new RealConstant<RealNumber>("result", new RealNumber(numerator.Value.RealPart / denominator.Value.RealPart));
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AxioMath.Core
{


    public interface IExpression
    {

    }

    public class Variable<T> : IExpression, IVariable where T : INumber
    {
        public Variable(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public string? Evaluate()
        {
            return Name;
        }

        public override string ToString()
        {
            return Name;
        }


    }

   

    public interface IVariable
    {
        string Name { get; }
    }



    public class Sum : IExpression
    {

        public Sum(params IExpression[] terms)
        {
            Term1 = terms[0];
            var remaining = terms.Skip(1).ToArray();
            Term2 = remaining.Length == 1 ? remaining[0] : new Sum(remaining);
        }
        public Sum(IExpression term1, IExpression term2)
        {
            Term1 = term1;
            Term2 = term2;
        }
        public IExpression Term1 { get; set; }
        public IExpression Term2 { get; set; }

      

        public override string ToString()
        {
            return $"({Term1})+({Term2})";
        }



    }

    public class Multiply : IExpression
    {

        public Multiply(params IExpression[] factors)
        {
            Factor1 = factors[0];
            var remaining = factors.Skip(1).ToArray();
            Factor2 = remaining.Length == 1 ? remaining[0] : new Multiply(remaining);
        }
        public Multiply(IExpression factor1, IExpression factor2)
        {
            Factor1 = factor1;
            Factor2 = factor2;
        }
        public IExpression Factor1 { get; set; }
        public IExpression Factor2 { get; set; }

       

        public override string ToString()
        {
            return $"({Factor1})*({Factor2})";
        }




    }

    public class Exponentiate : IExpression
    {
        public Exponentiate(IExpression baseExpression, IExpression exponent)
        {
            Base = baseExpression;
            Exponent = exponent;
        }
        public IExpression Base { get; set; }
        public IExpression Exponent { get; set; }

       

        public override string ToString()
        {
            return $"({Base})^({Exponent})";
        }


    }

    public class Divide : IExpression
    {
        public Divide(IExpression numerator, IExpression denominator)
        {
            Numerator = numerator;
            Denominator = denominator;
        }
        public IExpression Numerator { get; set; }
        public IExpression Denominator { get; set; }

      

        public override string ToString()
        {
            return $"({Numerator})/({Denominator})";
        }


    }


    public class Equals : IExpression
    {
        public Equals(params IExpression[] expressions)
        {
            Expressions = new List<IExpression>(expressions);
        }
        public List<IExpression> Expressions { get; set; }

      

        public override string ToString()
        {
            return string.Join("=", Expressions.Select(x => $"({x.ToString()})"));
        }


    }

}

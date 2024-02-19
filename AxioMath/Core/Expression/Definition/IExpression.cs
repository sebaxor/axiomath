using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AxioMath.Core.Expression.Definition
{


    public interface IExpression
    {
        string? Evaluate();
    }

}

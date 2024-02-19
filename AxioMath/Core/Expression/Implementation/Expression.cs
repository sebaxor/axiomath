using AxioMath.Core.Expression.Definition;

namespace AxioMath.Core.Expression.Implementation
{
    // Clase base abstracta para las expresiones
    public abstract class Expression<T>
    {
        public abstract T Accept(IAlgebra<T> algebra);
    }
}



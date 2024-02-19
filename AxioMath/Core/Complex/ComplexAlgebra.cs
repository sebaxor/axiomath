using AxioMath.Core.Expression.Definition;

namespace AxioMath.Core.Complex
{
    // Ejemplo de uso


    // Implementación personalizada de IAlgebra para enteros


    public class ComplexAlgebra : IAlgebra<ComplexValue>
    {
        public ComplexValue Add(ComplexValue left, ComplexValue right)
        {
            return left + right;
        }

        public ComplexValue Multiply(ComplexValue left, ComplexValue right)
        {
            return left * right;
        }

        public ComplexValue Number(ComplexValue value)
        {
            return value;
        }
    }
}



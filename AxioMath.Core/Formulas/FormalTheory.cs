
using System.Linq;

namespace AxioMath.Core.Formulas;
public class FormalTheory
{
    public FormalSystem System { get; }
    public IEnumerable<Theorem> Theorems => System.DeriveTheorems();

    public FormalTheory(FormalSystem system)
    {
        System = system;
    }

    public bool Proves(Formula formula) => Theorems.Any(t => t.Formula.Equals(formula));
}


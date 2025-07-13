

namespace AxioMath.Core.Formulas;
public class FormalTheory
{
    public FormalSystem System { get; }
    public IEnumerable<Formula> Theorems => System.DeriveTheorems();

    public FormalTheory(FormalSystem system)
    {
        System = system;
    }

    public bool Proves(Formula formula) => Theorems.Contains(formula);
}


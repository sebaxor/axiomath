

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

    public void PrintTheoremsByRule()
    {
        var grouped = Theorems
            .Where(t => t.Rule != null)
            .GroupBy(t => t.Rule!.GetType().Name)
            .OrderBy(g => g.Key);

        foreach (var group in grouped)
        {
            Console.WriteLine($"== {group.Key} ==");

            foreach (var theorem in group)
            {
                Console.WriteLine($"• {theorem}");
            }

            Console.WriteLine();
        }
    }


}


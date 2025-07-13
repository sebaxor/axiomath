
using AxioMath.Core.Syntax;

namespace AxioMath.Core.Formulas;
public class FormalSystem
{
    public FormalLanguage Language { get; }
    public IReadOnlyCollection<Formula> Axioms { get; }
    public IReadOnlyCollection<IDeductionRule> DeductionRules { get; }

    public FormalSystem(FormalLanguage language, IEnumerable<Formula> axioms, IEnumerable<IDeductionRule> rules)
    {
        Language = language;
        Axioms = axioms.ToList();
        DeductionRules = rules.ToList();
    }

    public IEnumerable<Formula> DeriveTheorems()
    {
        var known = new HashSet<Formula>(Axioms);
        var changed = true;

        while (changed)
        {
            changed = false;
            foreach (var rule in DeductionRules)
            {
                var newFormulas = rule.Apply(known, Language).Where(f => !known.Contains(f)).ToList();
                if (newFormulas.Any())
                {
                    foreach (var f in newFormulas)
                        known.Add(f);
                    changed = true;
                }
            }
        }

        return known;
    }
}



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

    public IEnumerable<Theorem> DeriveTheorems()
    {
        var known = new Dictionary<Formula, Theorem>();
        foreach (var ax in Axioms)
        {
            if (!known.ContainsKey(ax))
                known[ax] = new Theorem(ax);
        }

        var changed = true;

        while (changed)
        {
            changed = false;
            foreach (var rule in DeductionRules)
            {
                var results = rule
                    .Apply(known.Keys, Language)
                    .Where(r => !known.ContainsKey(r.conclusion))
                    .ToList();

                foreach (var (conclusion, premises) in results)
                {
                    var premiseTheorems = premises.Select(p => known[p]).ToList();
                    known[conclusion] = new Theorem(conclusion, rule, premiseTheorems);
                    changed = true;
                }
            }
        }

        return known.Values;
    }
}


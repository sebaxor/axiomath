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
        // Permitimos múltiples deducciones por fórmula si son por distintas reglas
        var known = new List<Theorem>();

        // Registrar axiomas
        foreach (var ax in Axioms)
        {
            if (!known.Any(t => t.Formula.Equals(ax)))
                known.Add(new Theorem(ax));
        }

        var changed = true;

        while (changed)
        {
            changed = false;
            foreach (var rule in DeductionRules)
            {
                var currentFormulas = known.Select(t => t.Formula).ToList();

                var results = rule
                    .Apply(currentFormulas, Language)
                    .Where(result =>
                        !known.Any(t =>
                            t.Formula.Equals(result.conclusion) &&
                            t.Rule?.GetType() == rule.GetType()
                        ))
                    .ToList();

                foreach (var (conclusion, premises) in results)
                {
                    var premiseTheorems = premises
                        .Select(p => known.First(t => t.Formula.Equals(p)))
                        .ToList();

                    known.Add(new Theorem(conclusion, rule, premiseTheorems));
                    changed = true;
                }
            }
        }

        return known;
    }
}

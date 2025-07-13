using System.Collections.Generic;

namespace AxioMath.Core.Formulas;

/// <summary>
/// Represents a derived theorem along with the rule and premises used to derive it.
/// </summary>
public class Theorem
{
    public Formula Formula { get; }
    public IDeductionRule? Rule { get; }
    public IReadOnlyList<Theorem> Premises { get; }

    public Theorem(Formula formula, IDeductionRule? rule = null, IReadOnlyList<Theorem>? premises = null)
    {
        Formula = formula;
        Rule = rule;
        Premises = premises ?? new List<Theorem>();
    }
}

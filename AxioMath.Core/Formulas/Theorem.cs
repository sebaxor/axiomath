using System.Text;

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
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Formula: {Formula}");

        if (Rule != null)
        {
            sb.AppendLine($"Rule: {Rule.GetType().Name}");
        }
        else
        {
            sb.AppendLine("Rule: (axiom or initial)");
        }

        if (Premises.Count > 0)
        {
            sb.AppendLine("Premises:");
            for (int i = 0; i < Premises.Count; i++)
            {
                sb.AppendLine($"  [{i + 1}] {Premises[i].Formula}");
            }
        }
        else
        {
            sb.AppendLine("Premises: none");
        }

        return sb.ToString();
    }
}

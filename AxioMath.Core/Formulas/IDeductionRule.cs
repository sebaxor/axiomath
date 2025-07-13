using AxioMath.Core.Syntax;

namespace AxioMath.Core.Formulas;

public interface IDeductionRule
{
    /// <summary>Applies the rule to a set of premises and produces conclusions if applicable.</summary>
    IEnumerable<Formula> Apply(IEnumerable<Formula> premises, FormalLanguage language);
}

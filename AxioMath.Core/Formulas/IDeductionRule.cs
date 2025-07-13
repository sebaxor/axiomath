using AxioMath.Core.Syntax;

namespace AxioMath.Core.Formulas;

public interface IDeductionRule
{
    /// <summary>
    /// Applies the rule to a set of premises and produces conclusions if applicable.
    /// The returned tuple contains the derived formula and the premises used.
    /// </summary>
    IEnumerable<(Formula conclusion, IReadOnlyList<Formula> premises)> Apply(IEnumerable<Formula> premises, FormalLanguage language);
}

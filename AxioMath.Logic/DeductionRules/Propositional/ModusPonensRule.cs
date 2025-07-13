using AxioMath.Core.FormalSystems;
using AxioMath.Core.Formulas;
using AxioMath.Core.Syntax;


namespace AxioMath.Logic.DeductionRules.Propositional;

  
public class ModusPonensRule : IDeductionRule
{
    public IEnumerable<(Formula conclusion, IReadOnlyList<Formula> premises)> Apply(IEnumerable<Formula> premises, FormalLanguage language)
    {
        var list = premises.ToList();

        foreach (var p in list)
        {
            foreach (var implication in list)
            {
                if (implication.Root is BinaryNode imp &&
                    imp.Operator == "→" &&
                    AreEquivalent(imp.Left, p.Root))
                {
                    var content = Serialize(imp.Right);

                    var result = TryCreateFormula(language, content, imp.Right);
                    if (result != null)
                        yield return (result, new List<Formula> { p, implication });
                }
            }
        }
    }

    private Formula? TryCreateFormula(FormalLanguage language, string content, FormulaNode root)
    {
        try
        {
            if (language.BelongsToLanguage(content))
                return new Formula(content, root);
            else
                Console.WriteLine($"Skipped invalid formula: {content}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating formula from: {content} → {ex.Message}");
        }

        return null;
    }

    private bool AreEquivalent(FormulaNode a, FormulaNode b)
    {
        if (a is AtomNode atomA && b is AtomNode atomB)
            return atomA.Name == atomB.Name;
        if (a is UnaryNode una && b is UnaryNode unb)
            return una.Operator == unb.Operator && AreEquivalent(una.Operand, unb.Operand);
        if (a is BinaryNode bina && b is BinaryNode binb)
            return bina.Operator == binb.Operator &&
                   AreEquivalent(bina.Left, binb.Left) &&
                   AreEquivalent(bina.Right, binb.Right);
        return false;
    }

    private string Serialize(FormulaNode node)
    {
        return node switch
        {
            AtomNode a => a.Name,
            UnaryNode u => $"{u.Operator}{Serialize(u.Operand)}",
            BinaryNode b => $"({Serialize(b.Left)} {b.Operator} {Serialize(b.Right)})",
            _ => throw new NotSupportedException()
        };
    }
}


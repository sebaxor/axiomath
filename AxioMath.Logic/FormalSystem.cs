

using AxioMath.Core.Formulas;
using AxioMath.Core.Syntax;

namespace FormalSystems;

public interface IDeductionRule
{
    /// <summary>Aplica la regla sobre un conjunto de premisas y produce conclusiones si aplica.</summary>
    IEnumerable<Formula> Apply(IEnumerable<Formula> premises, FormalLanguage language);
}

public class ModusPonensRule : IDeductionRule
{
    public IEnumerable<Formula> Apply(IEnumerable<Formula> premises, FormalLanguage language)
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
                        yield return result;
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

public interface IInterpretation
{
    /// <summary>Evalúa si una fórmula es verdadera bajo esta interpretación.</summary>
    bool IsTrue(Formula formula);
}

public class Model
{
    public IInterpretation Interpretation { get; }

    public Model(IInterpretation interpretation)
    {
        Interpretation = interpretation;
    }

    public bool Satisfies(Formula formula) => Interpretation.IsTrue(formula);
}

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

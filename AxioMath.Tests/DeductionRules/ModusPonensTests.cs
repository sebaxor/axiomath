// Tests/DeductionRules/ModusPonensRuleTests.cs
using AxioMath.Core.Formulas;
using AxioMath.Core.Syntax;
using AxioMath.Logic.Propositional;
using AxioMath.Logic.Propositional.DeductionRules;
using Xunit;

namespace AxioMath.Tests.DeductionRules;

public class ModusPonensRuleTests
{
    // Build language including symbols used in all test cases
    private readonly FormalLanguage _lang = PropositionalLanguageBuilder.Build(new[] { "p", "q", "a", "b", "c", "a", "b", "c", "p", "q", "r", "x", "y" });

    #region ✅ Rule-Only: Apply Method Tests

    [Fact]
    public void Should_Apply_To_Basic_Case()
    {
        var p = _lang.CreateFormula("p");
        var pImpQ = _lang.CreateFormula("(p → q)");
        var rule = new ModusPonensRule();

        var results = rule.Apply(new[] { p, pImpQ }, _lang).ToList();
        var derived = results.Select(r => r.conclusion);

        Assert.Contains(_lang.CreateFormula("q"), derived);
    }

    [Fact]
    public void Should_Not_Apply_If_P_Missing()
    {
        var pImpQ = _lang.CreateFormula("(p → q)");
        var rule = new ModusPonensRule();

        var results = rule.Apply(new[] { pImpQ }, _lang).ToList();

        Assert.Empty(results);
    }

    [Fact]
    public void Should_Apply_Multiple_Implications()
    {
        var a = _lang.CreateFormula("a");
        var aImpB = _lang.CreateFormula("(a → b)");
        var aImpC = _lang.CreateFormula("(a → c)");
        var rule = new ModusPonensRule();

        var results = rule.Apply(new[] { a, aImpB, aImpC }, _lang).ToList();
        var derived = results.Select(r => r.conclusion);

        Assert.Contains(_lang.CreateFormula("b"), derived);
        Assert.Contains(_lang.CreateFormula("c"), derived);
        Assert.Equal(2, derived.Count());
    }

    #endregion

    #region ✅ Theory Integration: FormalSystem + FormalTheory

    [Fact]
    public void ModusPonens_Should_Derive_Q_From_P_And_Implication()
    {
        var p = _lang.CreateFormula("p");
        var imp = _lang.CreateFormula("(p → q)");
        var system = new FormalSystem(_lang, new[] { p, imp }, new[] { new ModusPonensRule() });
        var theory = new FormalTheory(system);

        var q = _lang.CreateFormula("q");
        var theorem = theory.Theorems.FirstOrDefault(t => t.Formula.Equals(q));
        Assert.NotNull(theorem);
        Assert.IsType<ModusPonensRule>(theorem!.Rule);
        Assert.Equal(2, theorem.Premises.Count);
        Assert.Contains(theorem.Premises, th => th.Formula.Equals(p));
        Assert.Contains(theorem.Premises, th => th.Formula.Equals(imp));
    }

    [Fact]
    public void ModusPonens_Should_Not_Derive_Q_Without_P()
    {
        var imp = _lang.CreateFormula("(p → q)");
        var system = new FormalSystem(_lang, new[] { imp }, new[] { new ModusPonensRule() });
        var theory = new FormalTheory(system);

        var q = _lang.CreateFormula("q");
        Assert.DoesNotContain(theory.Theorems, t => t.Formula.Equals(q));
    }

    #endregion


    #region 🧪 Edge Cases: Modus Ponens

    [Fact]
    public void Should_Apply_With_Complex_Antecedent()
    {
        var complexAntecedent = _lang.CreateFormula("(a ∧ b)");
        var implication = _lang.CreateFormula("((a ∧ b) → c)");
        var rule = new ModusPonensRule();

        var results = rule.Apply(new[] { complexAntecedent, implication }, _lang).ToList();

        Assert.Contains(_lang.CreateFormula("c"), results.Select(r => r.conclusion));
    }

    [Fact]
    public void Should_Work_Regardless_Of_Premise_Order()
    {
        var p = _lang.CreateFormula("p");
        var implication = _lang.CreateFormula("(p → q)");
        var rule = new ModusPonensRule();

        var results = rule.Apply(new[] { implication, p }, _lang).ToList();

        Assert.Contains(_lang.CreateFormula("q"), results.Select(r => r.conclusion));
    }

    [Fact]
    public void Should_Ignore_Irrelevant_Premises()
    {
        var p = _lang.CreateFormula("p");
        var implication = _lang.CreateFormula("(p → q)");
        var unrelated = _lang.CreateFormula("r");
        var rule = new ModusPonensRule();

        var results = rule.Apply(new[] { p, implication, unrelated }, _lang).ToList();

        Assert.Contains(_lang.CreateFormula("q"), results.Select(r => r.conclusion));
        Assert.Single(results); // Solo una conclusión esperada
    }

    [Fact]
    public void Should_Not_Produce_Duplicated_Conclusions()
    {
        var p = _lang.CreateFormula("p");
        var implication = _lang.CreateFormula("(p → q)");
        var rule = new ModusPonensRule();

        var results = rule.Apply(new[] { p, implication, implication }, _lang).ToList();

        Assert.Single(results);
        Assert.Equal(1, results.Select(r => r.conclusion.Content).Distinct().Count());

    }

    [Fact]
    public void Should_Not_Apply_With_Unrelated_Implication()
    {
        var p = _lang.CreateFormula("p");
        var unrelatedImp = _lang.CreateFormula("(x → y)");
        var rule = new ModusPonensRule();

        var results = rule.Apply(new[] { p, unrelatedImp }, _lang).ToList();

        Assert.Empty(results);
    }

    #endregion
}

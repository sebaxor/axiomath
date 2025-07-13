using AxioMath.Core.Formulas;
using AxioMath.Core.Syntax;
using AxioMath.Logic.DeductionRules.Propositional;



var language = PropositionalLanguageBuilder.Build();

var axioms = new[]
{
    language.CreateFormula("(p → q)"),      // MP
    language.CreateFormula("p"),            // MP
    language.CreateFormula("¬q"),           // MT
    language.CreateFormula("(p ∨ r)"),      // ∨E
    language.CreateFormula("(r → s)"),      // ∨E
    language.CreateFormula("(p → s)")       // ∨E
};

// Reglas
var rules = new IDeductionRule[]
{
    new ModusPonensRule(),
    new ModusTollensRule(),
    new DisjunctionEliminationRule()
};

var system = new FormalSystem(language, axioms, rules);
var theory = new FormalTheory(system);

// Mostrar resultados
foreach (var t in theory.Theorems)
    Console.WriteLine($"Teorema: {t}");

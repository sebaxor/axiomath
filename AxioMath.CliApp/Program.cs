using AxioMath.Core.Formulas;
using AxioMath.Core.Syntax;
using AxioMath.Logic.DeductionRules.Propositional;

// Símbolos
var not = new Symbol("¬", true);
var and = new Symbol("∧", true);
var or = new Symbol("∨", true);
var implies = new Symbol("→", true);
var iff = new Symbol("↔", true);
var lparen = new Symbol("(", true);
var rparen = new Symbol(")", true);

var p = new Symbol("p", true);
var q = new Symbol("q", true);
var r = new Symbol("r", true);
var s = new Symbol("s", true);

var atom = new Symbol("Atom", false);
var formula = new Symbol("Formula", false);

// Gramática
var grammar = new Grammar(formula);
grammar.AddRule(atom, new[] { p }, RuleInterpretation.Atom);
grammar.AddRule(atom, new[] { q }, RuleInterpretation.Atom);
grammar.AddRule(atom, new[] { r }, RuleInterpretation.Atom);
grammar.AddRule(atom, new[] { s }, RuleInterpretation.Atom);

grammar.AddRule(formula, new[] { atom }, RuleInterpretation.Atom);
grammar.AddRule(formula, new[] { not, formula }, RuleInterpretation.Unary, "¬");
grammar.AddRule(formula, new[] { lparen, formula, and, formula, rparen }, RuleInterpretation.Binary, "∧");
grammar.AddRule(formula, new[] { lparen, formula, or, formula, rparen }, RuleInterpretation.Binary, "∨");
grammar.AddRule(formula, new[] { lparen, formula, implies, formula, rparen }, RuleInterpretation.Binary, "→");
grammar.AddRule(formula, new[] { lparen, formula, iff, formula, rparen }, RuleInterpretation.Binary, "↔");

var language = new FormalLanguage(grammar);

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

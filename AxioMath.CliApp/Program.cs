

using AxioMath.Core.Syntax;
using FormalSystems;


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

var atom = new Symbol("Atom", false);
var formula = new Symbol("Formula", false);

var grammar = new Grammar(formula);

// Atom → p | q | r
grammar.AddRule(atom, new[] { p }, RuleInterpretation.Atom);
grammar.AddRule(atom, new[] { q }, RuleInterpretation.Atom);
grammar.AddRule(atom, new[] { r }, RuleInterpretation.Atom);

// Formula → Atom
grammar.AddRule(formula, new[] { atom }, RuleInterpretation.Atom);

// Formula → ¬ Formula
grammar.AddRule(formula, new[] { not, formula }, RuleInterpretation.Unary, "¬");

// Formula → ( Formula ∧ Formula )
grammar.AddRule(formula, new[] { lparen, formula, and, formula, rparen }, RuleInterpretation.Binary, "∧");

// Formula → ( Formula ∨ Formula )
grammar.AddRule(formula, new[] { lparen, formula, or, formula, rparen }, RuleInterpretation.Binary, "∨");

// Formula → ( Formula → Formula )
grammar.AddRule(formula, new[] { lparen, formula, implies, formula, rparen }, RuleInterpretation.Binary, "→");

// Formula → ( Formula ↔ Formula )
grammar.AddRule(formula, new[] { lparen, formula, iff, formula, rparen }, RuleInterpretation.Binary, "↔");

var language = new FormalLanguage(grammar);

var f1 = language.CreateFormula("p");
var f2 = language.CreateFormula("(p → q)");


var axioms = new[] { f1, f2 };
var rules = new IDeductionRule[] { new ModusPonensRule() };

var system = new FormalSystem(language, axioms, rules);
var theory = new FormalTheory(system);

foreach (var t in theory.Theorems)
    Console.WriteLine("Teorema: " + t);

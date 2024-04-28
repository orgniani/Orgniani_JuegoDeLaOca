
using System.Linq;

public class ThrowAgain : BoardRule
{
    private int[] rules;

    public ThrowAgain(int[] newRules)
    {
        rules = newRules;
    }

    public override bool IsCompatible(int posicionJugador)
    {
        return rules.ToList().Contains(posicionJugador);
    }

    public override BoardRuleResult Act(int idJugador, int posicionJugador)
    {
        return new BoardRuleResult(posicionJugador, idJugador == 2, idJugador == 1, " y tira de nuevo");
    }
}
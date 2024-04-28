
using System.Linq;

public class LooseTurn : BoardRule
{
    private int[] rules;

    public LooseTurn(int[] newRules)
    {
        rules = newRules;
    }

    public override bool IsCompatible(int posicionJugador)
    {
        return rules.ToList().Contains(posicionJugador);
    }

    public override BoardRuleResult Act(int idJugador, int posicionJugador)
    {
        return new BoardRuleResult(posicionJugador, idJugador == 1, idJugador == 2, " y pierde un turno");
    }
}


using System.Collections.Generic;

public class GoBackward : BoardRule
{
    private Dictionary<int, int> rules;

    public GoBackward(Dictionary<int, int> newRules)
    {
        rules = newRules;
    }

    public override bool IsCompatible(int posicionJugador)
    {
        return rules.ContainsKey(posicionJugador);
    }

    public override BoardRuleResult Act(int idJugador, int posicionJugador)
    {
        int nuevaPos = rules[posicionJugador];
        return new BoardRuleResult(nuevaPos, false, false, " y retrocede al casillero " + nuevaPos);
    }
}
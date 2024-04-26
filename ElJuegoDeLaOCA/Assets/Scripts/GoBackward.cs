
using System.Collections.Generic;

public class GoBackward : BoardRule
{
    private Dictionary<int, int> rules;

    public GoBackward()
    {
        //TAREA: RECIBIR EL DICCIONARO POR PARAMETRO ACA
        rules = new Dictionary<int, int>();
        rules.Add(12, 1);
        rules.Add(25, 9);
        rules.Add(30, 27);
        rules.Add(33, 20);
    }

    public override bool EsCompatible(int posicionJugador)
    {
        return rules.ContainsKey(posicionJugador);
    }

    public override BoardRuleResult Accionar(int idJugador, int posicionJugador)
    {
        int nuevaPos = rules[posicionJugador];
        return new BoardRuleResult(nuevaPos, false, false, "y retrocede al casillero " + nuevaPos);
    }
}
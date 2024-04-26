
using System.Collections.Generic;
using System.Linq;

public class GoForward : Casillero
{
    private Dictionary<int, int> rules;

    public GoForward()
    {
        //TAREA: RECIBIR EL DICCIONARO POR PARAMETRO ACA
        rules = new Dictionary<int, int>();
        rules.Add(2, 21);
        rules.Add(7, 11);
        rules.Add(14, 22);
        rules.Add(22, 24);
    }

    public override bool EsCompatible(int posicionJugador)
    {
        return rules.ContainsKey(posicionJugador);
    }

    public override ResultadoDeTirada Accionar(int idJugador, int posicionJugador)
    {
        int nuevaPos = rules[posicionJugador];
        return new ResultadoDeTirada(nuevaPos, false, false, "y avanza al casillero " + nuevaPos);
    }
}

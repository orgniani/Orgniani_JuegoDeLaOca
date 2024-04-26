
using System.Linq;

public class LooseTurn : Casillero
{
    private int[] rules;

    public LooseTurn()
    {
        //TAREA: RECIBIR EL ARRAY POR PARAMETRO ACA
        rules = new int[]{5, 18};
    }

    public override bool EsCompatible(int posicionJugador)
    {
        return rules.ToList().Contains(posicionJugador);
    }

    public override ResultadoDeTirada Accionar(int idJugador, int posicionJugador)
    {
        return new ResultadoDeTirada(posicionJugador, idJugador == 1, idJugador == 2, "y pierde un turno");
    }
}

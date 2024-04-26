
using System.Linq;

public class ThrowAgain : Casillero
{
    private int[] rules;

    public ThrowAgain()
    {
        //TAREA: RECIBIR EL ARRAY POR PARAMETRO ACA
        rules = new int[] { 31 };
    }

    public override bool EsCompatible(int posicionJugador)
    {
        return rules.ToList().Contains(posicionJugador);
    }

    public override ResultadoDeTirada Accionar(int idJugador, int posicionJugador)
    {
        return new ResultadoDeTirada(posicionJugador, idJugador == 2, idJugador == 1, "y tira de nuevo");
    }
}
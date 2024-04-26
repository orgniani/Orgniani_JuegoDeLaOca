public class ResultadoDeTirada
{
    public int nuevoCasillero;
    public bool jugador1PierdeTurno;
    public bool jugador2PierdeTurno;
    public string textoQuePaso;

    public ResultadoDeTirada(int nuevoCasillero, bool jugador1PierdeTurno, bool jugador2PierdeTurno, string textoQuePaso)
    {
        this.nuevoCasillero = nuevoCasillero;
        this.jugador1PierdeTurno = jugador1PierdeTurno;
        this.jugador2PierdeTurno = jugador2PierdeTurno;
        this.textoQuePaso = textoQuePaso;
    }

    public ResultadoDeTirada(int posicionJugador)
    {
        nuevoCasillero = posicionJugador;
        jugador1PierdeTurno = false;
        jugador2PierdeTurno = false;
        textoQuePaso = ".";
    }
}

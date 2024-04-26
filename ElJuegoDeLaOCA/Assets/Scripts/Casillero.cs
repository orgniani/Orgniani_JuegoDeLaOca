using System;

public class Casillero
{
    public virtual bool EsCompatible(int posicionJugador)
    {
        throw new NotImplementedException();
    }

    public virtual  ResultadoDeTirada Accionar(int idJugador, int posicionJugador)
    {
        throw new NotImplementedException();
    }
}

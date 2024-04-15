using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private TMP_Text labelCurrentPlayer;
    [SerializeField] private TMP_Text labelWhatHappened;
    [SerializeField] private Board board;
    [SerializeField] private TMP_Text labelDiceResult;

    private bool pierdeTurno1 = false;
    private bool pierdeTurno2 = false;
    private int posicionJugador1 = 1;
    private int posicionJugador2 = 1;
    private int turno = 1;
    private bool done = false;
    
    private int diceResult = 0;
    private bool waitingForDice = false;

    private void Start()
    {
        labelCurrentPlayer.text = "";
        labelWhatHappened.text = "";
        labelDiceResult.text = "?";

        posicionJugador1 = 1;
        posicionJugador2 = 1;
        turno = 1;
        done = false;
        StartCoroutine(PlayTurn());
    }

    private IEnumerator PlayTurn()
    {
        diceResult = 0;
        labelWhatHappened.text = "";

        if (turno == 1)
        {
            labelCurrentPlayer.text = "1";
            if (!pierdeTurno1)
            {
                waitingForDice = true;
                while (diceResult == 0)
                {
                    yield return new WaitForEndOfFrame();
                }
                waitingForDice = false;
                posicionJugador1 = Math.Min(36, posicionJugador1 + diceResult);
                labelWhatHappened.text = "Sacó un " + diceResult.ToString() + " y se mueve al casillero nro " + posicionJugador1.ToString();
                board.MovePlayerToCell(turno, posicionJugador1);
                yield return new WaitForSeconds(1);

                posicionJugador1 = CheckWhatHappens(1, posicionJugador1);
                board.MovePlayerToCell(turno, posicionJugador1);
            }
            else
            {
                labelWhatHappened.text = "había perdido el turno - no juega";
                pierdeTurno1 = false;
            }

            done = posicionJugador1 == 36;
            turno = 2;
        }
        else //if (turno == 2)
        {
            labelCurrentPlayer.text = "2";
            if (!pierdeTurno2)
            {
                waitingForDice = true;
                while (diceResult == 0)
                {
                    yield return new WaitForEndOfFrame();
                }
                waitingForDice = false;
                posicionJugador2 = Math.Min(36, posicionJugador2 + diceResult);
                labelWhatHappened.text = "Sacó un " + diceResult.ToString() + " y se mueve al casillero nro " + posicionJugador2.ToString();
                board.MovePlayerToCell(turno, posicionJugador2);
                yield return new WaitForSeconds(1);

                posicionJugador2 = CheckWhatHappens(2, posicionJugador2);
                board.MovePlayerToCell(turno, posicionJugador2);
            }
            else
            {
                labelWhatHappened.text = "había perdido el turno - no juega";
                pierdeTurno2 = false;
            }

            done = posicionJugador2 == 36;
            turno = 1;
        }

        if (done)
        {
            if (posicionJugador1 == 36)
                labelWhatHappened.text = "Gana el jugador 1 - fin del juego";
            else
                labelWhatHappened.text = "Gana el jugador 2 - fin del juego";
        }
        else
        {
            yield return new WaitForSeconds(2);
            StartCoroutine(PlayTurn());
        }
    }

    private int CheckWhatHappens(int idJugador, int posicionJugador)
    {
        int nuevoCasillero = posicionJugador;

        if (posicionJugador == 2)
        {
            nuevoCasillero = 21;
        }
        else if (posicionJugador == 5 || posicionJugador == 18)
        {
            if (idJugador == 1)
                pierdeTurno1 = true;
            else
                pierdeTurno2 = true;

            labelWhatHappened.text += " y pierde un turno.";
        }
        else if (posicionJugador == 7)
        {
            nuevoCasillero = 11;
        }
        else if (posicionJugador == 12)
        {
            nuevoCasillero = 1;
        }
        else if (posicionJugador == 14)
        {
            nuevoCasillero = 29;
        }
        else if (posicionJugador == 22)
        {
            nuevoCasillero = 24;
        }
        else if (posicionJugador == 25)
        {
            nuevoCasillero = 9;
        }
        else if (posicionJugador == 30)
        {
            nuevoCasillero = 27;
        }
        else if (posicionJugador == 31)
        {
            if (idJugador == 1)
                pierdeTurno2 = true;
            else
                pierdeTurno1 = true;

            labelWhatHappened.text += " y tira de nuevo el dado.";
        }
        else if (posicionJugador == 33)
        {
            nuevoCasillero = 20;
        }

        if (nuevoCasillero > posicionJugador)
            labelWhatHappened.text += " y avanza al casillero " + nuevoCasillero;
        else if (nuevoCasillero < posicionJugador)
            labelWhatHappened.text += " y retrocede al casillero " + nuevoCasillero;

        return nuevoCasillero;
    }

    public void OnDiceRoll()
    {
        if (!waitingForDice)
            return;

        System.Random r = new System.Random();

        int resultado = r.Next(1, 7);
        labelDiceResult.text = resultado.ToString();

        diceResult = resultado;
    }
}

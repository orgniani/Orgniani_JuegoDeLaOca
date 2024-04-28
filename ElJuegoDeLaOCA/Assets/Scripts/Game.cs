using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Game : MonoBehaviour
{
    [Header("Board")]
    [SerializeField] private Board board;

    [Header("Text")]
    [SerializeField] private TMP_Text labelCurrentPlayer;
    [SerializeField] private TMP_Text labelWhatHappened;
    [SerializeField] private TMP_Text labelDiceResult;

    private bool looseTurn1 = false;
    private bool looseTurn2 = false;

    private int player1Position = 1;
    private int player2Position = 1;

    private int turn = 1;
    private bool gameOver = false;
    
    private int diceResult = 0;
    private bool waitingForDice = false;

    private List<BoardRule> tablero = new List<BoardRule>();

    public void Initialize(List<BoardRule> tablero)
    {
        this.tablero = tablero;

        labelCurrentPlayer.text = "";
        labelWhatHappened.text = "";
        labelDiceResult.text = "X";

        player1Position = 1;
        player2Position = 1;

        turn = 1;
        gameOver = false;

        StartCoroutine(PlayTurn());
    }

    private IEnumerator PlayTurn()
    {
        if (gameOver) yield break;

        diceResult = 0;
        labelWhatHappened.text = "";

        int currentPlayerPosition = GetCurrentPlayerPosition();
        int currentPlayerTurn = GetCurrentPlayerTurn();
        bool currentPlayerLostTurn = GetCurrentPlayerLostTurn();

        labelCurrentPlayer.text = turn.ToString();

        if (currentPlayerLostTurn)
        {
            labelWhatHappened.text = "El jugador " + currentPlayerTurn + " había perdido el turno - no juega";
            if (turn == 1) looseTurn1 = false;
            else looseTurn2 = false;
        }

        else
        {
            yield return WaitForDice();
            MovePlayer(currentPlayerTurn, ref currentPlayerPosition);

            yield return new WaitForSeconds(1);
            MovePlayerAfterWhatHappens(currentPlayerTurn, ref currentPlayerPosition);
        }

        UpdatePosition(currentPlayerPosition);

        CheckIfGameOver(currentPlayerPosition, currentPlayerTurn);
        turn = (turn == 1) ? 2 : 1;

        yield return new WaitForSeconds(2);
        StartCoroutine(PlayTurn());
    }

    private int GetCurrentPlayerPosition()
    {
        return (turn == 1) ? player1Position : player2Position;
    }

    private int GetCurrentPlayerTurn()
    {
        return (turn == 1) ? 1 : 2;
    }

    private bool GetCurrentPlayerLostTurn()
    {
        return (turn == 1) ? looseTurn1 : looseTurn2;
    }

    private void CheckIfGameOver(int currentPlayerPosition, int currentPlayerTurn)
    {
        gameOver = (currentPlayerPosition == 36);

        if (!gameOver) return;
        labelWhatHappened.text = $"Gana el jugador {((currentPlayerTurn == 1) ? 1 : 2)} - fin del juego";
    }

    private void MovePlayer(int currentPlayerTurn, ref int currentPlayerPosition)
    {
        currentPlayerPosition = Math.Min(36, currentPlayerPosition + diceResult);

        labelWhatHappened.text = "Sacó un " + diceResult.ToString() + " y se mueve al casillero nro " + currentPlayerPosition.ToString();
        board.MovePlayerToCell(currentPlayerTurn, currentPlayerPosition);
    }

    private void MovePlayerAfterWhatHappens(int currentPlayerTurn, ref int currentPlayerPosition)
    {
        currentPlayerPosition = CheckWhatHappens(currentPlayerTurn, currentPlayerPosition);
        board.MovePlayerToCell(currentPlayerTurn, currentPlayerPosition);
    }

    private void UpdatePosition(int currentPlayerPosition)
    {
        if (turn == 1) player1Position = currentPlayerPosition;
        else player2Position = currentPlayerPosition;
    }

    private IEnumerator WaitForDice()
    {
        waitingForDice = true;

        while (diceResult == 0)
        {
            yield return new WaitForEndOfFrame();
        }

        waitingForDice = false;
    }

    private int CheckWhatHappens(int idJugador, int posicionJugador)
    {
        BoardRuleResult result = new BoardRuleResult(posicionJugador);

        foreach (var regla in tablero)
        {
            if (regla.IsCompatible(posicionJugador))
                result = regla.Act(idJugador, posicionJugador);
        }

        looseTurn1 = looseTurn1 || result.jugador1PierdeTurno;
        looseTurn2 = looseTurn2 || result.jugador2PierdeTurno;

        labelWhatHappened.text += result.textWhatHappened;

        return result.newPosition;
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

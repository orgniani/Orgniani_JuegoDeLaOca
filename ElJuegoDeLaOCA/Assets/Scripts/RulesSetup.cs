using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RulesSetup : MonoBehaviour
{
    [Header("Game Reference")]
    [SerializeField] private Game game;

    [Header("Go Backward Rules")]
    [SerializeField] private int[] backwardStartCell;
    [SerializeField] private int[] backwardEndCell;

    [Header("Go Forward Rules")]
    [SerializeField] private int[] forwardStartCell;
    [SerializeField] private int[] forwardEndCell;

    [Header("Loose Turn Rules")]
    [SerializeField] private int[] looseTurnCell;

    [Header("Throw Again Rules")]
    [SerializeField] private int[] throwAgainCell;

    private Dictionary<int, int> goBackwardRules = new Dictionary<int, int>();
    private Dictionary<int, int> goForwardRules = new Dictionary<int, int>();

    private List<BoardRule> tablero = new List<BoardRule>();

    private void Start()
    {
        FillDictionary(goBackwardRules, backwardStartCell, backwardEndCell);
        FillDictionary(goForwardRules, forwardStartCell, forwardEndCell);

        tablero.Add(new GoBackward(goBackwardRules));

        tablero.Add(new GoForward(goForwardRules));

        tablero.Add(new LooseTurn(looseTurnCell));

        tablero.Add(new ThrowAgain(throwAgainCell));

        game.Initialize(tablero);
    }

    private void FillDictionary(Dictionary<int, int> rules, int[] startCell, int[] endCell)
    {
        rules.Clear();

        if (startCell.Length != endCell.Length)
        {
            Debug.LogError("Start cell and End cell arrays must have the same length.");
            return;
        }

        for (int i = 0; i < startCell.Length; i++)
        {
            rules.Add(startCell[i], endCell[i]);
        }
    }
}

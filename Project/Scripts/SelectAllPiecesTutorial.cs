using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SelectAllPiecesTutorial : MonoBehaviour
{
    [Tooltip("Number of pieces needed to select to progress")]
    public int totalPieces;

    [Tooltip("Executes upon selecting 'Total Pieces' number of pieces")]
    public UnityEvent Complete;

    /// <summary>
    /// Which pieces have been selected
    /// </summary>
    private List<string> pieces = new List<string>();

    private int numPiecesSelected;
    

    public void SelectPiece(string name)
    {
        foreach (string p in pieces)
        {
            if (p == name)
            {
                // piece already selected
                return;
            }
        }
        // new piece selected
        pieces.Add(name);
        numPiecesSelected++;

        if (numPiecesSelected == totalPieces) 
        {
            Complete.Invoke();
        }

    }

    

}

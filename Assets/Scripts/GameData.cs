using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public bool whoseTurn;
    public List<PieceData> pieces;

    public GameData(Board board)
    {
        whoseTurn = board.whoseTurn;
        pieces = new List<PieceData>();

        Cell[,] cells = board.cells;
        foreach (Cell c in cells)
        {
            if (c.piece != null)
            {
                pieces.Add(new PieceData(c.piece));
            }
        }

    }
}

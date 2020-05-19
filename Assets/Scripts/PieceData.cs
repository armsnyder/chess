using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PieceData
{
    public bool team;
    public bool hasMoved;
    public string pieceType;
    public int x;
    public int y;

    public PieceData(Piece piece)
    {
        team = piece.team;
        hasMoved = piece.hasMoved;
        pieceType = piece.SpriteName;
        x = piece.cell.x;
        y = piece.cell.y;
    }
}

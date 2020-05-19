using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PieceData
{
    bool team;
    bool hasMoved;
    string pieceType;
    int x;
    int y;

    public PieceData(Piece piece)
    {
        team = piece.team;
        hasMoved = piece.hasMoved;
        pieceType = piece.SpriteName;
        x = piece.cell.x;
        y = piece.cell.y;
    }
}

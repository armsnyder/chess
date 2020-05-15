using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Piece
{
    protected override Vector2Int[] MoveSet()
    {
        return new Vector2Int[] { new Vector2Int(-1, 0), new Vector2Int(-1, -1), new Vector2Int(0, -1), new Vector2Int(1, -1), new Vector2Int(1, 0), new Vector2Int(1, 1), new Vector2Int(0, 1), new Vector2Int(-1, 1) };
    }

    protected override string PieceType()
    {
        return "King";
    }
}

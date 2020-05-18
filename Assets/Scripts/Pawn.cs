using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{
    protected override string SpriteName { get { return "King"; } }

    protected override Vector3Int[] MoveSet
    {
        get
        {
            return new Vector3Int[] {
                    new Vector3Int(0, 1, 1),
                    new Vector3Int(0, 2, 2),
                    new Vector3Int(-1, 1, 3),
                    new Vector3Int(1, 1, 3)
            };
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Piece
{
    protected override string SpriteName { get { return "Knight"; } }

    protected override Vector3Int[] MoveSet
    {
        get
        {
            return new Vector3Int[] {
                    new Vector3Int(-1, 2, 1),
                    new Vector3Int(-1, -2, 1),
                    new Vector3Int(1, 2, 1),
                    new Vector3Int(1, -2, 1),
                    new Vector3Int(2, 1, 1),
                    new Vector3Int(2, -1, 1),
                    new Vector3Int(-2, 1, 1),
                    new Vector3Int(-2, -1, 1)
            };
        }
    }
}

using UnityEngine;

public class Pawn : Piece
{
    protected override string SpriteName { get { return "Pawn"; } }

    protected override Vector3Int[] MoveSet
    {
        get
        {
            return new Vector3Int[] {
                    new Vector3Int(0, 1, MOVE),
                    new Vector3Int(0, 2, MOVE|FIRST_ONLY),
                    new Vector3Int(-1, 1, CAPTURE),
                    new Vector3Int(1, 1, CAPTURE)
            };
        }
    }
}

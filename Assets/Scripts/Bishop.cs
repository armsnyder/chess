using UnityEngine;

public class Bishop : Piece
{
    protected override string SpriteName { get { return "Bishop"; } }

    protected override Vector3Int[] MoveSet
    {
        get
        {
            return new Vector3Int[] {
                    new Vector3Int(-1, -1, -1),
                    new Vector3Int(-1, 1, -1),
                    new Vector3Int(1, 1, -1),
                    new Vector3Int(1, -1, -1)
            };
        }
    }
}

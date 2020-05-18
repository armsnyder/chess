using UnityEngine;

public class King : Piece
{
    protected override string SpriteName { get { return "King"; } }

    protected override Vector3Int[] MoveSet
    {
        get
        {
            return new Vector3Int[] {
                    new Vector3Int(-1, 0, MOVE|CAPTURE),
                    new Vector3Int(-1, -1, MOVE|CAPTURE),
                    new Vector3Int(0, -1, MOVE|CAPTURE),
                    new Vector3Int(1, -1, MOVE|CAPTURE),
                    new Vector3Int(1, 0, MOVE|CAPTURE),
                    new Vector3Int(1, 1, MOVE|CAPTURE),
                    new Vector3Int(0, 1, MOVE|CAPTURE),
                    new Vector3Int(-1, 1, MOVE|CAPTURE)
            };
        }
    }
}

using UnityEngine;

public class Knight : Piece
{
    public override string SpriteName { get { return "Knight"; } }

    protected override Vector3Int[] MoveSet
    {
        get
        {
            return new Vector3Int[] {
                    new Vector3Int(-1, 2, MOVE|CAPTURE),
                    new Vector3Int(-1, -2, MOVE|CAPTURE),
                    new Vector3Int(1, 2, MOVE|CAPTURE),
                    new Vector3Int(1, -2, MOVE|CAPTURE),
                    new Vector3Int(2, 1, MOVE|CAPTURE),
                    new Vector3Int(2, -1, MOVE|CAPTURE),
                    new Vector3Int(-2, 1, MOVE|CAPTURE),
                    new Vector3Int(-2, -1, MOVE|CAPTURE)
            };
        }
    }
}

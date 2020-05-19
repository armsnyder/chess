using UnityEngine;

public class Rook : Piece
{
    public override string SpriteName { get { return "Rook"; } }

    protected override Vector3Int[] MoveSet
    {
        get
        {
            return new Vector3Int[] {
                    new Vector3Int(-1, 0, MOVE|CAPTURE|RANGE),
                    new Vector3Int(0, -1, MOVE|CAPTURE|RANGE),
                    new Vector3Int(1, 0, MOVE|CAPTURE|RANGE),
                    new Vector3Int(0, 1, MOVE|CAPTURE|RANGE)
            };
        }
    }
}

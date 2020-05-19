using UnityEngine;

public class Bishop : Piece
{
    public override string SpriteName { get { return "Bishop"; } }

    protected override Vector3Int[] MoveSet
    {
        get
        {
            return new Vector3Int[] {
                    new Vector3Int(-1, -1, MOVE|CAPTURE|RANGE),
                    new Vector3Int(-1, 1, MOVE|CAPTURE|RANGE),
                    new Vector3Int(1, 1, MOVE|CAPTURE|RANGE),
                    new Vector3Int(1, -1, MOVE|CAPTURE|RANGE)
            };
        }
    }
}

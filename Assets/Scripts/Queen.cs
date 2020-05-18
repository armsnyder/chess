using UnityEngine;

public class Queen : Piece
{
    protected override string SpriteName { get { return "Queen"; } }

    protected override Vector3Int[] MoveSet
    {
        get
        {
            return new Vector3Int[] {
                    new Vector3Int(-1, 0, -1),
                    new Vector3Int(0, -1, -1),
                    new Vector3Int(1, 0, -1),
                    new Vector3Int(0, 1, -1),
                    new Vector3Int(-1, -1, -1),
                    new Vector3Int(1, -1, -1),
                    new Vector3Int(1, 1, -1),
                    new Vector3Int(-1, 1, -1)
            };
        }
    }
}

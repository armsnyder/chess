using UnityEngine;

public class King2 : Piece2
{
    protected override string SpriteName { get { return "King"; } }

    protected override Vector2Int[] MoveSet()
    {
        return new Vector2Int[] {
                new Vector2Int(-1, 0),
                new Vector2Int(-1, -1),
                new Vector2Int(0, -1),
                new Vector2Int(1, -1),
                new Vector2Int(1, 0),
                new Vector2Int(1, 1),
                new Vector2Int(0, 1),
                new Vector2Int(-1, 1)
            };
    }
}

using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField]
    GameObject _cellPrefab = null;
    Cell[,] _cells;
    public const int WIDTH = 8;

    public Cell GetCell(int x, int y)
    {
        if (x < 0 || x >= WIDTH || y < 0 || y >= WIDTH)
        {
            return null;
        }
        return _cells[x, y];
    }

    public void Setup()
    {
        _cells = new Cell[WIDTH, WIDTH];

        for (int x = 0; x < WIDTH; x++)
        {
            for (int y = 0; y < WIDTH; y++)
            {
                var cellGameObject = Instantiate<GameObject>(_cellPrefab, transform);
                cellGameObject.transform.position += new Vector3(x - WIDTH / 2 + 0.5f, y - WIDTH / 2 + 0.5f);

                var cell = cellGameObject.GetComponent<Cell>();
                cell.Setup(x, y);

                _cells[x, y] = cell;
            }
        }
    }

    void OnDrawGizmos()
    {
        // Draw a chessboard preview in the editor.
        for (int x = -WIDTH / 2; x < WIDTH / 2; x++)
        {
            for (int y = -WIDTH / 2; y < WIDTH / 2; y++)
            {
                Gizmos.color = ((x ^ y) & 1) == 1 ? Color.white : Color.black;
                Gizmos.DrawCube(transform.position + new Vector3(x + 0.5f, y + 0.5f, 0), new Vector3(1, 1, 0));
            }
        }
    }
}

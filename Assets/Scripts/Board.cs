using System;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField]
    GameObject _cellPrefab = null;
    [SerializeField]
    GameObject _piecePrefab = null;
    Cell[,] _cells;

    Dictionary<string, Type> _typeMap = new Dictionary<string, Type>()
    {
        {"Rook",typeof(Rook)},
        {"Pawn",typeof(Pawn)},
        {"Knight",typeof(Knight)},
        {"Queen",typeof(Queen)},
        {"King",typeof(King)},
        {"Bishop",typeof(Bishop)},
    };

    public Cell[,] cells { get { return _cells; } }
    public bool whoseTurn;
    public const int WIDTH = 8;

    public static event Action<bool> GameOver;

    public void NextTurn()
    {
        whoseTurn ^= true;
        if (IsGameOver())
        {
            if (GameOver != null)
            {
                GameOver(!whoseTurn);
            }
        }
        FindObjectOfType<GameManager>().Save();
        // whoseTurn ^= true;  // debug
    }
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
    bool IsGameOver()
    {
        foreach (var p in FindObjectsOfType<Piece>())
        {
            if (p.team == whoseTurn && p.HasMoves() && p.enabled)
            {
                return false;
            }
        }
        return true;
    }

    public void ClearPieces()
    {
        foreach (var piece in FindObjectsOfType<Piece>())
        {
            Destroy(piece.gameObject);
        }
    }

    public void CreateAndPlacePiece(Type type, bool team, Cell cell, bool hasMoved = false)
    {
        var piece = Instantiate<GameObject>(_piecePrefab);
        piece.AddComponent(type);
        (piece.GetComponent(type) as Piece).Setup(team, hasMoved);
        (piece.GetComponent(type) as Piece).Place(cell);
    }

    public void CreateAndPlacePiece(String type, bool team, Cell cell, bool hasMoved)
    {
        CreateAndPlacePiece(_typeMap[type], team, cell, hasMoved);
    }
}

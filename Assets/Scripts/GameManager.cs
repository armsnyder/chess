using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // [SerializeField]
    // GameObject _piecePrefab = null;

    public void Reset()
    {
        var board = FindObjectOfType<Board>();
        board.ClearPieces();

        var firstRow = new Type[] { typeof(Rook), typeof(Knight), typeof(Bishop), typeof(Queen), typeof(King), typeof(Bishop), typeof(Knight), typeof(Rook) };

        for (int i = 0; i < firstRow.Length; i++)
        {
            board.CreateAndPlacePiece(firstRow[i], true, board.GetCell(i, 0));
        }
        for (int i = 0; i < Board.WIDTH; i++)
        {
            board.CreateAndPlacePiece(typeof(Pawn), true, board.GetCell(i, 1));
        }

        for (int i = 0; i < firstRow.Length; i++)
        {
            board.CreateAndPlacePiece(firstRow[i], false, board.GetCell(i, Board.WIDTH - 1));
        }
        for (int i = 0; i < Board.WIDTH; i++)
        {
            board.CreateAndPlacePiece(typeof(Pawn), false, board.GetCell(i, Board.WIDTH - 2));
        }

        board.whoseTurn = true;
    }

    public void Save()
    {
        var board = FindObjectOfType<Board>();
        StartCoroutine(SaveGame.Save(board));
    }

    public void Load()
    {
        StartCoroutine(SaveGame.Load(FindObjectOfType<Board>()));
    }

    // void CreateAndPlacePiece(Type type, bool team, Cell cell)
    // {
    //     var piece = Instantiate<GameObject>(_piecePrefab);
    //     piece.AddComponent(type);
    //     (piece.GetComponent(type) as Piece).Setup(team);
    //     (piece.GetComponent(type) as Piece).Place(cell);
    // }

    // Start is called before the first frame update
    void Start()
    {
        var board = FindObjectOfType<Board>();
        board.Setup();

        Reset();
    }
}

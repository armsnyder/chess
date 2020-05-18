using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject _piecePrefab = null;

    public void Reset() {
        foreach (var piece in FindObjectsOfType<Piece>()) { 
            Destroy(piece.gameObject);
        }
        CreateAndPlacePieces();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        var board = FindObjectOfType<Board>();
        board.Setup();

        CreateAndPlacePieces();
    }

    void CreateAndPlacePieces()
    {
        var board = FindObjectOfType<Board>();
        var firstRow = new Type[] { typeof(Rook), typeof(Knight), typeof(Bishop), typeof(Queen), typeof(King), typeof(Bishop), typeof(Knight), typeof(Rook) };
        for (int i = 0; i < firstRow.Length; i++)
        {
            CreateAndPlacePiece(firstRow[i], true, board.GetCell(i, 0));
        }
        for (int i = 0; i < Board.WIDTH; i++)
        {
            CreateAndPlacePiece(typeof(Pawn), true, board.GetCell(i, 1));
        }

        for (int i = 0; i < firstRow.Length; i++)
        {
            CreateAndPlacePiece(firstRow[i], false, board.GetCell(i, Board.WIDTH - 1));
        }
        for (int i = 0; i < Board.WIDTH; i++)
        {
            CreateAndPlacePiece(typeof(Pawn), false, board.GetCell(i, Board.WIDTH - 2));
        }
    }

    void CreateAndPlacePiece(Type type, bool team, Cell cell)
    {
        var piece = Instantiate<GameObject>(_piecePrefab);
        piece.AddComponent(type);
        (piece.GetComponent(type) as Piece).Setup(team);
        (piece.GetComponent(type) as Piece).Place(cell);
    }
}

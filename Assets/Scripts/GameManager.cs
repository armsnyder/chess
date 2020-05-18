using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject _piecePrefab = null;

    // Start is called before the first frame update
    void Start()
    {
        var board = FindObjectOfType<Board>();
        board.Setup();

        var piece = Instantiate<GameObject>(_piecePrefab);
        piece.AddComponent<King>();
        piece.GetComponent<Piece>().Setup(true);
        piece.GetComponent<Piece>().Place(board.GetCell(4, 0));

        piece = Instantiate<GameObject>(_piecePrefab);
        piece.AddComponent<Rook>();
        piece.GetComponent<Piece>().Setup(true);
        piece.GetComponent<Piece>().Place(board.GetCell(3, 0));

        piece = Instantiate<GameObject>(_piecePrefab);
        piece.AddComponent<King>();
        piece.GetComponent<Piece>().Setup(false);
        piece.GetComponent<Piece>().Place(board.GetCell(4, 7));
    }
}

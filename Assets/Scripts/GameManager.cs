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

        var wk = Instantiate<GameObject>(_piecePrefab);
        wk.AddComponent<King>();
        wk.GetComponent<SpriteRenderer>().color = Color.white;
        wk.GetComponent<Piece>().Place(board.GetCell(4, 0));
        // TODO piece.setup() to set own color and team etc.

        var wq = Instantiate<GameObject>(_piecePrefab);
        wq.AddComponent<King>();
        wq.GetComponent<SpriteRenderer>().color = Color.white;
        wq.GetComponent<Piece>().Place(board.GetCell(3, 0));

        var bk = Instantiate<GameObject>(_piecePrefab);
        bk.AddComponent<King>();
        bk.GetComponent<SpriteRenderer>().color = Color.black;
        bk.GetComponent<Piece>().Place(board.GetCell(4, 7));
    }
}

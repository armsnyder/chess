using UnityEngine;

public class GameManager2 : MonoBehaviour
{
    [SerializeField]
    GameObject _piecePrefab = null;

    // Start is called before the first frame update
    void Start()
    {
        var board = FindObjectOfType<Board2>();
        board.Setup();

        var piece = Instantiate<GameObject>(_piecePrefab);
        piece.AddComponent(typeof(King2));
        piece.GetComponent<Piece2>().Place(board.GetCell(4, 0));
    }
}

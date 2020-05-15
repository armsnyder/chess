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
        piece.AddComponent(typeof(King));
        piece.GetComponent<Piece>().Place(board.GetCell(4, 0));
    }
}

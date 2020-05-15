using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject piecePrefab;
    public Color whitePiece;
    public Color blackPiece;

    // Start is called before the first frame update
    void Start()
    {
        var board = FindObjectOfType<Board>();
        board.Setup();
        var piece = Instantiate<GameObject>(piecePrefab);
        piece.transform.SetParent(board.transform);
        piece.transform.SetAsLastSibling();
        piece.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        piece.GetComponent<Image>().color = whitePiece;
        piece.AddComponent(typeof(King));
        piece.GetComponent<Piece>().Place(board.cells[4, 0]);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

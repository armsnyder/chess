using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    public GameObject cellPrefab;
    public Color white = new Color(0.95f, 0.96f, 0.8f);
    public Color black = new Color(0.1f, 0.4f, 0.2f);

    private static readonly int boardWidth = 8;

    public Cell[,] cells;

    public void Setup()
    {
        cells = new Cell[boardWidth, boardWidth];

        var size = cellPrefab.GetComponent<RectTransform>().rect.width;

        for (int i = 0; i < boardWidth; i++)
        {
            for (int j = 0; j < boardWidth; j++)
            {
                var cellGameObject = Instantiate<GameObject>(cellPrefab, transform);
                cellGameObject.transform.SetAsFirstSibling();
                cellGameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2((i + 0.5f) * size, (j + 0.5f) * size);
                if ((i + j) % 2 == 1)
                {
                    cellGameObject.GetComponent<Image>().color = white;
                }
                else
                {
                    cellGameObject.GetComponent<Image>().color = black;
                }
                var cell = cellGameObject.GetComponent<Cell>();
                cell.Rank = i;
                cell.File = j;
                cells[i, j] = cell;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Cell GetCell(int rank, int file)
    {
        if (rank < 0 || rank >= boardWidth || file < 0 || file >= boardWidth)
        {
            return null;
        }
        return cells[rank, file];
    }
}

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class Piece : EventTrigger
{
    public bool team;
    private Vector3 originalPosition;
    private Cell originalCell;

    public override void OnPointerDown(PointerEventData eventData)
    {
        originalPosition = transform.position;
    }
    public override void OnDrag(PointerEventData eventData)
    {
        transform.position += (Vector3)eventData.delta;
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        var board = FindObjectOfType<Board>();
        foreach (var move in MoveSet())
        {
            var cell = board.GetCell(originalCell.Rank + move.x, originalCell.File + move.y);
            if (cell != null && RectTransformUtility.RectangleContainsScreenPoint(cell.GetComponent<RectTransform>(), Input.mousePosition))
            {
                Place(cell);
                return;
            }
        }

        transform.position = originalPosition;
    }

    protected abstract Vector2Int[] MoveSet();

    protected abstract string PieceType();

    public void Place(Cell cell)
    {
        if (originalCell != null)
        {
            originalCell.piece = null;
        }
        transform.position = cell.transform.position;
        originalCell = cell;
        cell.piece = this;
    }

    public void Start()
    {
        GetComponent<Image>().sprite = Resources.Load<Sprite>(PieceType());
    }
}

using UnityEngine;

public abstract class Piece2 : MonoBehaviour
{
    Cell2 _cell;
    bool _isDragging;
    Vector3 _grabOffset;

    public void Place(Cell2 cell)
    {
        transform.position = cell.transform.position + new Vector3(0, 0, -1);
        _cell = cell;
    }

    protected abstract string SpriteName { get; }

    protected abstract Vector2Int[] MoveSet();

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(SpriteName);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && IsMouseOverPieceCell())
        {
            BeginDrag();
        }

        if (_isDragging)
        {
            OnDrag();

            if (Input.GetMouseButtonUp(0))
            {
                EndDrag();
            }
        }
    }

    void BeginDrag()
    {
        _isDragging = true;
        _grabOffset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Cursor.visible = false;
    }

    void EndDrag()
    {
        _isDragging = false;
        Cursor.visible = true;

        var targetCell = FindCellUnderMouse();

        if (CanPlace(targetCell))
        {
            Place(targetCell);
        }
        else
        {
            Place(_cell);
        }
    }

    void OnDrag()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x + _grabOffset.x, mousePos.y + _grabOffset.y, transform.position.z);
    }

    bool IsMouseOverPieceCell()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);

        if (hits.Length > 0)
        {
            var collider = _cell.GetComponent<Collider>();
            foreach (var hit in hits)
            {
                if (hit.collider == collider)
                {
                    return true;
                }
            }
        }

        return false;
    }

    Cell2 FindCellUnderMouse()
    {
        var hits = Physics.RaycastAll(transform.position, Vector3.forward);
        var cells = FindObjectsOfType<Cell2>();

        foreach (var cell in cells)
        {
            var collider = cell.GetComponent<Collider>();

            foreach (var hit in hits)
            {
                if (hit.collider == collider)
                {
                    return cell;
                }
            }
        }

        return null;
    }

    bool CanPlace(Cell2 cell)
    {
        if (cell == null)
        {
            return false;
        }

        foreach (var move in MoveSet())
        {
            if (_cell.x + move.x == cell.x && _cell.y + move.y == cell.y)
            {
                return true;
            }
        }

        return false;
    }
}

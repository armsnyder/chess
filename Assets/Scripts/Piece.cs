using UnityEngine;

public abstract class Piece : MonoBehaviour
{
    Cell _cell;
    bool _isDragging;
    Vector3 _grabOffset;

    public void Place(Cell cell)
    {
        transform.position = cell.transform.position + new Vector3(0, 0, -1);
        if (_cell)
        {
            _cell.piece = null;
        }
        _cell = cell;
        cell.piece = this;
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
        transform.position += new Vector3(0, 0, -1);
        Cursor.visible = false;
    }

    void EndDrag()
    {
        _isDragging = false;
        Cursor.visible = true;

        var targetCell = FindCellUnderPiece();

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

    Cell FindCellUnderPiece()
    {
        var hits = Physics.RaycastAll(transform.position, Vector3.forward);
        var cells = FindObjectsOfType<Cell>();

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

    bool CanPlace(Cell cell)
    {
        if (cell == null)
        {
            return false;
        }

        foreach (var move in MoveSet())
        {
            if (_cell.x + move.x == cell.x && _cell.y + move.y == cell.y)
            {
                //return true;
                if (cell.piece == null)
                {
                    return true;
                }
                if (cell.piece.GetComponent<SpriteRenderer>().color != GetComponent<SpriteRenderer>().color)
                {
                    return true;
                }
            }
        }

        return false;
    }
}

using System.Collections.Generic;
using UnityEngine;

public abstract class Piece : MonoBehaviour
{
    Cell _cell;
    bool _isDragging;
    Vector3 _grabOffset;
    bool team;
    List<Cell> curPotentialMoves;

    public void Setup(bool team)
    {
        GetComponent<SpriteRenderer>().color = (team) ? Color.white : Color.black;
        this.team = team;
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(SpriteName);
    }

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

    protected abstract Vector3Int[] MoveSet { get; }

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
        curPotentialMoves = ValidMoves();
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
        // if (cell == null)
        // {
        //     return false;
        // }
        return curPotentialMoves.Contains(cell);
    }

    List<Cell> ValidMoves()
    {
        var result = new List<Cell>();
        foreach (var move in MoveSet)
        {
            for (int i = 1; i == 1 || move.z == -1; i++)
            {
                Cell potentialMove = FindObjectOfType<Board>().GetCell(_cell.x + (i * move.x), _cell.y + (i * move.y));
                if (potentialMove == null)
                {
                    break;
                }
                if (potentialMove.piece == null)
                {
                    result.Add(potentialMove);
                }
                else if (potentialMove.piece.team == team)
                {
                    break;
                }
                else
                {
                    result.Add(potentialMove);
                    break;
                }
            }
        }
        return result;
    }
}

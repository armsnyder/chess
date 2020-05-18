using System.Collections.Generic;
using UnityEngine;

public abstract class Piece : MonoBehaviour
{
    Cell _cell;
    bool _isDragging;
    Vector3 _grabOffset;
    bool _team;
    public bool team { get { return _team; } }
    List<Cell> _curPotentialMoves;
    bool _hasMoved;

    protected const int MOVE = 1;
    protected const int CAPTURE = 1 << 1;
    protected const int RANGE = 1 << 2;
    protected const int SPECIAL = 1 << 3;


    public void Setup(bool team)
    {
        this._team = team;
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>((team ? "White" : "Black") + SpriteName);
    }

    public void Place(Cell cell)
    {
        transform.position = cell.transform.position + new Vector3(0, 0, -1);
        if (_cell)
        {
            _cell.piece = null;
        }
        if (cell.piece)
        {
            // capture piece
            Destroy(cell.piece.gameObject);
        }
        _cell = cell;
        cell.piece = this;
    }

    public bool HasMoves() {
        return ValidMoves().Count > 0;
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
        if (_team == FindObjectOfType<GameManager>().whoseTurn) {
            _isDragging = true;
            _grabOffset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position += new Vector3(0, 0, -1);
            Cursor.visible = false;
            _curPotentialMoves = ValidMoves();
        }
    }

    void EndDrag()
    {
        _isDragging = false;
        Cursor.visible = true;

        var targetCell = FindCellUnderPiece();

        if (CanPlace(targetCell))
        {
            Place(targetCell);
            _hasMoved = true;
            FindObjectOfType<GameManager>().NextTurn();
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
        return _curPotentialMoves.Contains(cell);
    }

    List<Cell> ValidMoves()
    {
        var result = new List<Cell>();
        var board = FindObjectOfType<Board>();
        foreach (var move in MoveSet)
        {
            if ((move.z & SPECIAL) == SPECIAL && _hasMoved)
            {
                continue;
            }
            for (int i = 1; i == 1 || (move.z & RANGE) == RANGE; i++)
            {
                int reverse = _team ? 1 : -1;
                Cell potentialCell = board.GetCell(_cell.x + (i * move.x), _cell.y + (i * move.y * reverse));
                if (potentialCell == null)
                // edge of the board
                {
                    break;
                }
                if (potentialCell.piece == null)
                // no piece on target cell
                {
                    if ((move.z & SPECIAL) == SPECIAL && board.GetCell(_cell.x + (move.x / 2), _cell.y + (move.y / 2 * reverse)).piece)
                    {
                        break;
                    }
                    if ((move.z & MOVE) == MOVE)
                    {
                        result.Add(potentialCell);
                    }
                }
                else if (potentialCell.piece._team == _team)
                // same team piece on target cell
                {
                    break;
                }
                else
                {
                    if ((move.z & CAPTURE) == CAPTURE)
                    {
                        result.Add(potentialCell);
                    }
                    break;
                }
            }
        }
        return result;
    }
}

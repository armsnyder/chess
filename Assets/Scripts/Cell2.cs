using UnityEngine;

public class Cell2 : MonoBehaviour
{
    [SerializeField]
    Material _whiteMaterial = null, _blackMaterial = null;
    int _x, _y;
    public int x { get { return _x; } }
    public int y { get { return _y; } }

    public void Setup(int x, int y)
    {
        _x = x;
        _y = y;

        // Set the cell color.
        var material = ((x ^ y) & 1) == 1 ? _whiteMaterial : _blackMaterial;
        GetComponent<MeshRenderer>().materials = new Material[] { material };

        // Name the cell according to its rank and file.
        name = (y + 1).ToString() + (char)('a' + x);
    }
}

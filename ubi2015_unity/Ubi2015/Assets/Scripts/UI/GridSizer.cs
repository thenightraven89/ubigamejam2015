using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GridSizer : MonoBehaviour {

    public GridLayoutGroup grid;
    public float scaleFactor = 3.805f;

    void Update()
    {
        float newSize = Screen.height / scaleFactor;
        grid.cellSize = new Vector2(newSize, newSize);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Testing : MonoBehaviour
{   
    private GridXZ<GridObject> grid;

    public int gridWidth;
    public int gridHeight;
    public float cellSize;

    public bool showDebug;


    // Start is called before the first frame update
    void Awake()
    {
        grid = new GridXZ<GridObject>(gridWidth, gridHeight, cellSize, Vector3.zero, (GridXZ<GridObject> g, int x, int z) => new GridObject(g, x, z), showDebug);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
           
        }

        if (Input.GetMouseButtonDown(1))
        {
           
        }
    }

    public class GridObject
    {
        private GridXZ<GridObject> grid;
        private int x;
        private int z;

        public GridObject(GridXZ<GridObject> grid, int x, int z)
        {
            this.grid = grid;
            this.x = x;
            this.z = z;
        }

        public override string ToString()
        {
            return x + "," + z;
        }
    }
}

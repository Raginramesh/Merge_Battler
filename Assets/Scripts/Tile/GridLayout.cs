using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class GridLayout : MonoBehaviour
{
    public TileGenerationSettings tileSettings;
    public GameManager gameManager;
    
    [Header("Grid Settings")] 
    public Vector2Int gridSize;

    [Header("Tile Settings")] 
    public bool isHexShaped;
    public bool drawMesh;
    public float outerSize = 1f;
    public float innerSize = 0f;
    public float height = 0.1f;
    public Material material;
    
    [Header("For Hex Grids Only")]
    public bool isFlatTopped;
    
    [Header("For Square Grids Only")]
    [Range(2f, 3f)]
    public float squareTileOffset = 2f;

    private void OnEnable()
    {
        LayoutGrid();
    }

    private void LayoutGrid()
    {
        for (int y = 0; y < gridSize.y; y++)
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                GameObject tile = new GameObject($"Cell({x},{y})", typeof(ShapeRenderer),typeof(Cell));
                tile.transform.position = GetPositionForHexFromCoordinate(new Vector2Int(x,y));
                
                tile.GetComponent<Cell>().cellWorldPos = GetPositionForHexFromCoordinate(new Vector2Int(x, y));
                tile.GetComponent<Cell>().cellGridPos = new Vector2Int(x, y);
                
                if (drawMesh)
                {
                    ShapeRenderer shapeRenderer = tile.GetComponent<ShapeRenderer>();
                    shapeRenderer.isFlatTopped = isFlatTopped;
                    shapeRenderer.isHex = isHexShaped;
                    shapeRenderer.outerSize = outerSize;
                    shapeRenderer.innerSize = innerSize;
                    shapeRenderer.height = height;
                    shapeRenderer.m_meshRenderer.material = material;
                    shapeRenderer.DrawMesh();
                }
                
                tile.transform.SetParent(transform,true);
                gameManager.cells.Add(tile);
            }
        }
    }

    public Vector3 GetPositionForHexFromCoordinate(Vector2Int coordinate)
    {
        int column = coordinate.x;
        int row = coordinate.y;

        float width;
        float height;
        float xPosition = 0f;
        float yPosition = 0f;
        bool shouldOffset;
        float horizontalDistance;
        float verticalDistance;
        float offset;
        float size = outerSize;

        if (!isFlatTopped && isHexShaped)
        {
            shouldOffset = (row % 2) == 0;
            width = Mathf.Sqrt(3) * size;
            height = 2f * size;

            horizontalDistance = width;
            verticalDistance = height * (3f / 4f);

            offset = (shouldOffset) ? width / 2 : 0;
            
            xPosition = (column *(horizontalDistance)) + offset;
            yPosition = (row * verticalDistance);
        }
        else if (isFlatTopped && isHexShaped)
        {
            shouldOffset = (column % 2) == 0;
            height = Mathf.Sqrt(3) * size;
            width = 2f * size;

            horizontalDistance = width * (3f / 4f);
            verticalDistance = height;

            offset = (shouldOffset) ? height / 2 : 0;
            
            xPosition = (column * (horizontalDistance));
            yPosition = (row * verticalDistance) - offset;
        }
        
        if (!isHexShaped)
        {
            width = Mathf.Sqrt(squareTileOffset) * size;
            height = Mathf.Sqrt(squareTileOffset) * size;

            horizontalDistance = width;
            verticalDistance = height;
            
            xPosition = (column *(horizontalDistance));
            yPosition = (row * verticalDistance);
        }

        return new Vector3(xPosition, 0, -yPosition);
    }
}

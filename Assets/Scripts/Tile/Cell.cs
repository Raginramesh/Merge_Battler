using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cell : MonoBehaviour
{
    public List<GameObject> tiles;
    public Vector3 cellWorldPos;
    public Vector2Int cellGridPos;
}

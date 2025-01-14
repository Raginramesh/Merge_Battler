using System;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public struct Face
{
    public List<Vector3> vertices { get; private set; }
    public List<int> triangles { get; private set; }
    public List<Vector2> uvs { get; private set; }

    public Face(List<Vector3> vertices, List<int> triangles, List<Vector2> uvs)
    {
        this.vertices = vertices;
        this.triangles = triangles;
        this.uvs = uvs;
    }
}

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class ShapeRenderer : MonoBehaviour
{
    public float innerSize;
    public float outerSize;
    public float height;
    
    public Mesh m_mesh;
    public MeshFilter m_meshFilter;
    public MeshRenderer m_meshRenderer;

    private List<Face> m_faces;
    public Material material;

    public bool isFlatTopped;
    public bool isHex;

    private void Awake()
    {
        m_meshFilter = GetComponent<MeshFilter>();
        m_meshRenderer = GetComponent<MeshRenderer>();

        m_mesh = new Mesh();
        m_mesh.name = "Hex";

        m_meshFilter.mesh = m_mesh;
        m_meshRenderer.material = material;
    }

    private void OnEnable()
    {
        DrawMesh();
    }

    public void DrawMesh()
    {
        DrawFaces();
        CombineFaces();
    }

    public void DrawFaces()
    {
        m_faces = new List<Face>();

        if (isHex)
        {
            //Top face
            for (int point = 0; point < 6; point++)
            {
                m_faces.Add(CreateFace(innerSize, outerSize, height / 2f, height/2f, point));
            }
            //Bottom face
            for (int point = 0; point < 6; point++)
            {
                m_faces.Add(CreateFace(innerSize, outerSize, -height / 2f, -height/2f, point, true));
            }
            //Outer face
            for (int point = 0; point < 6; point++)
            {
                m_faces.Add(CreateFace(outerSize, outerSize, height / 2f, -height/2f, point, true));
            }
            //Top face
            for (int point = 0; point < 6; point++)
            {
                m_faces.Add(CreateFace(innerSize, innerSize, -height / 2f, height/2f, point, false));
            }
        }
        else
        {
            //Top face
            for (int point = 0; point < 4; point++)
            {
                m_faces.Add(CreateFace(innerSize, outerSize, height / 2f, height/2f, point));
            }
            //Bottom face
            for (int point = 0; point < 4; point++)
            {
                m_faces.Add(CreateFace(innerSize, outerSize, -height / 2f, -height/2f, point, true));
            }
            //Outer face
            for (int point = 0; point < 4; point++)
            {
                m_faces.Add(CreateFace(outerSize, outerSize, height / 2f, -height/2f, point, true));
            }
            //Top face
            for (int point = 0; point < 4; point++)
            {
                m_faces.Add(CreateFace(innerSize, innerSize, -height / 2f, height/2f, point, false));
            }
        }
    }
    
    private void CombineFaces()
    {
        List<Vector3> vertices = new List<Vector3>();
        List<int> tris = new List<int>();
        List<Vector2> uvs = new List<Vector2>();

        for (int i = 0; i < m_faces.Count; i++)
        {
          vertices.AddRange(m_faces[i].vertices);
          uvs.AddRange(m_faces[i].uvs);

          int offset = (4 * i);
          foreach (var triangle in m_faces[i].triangles)
          {
              tris.Add(triangle + offset);
          }
        }

        m_mesh.vertices = vertices.ToArray();
        m_mesh.triangles = tris.ToArray();
        m_mesh.uv = uvs.ToArray();
        m_mesh.RecalculateNormals();
    }
    
    private Face CreateFace(float innerRad, float outerRad, float heightA, float heightB, int point,
        bool reverse = false)
    {
        Vector3 pointA;
        Vector3 pointB;
        Vector3 pointC;
        Vector3 pointD;
        
        if (isHex)
        {
             pointA = GetPoint(innerRad, heightB, point);
             pointB = GetPoint(innerRad, heightB, (point < 5) ? point + 1 : 0);
             pointC = GetPoint(outerRad, heightA, (point < 5) ? point + 1 : 0);
             pointD = GetPoint(outerRad, heightA, point);
        }
        else
        {
            pointA = GetPoint(innerRad, heightB, point);
            pointB = GetPoint(innerRad, heightB, (point < 3) ? point + 1 : 0);
            pointC = GetPoint(outerRad, heightA, (point < 3) ? point + 1 : 0);
            pointD = GetPoint(outerRad, heightA, point);
        }

        List<Vector3> vertices = new List<Vector3>() { pointA, pointB, pointC, pointD };
        List<int> triangles = new List<int>() { 0, 1, 2, 2, 3, 0 };
        List<Vector2> uvs = new List<Vector2>()
            { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1) };

        if (reverse)
        {
            vertices.Reverse();
        }

        return new Face(vertices, triangles, uvs);
    }

    protected Vector3 GetPoint(float size, float height, int index)
    {
        float angleDeg;
        if (isHex)
        {
            angleDeg = isFlatTopped ? 60 * index : 60 * index - 30;
        }
        else
        {
            angleDeg = 90 * index - 45;
        }
        
        float angleRad = Mathf.PI / 180f * angleDeg;
        return new Vector3((size * Mathf.Cos(angleRad)), height, size * Mathf.Sin(angleRad));
    }
}

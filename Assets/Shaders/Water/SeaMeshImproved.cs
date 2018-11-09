using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class SeaMeshImproved : MonoBehaviour
{

    [SerializeField]
    private int m_XSize, m_YSize;

    [SerializeField]
    private float m_Resolution;

    private Vector3[] m_Vertices;

    private Mesh m_Mesh;

    private void Awake()
    {
        Generate();
    }

    private void Generate()
    {
        m_Mesh = new Mesh();
        m_Mesh.name = "Procedural Grid";

        List<Vector3> vertices = new List<Vector3>();
        for (int y = 0; y < m_YSize; y++)
        {
            for (int x = 0; x < m_XSize; x++)
            {
                vertices.Add(new Vector3(x + m_Resolution, 0, y + m_Resolution) * 0.5f) ;
            }
        }

        m_Vertices = vertices.ToArray();
        m_Mesh.vertices = m_Vertices;

        List<int> triangles = new List<int>();
        int triangleQuads = (m_XSize - 1) * (m_YSize - 1); //Triangle Quad consist of two triangles.
        Debug.Log(triangleQuads);

        int xQuad = 0;
        for (int i = 0; i < triangleQuads; i++)
        {
            xQuad++;
            //3 2
            //1 0
            
            //Skip Edge
            if(xQuad == m_XSize) { xQuad = 0; triangleQuads++; continue; }

            triangles.Add(m_XSize + 1 + i);
            triangles.Add(i+1); //1
            triangles.Add(i); //0

            triangles.Add(i); //0
            triangles.Add(m_XSize + i); //2
            triangles.Add(m_XSize + 1 + i); //3
        }


        m_Mesh.triangles = triangles.ToArray();
        m_Mesh.RecalculateTangents();
        m_Mesh.RecalculateBounds();

        GetComponent<MeshFilter>().mesh = m_Mesh;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class SeaMesh : MonoBehaviour {

    [SerializeField]
    private int _XSize, _YSize;

    [SerializeField]
    private float _Resolution;

    private Vector3[] _Vertices;

    private Mesh _Mesh;

    private void Awake()
    {
        Generate();
    }

    private void Generate()
    {
        GetComponent<MeshFilter>().mesh = _Mesh = new Mesh();
        _Mesh.name = "Procedural Grid";

        _Vertices = new Vector3[(_XSize + 1) * (_YSize + 1)];
        for (int i = 0, y = 0; y <= _YSize; y++)
        {
            for (int x = 0; x <= _XSize; x++, i++)
            {
                _Vertices[i] = new Vector3(x * _Resolution, y * _Resolution);
            }
        }
       _Mesh.vertices = _Vertices;

        int[] triangles = new int[_XSize * _YSize * 6];
        for (int ti = 0, vi = 0, y = 0; y < _YSize; y++, vi++)
        {
            for (int x = 0; x < _XSize; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + _XSize + 1;
                triangles[ti + 5] = vi + _XSize + 2;
            }
        }
        _Mesh.triangles = triangles;
        _Mesh.RecalculateNormals();
    }

}

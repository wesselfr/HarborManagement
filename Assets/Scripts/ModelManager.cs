using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class ModelManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Mesh loadMeshFromFile(string file)
    {
        Mesh m = new Mesh();
        if (File.Exists(file))
        {
            
            

                Debug.Log("Start reading model: " + file);

                List<int> vertexIndices = new List<int>(), uvIndices = new List<int>(), normalIndices = new List<int>(); ;

                List<Vector3> temp_vertices = new List<Vector3>();
                List<Vector2> temp_uvs = new List<Vector2>();
                List<Vector3> temp_normals = new List<Vector3>();

                List<Vector3> vertices = new List<Vector3>();
                List<Vector2> uvs = new List<Vector2>();
                List<Vector3> normals = new List<Vector3>();

            List<int> triangles = new List<int>();

                foreach (string ln in File.ReadAllLines(file))
                {
                    string l = ln.Trim().Replace("  ", " ");
                    string[] cmps = l.Split(' ');
                    string data = l.Remove(0, l.IndexOf(' ') + 1);

                    if(cmps[0] == "o")
                    {
                        m.name = cmps[1];
                    }

                    else if (cmps[0] == "v")
                    {
                        temp_vertices.Add(new Vector3(float.Parse(cmps[1]), float.Parse(cmps[2]), float.Parse(cmps[3])));
                        Debug.Log("Vertice: X: " + cmps[1] + " Y: " + cmps[2] + " Z: " + cmps[3]);
                    }
                    else if(cmps[0] == "vt")
                    {
                        temp_uvs.Add(new Vector2(float.Parse(cmps[1]), float.Parse(cmps[2])));
                        Debug.Log("UV's: X: " + cmps[1] + " Y: " + cmps[2]);
                    }
                    else if (cmps[0] == "vn")
                    {
                        temp_normals.Add(new Vector3(float.Parse(cmps[1]), float.Parse(cmps[2]), float.Parse(cmps[3])));
                        Debug.Log("Normals: X: " + cmps[1] + " Y: " + cmps[2] + " Z: " + cmps[3]);
                    }
                    else if(cmps[0] == "f")
                    {

                        int[] vertexIndex = new int[3];
                        int[] uvIndex = new int[3];
                        int[] normalIndex = new int[3];

                        
                            for (int i = 1; i < cmps.Length; i++)
                            {
                                string felement = cmps[i];
                                
                             
                                if (felement.Contains("//"))
                                {
                                    //doubleslash, no UVS.
                                    string[] elementComps = felement.Split('/');
                                    Debug.Log(elementComps[0]);
                                    Debug.Log(elementComps[2]);
                                    vertexIndex[i - 1] = int.Parse(elementComps[0]);
                                    normalIndex[i - 1] = int.Parse(elementComps[2]);
                                }
                           }

                    triangles.Add(vertexIndex[0]);
                    triangles.Add(vertexIndex[1]);
                    triangles.Add(vertexIndex[2]);

                    vertexIndices.Add(vertexIndex[0]);
                        vertexIndices.Add(vertexIndex[1]);
                        vertexIndices.Add(vertexIndex[2]);

                        normalIndices.Add(normalIndex[0]);
                        normalIndices.Add(normalIndex[1]);
                        normalIndices.Add(normalIndex[2]);

                       }

                    for(int i = 0; i < vertexIndices.Count; i++)
                    {
                        int vertexIndex = vertexIndices[i];
                        Vector3 vertex = temp_vertices[vertexIndex - 1];
                        vertices.Add(vertex);
                    }

                    for(int i = 0; i < normalIndices.Count; i++)
                    {
                        int normalIndex = normalIndices[i];
                        Vector3 normal = temp_normals[normalIndex - 1];
                        normals.Add(normal);
                    }
                //triangles = new int[6 * 6];
                //for (int ti = 0, vi = 0, y = 0; y < (int)Mathf.Sqrt(temp_vertices.Count); y++, vi++)
                //{
                //    for (int x = 0; x < (int)Mathf.Sqrt(temp_vertices.Count); x++, ti += 6, vi++)
                //    {
                //        triangles[ti] = vi;
                //        triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                //        triangles[ti + 4] = triangles[ti + 1] = vi + (int)Mathf.Sqrt(temp_vertices.Count) + 1;
                //        triangles[ti + 5] = vi + (int)Mathf.Sqrt(temp_vertices.Count) + 2;
                //    }

                //}
                
                }

            m.vertices = temp_vertices.ToArray();
            m.normals = temp_normals.ToArray();
            m.triangles = triangles.ToArray();
                
                m.RecalculateNormals();
                m.RecalculateBounds();


                
               
            


         
        }
        else
        {
            Debug.LogError("File not found");
        }

            return m;
    }
}

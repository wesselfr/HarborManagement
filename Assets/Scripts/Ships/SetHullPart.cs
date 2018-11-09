using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHullPart : MonoBehaviour
{
    [SerializeField]
    int Index;

    [SerializeField]
    int ID = 1;

    MeshFilter mesh;

    // Use this for initialization
    void Start()
    {
        mesh = GetComponent<MeshFilter>();
        UpdateMesh();
    }

    void UpdateMesh()
    {
        FastObjImporter importer = new FastObjImporter();
        mesh.mesh = importer.ImportFile(GameDataManager.instance.ReturnHullWithID(ID).ModelPath);
    }
}

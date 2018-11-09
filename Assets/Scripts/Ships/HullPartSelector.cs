using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HullPartSelector : MonoBehaviour {

    [SerializeField]
    int Index;

    [SerializeField]
    int ID;

    MeshFilter mesh;

	// Use this for initialization
	void Start () {
        ID = 1;
        mesh = GetComponent<MeshFilter>();
        UpdateMesh();
	}

    public void SetID(int id)
    {
        ID = id;
        UpdateMesh();
    }

    public int GetIndex()
    {
        return Index;
    }

    public void Reset()
    {
        ID = 1;
        UpdateMesh();
    }

    void UpdateMesh()
    {
        FastObjImporter importer = new FastObjImporter();
        mesh.mesh = importer.ImportFile(GameDataManager.instance.ReturnHullWithID(ID).ModelPath);
    }

	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()
    {
        ID = ShipBuilder.instance.ReturnID();
        Debug.Log("Placed: " + ID + " At " + Index);
        ShipBuilder.instance.SetValue(Index, ID);
        UpdateMesh();

        /*
         * Old
        ID++;

        if (ID >= GameDataManager.instance.MaxHullID())
        {
            ID = 0;
        }

        if(GameDataManager.instance.DoesHullExsist(ID) == false)
        {
            OnMouseDown();
        }
    
        ShipBuilder.instance.SetValue(Index, ID);
        UpdateMesh();
        */
    }
}

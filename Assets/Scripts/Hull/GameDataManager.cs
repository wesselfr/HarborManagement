using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEditor;


public class GameDataManager : MonoBehaviour
{
    public static GameDataManager instance;

    private List<GenericHull> _Hull;
    private List<GenericCargo> _Cargo;

    [SerializeField]
    private Material _Mat;

    [SerializeField]
    private GameObject[] _HullGameObjects;

    private void Awake() 
    {
        instance = this;

        _Hull = new List<GenericHull>();
        _Cargo = new List<GenericCargo>();

        Load();
        
        foreach(GenericHull hull in _Hull)
        {
            Debug.Log(hull.Name);
        }
        foreach(GenericCargo cargo in _Cargo)
        {
            Debug.Log(cargo.Name);
        }

    }

    public GenericHull ReturnHullWithID(int ID)
    {
        GenericHull Hull = new GenericHull();
        for (int i = 0; i < _Hull.Count; i++)
        {
            if (_Hull[i].ID == ID)
            {
                Hull = _Hull[i];
            }
        }
        return Hull;
    }

    public GenericCargo ReturnCargoWithID(int ID)
    {
        GenericCargo Cargo = new GenericCargo();
        for (int i = 0; i < _Cargo.Count; i++)
        {
            if (_Cargo[i].ID == ID)
            {
                Cargo = _Cargo[i];
            }
        }
        return Cargo;
    }

    public GenericHull[] ReturnHulls()
    {
        return _Hull.ToArray();
    }

    public bool DoesHullExsist(int ID)
    {
        bool exsist = true;

        if(_Hull[ID] == null)
        {
            exsist = false;
        }
        return exsist;
    }

    public int MaxHullID()
    {
        return _Hull[_Hull.Count-1].ID;
    }

    void Load()
    {
        string[] Directorys = Directory.GetDirectories(Application.streamingAssetsPath + "/GameData/");

        foreach(string dir in Directorys)
        {
            if (Directory.Exists(dir + "/HullParts"))
            {
                string[] Path = Directory.GetFiles(dir + "/HullParts/", "*.txt", SearchOption.AllDirectories);
                foreach (string path in Path)
                {
                    try
                    {
                        using (StreamReader reader = new StreamReader(File.Open(path, FileMode.Open)))
                        {

                            string json = reader.ReadToEnd();

                            Debug.Log("Loading: " + path);

                            GenericHull[] loadedHullParts = JsonConvert.DeserializeObject<GenericHull[]>(json);
                            reader.Close();



                            for (int i = 0; i < loadedHullParts.Length; i++)
                            {
                                _Hull.Add(loadedHullParts[i]);
                            }
                        }
                    }
                    catch
                    {
                        CreateNewHullData();
                    }
                }
            }
            if (Directory.Exists(dir + "/CargoItems/")) 
            {
                string[] Path = Directory.GetFiles(dir + "/CargoItems/", "*.txt", SearchOption.AllDirectories);

                foreach (string path in Path)
                {
                    try
                    {
                        using (StreamReader reader = new StreamReader(File.Open(path, FileMode.Open)))
                        {
                            string json = reader.ReadToEnd();

                            Debug.Log("Loading: " + path);

                            GenericCargo[] loadedCargoItems = JsonConvert.DeserializeObject<GenericCargo[]>(json);
                            reader.Close();



                            for (int i = 0; i < loadedCargoItems.Length; i++)
                            {
                                _Cargo.Add(loadedCargoItems[i]);
                            }
                        }
                    }
                    catch
                    {
                        CreateNewCargoData(path);
                    }
                }
            }
        }

        GenericHull[] HullSorted = _Hull.OrderBy(p => p.ID).ToArray();
        _Hull = HullSorted.ToList();

        GenericCargo[] CargoSorted = _Cargo.OrderBy(p => p.ID).ToArray();
        _Cargo = CargoSorted.ToList();

        GenerateGameObjects();
    }

    void GenerateGameObjects()
    {
        ModelManager ModelImporter = new ModelManager();
        FastObjImporter importer = new FastObjImporter();

        GameObject ObjectHolder = GameObject.FindGameObjectWithTag("ObjectHolder");

        _HullGameObjects = new GameObject[_Hull.Count];
        for(int i = 0; i<_Hull.Count; i++)
        {
            GameObject HullObject = new GameObject();
            HullObject.name = _Hull[i].Name;
            HullObject.AddComponent<MeshFilter>().mesh = importer.ImportFile(_Hull[i].ModelPath);
            //HullObject.AddComponent<MeshFilter>().mesh = ModelImporter.loadMeshFromFile(_Hull[i].ModelPath);
            HullObject.AddComponent<MeshRenderer>().material = new Material(_Mat);
            HullObject.transform.position = Vector3.zero;
            HullObject.transform.SetParent(ObjectHolder.transform);
            _HullGameObjects[i] = HullObject;
        }
        ObjectHolder.SetActive(false);

    }

    void CreateNewHullData()
    {
        using (StreamWriter writer = new StreamWriter(File.Create(Application.streamingAssetsPath + "/HullParts/hullParts.txt")))
        {
            Debug.Log("Generate new json file for HullParts");

            List<GenericHull> Hull = new List<GenericHull>();


            //GenericHull[] Hull = new GenericHull[3];
            //Hull[0] = new GenericHull();
            Hull.Add(new GenericHull());
            Hull[0].ID = 1;
            Hull[0].Name = "Plain Hull";

            Hull[0].ModelPath = Application.streamingAssetsPath + "/Models/Test.fbx";

            Hull[0].Price = 20f;

            Hull[0].MovementPenalty = 10f;
            Hull[0].Armor = 10f;

            Hull[0].Damage = 10f;
            Hull[0].DamageType = DamageType.Collision;

            Hull.Add(new GenericHull());
            Hull[1].ID = 2;
            Hull[1].Name = "Plain Hull";

            Hull[1].ModelPath = Application.streamingAssetsPath + "/Models/Test.fbx";

            Hull[1].Price = 20f;

            Hull[1].MovementPenalty = 10f;
            Hull[1].Armor = 10f;

            Hull[1].Damage = 10f;
            Hull[1].DamageType = DamageType.Collision;



            string json = JsonConvert.SerializeObject(Hull, Formatting.Indented);

            writer.Write(json);
            writer.Close();

            
            for (int i = 0; i < Hull.Count - 1; i++)
            {
                _Hull.Add(Hull[i]);
            }
        }

    }

    void CreateNewCargoData(string path)
    {
        using (StreamWriter writer = new StreamWriter(File.Create(path)))
        {
            Debug.Log("Generate new json file for Cargo");

            GenericCargo[] Cargo = new GenericCargo[3];
            Cargo[0] = new GenericCargo();
            Cargo[0].ID = 1;
            Cargo[0].Name = "Sugar";
            Cargo[0].Price = 20f;

            Cargo[1] = new GenericCargo();
            Cargo[1].ID = 2;
            Cargo[1].Name = "Pepper";
            Cargo[1].Price = 40f;

            Cargo[2] = new GenericCargo();
            Cargo[2].ID = 3;
            Cargo[2].Name = "Beer";
            Cargo[2].Price = 25f;


            string json = JsonConvert.SerializeObject(Cargo, Formatting.Indented);

            writer.Write(json);
            writer.Close();

            
            for (int i = 0; i < Cargo.Length; i++)
            {
                _Cargo.Add(Cargo[i]);
            }
        }

    }
}
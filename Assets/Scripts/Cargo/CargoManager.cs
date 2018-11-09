using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;



public class CargoManager : MonoBehaviour
{

    private GenericCargo[] _Cargo;

    private void Start()
    {

        Load();
        Debug.Log(_Cargo[0].Name);
        Debug.Log(_Cargo[1].Name);

        Debug.Log(ReturnCargoWithID(3).Name);
        Debug.Log(ReturnCargoWithID(4).Name);

    }

    public GenericCargo ReturnCargoWithID(int ID)
    {
        GenericCargo Cargo = new GenericCargo();
        for(int i = 0; i < _Cargo.Length; i++)
        {
            if(_Cargo[i].ID == ID)
            {
                Cargo = _Cargo[i];
            }
        }
        return Cargo;
    }

    void Load()
    {

        try
        {
            using (StreamReader reader = new StreamReader(File.Open(Application.streamingAssetsPath + "/cargo.txt", FileMode.Open)))
            {
                string json = reader.ReadToEnd();

                Debug.Log("Loading Cargo");

                GenericCargo[] cargo = JsonConvert.DeserializeObject<GenericCargo[]>(json);
                reader.Close();

                _Cargo = new GenericCargo[cargo.Length];

                for (int i = 0; i < cargo.Length; i++)
                {
                    _Cargo[i] = cargo[i];
                }
            }
        }
        catch
        {
            CreateNewData();
        }
    }

    void CreateNewData()
    {
        using (StreamWriter writer = new StreamWriter(File.Create(Application.streamingAssetsPath + "/cargo.txt")))
        {
            Debug.Log("Generate new json file");

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

            _Cargo = new GenericCargo[Cargo.Length];
            for (int i = 0; i < Cargo.Length; i++) {
                _Cargo[i] = Cargo[i];
            }
        }

    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class ShipManager : MonoBehaviour {

    [SerializeField]
    private List<GenericShip> _Ship;

    public static ShipManager instance;
    
    // Use this for initialization
    void Awake () {
        instance = this;

        _Ship = new List<GenericShip>();
        OpenFile();
    }
	

    public void CreateNewShip(GenericShip ship)
    {
        ship.ID = _Ship.Count;

        _Ship.Add(ship);
        SaveToFile();
    }

    public void UpdateShip(GenericShip ship)
    {
        for(int i = 0; i < _Ship.Count - 1; i++)
        {
            if(_Ship[i].ID == ship.ID)
            {
                _Ship[i] = ship;
                break;
            }
        }

        SaveToFile();
    }

    public GenericShip ReturnShipAt(int index)
    {
        return _Ship[index];
    }

    public void OpenFile()
    {
        try
        {
            using (StreamReader reader = new StreamReader(File.Open(Application.streamingAssetsPath + "/Ships/Shiplibrary.txt", FileMode.Open)))
            {
                Debug.Log("Loading ships from file");

                string json = reader.ReadToEnd();

                _Ship = JsonConvert.DeserializeObject<List<GenericShip>>(json);

                if(_Ship == null)
                {
                    _Ship = new List<GenericShip>();
                }


            }
        }
        catch
        {
            Debug.Log("No Ship Library found!");
        }
    }

    public void SaveToFile()
    {
        using (StreamWriter writer = new StreamWriter(File.Create(Application.streamingAssetsPath + "/Ships/Shiplibrary.txt")))
        {
            Debug.Log("Generate new json file for Ships");

            string json = JsonConvert.SerializeObject(_Ship, Formatting.Indented);
            writer.Write(json);
            writer.Close();
        }
    }
}

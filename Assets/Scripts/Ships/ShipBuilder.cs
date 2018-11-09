using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipBuilder : MonoBehaviour {

    GenericShip _Ship;

    public static ShipBuilder instance;

    [SerializeField]
    private InputField _ShipName;

    [SerializeField]
    private int _SelectedID = 1;

    private string _LastShipName;

    private bool _FirstSave;
    private bool _Running = false;

    [SerializeField]
    private Transform _Content;

    [SerializeField]
    private GameObject _IDSelectorPrefab;

	// Use this for initialization
	void Start () {
        instance = this;
        NewShip();

        GenericHull[] hulls = GameDataManager.instance.ReturnHulls();

        foreach(GenericHull hull in hulls)
        {
            GameObject button =  Instantiate(_IDSelectorPrefab, _Content);
            button.GetComponent<IDSelector>().SetID(hull.ID);
        }

	}
	
	// Update is called once per frame
	void Update () {
		if(_ShipName.text != _LastShipName)
        {
            _Ship.Name = _ShipName.text;
            _LastShipName = _Ship.Name;
        }
	}

    public void NewShip()
    {
        _FirstSave = true;

        _Ship = new GenericShip();
        _Ship.HullParts = new int[5];

        if (_Running)
        {
            foreach (GameObject hullSelector in GameObject.FindGameObjectsWithTag("HullBuilder"))
            {
                hullSelector.GetComponent<HullPartSelector>().Reset();
            }
        }

        GenerateShipName();

        _Running = true;

    }

    public void LoadShip(GenericShip shipToLoad) 
    {

        _Ship = shipToLoad;

        GameObject[] rawHullSelectors = GameObject.FindGameObjectsWithTag("HullBuilder");
        HullPartSelector[] HullSelectors = new HullPartSelector[rawHullSelectors.Length];

        foreach(GameObject partSelector in rawHullSelectors)
        {
            HullPartSelector selector = partSelector.GetComponent<HullPartSelector>();
            HullSelectors[selector.GetIndex()] = selector;
        }

        for (int i = 0; i < _Ship.HullParts.Length; i++)
        {
            HullSelectors[i].SetID(_Ship.HullParts[i]);
        }

        _ShipName.text = _Ship.Name;

        _Running = true;
    }

    public void SetValue(int index, int ID)
    {
        _Ship.HullParts[index] = ID;
    }

    public void SaveShip()
    {
        if (_FirstSave)
        {
            ShipManager.instance.CreateNewShip(_Ship);
            _FirstSave = false;
        }
        else
        {
            ShipManager.instance.UpdateShip(_Ship);
        }
    }

    public void SetID(int id)
    {
        _SelectedID = id;
    }

    public int ReturnID()
    {
        return _SelectedID;
    }


    public void OnNameChange()
    {
        _Ship.Name = _ShipName.text;
        _ShipName.text = _Ship.Name;
    }

    public void GenerateShipName()
    {
        string[] FirstNames = 
        {
            "The",
            "The Bloody",
            "The Black",
            "Black",
            "Royal",
            "Golden",
            "The CSS",
            "The Flying",
 
        };

        string[] MiddelNames = 
        {
            "Fearsome",
            "Red",
            "Chaotic",
            "Sailing",
        };

        string[] LastNames = 
        {
            "Mermaid",
            "Sailer",
            "Pirate's Dream",
            "Dutchman",
            "Pearl",
            "Fortune",
            "Executioner",
            "Shame",
            "Death",
            "Scream",

        };

        int lenght = Random.Range(2, 3);

        string name = "";

        switch (lenght)
        {
            case 2:
                int first = Random.Range(0, FirstNames.Length);
                int last = Random.Range(0, LastNames.Length);

                name = FirstNames[first] + " " + LastNames[last];

                break;
            case 3:
                first = Random.Range(0, FirstNames.Length);
                int mid = Random.Range(0, MiddelNames.Length);
                last = Random.Range(0, LastNames.Length);

                name = FirstNames[first] + " " + MiddelNames[mid] + " " + LastNames[last];

                break;
        }

        _Ship.Name = name;
        _ShipName.text = _Ship.Name;

        
    }
}


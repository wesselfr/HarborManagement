using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IDSelector : MonoBehaviour {

    [SerializeField]
    private Text _Text;

    [SerializeField]
    private int ID;

	// Use this for initialization
	void Start () {
        UpdateText();
	}
	
    private void UpdateText()
    {
        _Text.text = ID + ": " + GameDataManager.instance.ReturnHullWithID(ID).Name;
    }

    public void SetID(int id)
    {
        ID = id;
        UpdateText();
    }

    public void ButtonClicked()
    {
        ShipBuilder.instance.SetID(ID);
        Debug.Log("ID = " + ID);
    }

    public void OnMouseDown()
    {
        ShipBuilder.instance.SetID(ID);
        Debug.Log("ID = " + ID);
    }

    // Update is called once per frame
    void Update () {
		if(_Text.text == "")
        {
            UpdateText();
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipLoader : MonoBehaviour {

    [SerializeField]
    private bool _inShipBuilder;

    [SerializeField]
    private GenericShip _ShipToLoad;

    [SerializeField]
    private int _Index;

    public void Start()
    {
        _ShipToLoad = ShipManager.instance.ReturnShipAt(_Index);
    }

    public void SetShip(GenericShip ship)
    {
        _ShipToLoad = ship;
    }

    public void LoadShip()
    {
        
        if (_inShipBuilder)
        {
            Debug.Log("Loading ship: " + _ShipToLoad.Name + " into ship editor");
            ShipBuilder.instance.LoadShip(_ShipToLoad);
        }

    }


}

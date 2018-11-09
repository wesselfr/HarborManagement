using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShipMover : MonoBehaviour {

    private bool _Selected = false;

    [SerializeField]
    GameObject _Indicator;

    NavMeshAgent _Agent;

    private void OnEnable()
    {
        WaypointSetter.OnNewWaypoint += SetWaypoint;
    }

    private void OnDisable()
    {
        WaypointSetter.OnNewWaypoint -= SetWaypoint;
    }

    // Use this for initialization
    void Start () {
        _Agent = GetComponent<NavMeshAgent>();
	}
	
    public void SetWaypoint(Vector3 waypoint)
    {
        if (_Selected)
        {
            _Agent.SetDestination(waypoint);
        }
    }

	// Update is called once per frame
	void Update () {

        if(Selector.instance.IsSelecting() == true)
        {
            _Selected = Selector.instance.IsWithinSelectionBounds(this.gameObject);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _Selected = false;
        }

        _Indicator.SetActive(_Selected);
	}

    private void OnMouseDown()
    {
        _Selected = !_Selected;
    }
}

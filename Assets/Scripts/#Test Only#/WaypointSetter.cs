using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointSetter : MonoBehaviour {

    public delegate void WaypointClickEvent(Vector3 waypoint);
    public static event WaypointClickEvent OnNewWaypoint;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            OnMouseDown();
        }
	}

    private void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitinfo = new RaycastHit();
        if (Physics.Raycast(ray, out hitinfo))
        {
            Vector3 Waypoint = hitinfo.point;
            Debug.Log("Click Pos: " + Waypoint);
            if (OnNewWaypoint != null)
            {
                OnNewWaypoint(Waypoint);
                Debug.Log("Waypoint Set: " + Waypoint);
            }
        }

    }
}

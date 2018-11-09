using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShipMover : MonoBehaviour {

    private bool m_IsSelected = false;

    [SerializeField]
    GameObject m_Indicator;

    [SerializeField]
    private LineRenderer m_LineRenderer;

    NavMeshAgent m_Agent;

    [SerializeField]
    private Transform[] m_PreMadeWaypoints;

    private LinkedList<VisualWaypoint> m_Waypoints;
    private VisualWaypoint m_LastWaypoint;

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
        m_Agent = GetComponent<NavMeshAgent>();
        m_Waypoints = new LinkedList<VisualWaypoint>();
        if(m_PreMadeWaypoints.Length > 0)
        {
            for(int i = 0; i < m_PreMadeWaypoints.Length; i++)
            {
                SetWaypoint(m_PreMadeWaypoints[i].position);
                m_Waypoints.Last.Value.Hide();
            }
        }
	}
	
    public void SetWaypoint(Vector3 waypoint)
    {
        if (m_IsSelected)
        {
            GameObject waypointObject = Instantiate(Resources.Load<GameObject>("Waypoint"), waypoint, Quaternion.identity);
            VisualWaypoint visual = waypointObject.GetComponent<VisualWaypoint>();
            visual.Initialize(waypoint);
            visual.onWaypointDestroyed += RemoveWaypoint;
            m_Waypoints.AddLast(visual);

            UpdateLines();
        }
    }

	// Update is called once per frame
	void Update () {

        if(Selector.instance.IsSelecting() == true)
        {
            m_IsSelected = Selector.instance.IsWithinSelectionBounds(this.gameObject);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_IsSelected = false;
        }

        if(m_Agent.pathStatus == NavMeshPathStatus.PathComplete && m_Agent.remainingDistance <= (m_Agent.radius /2f))
        {
            if (m_LastWaypoint != null)
            {
                m_LastWaypoint.Destroy();
            }

            if(m_Waypoints.Count > 0)
            {
                m_Agent.SetDestination(m_Waypoints.First.Value.position);
                m_LastWaypoint = m_Waypoints.First.Value;
                m_Waypoints.RemoveFirst();
            }
        }

        m_Indicator.SetActive(m_IsSelected);

        UpdateNavigationLine();
	}

    private void UpdateLines()
    {
        LinkedListNode<VisualWaypoint> node = m_Waypoints.First;

        if (node != null)
        {
            if(m_LastWaypoint != null)
            {
                m_LastWaypoint.DrawLineTo(node.Value.position);
            }
            node.Value.DrawLineTo(transform.position);
            for (int i = 0; i < m_Waypoints.Count; i++)
            {
                if (node.Next != null)
                {
                    node.Value.DrawLineTo(node.Next.Value.position);
                    node = node.Next;
                }
                else
                {
                    node.Value.DrawLineTo(node.Value.position);
                }
            }
        }

    }

    private void UpdateNavigationLine()
    {
        if (m_LastWaypoint != null)
        {
            m_LineRenderer.SetPosition(0, transform.position + Vector3.up);
            m_LineRenderer.SetPosition(1, m_LastWaypoint.position + Vector3.up);
        }
        else
        {
            m_LineRenderer.SetPosition(0, transform.position + Vector3.up);
            m_LineRenderer.SetPosition(1, transform.position + Vector3.up);
        }
    }

    private void RemoveWaypoint(VisualWaypoint waypoint)
    {
        m_Waypoints.Remove(waypoint);
        waypoint.Destroy();

        UpdateLines();
    }

    private void OnMouseDown()
    {
        m_IsSelected = !m_IsSelected;
    }
}

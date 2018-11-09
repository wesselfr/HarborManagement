using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class VisualWaypoint : MonoBehaviour {

    public delegate void VisualWaypointEvent(VisualWaypoint node);
    public VisualWaypointEvent onWaypointDestroyed;

    private Vector3 m_Position;

    [SerializeField]
    private LineRenderer m_LineRenderer;

    private Vector3 m_LineRenderTarget;

    public void Initialize(Vector3 position)
    {
        m_Position = position;
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (onWaypointDestroyed != null)
            {
                onWaypointDestroyed(this);
            }
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (onWaypointDestroyed != null)
            {
                onWaypointDestroyed(this);
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void DrawLineTo(Vector3 position)
    {
        m_LineRenderer.SetPosition(0, m_Position + Vector3.up);
        m_LineRenderer.SetPosition(1, position + Vector3.up);
    }

    public void Hide()
    {
        m_LineRenderer.enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }

    public Vector3 position { get { return m_Position; } }
}

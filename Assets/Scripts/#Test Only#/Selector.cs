using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{ 
    private bool m_IsSelecting = false;
    private Vector3 m_FirstMousePosition;

    public static Selector instance;

    [SerializeField]
    private Color m_MainColor, m_BorderColor;

    private void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftShift))
        {
            m_IsSelecting = true;
            m_FirstMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            m_IsSelecting = false;
        }
    }

    void OnGUI()
    {
        if (m_IsSelecting)
        {
            Rect selectionBoxRect = Utilities.GetScreenRect(m_FirstMousePosition, Input.mousePosition);
            Utilities.DrawScreenRect(selectionBoxRect, m_MainColor);
            Utilities.DrawScreenRectBorder(selectionBoxRect, 2, m_BorderColor);
        }
    }

    public bool IsWithinSelectionBounds(GameObject gameObject)
    {
        if (!m_IsSelecting)
        {
            return false;
        }

        Camera camera = Camera.main;
        Bounds viewportBounds = Utilities.GetViewportBounds(camera, m_FirstMousePosition, Input.mousePosition);

        return viewportBounds.Contains(camera.WorldToViewportPoint(gameObject.transform.position));
    }

    public bool IsSelecting()
    {
        return m_IsSelecting;
    }
}

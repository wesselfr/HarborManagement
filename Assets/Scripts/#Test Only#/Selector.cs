using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{

    private bool _IsSelecting = false;
    private Vector3 _FirstMousePosition;

    public static Selector instance;

    [SerializeField]
    private Color _MainColor, _BorderColor;

    private void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftShift))
        {
            _IsSelecting = true;
            _FirstMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            _IsSelecting = false;
        }
    }

    void OnGUI()
    {
        if (_IsSelecting)
        {
            Rect selectionBoxRect = Utilities.GetScreenRect(_FirstMousePosition, Input.mousePosition);
            Utilities.DrawScreenRect(selectionBoxRect, _MainColor);
            Utilities.DrawScreenRectBorder(selectionBoxRect, 2, _BorderColor);
        }
    }

    public bool IsWithinSelectionBounds(GameObject gameObject)
    {
        if (!_IsSelecting)
            return false;

        var camera = Camera.main;
        var viewportBounds =
            Utilities.GetViewportBounds(camera, _FirstMousePosition, Input.mousePosition);

        return viewportBounds.Contains(
            camera.WorldToViewportPoint(gameObject.transform.position));
    }

    public bool IsSelecting()
    {
        return _IsSelecting;
    }
}

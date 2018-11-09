using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    [SerializeField]
    private Transform m_MoveHandle;

    [SerializeField]
    private Transform m_ZoomHandle;

    [SerializeField]
    private Transform m_RotationHandle;

    [SerializeField]
    private Transform m_RotateAround;

    [SerializeField]
    private float m_Speed;

    [SerializeField]
    private float m_ZoomSpeed;

    [SerializeField]
    private float m_RotationSpeed;

    private Vector3 m_OldMousePosition;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //m_MoveHandle.position += new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * m_Speed * Time.deltaTime;
        m_ZoomHandle.position += m_ZoomHandle.forward * Input.GetAxis("Mouse ScrollWheel") * m_ZoomSpeed * Time.deltaTime;

        m_MoveHandle.position += m_MoveHandle.forward * Input.GetAxis("Vertical") * m_Speed * Time.deltaTime;
        m_MoveHandle.position += m_MoveHandle.right * Input.GetAxis("Horizontal") * m_Speed * Time.deltaTime;


        if (Input.GetKey(KeyCode.Q))
        {
            m_RotationHandle.RotateAround(m_RotateAround.position, m_RotationHandle.up, -m_RotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.E))
        {
            m_RotationHandle.RotateAround(m_RotateAround.position, m_RotationHandle.up, m_RotationSpeed * Time.deltaTime);
        }

        //Vector3 newMousePosition = Input.mousePosition;
        //Vector3 mouseDelta = newMousePosition - m_OldMousePosition;

        //if (Input.GetMouseButton(2))
        //{
        //    if (Mathf.Abs(mouseDelta.x) < Mathf.Abs(mouseDelta.y))
        //    {
        //        m_RotationHandle.RotateAround(m_RotateAround.position, m_RotationHandle.right, mouseDelta.x * m_RotationSpeed * Time.deltaTime);
        //    }
        //    else
        //    {
        //        m_RotationHandle.RotateAround(m_RotateAround.position, m_RotationHandle.up, mouseDelta.y * m_RotationSpeed * Time.deltaTime);
        //    }
        //}

        //m_OldMousePosition = newMousePosition;
        
	}
}

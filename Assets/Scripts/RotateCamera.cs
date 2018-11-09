using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour {

    [SerializeField]
    Transform _Target;

    [SerializeField]
    float _Speed;

    bool _CanMove = true;

    public void DontMove()
    {
        _CanMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (Input.GetKey(KeyCode.LeftAlt))
            {
                _Target.position = new Vector3(_Target.position.x +  Input.GetAxis("Mouse X") * _Speed, _Target.position.y, _Target.position.z + Input.GetAxis("Mouse Y"));
                transform.position = new Vector3(transform.position.x + Input.GetAxis("Mouse X") * _Speed, transform.position.y, transform.position.z + Input.GetAxis("Mouse Y"));
                transform.LookAt(_Target);
            }
            else
            {
                transform.LookAt(_Target);
                transform.RotateAround(_Target.position, Vector3.up, Input.GetAxis("Mouse X") * _Speed);
                transform.RotateAround(_Target.position, Vector3.left, Input.GetAxis("Mouse Y") * _Speed);
               
            }
        }
        _CanMove = true;
    }
}

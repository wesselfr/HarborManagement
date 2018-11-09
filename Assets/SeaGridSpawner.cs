using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaGridSpawner : MonoBehaviour {

    [SerializeField]
    private int m_XSize, m_YSize;

    [SerializeField]
    private GameObject m_SeaObject;

    [SerializeField]
    private int m_SeaXSize, m_SeaYSize;

    [SerializeField]
    private float m_Resolution;

	// Use this for initialization
	void Start () {
		for(int x = 0; x < m_XSize; x++)
        {
            for(int y = 0; y < m_YSize; y++)
            {
                GameObject sea = Instantiate(m_SeaObject, new Vector3(x * m_SeaXSize * m_Resolution, 0, y * m_SeaYSize * m_Resolution), Quaternion.identity);
                sea.transform.parent = this.transform;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

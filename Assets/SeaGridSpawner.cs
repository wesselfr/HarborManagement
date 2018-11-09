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
        StartCoroutine(GenerateGrid());
	}

    IEnumerator GenerateGrid()
    {
        float xOffset = ((m_SeaXSize / 2) * m_Resolution) - 0.5f;
        float yOffset = ((m_SeaYSize / 2) * m_Resolution) - 0.5f;
        for (int x = 0; x < m_XSize; x++)
        {
            for (int y = 0; y < m_YSize; y++)
            {
                GameObject sea = Instantiate(m_SeaObject, transform.position + new Vector3(x * xOffset, 0, y * yOffset), Quaternion.identity);
                //if(x != 0) { transform.position -= Vector3.left * 0.5f; }
                //if(y!= 0) { transform.position -= Vector3.back * 0.5f; }
                //sea.transform.parent = this.transform;

                yield return new WaitForEndOfFrame();
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCargo : MonoBehaviour {

    [SerializeField]
    Vector3[] m_BottomRows;

    [SerializeField]
    Vector3 m_HeightOffset;

    [SerializeField]
    private int m_MaxHeight;

    [SerializeField]
    private GameObject m_CargoPrefab;

    [SerializeField]
    private Material[] m_CargoColors;

	// Use this for initialization
	void Start () {
        GenerateCargo();
	}
	
    private void GenerateCargo()
    {
        for(int i = 0; i < m_BottomRows.Length; i++)
        {
            int height = Random.Range(1, m_MaxHeight + 1);
            for (int j = 0; j < height; j++)
            {
                GameObject cargo = Instantiate(m_CargoPrefab, transform);
                cargo.transform.localPosition = m_BottomRows[i] + m_HeightOffset * j;
                cargo.GetComponent<MeshRenderer>().material = m_CargoColors[Random.Range(0, m_CargoColors.Length)];
            }
        
        }
    }
}

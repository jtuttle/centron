using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public float m_SpawnCircleRadius = 8f;
    public int m_NumSpawnPoints = 5;
    public GameObject m_EnemyPrefab;

    // Use this for initialization
    void Start()
    {

    }

    void Awake()
    {
        // create spawn points
        for (int i = 0; i < m_NumSpawnPoints; i++)
        {
            float aa = i / (float)m_NumSpawnPoints * Mathf.PI * 2;
            float xx = Mathf.Cos(aa) * m_SpawnCircleRadius;
            float yy = Mathf.Sin(aa) * m_SpawnCircleRadius;

            Vector3 pos = new Vector3(xx, yy, 0f);

            GameObject tmpObj = GameObject.Instantiate(m_EnemyPrefab, pos, Quaternion.identity) as GameObject;

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}


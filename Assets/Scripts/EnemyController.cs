using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
  public float m_SpawnCircleRadius = 8f;
  public int m_NumSpawnPoints = 5;
  private Vector3[] m_SpawnSpots;
  public GameObject m_EnemyPrefab;
  public float m_PlanetSize;

  private List<GameObject> m_Enemies;
  private float timer;
  private float nextSpawnTime;

  //private GameObject[] m_Enemies;


  void Awake() {
    m_SpawnSpots = new Vector3[m_NumSpawnPoints];
    m_Enemies = new List<GameObject>();

    // create spawn points
    for (int i = 0; i < m_NumSpawnPoints; i++) {
      float aa = i / (float)m_NumSpawnPoints * Mathf.PI * 2;
      float xx = Mathf.Cos(aa) * m_SpawnCircleRadius;
      float yy = Mathf.Sin(aa) * m_SpawnCircleRadius;

      m_SpawnSpots[i] = new Vector3(xx, yy, 0f);

    }
  }

  // Use this for initialization
  void Start() {
    foreach (Vector3 pos in m_SpawnSpots) {
      m_Enemies.Add(GameObject.Instantiate(m_EnemyPrefab, pos, Quaternion.identity) as GameObject);
    }
    timer = 0f;
    nextSpawnTime = GetRandomSpawnTime(); 
  }


  // Update is called once per frame
  void Update() {

    timer += Time.deltaTime;

    foreach (GameObject enemy in m_Enemies) {
      enemy.transform.position = Vector3.MoveTowards(
        enemy.transform.position, 
        Vector3.zero, 
        Tuning.Get.EnemyMovementRate * Time.deltaTime);

      if (enemy.transform.position.magnitude < m_PlanetSize)
        HitPlanet(enemy);
    }

    CheckSpawnation();
  }

  private void CheckSpawnation() {
    if (timer > nextSpawnTime) {
      nextSpawnTime = GetRandomSpawnTime() + timer;

      int lane = UnityEngine.Random.Range(0, m_NumSpawnPoints);

      m_Enemies.Add(GameObject.Instantiate(m_EnemyPrefab, m_SpawnSpots[lane], Quaternion.identity) as GameObject);
    }
  }

  private float GetRandomSpawnTime() {
    return UnityEngine.Random.Range(
        Tuning.Get.UnitSpawnFrequencyMinimum,
        Tuning.Get.UnitSpawnFrequencyMaximum);
  }

  private void HitPlanet(GameObject enemy) {
    if (enemy.activeInHierarchy) EventModule.Event(EventType.ENEMY_HIT);
    enemy.SetActive(false);
  }
}


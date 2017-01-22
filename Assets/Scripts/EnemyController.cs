using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
  public float m_SpawnCircleRadius = 9f;
  public int m_NumSpawnPoints = 5;
  private Vector3[] m_SpawnSpots;
  public GameObject m_EnemyPrefab;
  public float m_PlanetSize;

  private List<GameObject> m_Enemies;
  private Stack<GameObject> m_EnemiesSpawnPool = new Stack<GameObject>();
  private float timer;
  private float nextSpawnTime;

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
      spawnEnemy(pos);
    }
    timer = 0f;
    nextSpawnTime = GetRandomSpawnTime(); 
    EventModule.Subscribe(handleGameObjectEvent);
  }

  void OnDestroy() {
    EventModule.Unsubscribe(handleGameObjectEvent);
  }

  void spawnEnemy(Vector3 m_spawnPos) {
    m_Enemies.Add(getEnemyInstance(m_spawnPos));
    EventModule.Event(EventType.SPAWN_ENEMY);
  }

  GameObject getEnemyInstance(Vector3 m_spawnPos) {
    if(spawnPoolHasEnemies()) { 
      return getEnemyInstanceFromSpawnPool(m_spawnPos);
    } else {
      return createNewEnemyInstance(m_spawnPos);
    }
  }

  GameObject createNewEnemyInstance(Vector3 m_spawnPos) {
    return GameObject.Instantiate(m_EnemyPrefab, m_spawnPos, Quaternion.identity) as GameObject;
  }

  GameObject getEnemyInstanceFromSpawnPool(Vector3 m_spawnPos) {
    GameObject enemy = m_EnemiesSpawnPool.Pop();
    enemy.transform.position = m_spawnPos;
    enemy.SetActive(true);
    return enemy;
  }

  bool spawnPoolHasEnemies() {
    return m_EnemiesSpawnPool.Count > 0;
  }
    
  void handleEnemyKilled(GameObject enemy) {
    m_Enemies.Remove(enemy);
    enemy.SetActive(false);
    m_EnemiesSpawnPool.Push(enemy);
  }

  // Update is called once per frame
  void Update() {
    //CheckSpawn();
    List<GameObject> enemiesToDestroy = new List<GameObject>();
    timer += Time.deltaTime;
    foreach (GameObject enemy in m_Enemies) {
      enemy.transform.position = Vector3.MoveTowards(
        enemy.transform.position, 
        Vector3.zero, 
        Tuning.Get.EnemyMovementRate * Time.deltaTime);

      if (enemy.transform.position.magnitude < m_PlanetSize) {
        HitPlanet(enemy);
        enemiesToDestroy.Add(enemy);
      }
    }
    for (int i = 0; i < enemiesToDestroy.Count; i++) {
      handleEnemyKilled(enemiesToDestroy[i]);
    }

    CheckSpawnation();
  }

  private void CheckSpawnation() {
    if (timer > nextSpawnTime) {
      nextSpawnTime = GetRandomSpawnTime() + timer;
      int lane = UnityEngine.Random.Range(0, m_NumSpawnPoints);
      spawnEnemy(m_SpawnSpots[lane]);
    }
  }

  private float GetRandomSpawnTime() {
    return UnityEngine.Random.Range(
        Tuning.Get.UnitSpawnFrequencyMinimum,
        Tuning.Get.UnitSpawnFrequencyMaximum);
  }

  private void HitPlanet(GameObject enemy) {
    if (enemy.activeInHierarchy) EventModule.Event(EventType.ENEMY_HIT);
  }

  void handleGameObjectEvent(string eventName, GameObject gameObject) {
    if(eventName == EventType.ENEMY_KILLED) {
      handleEnemyKilled(gameObject);
    }
  }

}

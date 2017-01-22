using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
  public GameObject trailerRendererPrefab;
  public Gradient enemyTrailGradient;
  public float m_SpawnCircleRadius = 9f;
  public int m_NumSpawnPoints = 5;
  private Vector3[] m_SpawnSpots;
  public GameObject m_EnemyPrefab;
  public float m_PlanetSize;

  private List<GameObject> m_Enemies;
  private Stack<GameObject> m_EnemiesSpawnPool = new Stack<GameObject>();
  private float _timer;
  private float _nextSpawnTime;
  private float _doneness;

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
    _timer = 0f;
    _nextSpawnTime = GetRandomSpawnTime(); 
    EventModule.Subscribe(handleGameObjectEvent);
  }

  void OnDestroy() {
    EventModule.Unsubscribe(handleGameObjectEvent);
  }

  void spawnEnemy(Vector3 m_spawnPos) {
    GameObject enemy = getEnemyInstance(m_spawnPos);
    m_Enemies.Add(enemy);
    GameObject trail = Instantiate(trailerRendererPrefab, enemy.transform);
    trail.transform.localPosition = Vector3.zero;
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
    Destroy(enemy.transform.GetChild(0).gameObject);
    m_EnemiesSpawnPool.Push(enemy);
  }

  // Update is called once per frame
  void Update() {
    //CheckSpawn();
    List<GameObject> enemiesToDestroy = new List<GameObject>();
    _timer += Time.deltaTime;
    _doneness = _timer / ((float)Tuning.Get.GameDuration * 60f);
    if (_doneness > 1) _doneness = 1;

    float moveRate = (Tuning.Get.LateGameMovementRate
      - Tuning.Get.StartingMovementRate) * _doneness
      + Tuning.Get.StartingMovementRate;

    foreach (GameObject enemy in m_Enemies) {
      if (enemy) {
        enemy.transform.position = Vector3.MoveTowards(
          enemy.transform.position, 
          Vector3.zero,
          moveRate * Time.deltaTime);

        if (enemy.transform.position.magnitude < m_PlanetSize) {
          HitPlanet(enemy);
          enemiesToDestroy.Add(enemy);
        }
      }
    }
    for (int i = 0; i < enemiesToDestroy.Count; i++) {
      handleEnemyKilled(enemiesToDestroy[i]);
    }

    CheckSpawnation();
  }

  private void CheckSpawnation() {
    if (_timer > _nextSpawnTime) {
      _nextSpawnTime = GetRandomSpawnTime() + _timer;


      float aa = UnityEngine.Random.Range(0, Mathf.PI * 2);
      Vector3 pos = new Vector3(
        Mathf.Cos(aa) * m_SpawnCircleRadius,
        Mathf.Sin(aa) * m_SpawnCircleRadius, 0);
      spawnEnemy(pos);

      //float xx = Mathf.Cos(aa) * m_SpawnCircleRadius;
      //float yy = Mathf.Sin(aa) * m_SpawnCircleRadius;
      //int lane = UnityEngine.Random.Range(0, m_NumSpawnPoints);
      //spawnEnemy(m_SpawnSpots[lane]);
    }
  }

  private float GetRandomSpawnTime() {
    float min = (Tuning.Get.LateGameUnitSpawnRateMinimum
      - Tuning.Get.StartingUnitSpawnRateMinimum) * _doneness
      + Tuning.Get.StartingUnitSpawnRateMinimum;
    float max = (Tuning.Get.LateGameUnitSpawnRateMaximum
      - Tuning.Get.StartingUnitSpawnRateMaximum) * _doneness
      + Tuning.Get.StartingUnitSpawnRateMaximum;
    return UnityEngine.Random.Range(min, max);
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

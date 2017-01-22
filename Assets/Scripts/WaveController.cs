using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour {
  public PlayerBase PlayerBase;

  private List<Wave> _waves;

  void Awake() {
    _waves = new List<Wave>();
    EventModule.Subscribe(OnEvent);
  }

  void OnDestroy() {
    EventModule.Unsubscribe(OnEvent);
  }

  void Update() {
    foreach(Wave wave in _waves) {
      wave.ElapsedDuration += Time.deltaTime;

      if(wave.WaveType == WaveType.Low) {
        UpdateLowWave(wave);
      } else {
        UpdateHighWave(wave);
      }
    }

    for(int i = _waves.Count - 1; i >= 0; i--) {
      Wave wave = _waves[i];

      if(wave.ElapsedDuration > wave.GetWaveDuration()) {
        GameObject.Destroy(wave.gameObject);
        _waves.RemoveAt(i);
      }
    }
  }

  private void OnEvent(string eventType) {
    if(eventType == EventType.SHOOT_HIGH_WAVE) {
      _waves.Add(SpawnHighWave());
    } else if (eventType == EventType.SHOOT_LOW_WAVE) {
      _waves.Add(SpawnLowWave());
    }
  }

  private Wave SpawnLowWave() {
    GameObject prototype = Resources.Load<GameObject>("Prefabs/LowWave");
    return GameObject.Instantiate(prototype).GetComponent<Wave>();
  }

  private Wave SpawnHighWave() {
    GameObject prototype = Resources.Load<GameObject>("Prefabs/HighWave");
    Wave wave = GameObject.Instantiate(prototype).GetComponent<Wave>();

    wave.transform.rotation = PlayerBase.transform.rotation * Quaternion.Euler(0, 0, -90);

    return wave;
  }

  private void UpdateLowWave(Wave wave) {
    float newScale =
      wave.GetStartScale()
      + (wave.GetEndScale() - wave.GetStartScale())
      * (wave.ElapsedDuration / wave.GetWaveDuration());
    
    wave.transform.localScale = new Vector2(newScale, newScale);
  }

  private void UpdateHighWave(Wave wave) {
    // Update position
    float elapsed = (wave.ElapsedDuration / wave.GetWaveDuration());
    float radius = elapsed * Tuning.Get.HighPitchAttackRange;
    float rotation =
      (wave.gameObject.transform.rotation * Quaternion.Euler(0, 0, 90)).eulerAngles.z;

    float x = Mathf.Cos(rotation * Mathf.Deg2Rad) * radius;
    float y = Mathf.Sin(rotation * Mathf.Deg2Rad) * radius;

    wave.transform.position = new Vector2(x, y);

    // Update scale
    float newScale =
      wave.GetStartScale()
      + (wave.GetEndScale() - wave.GetStartScale())
      * elapsed;

    wave.transform.localScale = new Vector2(newScale, newScale);
  }
}

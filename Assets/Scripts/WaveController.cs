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

  void Update() {
    foreach(Wave wave in _waves) {
      wave.ElapsedDuration += Time.deltaTime;

      if(wave.WaveType == WaveType.Low) {
        UpdateLowWave(wave);
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
    if(eventType == EventType.SHOOT_WAVE) {
      GameObject prototype;

      if(PlayerBase.CurrentWaveType == WaveType.Low) {
        prototype = Resources.Load<GameObject>("Prefabs/LowWave");
      } else {
        prototype = Resources.Load<GameObject>("Prefabs/HighWave");
      }

      _waves.Add(GameObject.Instantiate(prototype).GetComponent<Wave>());
    }
  }

  private void UpdateLowWave(Wave wave) {
    float newScale =
      wave.GetStartScale()
      + (wave.GetEndScale() - wave.GetStartScale())
      * (wave.ElapsedDuration / wave.GetWaveDuration());
    
    wave.transform.localScale = new Vector2(newScale, newScale);
  }
}

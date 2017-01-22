using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WaveType {
  Low, High
}

public class Base : MonoBehaviour {
  public int Health = 100;
  public float Cooldown = 100;
  public WaveType CurrentWaveType;

  public void Awake() {
    EventModule.Subscribe(OnSwitchWaveType);
  }

  private void OnSwitchWaveType(string eventType) {
    if(eventType == EventType.SWITCH_WAVE_TYPE) {
      CurrentWaveType =
        (CurrentWaveType == WaveType.Low) ? WaveType.High : WaveType.Low;
    }
  }
}

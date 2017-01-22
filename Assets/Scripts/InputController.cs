using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {
  private bool _shootWaveEnabled = true;

  public enum InputEvent {
    ShootWave, SwitchWaveType
  }

  void Awake() {
    EventModule.Subscribe(OnEvent);
  }

  void Update() {
    if(_shootWaveEnabled && Input.GetMouseButtonDown(0)) {
      ShootWave();
    }

    if(Input.GetMouseButtonDown(1)) {
      SwitchWaveType();
    }
  }

  public void OnEvent(string eventType) {
    if(eventType == EventType.WAVE_SHOOT_READY) {
      _shootWaveEnabled = true;
    }
  }

  private void ShootWave() {
    _shootWaveEnabled = false;
    EventModule.Event(EventType.SHOOT_WAVE);
  }

  private void SwitchWaveType() {
    EventModule.Event(EventType.SWITCH_WAVE_TYPE);
  }
}

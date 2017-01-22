using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {
  public enum InputEvent {
    ShootWave, SwitchWaveType
  }

  void Update() {
    if(Input.GetMouseButtonDown(0)) {
      ShootWave();
    }

    if(Input.GetMouseButtonDown(1)) {
      SwitchWaveType();
    }
  }

  private void ShootWave() {
    EventModule.Event(EventType.SHOOT_WAVE);
  }

  private void SwitchWaveType() {
    EventModule.Event(EventType.SWITCH_WAVE_TYPE);
  }
}

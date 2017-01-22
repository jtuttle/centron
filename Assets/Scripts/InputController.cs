using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {
  private bool _highWaveReady = true;
  private bool _lowWaveReady = true;
  [SerializeField]
  PlayerBase player;

  [SerializeField] KeyCode switchWaveKey = KeyCode.Space;
  public enum InputEvent {
    ShootWave, SwitchWaveType
  }

  void Awake() {
    EventModule.Subscribe(OnEvent);
  }

  void OnDestroy() {
    EventModule.Unsubscribe(OnEvent);
  }

  void Update() {
    if (player.Health > 0) {
      if (canShootCurrentWave() && Input.GetMouseButtonDown(0)) {
        ShootWave(player.CurrentWaveType);
      }

      if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(switchWaveKey)) {
        SwitchWaveType();
      }
    }
    if(Input.GetMouseButtonDown(0)) {
      EventModule.Event(EventType.CLICK);
    }
  }

  bool canShootCurrentWave() {
    return (_highWaveReady && player.CurrentWaveType == WaveType.High) ||
      (_lowWaveReady && player.CurrentWaveType == WaveType.Low);
  }

  public void OnEvent(string eventType) {
    if(eventType == EventType.HIGH_WAVE_OVERHEATED) {
      _highWaveReady = false;
    } else if(eventType == EventType.HIGH_WAVE_READY) {
      _highWaveReady = true;
    } else if(eventType == EventType.LOW_WAVE_READY) {
      _lowWaveReady = true;
    }
  }

  private void ShootWave(WaveType waveType) {
    switch(waveType) {
      case WaveType.High:
        EventModule.Event(EventType.SHOOT_HIGH_WAVE);
        break;
      case WaveType.Low:
        _lowWaveReady = false;
        EventModule.Event(EventType.SHOOT_LOW_WAVE);
        break;
    }
  }

  private void SwitchWaveType() {
    EventModule.Event(EventType.SWITCH_WAVE_TYPE);
  }
}

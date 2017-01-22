using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WaveType {
  Low, High
}

public class PlayerBase : MonoBehaviour {
  public int Health = 100;

  public float Cooldown = 0;
  public WaveType LastShootWaveType;

  public WaveType CurrentWaveType;

  public void Awake() {
    EventModule.Subscribe(OnEvent);
  }

  public void Update() {
    UpdateCooldown();
  }

  public float GetCooldownPercentage() {
    float total = GetCooldownForWaveType(LastShootWaveType);
    return Cooldown / total;
  }

  public bool WaveShootIsAllowed() {
    return Cooldown == 0;
  }

  private void UpdateCooldown() {
    float nextCooldown = Mathf.Max(0, Cooldown - Time.deltaTime);

    if(Cooldown > 0 && nextCooldown == 0) {
      EventModule.Event(EventType.WAVE_SHOOT_READY);
    }

    Cooldown = nextCooldown;
  }

  private float GetCooldownForWaveType(WaveType waveType) {
    return (waveType == WaveType.Low) ?
      Tuning.Get.LowPitchAttackCooldown : Tuning.Get.HighPitchAttackCooldown;
  }

  private void OnEvent(string eventType) {
    if(eventType == EventType.SHOOT_WAVE) {
      OnShootWave();
    } else if(eventType == EventType.SWITCH_WAVE_TYPE) {
      OnSwitchWaveType();
    }
  }

  private void OnShootWave() {
    LastShootWaveType = CurrentWaveType;
    Cooldown = GetCooldownForWaveType(CurrentWaveType);
  }

  private void OnSwitchWaveType() {
    CurrentWaveType =
      (CurrentWaveType == WaveType.Low) ? WaveType.High : WaveType.Low;
  }
}
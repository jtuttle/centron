using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WaveType {
  Low, High
}

public class PlayerBase : MonoBehaviour {
  public int Health;

  public float Cooldown = 0;
  public WaveType LastShootWaveType;

  public WaveType CurrentWaveType;

  public void Awake() {
    EventModule.Subscribe(OnEvent);
    Health = Tuning.Get.MaxPlayerBaseHealth;
  }

  public void Update() {
    UpdateCooldown();
  }

  public float GetHealthPercentage() {
    return Health / (float)Tuning.Get.MaxPlayerBaseHealth;
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
    } else if (eventType == EventType.SWITCH_WAVE_TYPE) {
      OnSwitchWaveType();
    } else if(eventType == EventType.ENEMY_HIT) {
      OnEnemyHit();
}
  }

  private void OnShootWave() {
    LastShootWaveType = CurrentWaveType;
    Cooldown = GetCooldownForWaveType(CurrentWaveType);
  }

  private void OnEnemyHit() {
    Health--;
    if(Health <= 0) {
      EventModule.Event(EventType.GAME_OVER);
    }
  }

  private void OnSwitchWaveType() {
    CurrentWaveType =
      (CurrentWaveType == WaveType.Low) ? WaveType.High : WaveType.Low;
  }
}
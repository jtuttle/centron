using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WaveType {
  Low, High
}

public class PlayerBase : MonoBehaviour {
  public int Health;

  public float HighWaveCooldown = 0;
  public float LowWaveCooldown = 0;

  public WaveType LastShootWaveType;

  public WaveType CurrentWaveType;

  public void Awake() {
    EventModule.Subscribe(OnEvent);
    Health = Tuning.Get.MaxPlayerBaseHealth;
  }

  public void Update() {
    UpdateCooldown();
  }

  public void OnDestroy() {
    EventModule.Unsubscribe(OnEvent);
  }

  public float GetHealthPercentage() {
    return Health / (float)Tuning.Get.MaxPlayerBaseHealth;
  }

  public float GetCooldownPercentage(WaveType type) {
    float total = GetCooldownForWaveType(type);
    switch(type) {
      case WaveType.High:
        return HighWaveCooldown / total;
      case WaveType.Low:
        return LowWaveCooldown / total;
      default:
        return default(float);
    }
  }

  public bool WaveShootIsAllowed(WaveType type) {
    switch(type) {
      case WaveType.High:
        return HighWaveCooldown <= 0;
      case WaveType.Low:
        return LowWaveCooldown <= 0;
      default:
        return default(bool);
    }
  }

  private void UpdateCooldown() {
    float nextHighCooldown = Mathf.Max(0, HighWaveCooldown - Time.deltaTime);
    if(HighWaveCooldown > 0 && nextHighCooldown <= 0) {
      EventModule.Event(EventType.HIGH_WAVE_READY);
    }
    HighWaveCooldown = nextHighCooldown;

    float nextLowCooldown = Mathf.Max(0, LowWaveCooldown - Time.deltaTime);
    if(LowWaveCooldown > 0 && nextLowCooldown <= 0) {
      EventModule.Event(EventType.LOW_WAVE_READY);
    }
    LowWaveCooldown = nextLowCooldown;
  }

  private float GetCooldownForWaveType(WaveType waveType) {
    return (waveType == WaveType.Low) ?
      Tuning.Get.LowPitchAttackCooldown : Tuning.Get.HighPitchAttackCooldown;
  }

  private void OnEvent(string eventType) {
    if(eventType == EventType.SHOOT_HIGH_WAVE) {
      OnShootWave(WaveType.High);
    } else if (eventType == EventType.SHOOT_LOW_WAVE) {
      OnShootWave(WaveType.Low);
    } else if (eventType == EventType.SWITCH_WAVE_TYPE) {
      OnSwitchWaveType();
    } else if(eventType == EventType.ENEMY_HIT) {
      OnEnemyHit();
    }
  }

  private void OnShootWave(WaveType waveType) {
    LastShootWaveType = CurrentWaveType;
    switch (waveType) {
      case WaveType.High:
        HighWaveCooldown = GetCooldownForWaveType(CurrentWaveType);
        break;
      case WaveType.Low:
        LowWaveCooldown = GetCooldownForWaveType(CurrentWaveType);
        break;
    }
  }

  private void OnEnemyHit() {
    if(Health <= 0) { return; }

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
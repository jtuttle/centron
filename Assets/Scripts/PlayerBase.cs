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

  private float _highWaveEnergy = 0;
  private float _highWaveEnergyMax;
  private float _highWaveEnergyDecrement;
  private float _highWaveEnergyGrowth;

  private SpriteRenderer _cannonSprite;
  private SpriteRenderer _cannonGlowSprite;
  private SpriteRenderer _ringGlowSprite;

  public void Awake() {
    EventModule.Subscribe(OnEvent);
    Health = Tuning.Get.MaxPlayerBaseHealth;

    _highWaveEnergyMax = Tuning.Get.MaxEnergyOfHighPitchAttack;
    _highWaveEnergyDecrement = Tuning.Get.EnergyLostPerShotOfHighPitchAttack;
    _highWaveEnergyGrowth = 0.1f; //Tuning.Get.EnergyGrowthRateOfHighPitchAttack;

    _highWaveEnergy = _highWaveEnergyMax;

    _cannonSprite = transform.Find("Cannon").GetComponent<SpriteRenderer>();
    _cannonGlowSprite = transform.Find("CannonGlow").GetComponent<SpriteRenderer>();
    _ringGlowSprite = transform.Find("RingGlow").GetComponent<SpriteRenderer>();
    _ringGlowSprite.enabled = false;
  }

  public void Update() {
    UpdateCooldown();

    if(HighWaveCooldown == 0) {
      _highWaveEnergy =
        Mathf.Min(_highWaveEnergy + _highWaveEnergyGrowth, _highWaveEnergyMax);
    }
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

  public float GetHighWaveEnergyPercentage() {
    return 1 - Mathf.Min(_highWaveEnergy / _highWaveEnergyMax, _highWaveEnergyMax);
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
      if(CurrentWaveType == WaveType.High)
        _cannonGlowSprite.enabled = true;

      EventModule.Event(EventType.HIGH_WAVE_READY);
    }
    HighWaveCooldown = nextHighCooldown;

    float nextLowCooldown = Mathf.Max(0, LowWaveCooldown - Time.deltaTime);
    if(LowWaveCooldown > 0 && nextLowCooldown <= 0) {
      if(CurrentWaveType == WaveType.Low)
        _ringGlowSprite.enabled = true;

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

    switch(waveType) {
      case WaveType.High:
        _highWaveEnergy = Mathf.Max(0, _highWaveEnergy - _highWaveEnergyDecrement);

        if(_highWaveEnergy == 0) {
          _cannonGlowSprite.enabled = false;
          HighWaveCooldown = GetCooldownForWaveType(CurrentWaveType);
          _highWaveEnergy = _highWaveEnergyMax;
          EventModule.Event(EventType.HIGH_WAVE_OVERHEATED);
        }

        break;
      case WaveType.Low:
        LowWaveCooldown = GetCooldownForWaveType(CurrentWaveType);
        _ringGlowSprite.enabled = false;
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
    if(CurrentWaveType == WaveType.Low) {
      _cannonSprite.enabled = true;

      if(HighWaveCooldown == 0)
        _cannonGlowSprite.enabled = true;

      _ringGlowSprite.enabled = false;
      CurrentWaveType = WaveType.High;
    } else {
      _cannonSprite.enabled = false;
      _cannonGlowSprite.enabled = false;
      _ringGlowSprite.enabled = true;
      CurrentWaveType = WaveType.Low;
    }
  }
}
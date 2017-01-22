﻿/*
 * Author(s): Isaiah Mann
 * Description: Loads in tuning variables
 * Usage: Singleton pattern
 */

using UnityEngine;

[System.Serializable]
public class Tuning {
  public static Tuning Get {
    get {
      if (!isInitialized) {
        _instance = init();
      }
      return _instance;
    }
  }

  static Tuning _instance;
  static bool isInitialized = false;

  static Tuning init() {
    string json = Resources.Load<TextAsset>("JSON/Tuning").text;
    Tuning tuning = JsonUtility.FromJson<Tuning>(json);
    isInitialized = true;
    return tuning;
  }

  public float GameDuration;
  public int MaxPlayerBaseHealth;
  public float StartingUnitSpawnRateMinimum;
  public float StartingUnitSpawnRateMaximum;
  public float LateGameUnitSpawnRateMinimum;
  public float LateGameUnitSpawnRateMaximum;
  public float StartingMovementRate;
  public float LateGameMovementRate;
  public float PlayerWaveAttacksPerCooldown;
  public float MaxEnergyOfHighPitchAttack;
  public float EnergyLostPerShotOfHighPitchAttack;
  public float EnergyGrowthRateOfHighPitchAttack;
  public float HighPitchAttackRange;
  public float HighPitchAttackDuration;
  public float HighPitchAttackCooldown;
  public float LowPitchAttackDuration;
  public float LowPitchAttackCooldown;

  //// These are probably going to get deleted
  //public int MaxNumberOfTimesALaneCanSpawnUnitsInSuccession;
  //public float LowPitchAttackRange;
}

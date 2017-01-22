/*
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
  public float PlayerWaveAttacksPerCooldown;
  public float StartingUnitSpawnRateMinimum;
  public float StartingUnitSpawnRateMaximum;
  public float LateGameUnitSpawnRateMinimum;
  public float LateGameUnitSpawnRateMaximum;
  public float MaxNumberOfTimesALaneCanSpawnUnitsInSuccession;
  public float StartingMovementRate;
  public float LateGameMovementRate;

  public float HighPitchAttackRange;
  public float HighPitchAttackDuration;
  public float HighPitchAttackCooldown;
  public float AngleofHighPitchAttack;
  public float LowPitchAttackDuration;
  public float LowPitchAttackCooldown;
  public float AngleofLowPitchAttack;
}

/*
 * Author(s): Isaiah Mann
 * Description: Loads in tuning variables
 * Usage: Singleton pattern
 */

using UnityEngine;

[System.Serializable]
public class Tuning 
{
	public static Tuning Get {
		get {
			if(!isInitialized)
			{
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
		
	public int MaxPlayerBaseHealth;
	public float PlayerWaveAttacksPerCooldown;
	public float PlayerAttackCooldown;
	public float UnitSpawnFrequency;
	public float EnemyMovementRate;
	public float HighPitchAttackDistance;
}

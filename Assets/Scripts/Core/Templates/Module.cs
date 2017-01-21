/*
 * Author(s): Isaiah Mann
 * Description: Meta class that all controllers inherit from (all controllers are singletons)
 */

using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public abstract class Module : MonoBehaviourExtended {
	protected const string SPRITE_DIR = "Sprites";
	protected const string JSON_DIR = "JSON";
	protected const string CSV_DIR = "CSV";
	protected const string AUDIO_DIR = "Audio";
	protected const string PREFABS_DIR = "Prefabs";
	protected const string ENEMIES_DIR = "Enemies";

	[SerializeField]
	protected string id;

	public virtual string GetId () {
		return id;
	}
		
	protected void loadGameOver()
	{
		SceneManager.LoadScene(GAME_OVER_INDEX);
	}
		
	protected override void CleanupReferences ()
	{
		// NOTHING
	}

	protected override void HandleNamedEvent (string eventName)
	{
		// NOTHING
	}

	protected string spritePath(string fileName)
	{
		return Path.Combine(SPRITE_DIR, fileName);
	}

	protected string jsonPath(string fileName)
	{
		return Path.Combine(JSON_DIR, fileName);
	}

	protected string csvPath(string fileName)
	{
		return Path.Combine(CSV_DIR, fileName);
	}

	protected string audioPath(string fileName)
	{
		return Path.Combine(AUDIO_DIR, fileName);
	}
}

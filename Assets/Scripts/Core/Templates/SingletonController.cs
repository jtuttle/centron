using UnityEngine;
using System.Collections;
using System.Reflection;

public class SingletonController<T> : MonoBehaviourExtended where T : class {
	public static T Instance;
	protected bool dontDestroyOnLoad = false;
	protected override void SetReferences () {
		tryInit(ref Instance, this as T, gameObject, dontDestroyOnLoad);
	}
		
	protected override void FetchReferences () {
		// NOTHING
	}

	protected override void CleanupReferences () {
		tryCleanupSingleton(ref Instance, this as T);
	}

	protected override void HandleNamedEvent (string eventName) {
		// NOTHING
	}

	bool tryInit (ref T singleton, T instance, GameObject gameObject, bool dontDestroyOnLoad = false) {
		if (singleton == null) {
			singleton = instance;
			if (dontDestroyOnLoad) {
				Object.DontDestroyOnLoad(gameObject);
			}
			return true;
		} else {
			Object.Destroy(gameObject);
			return false;
		}
	}

	bool tryCleanupSingleton (ref T singleton, T instance) {
		if (singleton == instance) {
			singleton = default(T);
			return true;
		} else {
			return false;
		}
	}
}

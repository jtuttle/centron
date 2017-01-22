using UnityEngine;

public class EnemySpawnAnimationEvents : MonoBehaviour {
  public void OnAnimationEnd() {
    EventModule.Event(EventType.ENEMY_SPAWN_ANIMATION_COMPLETE, gameObject);
  }
}

using UnityEngine;

public class EnemySpawnAnimationEvents : MonoBehaviour {
  private bool _triggered = false;

  public void OnAnimationEnd() {
    // Hack to prevent this from happening multiple times.
    if(_triggered) { return; }
    _triggered = true;

    EventModule.Event(EventType.ENEMY_SPAWN_ANIMATION_COMPLETE,
                      gameObject.transform.position);
    
    GameObject.Destroy(gameObject);
  }
}

using UnityEngine;

public class Wave : MonoBehaviour {
  public WaveType WaveType;
  public float ElapsedDuration = 0;

  private float _startScale;

  public void Awake() {
    _startScale = gameObject.transform.localScale.x;
  }

  public float GetWaveDuration() {
    return (WaveType == WaveType.Low) ?
      Tuning.Get.LowPitchAttackDuration : 
      Tuning.Get.HighPitchAttackDuration;
  }

  public float GetStartScale() {
    return _startScale;
  }

  public float GetEndScale() {
    return (WaveType == WaveType.Low) ? 0.8f : 2.0f;
  }

  void OnTriggerEnter2D(Collider2D collider) {
    GameObject enemy = collider.gameObject;
    if(enemy.tag.Equals(Tags.ENEMY)) {
      EventModule.Event(EventType.ENEMY_KILLED, enemy);
    }
  }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
  private Animator _gameOverAnimation;

  public void Awake() {
    EventModule.Subscribe(OnEvent);
  }

  public void OnDestroy() {
    EventModule.Unsubscribe(OnEvent);
  }

  public void OnEvent(string eventType) {
    if(eventType == EventType.GAME_OVER) {
      GameObject prototype = Resources.Load<GameObject>("Prefabs/vfx_deathA");
      _gameOverAnimation =
          GameObject.Instantiate(prototype).GetComponent<Animator>();
    }
  }
}

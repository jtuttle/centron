using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
  public void Awake() {
    EventModule.Subscribe(OnEvent);
  }

  public void OnEvent(string eventType) {
    if(eventType == EventType.GAME_OVER) {
      GameObject prototype = Resources.Load<GameObject>("Prefabs/vfx_deathA");
      GameObject explosionAnimation = GameObject.Instantiate(prototype);
    }
  }
}

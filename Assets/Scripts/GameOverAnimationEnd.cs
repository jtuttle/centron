using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverAnimationEnd : MonoBehaviour {
  public void OnAnimationEnd() {
    EventModule.Event(EventType.GAME_OVER_ANIMATION_COMPLETE);
  }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverAnimationEvents : MonoBehaviour {
  public void OnPlanetExplode() {
    GameObject planet = GameObject.Find("Planet");
    GameObject.Destroy(planet);
  }

  public void OnAnimationEnd() {
    EventModule.Event(EventType.GAME_OVER_ANIMATION_COMPLETE);
  }
}

using UnityEngine;

public class GameOverAnimationEvents : MonoBehaviour {
  public void OnPlanetExplode() {
    GameObject planet = GameObject.Find("Planet");
    GameObject.Destroy(planet);

    GameObject playerBase = GameObject.Find("PlayerBase");
    GameObject.Destroy(playerBase);
  }

  public void OnAnimationEnd() {
    EventModule.Event(EventType.GAME_OVER_ANIMATION_COMPLETE);
  }
}

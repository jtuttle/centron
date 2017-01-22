/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : UI 
{
  [SerializeField]
  GameObject pauseScreen;

  public void Pause() {
    Time.timeScale = 0;
    pauseScreen.SetActive(true);
  }

  public void ResumeGame() {
    Time.timeScale = 1;
    pauseScreen.SetActive(false);
  }

  void OnDestroy() {
    Time.timeScale = 1;
  }

}

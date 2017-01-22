/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreenUI : UI {
  [SerializeField]
  Text highScoreDisplay;
  [SerializeField]
  GameObject quitButton;

  void Start() {
    highScoreDisplay.text = getHighScoreText();
    #if UNITY_WEBGL
    quitButton.SetActive(false);
    #endif
  }

}

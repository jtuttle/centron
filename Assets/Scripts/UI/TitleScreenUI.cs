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

  void Start() {
    highScoreDisplay.text = getHighScoreText();
  }

}

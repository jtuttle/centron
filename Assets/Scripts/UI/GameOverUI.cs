/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : UI 
{
  [SerializeField] 
  Text timeSurvived;

  void Awake() {
    timeSurvived.text = PlayerPrefs.GetString(TIME_SURVIVED);
  }
 
}

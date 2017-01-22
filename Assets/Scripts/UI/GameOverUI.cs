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
  [SerializeField]
  Text enemiesKilled;

  string enemiesDestroyedFormat = "{0} Enemies";

  void Awake() {
    timeSurvived.text = PlayerPrefs.GetString(TIME_SURVIVED);
    enemiesKilled.text = string.Format(enemiesDestroyedFormat, enemiesDestroyed);
  }
 
}

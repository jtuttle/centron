/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : UI {
  public PlayerBase PlayerBase;
  [SerializeField]
  Image healthBar;
  [SerializeField]
  Image cooldownBar;
  [SerializeField]
  Text highScoreTimerText;
  [SerializeField]
  Text waveTypeText;
  Timer highScoreTimer;

  void Awake () {
    highScoreTimer = new Timer(0, -1);
    highScoreTimer.SubscribeToTimeChange(handleTimerTimeChange);
    highScoreTimer.Begin();
  }

  public void Update() {
    if(PlayerBase != null) {
      UpdateCooldown(PlayerBase.GetCooldownPercentage());
      UpdateHealth(PlayerBase.GetHealthPercentage());
      UpdateWaveType(PlayerBase.CurrentWaveType.ToString());
    }
    // DEGBUGING ONLY:
    #if UNITY_EDITOR
    if (Input.GetKeyDown(KeyCode.Q)) {
      LoadGameOver();
    }
    #endif
  }

  void OnDestroy() {
    PlayerPrefs.SetString(TIME_SURVIVED, highScoreTimer.TimeRemainingStr);
    highScoreTimer.UnsubscribeFromTimeChange(handleTimerTimeChange);
  }

  void handleTimerTimeChange(float timeRemaining) {
    highScoreTimerText.text = highScoreTimer.TimeRemainingStr;
  }

  // Amount should be between 0..1.0f
  public void UpdateHealth(float amount) {
    healthBar.fillAmount = amount;
  }

  // Amount should be between 0..1.0f
  public void UpdateCooldown(float amount) {
    cooldownBar.fillAmount = amount;
  }
  
  public float GetGameTime() {
    return highScoreTimer.TimeRemaining;
  }

  private void UpdateWaveType(string waveType) {
    waveTypeText.text = waveType;
  }
}

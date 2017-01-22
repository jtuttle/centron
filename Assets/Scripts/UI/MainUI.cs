/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour {
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
    UpdateWaveType();
    EventModule.Subscribe(OnSwitchWaveType);
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
    healthBar.fillAmount = amount;
  }
      
  public float GetGameTime() {
    return highScoreTimer.TimeRemaining;
  }
      
  public void OnSwitchWaveType(string eventType) {
    if(eventType == EventType.SWITCH_WAVE_TYPE) {
      UpdateWaveType();
    }
  }

  private void UpdateWaveType() {
    PlayerBase playerBase =
      GameObject.Find("PlayerBase").GetComponent<PlayerBase>();
    waveTypeText.text = playerBase.CurrentWaveType.ToString();
  }
}

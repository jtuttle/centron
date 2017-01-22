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
    EventModule.Subscribe(OnEvent);

    highScoreTimer = new Timer(0, -1);
    highScoreTimer.SubscribeToTimeChange(handleTimerTimeChange);
    highScoreTimer.Begin();

    UpdateWaveType();
  }

  public void Update() {
    if(PlayerBase != null) {
      UpdateCooldown(PlayerBase.GetCooldownPercentage());
    }
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

  private void OnEvent(string eventType) {
    if(eventType == EventType.SWITCH_WAVE_TYPE) {
      UpdateWaveType();
    }
  }

  private void UpdateWaveType() {
    waveTypeText.text = PlayerBase.CurrentWaveType.ToString();
  }
}

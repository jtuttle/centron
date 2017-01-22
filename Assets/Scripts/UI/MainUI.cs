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
  Text waveTypeText;

  public void Awake() {
    EventModule.Subscribe(OnEvent);
    UpdateWaveType();
  }

  public void Update() {
    if(PlayerBase != null) {
      UpdateCooldown(PlayerBase.GetCooldownPercentage());
    }
  }

  // Amount should be between 0..1.0f
  public void UpdateHealth(float amount) {
    healthBar.fillAmount = amount;
  }

  // Amount should be between 0..1.0f
  public void UpdateCooldown(float amount) {
    cooldownBar.fillAmount = amount;
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

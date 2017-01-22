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
  Image highWaveCooldownBar;
  [SerializeField]
  Image lowWaveCooldownBar;
  [SerializeField]
  Text highScoreTimerText;
  [SerializeField]
  Text enemiesDestroyedText;
  [SerializeField]
  int stringLengthEnemiesDestroyed = 5;
  Timer highScoreTimer;
  [SerializeField]
  Image highSelector;
  [SerializeField]
  Image lowSelector;

  void Awake() {
    highScoreTimer = new Timer(0, -1);
    highScoreTimer.SubscribeToTimeChange(handleTimerTimeChange);
    highScoreTimer.Begin();
    enemiesDestroyed = 0;
  }
    
  void Start() {
    EventModule.Subscribe(handleNamedEvent);
  }

  public void Update() {
    if(PlayerBase != null) {
      UpdateCooldown(PlayerBase);
      UpdateHealth(PlayerBase.GetHealthPercentage());
      UpdateWaveType(PlayerBase.CurrentWaveType);
    } else {
      UpdateHealth(0);
    }

    // DEGBUGING ONLY:
    #if UNITY_EDITOR
    if (Input.GetKeyDown(KeyCode.Q)) {
      EventModule.Event(EventType.GAME_OVER);
    }
    #endif
  }

  void OnDestroy() {
    PlayerPrefs.SetString(TIME_SURVIVED, highScoreTimer.TimeRemainingStr);
    highScoreTimer.UnsubscribeFromTimeChange(handleTimerTimeChange);
    EventModule.Unsubscribe(handleNamedEvent);
  }

  void handleNamedEvent(string eventName) {
    if(eventName == EventType.GAME_OVER_ANIMATION_COMPLETE) {
      LoadGameOver();
    } else if (eventName == EventType.ENEMY_KILLED) {
      handleEnemyKilled();
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
  public void UpdateCooldown(PlayerBase player) {
    if(player.HighWaveCooldown == 0) {
      highWaveCooldownBar.fillAmount = player.GetHighWaveEnergyPercentage();
    } else {
      highWaveCooldownBar.fillAmount = player.GetCooldownPercentage(WaveType.High);
    }

    lowWaveCooldownBar.fillAmount = player.GetCooldownPercentage(WaveType.Low);
  }

  void handleEnemyKilled() {
    enemiesDestroyed++;
    enemiesDestroyedText.text = padWithZeroes(enemiesDestroyed, stringLengthEnemiesDestroyed);
  }

  public float GetGameTime() {
    return highScoreTimer.TimeRemaining;
  }

  private void UpdateWaveType(WaveType waveType) {
    switch(waveType) {
      case WaveType.High:
        highSelector.enabled = true;
        lowSelector.enabled = false;
        break;
      case WaveType.Low:
        highSelector.enabled = false;
        lowSelector.enabled = true;
        break;
    }
  }
}

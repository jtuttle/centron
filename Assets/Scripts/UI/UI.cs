/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour 
{
  const int TITLE_SCREEN = 0;
  const int MAIN_GAME = 1;
  const int CREDITS = 2;
  const int GAME_OVER = 3;

  protected const string TIME_SURVIVED = "TimeSurvived";
  const string ENEMIES_KILLED = "EnemiesKilled";

  protected int enemiesDestroyed {
    get {
      return PlayerPrefs.GetInt(ENEMIES_KILLED, 0);
    }
    set {
      PlayerPrefs.SetInt(ENEMIES_KILLED, value);
    }
  }

  public void LoadTitleScreen() {
    SceneManager.LoadScene(TITLE_SCREEN);
  }

  public void PlayGame() {
    SceneManager.LoadScene(MAIN_GAME);
  }

  public void LoadCredits() {
    SceneManager.LoadScene(CREDITS);
  }

  public void LoadGameOver() {
    SceneManager.LoadScene(GAME_OVER); 
  }

  public void QuitGame() {
    #if UNITY_EDITOR
      UnityEditor.EditorApplication.isPlaying = false;
    #else
      Application.Quit();
    #endif
  }

  protected string padWithZeroes(int number, int desiredLength) 
  {
    string numberAsString = number.ToString();
    int numberLength = numberAsString.Length;
    if(numberLength < desiredLength) 
    {
      return numberAsString.PadLeft(desiredLength, '0');
    } 
    else 
    {
      return numberAsString;
    }
  }
}

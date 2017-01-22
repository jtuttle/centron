/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : UI {
  [SerializeField]
  TutorialStep[] tutorialSteps;
  [SerializeField]
  Text stepText;
  [SerializeField]
  Image displayIcon;
  int currentTutorialStepIndex = 0;
  TutorialStep currentStep {
    get {
      return tutorialSteps[currentTutorialStepIndex];
    }
  }

  void Awake() {
    Show();
    EventModule.Subscribe(handleNamedEvent);
  }

  public override void Show () {
    setTutorialStep(currentTutorialStepIndex);
    base.Show ();
    Time.timeScale = 0;
  }
    
  public override void Hide () {
    base.Hide();
    Time.timeScale = 1;
  }
    
  void OnDestroy() {
    EventModule.Unsubscribe(handleNamedEvent);
    Time.timeScale = 1;
  }

  void handleNamedEvent(string eventName) {
    if(checkToAdvance(eventName)) { 
      if(hasNext()) {
        Advance();
      } else {
        Hide();
      }
    }
  }

  bool checkToAdvance(string eventName) {
    return currentStep.CheckToAdvance(eventName);
  }

  void setTutorialStep(int index) {
    this.currentTutorialStepIndex = index;
    this.stepText.text = tutorialSteps[index].Text;
    this.displayIcon.sprite = tutorialSteps[index].Icon;
  }

  void Update() {
    if(Input.GetKeyDown(KeyCode.RightArrow)) {
      EventModule.Event("Foo");
    } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
      Previous();
    }
  }

  bool hasNext () {
    return currentTutorialStepIndex + 1 < tutorialSteps.Length;
  }

  public void Advance() {
    if(IntUtil.InRange(currentTutorialStepIndex+1, tutorialSteps.Length)) {
      currentTutorialStepIndex++;
      setTutorialStep(currentTutorialStepIndex);
    }
  }

  public void Previous() {
    if(IntUtil.InRange(currentTutorialStepIndex-1, tutorialSteps.Length)) {
      currentTutorialStepIndex--;
      setTutorialStep(currentTutorialStepIndex);
    }
  }

}
  
[System.Serializable]
public class TutorialStep : Data {
  public string Text {
    get {
      return stepText;
    }
  }

  public Sprite Icon {
    get {
      return displayIcon;
    }
  }

  public bool CheckToAdvance(string eventName) {
    return ArrayUtil.Contains(eventsToAdvance, eventName);
  }

  [SerializeField]
  string stepTitle;
  [SerializeField]
  string stepText;
  [SerializeField]
  Sprite displayIcon;
  [SerializeField]
  string[] eventsToAdvance;
}

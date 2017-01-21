using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {
  void Update() {
    if(Input.GetMouseButtonDown(0)) {
      ShootWave();
    }

    if(Input.GetMouseButtonDown(1)) {
      SwitchWaveType();
    }
  }

  private void ShootWave() {
    //EventModule.Event("ShootWave");
  }

  private void SwitchWaveType() {
    //EventModule.Event("SwitchWaveType");
  }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {
  void Start() {
    
  }
	
  void Update() {
    Vector3 mousePos = Input.mousePosition;
    Debug.Log(mousePos);
  }
}

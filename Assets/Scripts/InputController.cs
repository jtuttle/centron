using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {
  void Start() {
    
  }
	
  void Update() {
    Vector3 mousePos = Input.mousePosition;
//    Debug.Log(mousePos);

    Vector2 basePos = Camera.main.WorldToScreenPoint(Base.transform.position);
    Vector2 dir = Input.mousePosition - basePos;
    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    Vector3 rotation = Quaternion.AngleAxis(angle, Vector3.forward);
Debug.Log(rotation);    
    Base.transform.rotation = rotation;


  }
}

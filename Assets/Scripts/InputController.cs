using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {
  public GameObject Base;

  private Vector2 _basePos;

  void Start() {
    _basePos = Camera.main.WorldToScreenPoint(Base.transform.position);
  }
	
  void Update() {
    FollowMouse();
  }

  private void FollowMouse() {
    Vector2 dir = (Vector2)Input.mousePosition - _basePos;
    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    Base.transform.rotation = rotation;
  }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour {
	void Update() {
    Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);
    Vector2 dir = (Vector2)Input.mousePosition - pos;
    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    transform.rotation = rotation;
	}
}

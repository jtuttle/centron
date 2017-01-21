using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WaveType {
  Low, High
}

public class Base : MonoBehaviour {
  public int Health = 100;
  public float Cooldown = 100;
}

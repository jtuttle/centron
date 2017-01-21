/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuningTest : MonoBehaviour 
{
	// Use this for initialization
	void Start() 
	{
		Tuning tuning = Tuning.Get;
		Debug.Log(tuning.MaxPlayerBaseHealth);
		Debug.Log(tuning.HighPitchAttackDistance);
	}

}

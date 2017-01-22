/*
 * Author: Isaiah Mann
 * Description: Abstract class for storing data in Pickup Pup
 */

using System;
using System.Globalization;
using UnityEngine;

[Serializable]
public abstract class Data {
	public delegate void DataAction();
	public delegate void DataActionf(float value);
	
	protected string padWithZeroes(int number, int desiredLength) {
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

	protected string formatCost(int cost) 
	{
		// String formatter to concat integer with dollar sign:
		return string.Format("${0}", cost);	
	}

	protected string formatTime(float time)
	{
		int hours = (int) time / 3600;
		int minutes = ((int) time / 60) % 60;
		int seconds = (int) time % 60;
		return string.Format("{0}:{1}:{2}", 
			padWithZeroes(hours, 2), 
			padWithZeroes(minutes, 2),
			padWithZeroes(seconds, 2)
		);

	}

}

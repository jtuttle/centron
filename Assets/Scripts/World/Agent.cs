/*
 * Author(s): Isaiah Mann
 * Description: Super class for all agents operating in the game world
 */

using System;
using UnityEngine;
using System.Collections;

public abstract class Agent : MobileObjectBehaviour {
	protected const string FRONT = "Front";
	protected const string BACK = "Back";
	protected const string LEFT = "Left";
	protected const string RIGHT = "Right";
	protected const string IS_MOVING = "IsMoving";
	protected const string MAGIC = "Magic";
	protected const string MELEE = "Melee";
	protected const string ATTACK = "Attack";

	public bool HasAttackedDuringTurn{get; protected set;}

	protected bool canBeAttacked;

	Color canAttackColor = Color.Lerp(Color.red, Color.white, 0.25f);
	SpriteRenderer spriteR;

	protected int remainingAgilityForTurn;
	protected int remainingHealth;

	public abstract AgentType GetAgentType();

	public int Health () {
		return remainingHealth;
	}

	public virtual void UpdateRemainingHealth(int healthRemaing) {
		this.remainingHealth = healthRemaing;
	}

	protected override void SetReferences()
	{
		base.SetReferences();
		spriteR = GetComponent<SpriteRenderer>();
	}

	public virtual void Attack () {
		HasAttackedDuringTurn = true;
	}

	public void SetSprite(Sprite sprite) {
		this.spriteR.sprite = sprite;
	}

	protected bool isNorthKeyDown() {
		return Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
	}

	protected bool isSouthKeyDown() {
		return Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);
	}

	protected bool isWestKeyDown() {
		return Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow);
	}

	protected bool isEastKeyDown() {
		return Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow);
	}

	protected virtual void stopMoving(){
		// NOTHING
	}

	public void HighlightToAttack () {
		spriteR.color = canAttackColor;
		canBeAttacked = true;
	}

	public void Unhighlight () {
		spriteR.color = Color.white;
		canBeAttacked = false;
	}
		
	protected virtual bool trySpendAgility (int agilityPointsReq) {
		if (remainingAgilityForTurn >= agilityPointsReq) {
			remainingAgilityForTurn -= agilityPointsReq;
			return true;
		} else {
			return false;
		}
	}

	public static int AgentTypeCount () {
		return Enum.GetNames(typeof(AgentType)).Length;
	}
	
}

public enum AgentType {
	Player,
	Enemy,
}
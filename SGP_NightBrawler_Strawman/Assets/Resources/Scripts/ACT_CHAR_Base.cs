using UnityEngine;
using System.Collections;

public class ACT_CHAR_Base : ACT_Base 
{
	// Remove later
	public Sprite[] sprites;
	// 

	public enum STATES { IDLE = 0, WALKING, DASHING, 
		ATTACK_1, ATTACK_2, ATTACK_3, SPECIAL, HURT, DYING, USE };
	public STATES state;

	public float cooldownTmrBase;
	public float cooldownTmr;
	public int characterIndex;

	// Use this for initialization
	public void Start () 
	{
		cooldownTmrBase = 3;
		cooldownTmr = 0;
	}
	
	// Update is called once per frame
	public void Update () 
	{
		cooldownTmr -= Time.deltaTime;
		if (cooldownTmr < 0)
		{
			cooldownTmr = 0;
		}
	}

	void Render()
	{
		switch (state)
		{
		case STATES.IDLE:
				break;
		case STATES.WALKING:
				break;
		case STATES.DASHING:
				break;
		case STATES.ATTACK_1:
				break;
		case STATES.ATTACK_2:
				break;
		case STATES.ATTACK_3:
				break;
		case STATES.SPECIAL:
				break;
		case STATES.HURT:
				break;
		case STATES.DYING:
				break;
		case STATES.USE:
				break;
		}
	}

	public virtual void ActivateSpecial()
	{

	}
}

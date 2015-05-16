using UnityEngine;
using System.Collections;

public class ACT_Enemy : MonoBehaviour
{
	public enum STATES
	{
		IDLE, WALKING, RUNNING,
		ATTACKING, HURT, DEAD
	}

	public STATES state;

	public bool nightThresh;
	public int hpThresh;
	public float speedThresh;
	public float distThresh;
	public float coolThresh;

	BHR_Base[] behaviors;
	BHR_Base currBehavior;

	GameObject target;

	PROJ_Base projectile;

	public int Act_baseHP;          //The base HP of the current Actor
	public int Act_basePower;       //The base Power of the current Actor
	public int Act_baseSpeed;       //The base Speed of the current Actor

	public int Act_currHP;          //The current HP of the Actor, can be modifie and change in play
	public int Act_currPower;       //The current Power of the Actor, can be modifie and change in play
	public int Act_currSpeed;       //The current Speed of the Actor, can be modifie and change in play

	public bool Act_facingRight;    //The direction the Actor is facing, use fro back attacks and shilds
	public bool Act_HasMod;         //Does the Actor has a Modification acting on it

	//Mutators
	public void SetCurrHP(int n_hp)
	{
		Act_currHP = n_hp;
	}
	public void SetCurrPower(int n_pwr)
	{
		Act_currPower = n_pwr;
	}
	public void SetCurrSpeed(int n_spd)
	{
		Act_currSpeed = n_spd;
	}

	public void SetBaseHP(int n_hp)
	{
		Act_baseHP = n_hp;
	}
	public void SetBasePower(int n_pwr)
	{
		Act_basePower = n_pwr;
	}
	public void SetBaseSpeed(int n_spd)
	{
		Act_baseSpeed = n_spd;
	}

	//Interface
	public void ChangeHP(int Dmg)       //Applies current HP by set amount can be use to Heal as well
	{                                   //Damage needs to be negative.
		Act_currHP += Dmg;

		if (Act_currHP > Act_baseHP)
			Act_currHP = Act_baseHP;
		if (Act_currHP < 0)
			Act_currHP = 0;
	}
	public void RestoreToBaseHP()       //Restores current HP to its base value
	{
		Act_currHP = Act_baseHP;
	}
	public void RestoreToBasePower()    //Restores current Power to its base value
	{
		Act_currPower = Act_basePower;
	}
	public void RestoreToBaseSpeed()    //Restores current Speed to its base value
	{
		Act_currSpeed = Act_baseSpeed;
	}

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Act_currHP <= 0)
			Destroy(gameObject);
	}

	void CheckThresholds()
	{

	}
}

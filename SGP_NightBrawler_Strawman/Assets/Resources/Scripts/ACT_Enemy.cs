using System.Collections;
using UnityEngine;

public class ACT_Enemy : MonoBehaviour
{
	

	public int Act_baseHP;          //The base HP of the current Actor
	public int Act_basePower;       //The base Power of the current Actor
	public int Act_baseSpeed;       //The base Speed of the current Actor

	public int Act_currHP;          //The current HP of the Actor, can be modifie and change in play
	public int Act_currPower;       //The current Power of the Actor, can be modifie and change in play
	public int Act_currSpeed;       //The current Speed of the Actor, can be modifie and change in play

	public bool Act_facingRight;    //The direction the Actor is facing, use fro back attacks and shilds
	public bool Act_HasMod;         //Does the Actor has a Modification acting on it

	public enum STATES
	{
		IDLE, WALKING, RUNNING,
		ATTACKING, SPECIAL, HURT, DEAD
	}

	public STATES state;
	public int randomState;
	public float curTime;
	public float[] stateTime;


	public bool nightThresh;
	public int hpThresh;
	public float speedThresh;
	public float distThresh;
	public float coolThresh;

	public BHR_Base[] behaviors;
	public BHR_Base currBehavior;

	public GameObject target;

	public PROJ_Base projectile;

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
							// IDLE, WALK, DODGE, ATT1, ATT2, ATT3, SPEC, HURT, DED,  USE
		stateTime = new float[] { 2.0f, 0.75f, 0.5f, 0.3f, 0.2f, 0.5f, 1.0f, 0.1f, 1.0f, 1.0f };

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Act_currHP <= 0)
			Destroy(gameObject);

		curTime -= Time.deltaTime;

		if (curTime <= 0.0f)
			NewState();

		switch (state)
		{
			case STATES.IDLE:
				{
					GetComponent<Rigidbody2D>().velocity = Vector3.zero;
					break;
				}
			case STATES.WALKING:
				{
					Act_currSpeed = 1;
					if (target.transform.position.x > transform.position.x)
					{
						Vector2 vel = GetComponent<Rigidbody2D>().velocity;
						vel = new Vector2(Act_currSpeed, vel.y);
						GetComponent<Rigidbody2D>().velocity = vel;
					}
					else
					{
						Vector2 vel = GetComponent<Rigidbody2D>().velocity;
						vel = new Vector2(-Act_currSpeed, vel.y);
						GetComponent<Rigidbody2D>().velocity = vel;
					}

					if (target.transform.position.y > transform.position.y)
					{
						Vector2 vel = GetComponent<Rigidbody2D>().velocity;
						vel = new Vector2(vel.x, Act_currSpeed);
						GetComponent<Rigidbody2D>().velocity = vel;
					}
					else
					{
						Vector2 vel = GetComponent<Rigidbody2D>().velocity;
						vel = new Vector2(vel.x, -Act_currSpeed);
						GetComponent<Rigidbody2D>().velocity = vel;
					}
					break;
				}
			case STATES.RUNNING:
				{
					Act_currSpeed = 2;
					if (target.transform.position.x > transform.position.x)
					{
						Vector2 vel = GetComponent<Rigidbody2D>().velocity;
						vel = new Vector2(Act_currSpeed, vel.y);
						GetComponent<Rigidbody2D>().velocity = vel;
					}
					else
					{
						Vector2 vel = GetComponent<Rigidbody2D>().velocity;
						vel = new Vector2(-Act_currSpeed, vel.y);
						GetComponent<Rigidbody2D>().velocity = vel;
					}
					if (target.transform.position.y > transform.position.y)
					{
						Vector2 vel = GetComponent<Rigidbody2D>().velocity;
						vel = new Vector2(vel.x, Act_currSpeed);
						GetComponent<Rigidbody2D>().velocity = vel;
					}
					else
					{
						Vector2 vel = GetComponent<Rigidbody2D>().velocity;
						vel = new Vector2(vel.x, -Act_currSpeed);
						GetComponent<Rigidbody2D>().velocity = vel;
					}
					break;
				}
			case STATES.ATTACKING:
				{

					break;
				}
			case STATES.SPECIAL:
				{

					break;
				}
			case STATES.HURT:
				{

					break;
				}
			case STATES.DEAD:
				{

					break;
				}
		}
	}

	void CheckThresholds()
	{

	}

	void NewState()
	{
		randomState = (int)Random.Range(0.0f, 3.999f);

		state = (STATES)randomState;
		curTime = stateTime[(int)state];
	}
}

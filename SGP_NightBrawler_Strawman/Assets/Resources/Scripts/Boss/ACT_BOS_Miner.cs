using UnityEngine;
using System.Collections;

public class ACT_BOS_Miner : ACT_Enemy 
{
	public enum ATTACKS { BEER = 0, PICKAXE, DIRT, DIG, DYNAMITE };

	public ATTACKS currAttack;
	public bool hasAttack;
	public bool performingAttack;
	public float attackTimer;
	public float[] attackTimerBases;

	public PROJ_Base[] projectiles;
	public GameObject[] waypoints;

	// Use this for initialization
	void Start()
	{
								// IDLE, WALK, RUN, ATTK, SPEC, HURT, DED,  USE
		stateTime = new float[] { 2.0f, 0.75f, 0.5f, 0.5f, 0.6f, 0.3f, 1.0f, 1.0f };


									// BEER, PICKAXE, DIRT, DIG, DYNAMITE
		attackTimerBases = new float[] { 2.0f, 0.75f, 0.5f, 0.5f, 0.6f };

		projectiles = new PROJ_Base[3];

		performingAttack = false;
		hasAttack = false;
		Act_facingRight = false;
		state = STATES.IDLE;
		currTime = 5.0f;
	}
	
	// Update is called once per frame
	void Update()
	{
		if (hasAttack)
		{
			attackTimer -= Time.deltaTime;

			if (attackTimer <= 0.0f)
			{
				hasAttack = false;
				attackTimer = attackTimerBases[(int)currAttack];
			}
		}

		if (!performingAttack)
		{
			currTime -= Time.deltaTime;

			if (currTime <= 0.0f)
			{
				NewState();
			}
		}

		CheckThresholds();

		if (Act_currHP <= 0.0f)
		{
			state = STATES.DEAD;
			currTime = stateTime[(int)state];
		}

		switch (state)
		{
			case STATES.IDLE:
				break;
			case STATES.ATTACKING:
				if (!hasAttack)
					NewAttack();
				else
					StartCoroutine("PerformAttack", currAttack);
				break;
			case STATES.SPECIAL:
				break;
			case STATES.HURT:
				break;
			case STATES.DEAD:
				Destroy(gameObject);
				break;
		}

		if (Act_currHP != Act_baseHP && state != STATES.ATTACKING)
		{
			state = STATES.ATTACKING;
			currTime = stateTime[(int)state];
			target = GameObject.FindGameObjectWithTag("Player");
		}
	}

	public override void CheckThresholds()
	{
		//if (target != null)
		//{
		//	if (distThresh >= distanceToTarget)
		//	{
				
		//	}
		//	else if (distThresh < distanceToTarget)
		//	{
				
		//	}
		//}

		//if (hpThresh >= Act_currHP)
		//{
			
		//}
	}

	public override void NewState()
	{
		RestoreToBaseSpeed();
		if ((state != STATES.HURT || state != STATES.DEAD) && !(Act_currHP == Act_baseHP))
		{
			randomState = Random.Range(0, 5);

			if (randomState != 3) // If we dont get an attack state, reroll once (this increases the enemy attack frequency)
				randomState = Random.Range(0, 5);

			state = (STATES)randomState;
			currTime = stateTime[(int)state];
		}
	}

	public void NewAttack()
	{
		if (hasAttack)
			return;

		hasAttack = true;
		currAttack = (ATTACKS)Random.Range(0, 3);
		attackTimer = attackTimerBases[(int)currAttack];
	}

	IEnumerable PerformAttack(ATTACKS _attack)
	{
		switch (_attack)
		{
			case ATTACKS.BEER:
				{
					projectile = projectiles[(int)ATTACKS.BEER];

					PROJ_Base clone = (PROJ_Base)Instantiate(projectile, transform.position, new Quaternion(0, 0, 0, 0));
					clone.owner = gameObject;
					clone.Initialize();
					Act_currAttackSpeed = Act_baseAttackSpeed;
					GetComponent<Rigidbody2D>().velocity = Vector2.zero;
					break;
				}
			case ATTACKS.PICKAXE:
				{
					projectile = projectiles[(int)ATTACKS.PICKAXE];

					PROJ_Base clone = (PROJ_Base)Instantiate(projectile, transform.position, new Quaternion(0, 0, 0, 0));
					clone.owner = gameObject;
					clone.Initialize();
					Act_currAttackSpeed = Act_baseAttackSpeed;
					GetComponent<Rigidbody2D>().velocity = Vector2.zero;
					break;
				}
			case ATTACKS.DIRT:
				{
					projectile = projectiles[(int)ATTACKS.DIRT];

					PROJ_Base clone = (PROJ_Base)Instantiate(projectile, transform.position, new Quaternion(0, 0, 0, 0));
					clone.owner = gameObject;
					clone.Initialize();
					Act_currAttackSpeed = Act_baseAttackSpeed;
					GetComponent<Rigidbody2D>().velocity = Vector2.zero;
					break;
				}
			case ATTACKS.DIG:
				{

					break;
				}
			case ATTACKS.DYNAMITE:
				{

					break;
				}
		}
		yield return null;
	}
}

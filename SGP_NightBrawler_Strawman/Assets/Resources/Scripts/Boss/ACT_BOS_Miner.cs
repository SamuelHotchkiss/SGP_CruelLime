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
	public PROJ_Pickaxe pickaxe;
	public GameObject[] waypoints;

	// Use this for initialization
	void Start()
	{
								// IDLE, WALK, RUN, ATTK, HURT, DED,  USE
		stateTime = new float[] { 2.0f, 0.75f, 0.5f, 0.5f, 0.3f, 1.0f, 1.0f };


									// BEER, PICKAXE, DIRT, DIG, DYNAMITE
		attackTimerBases = new float[] { 2.0f, 0.75f, 0.5f, 0.5f, 0.6f };

		performingAttack = false;
		hasAttack = false;
		Act_facingRight = false;
		state = STATES.IDLE;
		currTime = 5.0f;
	}
	
	// Update is called once per frame
	public override void Update()
	{

        Vector2 slowVel = GetComponent<Rigidbody2D>().velocity;
        slowVel *= 0.5f;
        GetComponent<Rigidbody2D>().velocity = slowVel;


		if (hasAttack)
		{
			attackTimer -= Time.deltaTime;

			if (attackTimer <= 0.0f)
			{
				hasAttack = false;
				attackTimer = attackTimerBases[(int)currAttack];
			}
		}

		//if (!performingAttack)
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
			kamikazeActivated = true;
			currAttack = ATTACKS.DYNAMITE;
			hasAttack = true;
			state = STATES.ATTACKING;
			currTime = stateTime[(int)state];
		}

		switch (state)
		{
			case STATES.IDLE:
				break;
			case STATES.WALKING:
				if (target.transform.position.x > transform.position.x)
                {
                    Vector2 vel = GetComponent<Rigidbody2D>().velocity;
                    vel = new Vector2(Act_currSpeed, vel.y);
                    GetComponent<Rigidbody2D>().velocity = vel;
                    Act_facingRight = true;
                }
                else
                {
                    Vector2 vel = GetComponent<Rigidbody2D>().velocity;
                    vel = new Vector2(-Act_currSpeed, vel.y);
                    GetComponent<Rigidbody2D>().velocity = vel;
                    Act_facingRight = false;
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
			case STATES.RUNNING:
				Act_currSpeed += 1;
				if (target.transform.position.x > transform.position.x)
                {
                    Vector2 vel = GetComponent<Rigidbody2D>().velocity;
                    vel = new Vector2(Act_currSpeed, vel.y);
                    GetComponent<Rigidbody2D>().velocity = vel;
                    Act_facingRight = true;
                }
                else
                {
                    Vector2 vel = GetComponent<Rigidbody2D>().velocity;
                    vel = new Vector2(-Act_currSpeed, vel.y);
                    GetComponent<Rigidbody2D>().velocity = vel;
                    Act_facingRight = false;
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
			case STATES.ATTACKING:
				if (!hasAttack)
					NewAttack();
				else if (!performingAttack)
				{
					performingAttack = true;
					StartCoroutine("PerformAttack", currAttack);
				}
				break;
			case STATES.HURT:
				break;
			case STATES.DEAD:
				//Destroy(gameObject);
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
			randomState = Random.Range(0, 4);

			//if (randomState != 3) // If we dont get an attack state, reroll once (this increases the enemy attack frequency)
			//	randomState = Random.Range(0, 5);

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
		if (kamikazeActivated)
			currAttack = ATTACKS.DYNAMITE;

		attackTimer = attackTimerBases[(int)currAttack];
	}

	IEnumerator PerformAttack(ATTACKS _attack)
	{
		switch (_attack)
		{
			case ATTACKS.BEER:
				{
					projectile = projectiles[0];

					PROJ_Base clone = (PROJ_Base)Instantiate(projectile, transform.position, new Quaternion(0, 0, 0, 0));
					clone.owner = gameObject;
					clone.Initialize();
					Act_currAttackSpeed = Act_baseAttackSpeed;
					GetComponent<Rigidbody2D>().velocity = Vector2.zero;

					yield return new WaitForSeconds(0.3f);

					GetComponent<Rigidbody2D>().velocity = Vector2.zero;
					performingAttack = false;
					hasAttack = false;
					break;
				}
			case ATTACKS.PICKAXE:
				{

					PROJ_Pickaxe clone = (PROJ_Pickaxe)Instantiate(pickaxe, transform.position, new Quaternion(0, 0, 0, 0));
					clone.owner = gameObject;
					Act_currAttackSpeed = Act_baseAttackSpeed;
					GetComponent<Rigidbody2D>().velocity = Vector2.zero;
					yield return new WaitForSeconds(2.5f);

					GetComponent<Rigidbody2D>().velocity = Vector2.zero;
					performingAttack = false;
					hasAttack = false;
					break;
				}
			case ATTACKS.DIRT:
				{
					projectile = projectiles[1];

					for (int i = 0; i < 3; i++)
					{
						PROJ_Base clone = (PROJ_Base)Instantiate(projectile, transform.position, new Quaternion(0, 0, 0, 0));
						clone.owner = gameObject;
						clone.Initialize();
						Act_currAttackSpeed = Act_baseAttackSpeed;
						GetComponent<Rigidbody2D>().velocity = Vector2.zero;
					}

					GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, Random.Range(-300, 301)));

					yield return new WaitForSeconds(0.5f);

					for (int i = 0; i < 3; i++)
					{
						PROJ_Base clone = (PROJ_Base)Instantiate(projectile, transform.position, new Quaternion(0, 0, 0, 0));
						clone.owner = gameObject;
						clone.Initialize();
						Act_currAttackSpeed = Act_baseAttackSpeed;
						GetComponent<Rigidbody2D>().velocity = Vector2.zero;
					}

					GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, Random.Range(-300, 301)));

					yield return new WaitForSeconds(0.5f);

					for (int i = 0; i < 3; i++)
					{
						PROJ_Base clone = (PROJ_Base)Instantiate(projectile, transform.position, new Quaternion(0, 0, 0, 0));
						clone.owner = gameObject;
						clone.Initialize();
						Act_currAttackSpeed = Act_baseAttackSpeed;
						GetComponent<Rigidbody2D>().velocity = Vector2.zero;
					}

					yield return new WaitForSeconds(0.7f);

					GetComponent<Rigidbody2D>().velocity = Vector2.zero;
					performingAttack = false;
					hasAttack = false;
					break;
				}
			case ATTACKS.DIG:
				{

					break;
				}
			case ATTACKS.DYNAMITE:
				{
					GetComponent<SpriteRenderer>().color = Color.red;
					Act_baseSpeed *= 5;
					Act_currSpeed = Act_baseSpeed;

					float timer = 0.0f;

					while (timer < 3.0f)
					{
						if (target.transform.position.x > transform.position.x)
						{
							Vector2 vel = GetComponent<Rigidbody2D>().velocity;
							vel = new Vector2(Act_currSpeed, vel.y);
							GetComponent<Rigidbody2D>().velocity = vel;
							Act_facingRight = true;
						}
						else
						{
							Vector2 vel = GetComponent<Rigidbody2D>().velocity;
							vel = new Vector2(-Act_currSpeed, vel.y);
							GetComponent<Rigidbody2D>().velocity = vel;
							Act_facingRight = false;
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
						timer += Time.deltaTime;
						yield return null;
					}

					yield return new WaitForSeconds(0.3f);

					explosion.GetComponent<PROJ_Explosion>().power = 200;

					PROJ_Explosion clone = Instantiate(explosion, gameObject.transform.position, Quaternion.identity) as PROJ_Explosion;
					if (GetComponent<ITM_DropLoot>())
					{
						GetComponent<ITM_DropLoot>().DropCoin(transform.position);
					}
					clone.Initialize();
					Destroy(gameObject);

					break;
				}
		}
		yield return null;
	}
}

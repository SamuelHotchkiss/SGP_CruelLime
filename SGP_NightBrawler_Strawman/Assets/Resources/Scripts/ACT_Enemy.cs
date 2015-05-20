using System.Collections;
using UnityEngine;

public class ACT_Enemy : MonoBehaviour
{

    // 0 = GloblinFighter, 1 = GloblinArcher, 2 = GloblinWarchief, 3 = Maneater,
    // 4 = Ent, 5 = GloblinShaman, 6 = Trollgre, 7...
    public int Act_ID;
	public int Act_baseHP;          //The base HP of the current Actor
	public int Act_basePower;       //The base Power of the current Actor
	public int Act_baseSpeed;       //The base Speed of the current Actor

	public int Act_currHP;          //The current HP of the Actor, can be modifie and change in play
	public int Act_currPower;       //The current Power of the Actor, can be modifie and change in play
	public int Act_currSpeed;       //The current Speed of the Actor, can be modifie and change in play

	public bool Act_facingRight;    //The direction the Actor is facing, use fro back attacks and shilds
	public bool Act_HasMod;         //Does the Actor has a Modification acting on it

    public float Act_baseAttackSpeed;   //How fast the enemy can shoot a projectile, For Enemies ONLY
    public float Act_currAttackSpeed;   //Checks to see if I can actually shoot a projectile, For Enemies ONLY

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


	public int behaviorSize;
	public int[] behaviorID = new int[10];
	public BHR_Base[] behaviors;
	public BHR_Base currBehavior;

    public GameObject Spw_Critter;  //If it can divide or Spawn more enemies it will spawn this enemie

	public GameObject target;

	public PROJ_Base projectile;

	public bool isMelee;

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
        if (Dmg < 0)
        {
            state = STATES.HURT;
            curTime = stateTime[(int)state];
        }
		if (Act_currHP > Act_baseHP)
			Act_currHP = Act_baseHP;
        if (Act_currHP < 0)
        {
            Act_currHP = 0;
            state = STATES.DEAD;
            curTime = stateTime[(int)state];
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
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
								// IDLE, WALK, RUN, ATTK, SPEC, HURT, DED,  USE
		stateTime = new float[] { 2.0f, 0.75f, 0.5f, 0.5f, 0.6f, 0.3f, 1.0f, 1.0f };
		
		behaviors = new BHR_Base[behaviorSize];
        Act_facingRight = false;

		for (int i = 0; i < behaviorSize; i++)
		{
			behaviors[i] = Instantiate(GameObject.FindGameObjectWithTag("_Overlord").GetComponent<BHR_Overlord>().behaviors[behaviorID[i]]);
			behaviors[i].owner = GetComponent<ACT_Enemy>();
		}

		target = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () 
	{

        if (!MNGR_Game.isNight && Act_currHP == Act_baseHP)
            state = STATES.IDLE;


		curTime -= Time.deltaTime;
        Act_currAttackSpeed -= Time.deltaTime;

		if (state == STATES.DEAD && curTime <= 0)
			Destroy(gameObject);

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
					if (isMelee)
					{
						Act_currSpeed = 1;
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
					}
					else
					{
						Act_currSpeed = 2;
						if (target.transform.position.x > transform.position.x)
						{
							Vector2 vel = GetComponent<Rigidbody2D>().velocity;
							vel = new Vector2(-Act_currSpeed, vel.y);
							GetComponent<Rigidbody2D>().velocity = vel;
							Act_facingRight = true;
						}
						else
						{
							Vector2 vel = GetComponent<Rigidbody2D>().velocity;
							vel = new Vector2(Act_currSpeed, vel.y);
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
					}
				}
			case STATES.RUNNING:
				{
					if (isMelee)
					{
						Act_currSpeed = 2;
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
					}
					else
					{
						Act_currSpeed = 2;
						if (target.transform.position.x > transform.position.x)
						{
							Vector2 vel = GetComponent<Rigidbody2D>().velocity;
							vel = new Vector2(-Act_currSpeed, vel.y);
							GetComponent<Rigidbody2D>().velocity = vel;
							Act_facingRight = true;
						}
						else
						{
							Vector2 vel = GetComponent<Rigidbody2D>().velocity;
							vel = new Vector2(Act_currSpeed, vel.y);
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
					}
				}
			case STATES.ATTACKING:
				{
                    //if (isMelee)
                    //{
                    //    break;
                    //}
                    //else
                    //{
                    if (Act_currAttackSpeed <= 0.0f)
                    {
                        PROJ_Base clone = (PROJ_Base)Instantiate(projectile, transform.position, new Quaternion(0, 0, 0, 0));
                        clone.owner = gameObject;
                        clone.Initialize();
                        Act_currAttackSpeed = Act_baseAttackSpeed;
                        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    }
                    break;
				}
			case STATES.SPECIAL:
				{
					CheckThresholds();
					currBehavior.PerformBehavior();
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
		currBehavior = behaviors[0];
	}

	void NewState()
	{
        if (state != STATES.HURT || state != STATES.DEAD)
        {
            randomState = (int)Random.Range(2.0f, 3.999f);

            state = (STATES)randomState;
            curTime = stateTime[(int)state];
        }
	}
}

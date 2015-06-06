using System.Collections;
using UnityEngine;
using System.Collections.Generic;
public class ACT_Enemy : MonoBehaviour
{
	public GUIStyle BlackBar;
	public GUIStyle HealthBar;
	public Camera cam;

    // S: for use with buffs and debuffs ////////////////////////////////
    public MNGR_Item.BuffStates buffState = MNGR_Item.BuffStates.NEUTRAL;

    public List<MOD_Base> myBuffs = new List<MOD_Base>();
    public void KillBuffs()
    {
        for (int i = 0; i < myBuffs.Count; i++)
        {
            myBuffs[i].EndModifyEnemy();

        }
        myBuffs.Clear();
    }
    /////////////////////////////////////////////////////////////////////

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
	public bool Act_ModIsBuff;
    public bool Act_IsIntelligent;  //Is this Enemy inanimate.
    public bool Act_SpawnProjOnDed;

    public float Act_baseAttackSpeed;   //How fast the enemy can shoot a projectile, For Enemies ONLY
    public float Act_currAttackSpeed;   //Checks to see if I can actually shoot a projectile, For Enemies ONLY

    public float damageMod;

	public enum STATES
	{
		IDLE, WALKING, RUNNING,
		ATTACKING, SPECIAL, HURT, DEAD
	}

	public STATES state;
	public int randomState;
	public float curTime;
	public float[] stateTime;

    public int hpThresh;
	public bool nightThresh;
    public float TimeThresh;
	public float speedThresh;
	public float distThresh;
	public float coolThresh;

	public int behaviorSize;
	public int[] behaviorID = new int[10];
	public BHR_Base[] behaviors;
	public BHR_Base currBehavior;
	public BHR_Base basicBehavior;

/// <Behavior Variables>
	public List<GameObject> squad = new List<GameObject>();
	public float maxBuffRange;

	//public MOD_Base buff;         // S: shouldn't be needed anymore
	public int buffIndex;

    //Spawner
    public GameObject Spw_Critter;          //The Critter to spawn.
    public int Spw_CritterThreshold;        //The point to stop creating creatures.
    public int Spw_CrittersCreated;         //How many critters have been created.
    public float Spw_SpawnCoolDown;         //How often to spawn waves of enemies.
    public float Spw_BaseSpawnCoolDown;     //Keeps tracks of the original Spawn CoolDown.
    public float Spw_SpawnPerSec;           //How often to spawn a single enemy.
    public float Spw_baseSpawnPerSec;       //Keeps track of Spawn PerSec.
    public Vector3 Spw_SpawnPositionOffset; //Where to spawn the critters in comparation with the host Position.
    public Vector2 Spw_Force;               //Add a force to the critters.

    //KnockBack
    public float Knck_Cooldown;         //How long it takes to reuse the knockback.
    public float Knck_baseCooldown;     //Keeps track of the initial cooldown. 

	// Kamikaze
	public bool kamikazeActivated = false;
	public float kamikazeTimer;
	public GameObject explosion;

	// Divider
	public bool dividerActivated;
	public int numQuotient;
	public int numGeneration;

/// <Behavior Variables>

	public GameObject target;
	public float distanceToTarget;
	public float maxDistance;

	public PROJ_Base projectile;

	public bool isMelee;
	public bool paused = false;

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
		Act_currHP += (int)(Dmg * damageMod);
        if (Dmg < 0)
        {
            state = STATES.HURT;
            curTime = stateTime[(int)state];
        }
		if (Act_currHP > Act_baseHP)
			Act_currHP = Act_baseHP;
        if (Act_currHP <= 0)
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
        if (GetComponent<SpriteRenderer>() != null && GameObject.Find("Reference_Point") != null)
		    GetComponent<SpriteRenderer>().sortingOrder = (int)(GameObject.Find("Reference_Point").transform.position.y - transform.position.y);

		if (HealthBar.name == "Health")
			cam = GameObject.Find("Main Camera").GetComponent<Camera>();

        damageMod = 1.0f;

        Act_currAttackSpeed = Act_baseAttackSpeed;
        Act_currHP = Act_baseHP;
        Act_currPower = Act_basePower;
        Act_currSpeed = Act_baseSpeed;

        if (stateTime.Length != 8)
        {
                                    // IDLE, WALK, RUN, ATTK, SPEC, HURT, DED,  USE
            stateTime = new float[] { 2.0f, 0.75f, 0.5f, 0.5f, 1.2f, 0.3f, 1.0f, 1.0f };
        }
		
		behaviors = new BHR_Base[behaviorSize];
        Act_facingRight = false;

		for (int i = 0; i < behaviorSize; i++)
		{
			behaviors[i] = Instantiate(GameObject.FindGameObjectWithTag("_Overlord").GetComponent<BHR_Overlord>().behaviors[behaviorID[i]]);
            if (behaviors[i].owner == null)
                behaviors[i].owner = GetComponent<ACT_Enemy>();
		}

		if (basicBehavior)
		{
			basicBehavior = behaviors[0];
		}
        //target = null;
		if (Act_IsIntelligent)
		{
			target = GameObject.FindGameObjectWithTag("Player");
		}

		//squad = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () 
	{
        if (myBuffs.Count == 0)
            buffState = MNGR_Item.BuffStates.NEUTRAL;

		if (MNGR_Game.paused)
		{
			GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			return;
		}

		if (!MNGR_Game.isNight && Act_currHP == Act_baseHP)
			state = STATES.IDLE;
        
        if ((MNGR_Game.isNight || Act_currHP != Act_baseHP) &&
            target == null && Act_IsIntelligent)
            target = GameObject.FindGameObjectWithTag("Player");

		curTime -= Time.deltaTime;
        Act_currAttackSpeed -= Time.deltaTime;

		if (state == STATES.DEAD && curTime <= 0)
		{
			if (dividerActivated)
			{
				if (basicBehavior)
				{
					basicBehavior.PerformBehavior();
				}
				if (numGeneration == 0)
				{
					if (GetComponent<ITM_DropLoot>())
					{
						GetComponent<ITM_DropLoot>().DropCoin(transform.position);
					}
				}
				Destroy(transform.gameObject);
			}
			else
			{
				if (GetComponent<ITM_DropLoot>())
				{
					GetComponent<ITM_DropLoot>().DropCoin(transform.position);
				}

				Destroy(transform.gameObject);

				if (Act_SpawnProjOnDed)
				{
					PROJ_Base clone = (PROJ_Base)Instantiate(projectile, transform.position, new Quaternion(0, 0, 0, 0));
					clone.owner = gameObject;
					clone.Initialize();
				}
			}
		}

        if (curTime <= 0.0f)
            NewState();

        if (TimeThresh > 0.0f)
        {
            TimeThresh -= Time.deltaTime;

            if (TimeThresh < 0.0f)
                TimeThresh = 0.0f;

            if (TimeThresh == 0.0f)
                Destroy(gameObject);
        }

		if (kamikazeActivated)
		{
			if (kamikazeTimer > 0.0f)
			{
				kamikazeTimer -= Time.deltaTime;
			}

			if (kamikazeTimer <= 0.0f)
			{
				Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
				Destroy(gameObject);
			}
		}

        if (target != null)
            distanceToTarget = Mathf.Abs(target.transform.position.x - transform.position.x);

		switch (state)
		{
			case STATES.IDLE:
				{
                    if (Act_IsIntelligent)
                        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                    break; 
				}
			case STATES.WALKING:
				{
                    if (target != null)
                    {
                        if (isMelee)
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
                            break;
                        }
                        else
                        {
                            if (target.transform.position.x > transform.position.x)
                            {
                                if (distanceToTarget < maxDistance)
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
                                    Act_facingRight = true;
                                }
                            }
                            else if (target.transform.position.x < transform.position.x)
                            {
                                if (distanceToTarget < maxDistance)
                                {
                                    Vector2 vel = GetComponent<Rigidbody2D>().velocity;
                                    vel = new Vector2(Act_currSpeed, vel.y);
                                    GetComponent<Rigidbody2D>().velocity = vel;
                                    Act_facingRight = false;
                                }
                                else
                                {
                                    Vector2 vel = GetComponent<Rigidbody2D>().velocity;
                                    vel = new Vector2(-Act_currSpeed, vel.y);
                                    GetComponent<Rigidbody2D>().velocity = vel;
                                    Act_facingRight = false;
                                }
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
                    break;
				}
			case STATES.RUNNING:
				{
                    if (target != null)
                    {
                        if (isMelee)
                        {
                            SetCurrSpeed(Act_baseSpeed + 5);
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
                            SetCurrSpeed(Act_baseSpeed + 5);
                            if (target.transform.position.x > transform.position.x)
                            {
                                if (distanceToTarget < maxDistance)
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
                                    Act_facingRight = true;
                                }
                            }
                            else if (target.transform.position.x < transform.position.x)
                            {
                                if (distanceToTarget < maxDistance)
                                {
                                    Vector2 vel = GetComponent<Rigidbody2D>().velocity;
                                    vel = new Vector2(Act_currSpeed, vel.y);
                                    GetComponent<Rigidbody2D>().velocity = vel;
                                    Act_facingRight = false;
                                }
                                else
                                {
                                    Vector2 vel = GetComponent<Rigidbody2D>().velocity;
                                    vel = new Vector2(-Act_currSpeed, vel.y);
                                    GetComponent<Rigidbody2D>().velocity = vel;
                                    Act_facingRight = false;
                                }
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
                    break;
				}
			case STATES.ATTACKING:
				{
                    if (Act_currAttackSpeed <= 0.0f && projectile != null)
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
					if (dividerActivated)
					{
						break;
					}
					if (currBehavior)
					{
						currBehavior.PerformBehavior();
						if (!kamikazeActivated)
						{
							for (int i = 0; i < behaviorID.Length; i++)
							{
								if (behaviorID[i] == 5)
								{
									kamikazeActivated = true;
									kamikazeTimer = 3.0f;
									explosion.GetComponent<PROJ_Explosion>().power = 10;
								}
							}
						}
					}
					break;
				}
			case STATES.HURT:
				{
                    if (Act_IsIntelligent)
                    {
                        Vector2 vel = GetComponent<Rigidbody2D>().velocity;
                        vel *= 0.9f;
                        GetComponent<Rigidbody2D>().velocity = vel;
                    }
                    else if (curTime <= 0.0f)
                    {
                        state = STATES.IDLE;
                    }
					break;
				}
			case STATES.DEAD:
				{
					break;
				}
		} 
	}

	public virtual void CheckThresholds()
	{
		if (Act_currHP < hpThresh)
		{
			currBehavior = behaviors[0];
		}
		else if (behaviorSize > 1 && nightThresh && MNGR_Game.isNight)
		{
			currBehavior = behaviors[1];
		}
		else if (behaviorSize > 2 && distanceToTarget < distThresh)
		{
			currBehavior = behaviors[2];
		}
		else
			currBehavior = basicBehavior;
	}

	public virtual void NewState()
	{
        if (Act_IsIntelligent) // L: dummies dont change states.
        {
            RestoreToBaseSpeed();
            if (kamikazeActivated)
            {
                state = STATES.SPECIAL;
                curTime = stateTime[(int)state];
                return;
            }
            if ((state != STATES.HURT || state != STATES.DEAD) && !(!MNGR_Game.isNight && Act_currHP == Act_baseHP))
            {
                randomState = (int)Random.Range(0.0f, 4.999f);

			if (randomState != 3) // If we dont get an attack state, reroll once (this increases the enemy attack frequency)
				randomState = Random.Range(0, 6);

                state = (STATES)randomState;
                curTime = stateTime[(int)state];
            }
        }
	}

    // L: movin' this over here.
    public void ApplyKnockBack(Vector2 _Force)
    {

        GetComponent<Rigidbody2D>().velocity = _Force;

        state = STATES.HURT;
        curTime = stateTime[(int)state] + (_Force.magnitude * 0.01f);

    }

    public void ModifyDefense(float newDefense)
    {
        damageMod = newDefense;
    }

    public void RestoreDefense()
    {
        damageMod = 1.0f;
    }

	public void OnGUI()
	{
		if (cam != null)
		{
			Vector2 targetPos;
			targetPos = cam.WorldToScreenPoint(transform.position);
			SpriteRenderer spr = GetComponent<SpriteRenderer>();
			GUI.Box(new Rect(targetPos.x - 50, Screen.height - targetPos.y - 128, 100, 6), "", BlackBar);
			GUI.Box(new Rect(targetPos.x - 50, Screen.height - targetPos.y - 128, 100 * ((float)Act_currHP / (float)Act_baseHP), 6), "", HealthBar);
		}
	}
}

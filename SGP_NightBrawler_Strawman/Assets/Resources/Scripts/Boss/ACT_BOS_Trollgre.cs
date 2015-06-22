using UnityEngine;
using System.Collections;

public class ACT_BOS_Trollgre : ACT_Enemy {

    public float Tro_NewHpTresh;
    public float Tro_HpTreshReducer;
    public PROJ_Base[] Tro_AllProjectiles;

    public BHR_Base[] Tro_myBehaviours;
    ///Behaviors
    //[0] Diviver // Holy Diviver!
    //[1] Dodger
    ///Behaviors

	// Use this for initialization
	void Start () {
        stateTime = new float[] { 2.0f, 0.75f, 0.5f, 0.5f, 0.6f, 0.3f, 1.0f, 1.0f };

        behaviors = new BHR_Base[behaviorSize];
        Act_facingRight = false;

        nightThresh = true;                          //If its night this will activate.
        hpThresh = (int)(Act_baseHP * 0.75f);        //If its HP is at 75% this will activate.
        target = GameObject.FindGameObjectWithTag("Player");
        state = STATES.IDLE;
        isMelee = true;
        currTime = stateTime[(int)state];
        projectile = Tro_AllProjectiles[0];
        Tro_HpTreshReducer = 0.3f;
        Tro_NewHpTresh = Act_baseHP - (int)(Act_baseHP * Tro_HpTreshReducer);

        Act_currAttackSpeed = Act_baseAttackSpeed;
        Act_currHP = Act_baseHP;
        Act_currPower = Act_basePower;
        Act_currSpeed = Act_baseSpeed;

        for (int i = 0; i < Tro_myBehaviours.Length; i++)
        {
            if (Tro_myBehaviours[i].owner == null)
                Tro_myBehaviours[i].owner = GetComponent<ACT_BOS_Trollgre>();
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
        currTime -= Time.deltaTime;
        Act_currAttackSpeed -= Time.deltaTime;
        CheckThresholds();

        if (currTime <= 0)
            NewState();

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
                            SetCurrSpeed(Act_baseSpeed + 1);
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
                            SetCurrSpeed(Act_baseSpeed + 1);
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
                    if (projectile != null && Act_currAttackSpeed <= 0.0f)
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
                    break;
                }
            case STATES.HURT:
                {
                    break;
                }
            case STATES.DEAD:
                {
                    if (dividerActivated)
                    {
                        if ( Tro_myBehaviours[0].owner == null)
                            Tro_myBehaviours[0].owner = GetComponent<ACT_BOS_Trollgre>();


                        Tro_myBehaviours[0].PerformBehavior();
                        if (numGeneration == 0)
                            if (GetComponent<ITM_DropLoot>())
                                GetComponent<ITM_DropLoot>().DropCoin(transform.position);

                        Destroy(transform.gameObject);
                    }
                    break;
                }
        } 
	}

    public override void CheckThresholds()
    {
        if (Act_currHP <= Tro_NewHpTresh)
        {
            Tro_HpTreshReducer += 0.3f;
            Tro_NewHpTresh = Act_baseHP - (int)(Act_baseHP * Tro_HpTreshReducer);
            if (Act_currHP <= (int)(Act_baseHP * 0.33f))
            {
                isMelee = false;
                projectile = Tro_AllProjectiles[2];
                Debug.Log("Less Then 33%");
                GetComponent<MNGR_Animation_Enemy>().sprites = Resources.LoadAll<Sprite>("Sprites/Boss/TrollgreMage");
            }
            else if (Act_currHP >= (int)(Act_baseHP * 0.33f) && Act_currHP <= (int)(Act_baseHP * 0.66f))
            {
                isMelee = false;
                projectile = Tro_AllProjectiles[1];
                Debug.Log("Less Then 66%");
                GetComponent<MNGR_Animation_Enemy>().sprites = Resources.LoadAll<Sprite>("Sprites/Boss/TrollgreArcher");
            }

        }
    }
    public override void NewState()
    {
        if (Act_IsIntelligent) // L: dummies dont change states.
        {
            RestoreToBaseSpeed();
            if (state != STATES.HURT || state != STATES.DEAD)
            {
                float DisX = Mathf.Abs(target.transform.position.x - transform.position.x);
                m_distance = DisX;
                float DisY = Mathf.Abs(target.transform.position.y - transform.position.y);
                if (isMelee && DisX <= 4)
                    state = STATES.ATTACKING;
                else if (!isMelee && DisX >= 5 && DisY <= 0.5f)
                    state = STATES.ATTACKING;
                else
                    state = STATES.WALKING;


                currTime = stateTime[(int)state];
            }
        }
    }
}

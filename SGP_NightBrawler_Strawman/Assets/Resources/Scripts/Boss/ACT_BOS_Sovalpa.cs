using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ACT_BOS_Sovalpa : ACT_Enemy {

    public int Sov_CurrAttack;
    public bool Sov_ChangeAttack;
    public bool Sov_CallOnMinions;
    public bool Sov_CallOnClones;
    public bool Sov_WayPointLock;
    public float Sov_StunCooldown;

    //Debug Stuff
    public float Distance;
    //Debug Stuff

    public PROJ_Base[] Sov_Attacks;
    public BHR_Base[] Sov_myBehaviours;
    public GameObject[] Sov_WayPoints;

    public List<GameObject> Sov_SpawnPoints;

    private int Sov_NewHpTresh;
    private float Sov_HpTreshReducer;
    
    ///Behaviors
    /// [0] KnockBack.
    /// [1] Spawner.
    /// [2] Divider.
    ///Behaviors

	// Use this for initialization
	void Start () 
    {
        stateTime = new float[] { 2.0f, 0.75f, 0.5f, 0.5f, 0.6f, 0.3f, 1.0f, 1.0f };

        behaviors = new BHR_Base[behaviorSize];
        Act_facingRight = false;

        for (int i = 0; i < behaviorSize; i++)
        {
            behaviors[i] = Instantiate(GameObject.FindGameObjectWithTag("_Overlord").GetComponent<BHR_Overlord>().behaviors[behaviorID[i]]);
            if (behaviors[i].owner == null)
                behaviors[i].owner = GetComponent<ACT_BOS_Ent>();
        }

        Sov_StunCooldown = 0.0f;

        Act_currPower = (int)(Act_basePower * 0.5f); //The Tree will be weeker at firts.
        nightThresh = true;     //If its night this will activate.
        distThresh = 5.0f;      //If the Player is 3 units away this will activate.
        hpThresh = (int)(Act_baseHP * 0.75f);   //If its HP is at 75% this will activate.
        target = null;
        state = STATES.WALKING;

        target = GameObject.FindGameObjectWithTag("Player");

        Sov_WayPointLock = false;
        Sov_HpTreshReducer = 0.1f;
        Sov_NewHpTresh = Act_baseHP - (int)(Act_baseHP * Sov_HpTreshReducer);
        projectile = Sov_Attacks[0];
	}
	
	// Update is called once per frame
	void Update () 
    {
        CheckThresholds();
        Act_currAttackSpeed -= Time.deltaTime;

        if (curTime <= 0.0f)
            state = STATES.WALKING;


        Sov_StunCooldown -= Time.deltaTime;

        if (MNGR_Game.isNight)
            nightThresh = true;
        else
            nightThresh = false;

        if ((state == STATES.WALKING || state == STATES.RUNNING) && !Sov_WayPointLock)
        {
            if (target != null)
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
            }
        }
        else if (Sov_WayPointLock)
        {
            transform.position = target.transform.position;
            Act_facingRight = target.GetComponent<Platforms>().Plt_facingRight;
            state = STATES.ATTACKING;
        }

        if (Sov_CallOnMinions)
        {
            Sov_myBehaviours[1].GetComponent<BHR_Spawner>().Spw_SpawnAtLocation = true;
            for (int i = 0; i < Sov_SpawnPoints.Count; i++)
            {
                Sov_myBehaviours[1].GetComponent<BHR_Spawner>().Spw_NewLocation = Sov_SpawnPoints[i].transform.position;
                Sov_myBehaviours[1].GetComponent<BHR_Spawner>().PerformBehavior();
            } 
        }

        float Dis = Vector3.Distance(transform.position, target.transform.position);
        Distance = Dis;
        if (Dis <= 1.8f && !Sov_WayPointLock)
        {
            state = STATES.ATTACKING;
            curTime = stateTime[(int)state];
        }
        else if (Dis > 1.8f && !Sov_WayPointLock)
            state = STATES.WALKING;

        switch (state)
        {
            case STATES.WALKING:
                    RestoreToBaseSpeed();
                    break;
            case STATES.RUNNING:
                    SetCurrSpeed(Act_baseSpeed + 4);
                    break;
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

    public override void CheckThresholds()
    {
        if (target != null)
        {
            if (GetComponent<MOD_DMGProtection>() == null && nightThresh) // Is Night
                MNGR_Item.AttachModifier(2, gameObject);
            else if (!nightThresh && Sov_StunCooldown <= 0.0f)
            {
                MNGR_Item.AttachModifier(10, gameObject);
                Sov_StunCooldown = 60.0f;
            }

            if (Act_currHP <= Sov_NewHpTresh)
            {
                Sov_HpTreshReducer += 0.1f;
                Sov_NewHpTresh = Act_baseHP - (int)(Act_baseHP * Sov_HpTreshReducer);
                int ChangeAttacks;

                if (Act_currHP <= (int)(Act_baseHP * 0.4f))
                {
                    ChangeAttacks = Random.Range(0, 3);
                    ChangeAttackPatterns(ChangeAttacks);
                    Debug.Log("Less Then 40%");
                }
                else if (Act_currHP >= (int)(Act_baseHP * 0.4f) && Act_currHP <= (int)(Act_baseHP * 0.70f))
                {
                    ChangeAttacks = Random.Range(0, 2);
                    ChangeAttackPatterns(ChangeAttacks);
                    Debug.Log("Less Then 70%");
                }
                else if (Act_currHP >= (int)(Act_baseHP * 0.70f))
                {
                    ChangeAttackPatterns(0);
                    Debug.Log("Less Then 100%");
                }
            }


            if (target.tag != "Bounds")
            {
                Sov_WayPointLock = false;
            }
            else if (target.tag == "Bounds")
            {
                state = STATES.RUNNING;
                float Dis = Vector3.Distance(transform.position, target.transform.position);
                if (Dis <= 0.1f)
                    Sov_WayPointLock = true;
            }
        }
    }

    public override void NewState()
    {
        //Stop it from been called.
    }

    void ChangeAttackPatterns(int _A)
    {
        switch (_A)
        {
            case 0:
                //Change Weapond
                //If is a range move to a Waypoints and shoot from there.
                Sov_CallOnMinions = false;
                Sov_CallOnClones = false;
                ChangeCurrWeapond();
                break;
            case 1:
                //Call Minions.
                Sov_CallOnMinions = true;
                Sov_CallOnClones = false;
                break;
            case 2:
                Sov_CallOnMinions = false;
                Sov_CallOnClones = true;
                //Divide into 2 clones.
                break;  
        }
    }

    void ChangeCurrWeapond()
    {
        for (;;)
        {
            int NewAttack = Random.Range(0, 3);
            if (projectile.name != Sov_Attacks[NewAttack].name)
            {
                projectile = Sov_Attacks[NewAttack];
                break;
            } 
        }

        if (!projectile.name.Contains("Melee"))
        {
            for (;;)
            {
                int RandWaypoint = Random.Range(0, Sov_WayPoints.Length);
                if (Sov_WayPoints[RandWaypoint] != null)
                {
                    target = Sov_WayPoints[RandWaypoint];
                    break;
                } 
            }
        }
        else
            target = GameObject.FindGameObjectWithTag("Player");
    }

    void OnCollisionEnter2D(Collision2D Col)
    {
        if (state == STATES.RUNNING)
        {
            if (Col.gameObject.tag == "Player")
            {
                int Knck_Direction;
                if (Act_facingRight)
                    Knck_Direction = -1;
                else
                    Knck_Direction = 1;

                float Force = (float)(Act_currPower * Knck_Direction);
                target.GetComponent<PlayerController>().ApplyKnockBack(Force);
                target.GetComponent<PlayerController>().party[target.GetComponent<PlayerController>().currChar].ChangeHP(-Act_currPower / 5);
            }

               
        }
    }

}

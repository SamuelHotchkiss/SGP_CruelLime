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

    private float Sov_NewHpTresh;
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

        if (numGeneration == 2)
            Act_Parent = null;

        Sov_StunCooldown = 0.0f;

        nightThresh = true;                     //If its night this will activate.
        state = STATES.WALKING;

        target = GameObject.FindGameObjectWithTag("Player");

        Sov_WayPointLock = false;
        Sov_HpTreshReducer = 0.1f;
        Sov_NewHpTresh = Act_baseHP - (Act_baseHP * Sov_HpTreshReducer);
        projectile = Sov_Attacks[0];

        for (int i = 0; i < Sov_myBehaviours.Length; i++)
        {
            if (Sov_myBehaviours[i].owner == null)
                Sov_myBehaviours[i].owner = GetComponent<ACT_BOS_Sovalpa>(); 
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
        currTime -= Time.deltaTime;

        if (currTime <= 0.0f)
        {
            NewState();
            currTime = stateTime[(int)state];
        }

        CheckThresholds();
        Act_currAttackSpeed -= Time.deltaTime;

        Sov_StunCooldown -= Time.deltaTime;

        if (MNGR_Game.isNight)
            nightThresh = true;
        else
            nightThresh = false;

        if (Act_currHP <= 0)
        {
            state = STATES.DEAD;
            Sov_CallOnMinions = false;
            numGeneration = 0;
        }

        if (state != STATES.DEAD)
        {
            if ((state == STATES.WALKING || state == STATES.RUNNING) && !Sov_WayPointLock)
            {
                if (target != null)
                {
                    float Xpos;
                    float Ypos;
                    Ypos = Mathf.Lerp(transform.position.y, target.transform.position.y, (0.5f * Act_currSpeed * Time.deltaTime));
                    Xpos = Mathf.Lerp(transform.position.x, target.transform.position.x, (0.5f * Act_currSpeed * Time.deltaTime));

                    transform.position = new Vector3(Xpos, Ypos);

                    GetComponent<Rigidbody2D>().velocity *= Random.Range(0.1f, 0.9f);
                    if (target.transform.position.x > transform.position.x)
                        Act_facingRight = true;
                    else if (target.transform.position.x < transform.position.x)
                        Act_facingRight = false;

                    //if (target.transform.position.x > transform.position.x)
                    //{
                    //    Vector2 vel = GetComponent<Rigidbody2D>().velocity;
                    //    vel = new Vector2(Act_currSpeed, vel.y);
                    //    GetComponent<Rigidbody2D>().velocity = vel;
                    //    Act_facingRight = true;
                    //}
                    //else
                    //{
                    //    Vector2 vel = GetComponent<Rigidbody2D>().velocity;
                    //    vel = new Vector2(-Act_currSpeed, vel.y);
                    //    GetComponent<Rigidbody2D>().velocity = vel;
                    //    Act_facingRight = false;
                    //}
                    //if (target.transform.position.y > transform.position.y)
                    //{
                    //    Vector2 vel = GetComponent<Rigidbody2D>().velocity;
                    //    vel = new Vector2(vel.x, Act_currSpeed);
                    //    GetComponent<Rigidbody2D>().velocity = vel;
                    //}
                    //else
                    //{
                    //    Vector2 vel = GetComponent<Rigidbody2D>().velocity;
                    //    vel = new Vector2(vel.x, -Act_currSpeed);
                    //    GetComponent<Rigidbody2D>().velocity = vel;
                    //}
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
                Sov_myBehaviours[1].GetComponent<BHR_Spawner>().Update();
                Sov_myBehaviours[1].GetComponent<BHR_Spawner>().Spw_SpawnAtLocation = true;
                int RandPoint = Random.Range(0, Sov_SpawnPoints.Count);

                if (Sov_SpawnPoints[RandPoint] != null)
                {
                    Sov_myBehaviours[1].GetComponent<BHR_Spawner>().Spw_NewLocation = Sov_SpawnPoints[RandPoint].transform.position;
                    Sov_myBehaviours[1].GetComponent<BHR_Spawner>().PerformBehavior();
                }
                else
                    Spw_CrittersCreated++;

                if (!Sov_myBehaviours[1].GetComponent<BHR_Spawner>().Spw_SpawnAllCritters)
                {
                    Spw_CrittersCreated = 0;
                    Sov_CallOnMinions = false;
                }
            }

            if (Sov_CallOnClones)
            {
                if (target.transform.position == transform.position)
                {
                    Sov_myBehaviours[2].GetComponent<BHR_Divider>().ModDivide(4, 2, 2, 2, STATES.WALKING);
                    projectile = Sov_Attacks[0];
                    Sov_CallOnClones = false;
                    target = GameObject.FindGameObjectWithTag("Player");
                }
            }


            float Dis = Vector3.Distance(transform.position, target.transform.position);
            Distance = Dis;
            if (Dis <= 1.8f && (Sov_WayPointLock || target.tag == "Player"))
            {
                state = STATES.ATTACKING;
                currTime = stateTime[(int)state];
            }
            else if (Dis > 1.8f && target.tag == "Player")
                state = STATES.WALKING; 
        }


        switch (state)
        {
            case STATES.WALKING:
                    RestoreToBaseSpeed();
                    break;
            case STATES.RUNNING:
                    SetCurrSpeed(Act_baseSpeed + 3);
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
					if (GetComponent<ITM_DropLoot>())
					{
						GetComponent<ITM_DropLoot>().DropCoin(transform.position);
					}
                    Destroy(gameObject);
                    break;
                }
        }

        if (currTime <= 0.0f)
            state = STATES.WALKING;
        if (numGeneration < 2 && Act_Parent == null)
            ChangeHP(-Act_currHP);

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
                //int ChangeAttacks;

                if (Act_currHP <= (int)(Act_baseHP * 0.4f))
                {
                    Sov_NewHpTresh = 0;
                    //ChangeAttacks = Random.Range(0, 3);
                    ChangeAttackPatterns(2);
                    Debug.Log("Less Then 40%");
                }
                else if (Act_currHP >= (int)(Act_baseHP * 0.4f) && Act_currHP <= (int)(Act_baseHP * 0.70f))
                {
                    //ChangeAttacks = Random.Range(0, 2);
                    ChangeAttackPatterns(1);
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
                Distance = Dis;
                if (Dis <= 3.0f)
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
        if (numGeneration < 2)
            _A = 0;

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
                Sov_myBehaviours[1].GetComponent<BHR_Spawner>().Spw_SpawnAllCritters = true;
                ChangeCurrWeapond();
                break;
            case 2:
                Sov_CallOnMinions = false;
                Sov_CallOnClones = true;
                target = Sov_WayPoints[2];
                //Divide into 2 clones.
                break;  
        }
    }

    void ChangeCurrWeapond()
    {
        if (numGeneration == 2)
        {
            for (; ; )
            {
                int NewAttack = Random.Range(0, 2);
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
                    //int RandWaypoint = Random.Range(0, 2);
                    if (Sov_WayPoints[2] != null)
                    {
                        target = Sov_WayPoints[2];
                        break;
                    }
                }
            }
           else
            //projectile = Sov_Attacks[0];
            target = GameObject.FindGameObjectWithTag("Player");
        }
        else
        {
            projectile = Sov_Attacks[0];
            target = GameObject.FindGameObjectWithTag("Player"); 
        }
    }

    void OnCollisionEnter2D(Collision2D Col)
    {
        if (state == STATES.RUNNING)
        {
            if (Col.gameObject.tag == "Player")
            {
				//int Knck_Direction;
				//if (Act_facingRight)
				//	Knck_Direction = -1;
				//else
				//	Knck_Direction = 1;

                //float Force = (float)(Act_currPower * Knck_Direction);
                //target.GetComponent<PlayerController>().ApplyKnockBack(Force);
                //target.GetComponent<PlayerController>().party[target.GetComponent<PlayerController>().currChar].ChangeHP(-Act_currPower / 5);
            }

               
        }
    }

}

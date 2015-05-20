using UnityEngine;
using System.Collections;

/*
    Behaviors Order.
    [0] Spawner
    [1] KnockBack
*/

public class ACT_BOS_Ent : ACT_Enemy
{
    public BHR_Base[] myBehaviours;
    private bool Ent_HealHP;        //If Low HP grow root wall and heal.
    private float Ent_HealTimer;    //For how long will the tree heal. 
    private float Ent_HealCooldown; //Stops the regen from been been to fast;

    public GameObject Ent_Roots;    //This save and will replace the current Critter
    public GameObject Ent_RootWall;

    public float DISTANCE;
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
            if (behaviors[i].owner == null)
                behaviors[i].owner = GetComponent<ACT_BOS_Ent>();
        }

        Act_currPower = (int)(Act_basePower * 0.5f); //The Tree will be weeker at firts.
        nightThresh = true;     //If its night this will activate.
        distThresh = 5.0f;      //If the Player is 3 units away this will activate.
        hpThresh = (int)(Act_baseHP * 0.75f);   //If its HP is at 75% this will activate.
        target = null;
        state = STATES.IDLE;
        Ent_HealTimer = 7.0f;
        Ent_HealHP = false;
        Ent_HealCooldown = 0;
	}
	
	// Update is called once per frame
	void Update () 
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().isKinematic = true;
        CheckThresholds();

        if (Ent_HealHP)
        {
            if (Ent_HealCooldown <= 0.0f)
            {
                ChangeHP(5);
                Ent_HealCooldown = 0.1f;
            }
            
            currBehavior = behaviors[0]; //Spawn roots
            state = STATES.SPECIAL;
            curTime = stateTime[(int)state];

            Ent_HealTimer -= Time.deltaTime;
            Ent_HealCooldown -= Time.deltaTime;

            if (Ent_HealTimer <= 0.0f)
                Ent_HealHP = false;
                Ent_HealTimer = 7.0f;
        }


        if (Act_currHP <= 0.0f)
        {
            state = STATES.DEAD;
            curTime = stateTime[(int)state];
        }
        switch (state)
        {
            case STATES.IDLE:
                target = null;
                break;
            case STATES.ATTACKING:
                Spw_Critter = Ent_Roots;
                Spw_SpawnPosition = new Vector3(transform.position.x + 5.0f, target.transform.position.y);
                currBehavior.PerformBehavior();
                break;
            case STATES.SPECIAL:
                if (Ent_HealHP)
                {
                    Spw_Critter = Ent_RootWall;
                    currBehavior.PerformBehavior();
                }
                else if (!Ent_HealHP)
                {
                    state = STATES.ATTACKING;
                    curTime = stateTime[(int)state];
                }
                    
                break;
            case STATES.HURT:
                //Animations??
                Knck_Cooldown = 0.0f;
                break;
            case STATES.DEAD:
                Destroy(gameObject);
                break;
        }

        
        if (Act_currHP != Act_baseHP && state != STATES.ATTACKING)
        {
            state = STATES.ATTACKING;
            curTime = stateTime[(int)state];
            target = GameObject.FindGameObjectWithTag("Player");
        }
	}

    public override void CheckThresholds()
    {
        //base.CheckThresholds();
        if (hpThresh >= Act_currHP)
        {
            if (Act_currHP <= (int)(Act_baseHP * 0.25f))
            {
                //DoStuff
                Ent_HealHP = true;
                Spw_BaseSpawnCoolDown = Spw_baseSpawnPerSec * 5.0f;
            }
            else if (Act_currHP <= Act_baseHP * 0.5f)
            {
                //Do Stuff;
                Spw_BaseSpawnCoolDown = Spw_baseSpawnPerSec * 4.0f;
                Act_currPower = Act_basePower;          //Full Power potential
                hpThresh = (int)(Act_baseHP * 0.25f);   //If its HP is at 25% this will activate.
            }
            else if (Act_currHP <= Act_baseHP * 0.75f)
            {
                //Do Stuff;
                Act_currPower = (int)(Act_basePower * 0.9f);    //Power Increse to 90% of its potential
                hpThresh = (int)(Act_baseHP * 0.5f);            //If its HP is at 50% this will activate.
            }
        }

        if (target != null)
        {
            if (distThresh >= Vector3.Distance(target.transform.position, transform.position))
            {
                currBehavior = behaviors[1]; //Pushback
                Debug.Log("PUCH BACK U FOOLS");
            }
            else if (distThresh < Vector3.Distance(target.transform.position, transform.position))
            {
                Debug.Log("GET RECK FOOLS");
                currBehavior = behaviors[0]; //Spawn roots
            }

            DISTANCE = Vector3.Distance(target.transform.position, transform.position);
        }

        //if (nightThresh == MNGR_Game.isNight)
        //    Act_currPower = Act_currPower + (int)(Act_currPower * 0.1f); //When is night power will increse by 10%
        
            
        
    }
}

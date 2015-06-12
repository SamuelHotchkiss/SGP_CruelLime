﻿using UnityEngine;
using System.Collections;

public class ENY_Arms : ACT_Enemy {

    public GameObject Arm_HostBody;
    public Vector3 Arm_MoveLoc;
    public bool Arm_IsShadow;
    public bool Arm_TargetFound;
    public GameObject Arm_ShadowObj;

    public Sprite Bip_DayArms;
    public Sprite Bip_NightArms;
    public PROJ_Base Bip_DayProjectile;
    public PROJ_Base Bip_NightProjectile;

    public float Arm_DownTimer;
    public float Arm_IgnoreCollision;
    float Arm_BaseIgnoreCollision;
    float Arm_BaseDownTimer;

	// Use this for initialization
	void Start () 
    {
        if (Arm_IsShadow)
            Arm_ShadowObj = null;

         Act_baseHP = Arm_HostBody.GetComponent<ACT_Enemy>().Act_baseHP / 2;
         Act_currHP = Act_baseHP;
         Act_basePower = Arm_HostBody.GetComponent<ACT_Enemy>().Act_basePower;
         Act_currPower = Act_basePower;
         Arm_IgnoreCollision = 6.0f;
         Arm_BaseIgnoreCollision = Arm_IgnoreCollision;
         Arm_IgnoreCollision = 0.0f;
         Arm_DownTimer = 3.0f;
         Arm_BaseDownTimer = Arm_DownTimer;
         Arm_DownTimer = 0.0f;

         if (!Arm_IsShadow)
         {
             if (!MNGR_Game.isNight)
             {
                 GetComponent<SpriteRenderer>().sprite = Bip_DayArms;
                 projectile = Bip_DayProjectile;
             }
             else
             {
                 GetComponent<SpriteRenderer>().sprite = Bip_NightArms;
                 projectile = Bip_NightProjectile;
             }
                 
         }


	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Arm_HostBody.GetComponent<ACT_BOS_Bipolar>().Act_currHP <= 0)
        {
            Destroy(Arm_ShadowObj);
            Destroy(gameObject);
        }

        if (Arm_IsShadow)
        {
            Arm_DownTimer -= Time.deltaTime;
            Arm_IgnoreCollision -= Time.deltaTime;

            if (Arm_DownTimer <= 0)
                Arm_TargetFound = false;

            if (Arm_HostBody.GetComponent<ACT_BOS_Bipolar>().Bip_PatternId == 2 && Arm_DownTimer <= 0.0f && Arm_IgnoreCollision <= 0.0f)
            {
                Arm_DownTimer = 0.25f;
                Arm_IgnoreCollision = 0.4f;
                Arm_TargetFound = true;
            }
            if (!Arm_TargetFound)
            {
                float Xpos;
                float Ypos;
                Ypos = Mathf.Lerp(transform.position.y, Arm_MoveLoc.y, 0.02f);
                Xpos = Mathf.Lerp(transform.position.x, Arm_MoveLoc.x, 0.02f);
                transform.position = new Vector3(Xpos, Ypos);  
            }
        }
        else
        {
            float Xpos;
            float Ypos;

            Spw_SpawnPerSec -= Time.deltaTime;

            Arm_TargetFound = Arm_ShadowObj.GetComponent<ENY_Arms>().Arm_TargetFound;
            Arm_DownTimer = Arm_ShadowObj.GetComponent<ENY_Arms>().Arm_DownTimer;

            if (Arm_TargetFound || Arm_HostBody.GetComponent<ACT_BOS_Bipolar>().Bip_PatternId == 1)
            {
                if (Arm_HostBody.GetComponent<ACT_BOS_Bipolar>().Bip_PatternId != 2)
                    Ypos = Mathf.Lerp(transform.position.y, Arm_ShadowObj.transform.position.y, 0.15f);
                else
                    Ypos = Mathf.Lerp(transform.position.y, Arm_ShadowObj.transform.position.y, 0.9f);
                Ypos = Mathf.Lerp(transform.position.y, Arm_ShadowObj.transform.position.y, 0.15f);
                Xpos = Arm_ShadowObj.transform.position.x;

                transform.position = new Vector3(Xpos, Ypos, transform.position.z);
                if (Arm_HostBody.GetComponent<ACT_BOS_Bipolar>().Bip_PatternId == 1)
                {
                    Act_currAttackSpeed -= Time.deltaTime;

                    if (Act_currAttackSpeed <= 0.0f && projectile != null)
                    {
                        PROJ_Base clone = (PROJ_Base)Instantiate(projectile, transform.position, new Quaternion(0, 0, 0, 0));
                        clone.owner = gameObject;
                        clone.Initialize();

                        Act_currAttackSpeed = Act_baseAttackSpeed;
                        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    }
                }
                else if (Arm_HostBody.GetComponent<ACT_BOS_Bipolar>().Bip_PatternId == 2)
                {
                    float Dis = Mathf.Abs(transform.position.y - Arm_ShadowObj.transform.position.y);
                    if (Dis <= 0.5f && Spw_SpawnPerSec <= 0.0f)
                    {
                        Instantiate(Spw_Critter, transform.position, new Quaternion());
                        Spw_SpawnPerSec = Spw_baseSpawnPerSec;
                    }
                        
                }
            }
            else
            {
                if (Arm_HostBody.GetComponent<ACT_BOS_Bipolar>().Bip_PatternId != 2)
                {
                    Ypos = Mathf.Lerp(transform.position.y, 5.0f, 0.01f);
                }
                else
                {
                    Ypos = Mathf.Lerp(transform.position.y, 5.0f, 0.4f);
                }
                
                Xpos = Arm_ShadowObj.transform.position.x;
                transform.position = new Vector3(Xpos, Ypos, transform.position.z); 
            }
        }
	}

    public override void ChangeHP(int Dmg)       //Applies current HP by set amount can be use to Heal as well
    {                                            //Damage needs to be negative.
        if (!Arm_IsShadow)
        {
            Arm_HostBody.GetComponent<ACT_Enemy>().ChangeHP(Dmg);

            Act_currHP += (int)(Dmg * damageMod);
            if (Act_currHP > Act_baseHP)
                Act_currHP = Act_baseHP;
            if (Act_currHP <= 0)
            {
                Act_currHP = 0;
                //state = STATES.DEAD;
                //currTime = stateTime[(int)state];
                Destroy(Arm_ShadowObj);
                Destroy(gameObject);
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player" && Arm_DownTimer <= 0.0f && Arm_IgnoreCollision <= 0.0f && Arm_HostBody.GetComponent<ACT_BOS_Bipolar>().Bip_PatternId == 0)
       {
           Arm_TargetFound = true;
           Arm_DownTimer = Arm_BaseDownTimer;
           Arm_IgnoreCollision = Arm_BaseIgnoreCollision;
       }
    }
}
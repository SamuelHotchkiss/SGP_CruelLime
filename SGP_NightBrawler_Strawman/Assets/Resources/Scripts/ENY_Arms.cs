using UnityEngine;
using System.Collections;

public class ENY_Arms : ACT_Enemy {

    public GameObject Arm_HostBody;
    public Vector3 Arm_MoveLoc;
    public bool Arm_IsShadow;
    public bool Arm_TargetFound;
    public GameObject Arm_ShadowObj;

    public bool Arm_IsLeft;
    public bool Arm_IsRight;
    public Sprite Bip_LeftArm;
    public Sprite Bip_RightArms;
    public PROJ_Base Bip_LeftProjectile;
    public PROJ_Base Bip_RightProjectile;
    public PROJ_Base Bip_Smash;

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
             GetComponent<BoxCollider2D>().enabled = false;
             if (Arm_IsLeft)
             {
                 GetComponent<SpriteRenderer>().sprite = Bip_LeftArm;
                 projectile = Bip_LeftProjectile;
             }
             else if (Arm_IsRight)
             {
                 GetComponent<SpriteRenderer>().sprite = Bip_RightArms;
                 projectile = Bip_RightProjectile;
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
                Ypos = Mathf.Lerp(transform.position.y, Arm_MoveLoc.y, (2f * Time.deltaTime));
                Xpos = Mathf.Lerp(transform.position.x, Arm_MoveLoc.x, (2f * Time.deltaTime));
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

            //Shockwave
            if (Mathf.Abs(transform.position.y - Arm_ShadowObj.transform.position.y) <= 0.5f && !GetComponent<BoxCollider2D>().enabled)
            {
                if (Arm_IsRight)
                {
                    PROJ_Base clone = (PROJ_Base)Instantiate(Bip_Smash, transform.position, new Quaternion(0, 0, 0, 0));
                    clone.owner = gameObject;
                    clone.Initialize();
                    Act_currAttackSpeed = Act_baseAttackSpeed;
                    GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    GetComponent<BoxCollider2D>().enabled = true; 
                }
                else if (Arm_IsLeft)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        Vector3 NPos = new Vector3(Random.Range(transform.position.x - 5, transform.position.x + 5), transform.position.y + Random.Range(15,25));
                        PROJ_Base clone = (PROJ_Base)Instantiate(Bip_Smash, NPos, new Quaternion(0, 0, 0, 0));
                        clone.owner = gameObject;
                        clone.Initialize();
                        Act_currAttackSpeed = Act_baseAttackSpeed;
                        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                        GetComponent<BoxCollider2D>().enabled = true;  
                    }

                }
            }

            
            //Go to the Ground.
            if (Arm_TargetFound || Arm_HostBody.GetComponent<ACT_BOS_Bipolar>().Bip_PatternId == 1)
            {
                if (Arm_HostBody.GetComponent<ACT_BOS_Bipolar>().Bip_PatternId != 2)
                    Ypos = Mathf.Lerp(transform.position.y, Arm_ShadowObj.transform.position.y, (5f * Time.deltaTime));
                else
                    Ypos = Mathf.Lerp(transform.position.y, Arm_ShadowObj.transform.position.y, (9f * Time.deltaTime));

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
                    Ypos = Mathf.Lerp(transform.position.y, 5.0f, (2f * Time.deltaTime));
                else
                    Ypos = Mathf.Lerp(transform.position.y, 5.0f, (4f * Time.deltaTime));

                if (Mathf.Abs(transform.position.y - Arm_ShadowObj.transform.position.y) >= 1.5f)
                    GetComponent<BoxCollider2D>().enabled = false;

                Xpos = Arm_ShadowObj.transform.position.x;
                transform.position = new Vector3(Xpos, Ypos, transform.position.z); 
            }
        }
	}

    public override void ChangeHP(float Dmg)       //Applies current HP by set amount can be use to Heal as well
    {                                            //Damage needs to be negative.
        if (!Arm_IsShadow)
        {
            Arm_HostBody.GetComponent<ACT_Enemy>().ChangeHP(Dmg);

            Act_currHP += (Dmg * damageMod);
            if (Act_currHP > Act_baseHP)
                Act_currHP = Act_baseHP;
            if (Act_currHP <= 0)
            {
                Act_currHP = 0;
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

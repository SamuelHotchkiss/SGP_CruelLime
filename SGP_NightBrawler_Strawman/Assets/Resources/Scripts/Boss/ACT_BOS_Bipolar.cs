using UnityEngine;
using System.Collections;

public class ACT_BOS_Bipolar : ACT_Enemy {

    public int Bip_PatternId;

    public bool Bip_LeftArmDead;
    public bool Bip_RightArmDead;

    public bool Bip_LeftInTarget;
    public bool Bip_RightInTarget;

    public GameObject Bip_LeftArm;
    public GameObject Bip_RightArm;
    public BHR_Base[] Bip_myBehaviours;


    public Sprite Bip_DayBody;
    public Sprite Bip_NightBody;

    public Vector3 Bip_RandLeftSmashLoc;
    public Vector3 Bip_RandRightSmashLoc;

    private int Bip_NewHpTresh;
    private float Bip_HpTreshReducer;
    private float Bip_PatternTimer;


	// Use this for initialization
	void Start () {
        stateTime = new float[] { 2.0f, 0.75f, 0.5f, 0.5f, 0.6f, 0.3f, 1.0f, 1.0f };

        behaviors = new BHR_Base[behaviorSize];
        Act_facingRight = false;

        nightThresh = true;                          //If its night this will activate.
        hpThresh = (int)(Act_baseHP * 0.75f);        //If its HP is at 75% this will activate.
        target = GameObject.FindGameObjectWithTag("Player");

        Bip_LeftInTarget = true;
        Bip_RightInTarget = true;
        Bip_LeftArmDead = false;
        Bip_RightArmDead = false;

        Bip_PatternId = 0;
        Bip_HpTreshReducer = 0.2f;
        Bip_NewHpTresh = Act_baseHP - (int)(Act_baseHP * Bip_HpTreshReducer);

        if (!MNGR_Game.isNight)
            GetComponent<SpriteRenderer>().sprite = Bip_DayBody;
        else
            GetComponent<SpriteRenderer>().sprite = Bip_NightBody;
            

        target = GameObject.FindGameObjectWithTag("Player");

        for (int i = 0; i < Bip_myBehaviours.Length; i++)
        {
            if (Bip_myBehaviours[i].owner == null)
                Bip_myBehaviours[i].owner = GetComponent<ACT_BOS_Bipolar>();
        }
	}
	
	// Update is called once per frame
	void Update () {

        Camera Curr = Camera.current;
        float MaxLeft;
        float MaxRight;

        if (Act_currHP <= 0)
        {
            Curr.GetComponent<CameraFollower>().Cam_CurrTarget = target;
            GetComponent<Rigidbody2D>().gravityScale = 1;
            Bip_LeftArm.GetComponent<ENY_Arms>().ChangeHP(-Bip_LeftArm.GetComponent<ENY_Arms>().Act_baseHP);
            Bip_RightArm.GetComponent<ENY_Arms>().ChangeHP(-Bip_RightArm.GetComponent<ENY_Arms>().Act_baseHP);
        }


        if (Curr != null)
        {
            CheckThresholds();
            Bip_PatternTimer -= Time.deltaTime;
            if (Bip_PatternTimer <= 0)
                Bip_PatternId = 0;
            if (Bip_LeftArm == null)
                Bip_LeftArmDead = true;
            if (Bip_RightArm == null)
                Bip_RightArmDead = true;

            if (!Bip_LeftArmDead)
            {
                float Dis = Vector3.Distance(Bip_RandLeftSmashLoc, Bip_LeftArm.transform.position);
                if (Dis <= 0.5f)
                    Bip_LeftInTarget = true;
            }
            if (!Bip_RightArmDead)
            {
                float Dis = Vector3.Distance(Bip_RandRightSmashLoc, Bip_RightArm.transform.position);
                if (Dis <= 0.5f)
                    Bip_RightInTarget = true;
            }


            if (Bip_PatternId == 0)       //Act_currHP >= (Act_baseHP * 0.75f)
            {
                if (Bip_LeftInTarget && !Bip_LeftArmDead)
                {
                    if (!Bip_RightArmDead)
                    {
                        MaxLeft = transform.position.x - (Curr.orthographicSize * Curr.aspect);
                        Bip_RandLeftSmashLoc = new Vector3(Random.Range(transform.position.x, MaxLeft), Random.Range(-2.45f, 2f), 0);
                        Bip_LeftInTarget = false;
                    }
                    else
                        Bip_RandLeftSmashLoc = target.transform.position;
                    Bip_LeftArm.GetComponent<ENY_Arms>().Arm_MoveLoc = Bip_RandLeftSmashLoc;
                }
                if (Bip_RightInTarget && !Bip_RightArmDead)
                {
                    if (!Bip_LeftArmDead)
                    {
                        MaxRight = transform.position.x + (Curr.orthographicSize * Curr.aspect);
                        Bip_RandRightSmashLoc = new Vector3(Random.Range(transform.position.x, MaxRight), Random.Range(-2.45f, 2f), 0);
                        Bip_RightInTarget = false;
                    }
                    else
                        Bip_RandRightSmashLoc = target.transform.position;
                    Bip_RightArm.GetComponent<ENY_Arms>().Arm_MoveLoc = Bip_RandRightSmashLoc;
                }  
            }
            else if (Bip_PatternId == 1)
            {
                if (Bip_LeftInTarget && !Bip_LeftArmDead)
                {
                    MaxLeft = transform.position.x - (Curr.orthographicSize * Curr.aspect);
                    if (!Bip_RightArmDead)
                        Bip_RandLeftSmashLoc = new Vector3(MaxLeft + 2, Random.Range(0.5f, 2.5f), 0);
                    else
                        Bip_RandLeftSmashLoc = new Vector3(MaxLeft + 2, Random.Range(-2.45f, 2.5f), 0);
                    Bip_RandLeftSmashLoc = new Vector3(MaxLeft + 2, Random.Range(0.5f, 2.5f), 0);
                    Bip_LeftArm.GetComponent<ENY_Arms>().Arm_MoveLoc = Bip_RandLeftSmashLoc;
                    Bip_LeftInTarget = false;
                }
                if (Bip_RightInTarget && !Bip_RightArmDead)
                {
                    MaxRight = transform.position.x + (Curr.orthographicSize * Curr.aspect);
                    if (!Bip_LeftArmDead)
                        Bip_RandRightSmashLoc = new Vector3(MaxRight - 2, Random.Range(-2.45f, -0.5f), 0);
                    else
                        Bip_RandRightSmashLoc = new Vector3(MaxRight - 2, Random.Range(-2.45f, 2.5f), 0);
                    Bip_RightArm.GetComponent<ENY_Arms>().Arm_MoveLoc = Bip_RandRightSmashLoc;
                    Bip_RightInTarget = false;
                }  


            }
            else if (Bip_PatternId == 2)
            {
                if (Bip_LeftInTarget && !Bip_LeftArmDead)
                {
                    MaxLeft = transform.position.x - ((Curr.orthographicSize * Curr.aspect) / 2);
                    Bip_RandLeftSmashLoc = new Vector3(MaxLeft, -0.725f, 0);
                    Bip_LeftArm.GetComponent<ENY_Arms>().Arm_MoveLoc = Bip_RandLeftSmashLoc;
                    Bip_LeftInTarget = false;
                }
                if (Bip_RightInTarget && !Bip_RightArmDead)
                {
                    MaxRight = transform.position.x + ((Curr.orthographicSize * Curr.aspect) / 2);
                    Bip_RandRightSmashLoc = new Vector3(MaxRight, -0.725f, 0);
                    Bip_RightArm.GetComponent<ENY_Arms>().Arm_MoveLoc = Bip_RandRightSmashLoc;
                    Bip_RightInTarget = false;
                }  
            }
        }


	}

    public override void CheckThresholds()
    {
        if (Act_currHP <= Bip_NewHpTresh)
        {
            Bip_HpTreshReducer += 0.2f;
            Bip_NewHpTresh = Act_baseHP - (int)(Act_baseHP * Bip_HpTreshReducer);
            int ChangeAttacks;

            if (Act_currHP <= (int)(Act_baseHP * 0.4f))
            {
                Bip_NewHpTresh = 0;
                ChangeAttacks = Random.Range(1, 3);
                Bip_PatternId = ChangeAttacks;
                if (ChangeAttacks == 1)
                    Bip_PatternTimer = 6f;
                else
                    Bip_PatternTimer = 3f;
                Debug.Log("Less Then 40%");
            }
            else if (Act_currHP >= (int)(Act_baseHP * 0.4f) && Act_currHP <= (int)(Act_baseHP * 0.80f))
            {
                Bip_PatternId = 1;
                Bip_PatternTimer = 6f;
                Debug.Log("Less Then 80%");
            }

        }
    }
}

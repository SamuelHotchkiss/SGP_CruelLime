using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    public bool keyboard = true;

    public ACT_CHAR_Base[] party;
    public int currChar = 0;

    public GameObject warrior_GUI, ranger_GUI, mage_GUI;

    Vector3 currentChar_GUI, rightChar_GUI, leftChar_GUI;

    public int coins;

    public float[] maxTmr;
    public float curTmr;
    bool loop;
    public ACT_CHAR_Base.STATES nextState;

    //Testing projectile firing, will be removed later, projectile should be part of the character classes instead
    public PROJ_Base testProjectile;

    // L: just works better this way
    public float horz;
    public float vert;
    bool notjoydash;

    // Use this for initialization
    void Start()
    {
        party = new ACT_CHAR_Base[3];

        party[0] = new CHAR_Swordsman();
        party[1] = new CHAR_Archer();
        party[2] = new CHAR_Wizard();

        currentChar_GUI = new Vector3(150.0f, -100.0f, 0.0f);
        rightChar_GUI = new Vector3(250.0f, -50.0f, 0.0f);
        leftChar_GUI = new Vector3(50.0f, -50.0f, 0.0f);

        //-----Labels4dayz---- IDLE, WALK, DODGE, ATT1, ATT2, ATT3, SPEC, HURT, DED,  USE
        maxTmr = new float[] { 2.0f, 0.75f, 0.1f, 0.3f, 0.2f, 0.5f, 1.0f, 0.1f, 1.0f, 1.0f };

        curTmr = maxTmr[(int)party[currChar].state];
        loop = true;
        //current error value.  will not change states if set to this.
        nextState = ACT_CHAR_Base.STATES.IDLE;

        // Initialize other components
        GameObject.Find("GUI_Manager").GetComponent<UI_HUD>().Initialize();
        GetComponent<MNGR_Animation_Player>().Initialize();

        horz = 0.0f;
        vert = 0.0f;
        notjoydash = false;
    }

    void Update()
    {

        if (curTmr > 0)
        {
            curTmr -= Time.deltaTime;
            if (curTmr < 0)
            {
                //EndOfAnim(); // Engage things to do when the animation loops/ ends.
                curTmr = loop ? maxTmr[(int)party[currChar].state] : 0; // reset to maxTmr if looping, otherwise set to 0 and stop updating timer.
                if (curTmr == 0)
                {
                    if (party[currChar].state != ACT_CHAR_Base.STATES.DYING)
                    {
                        if (nextState == ACT_CHAR_Base.STATES.IDLE)
                        {
                            party[currChar].state = ACT_CHAR_Base.STATES.IDLE;
                            horz = 0.0f;
                            vert = 0.0f;
                        }
                        else
                        {
                            party[currChar].state = nextState;
                            nextState = ACT_CHAR_Base.STATES.IDLE;
                        }

                        curTmr = maxTmr[(int)party[currChar].state];
                        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    }
                    else
                    {
                        currChar--;
                        if (currChar < 0)
                            currChar = 2;
                        for (int i = 0; i < 2; i++)
                        {
                            if (party[currChar].Act_currHP > 0)
                                break;
                            else
                                currChar--;
                            if (currChar < 0)
                                currChar = 2;
                        }
                        if (party[currChar].Act_currHP <= 0)
                            Application.LoadLevel(Application.loadedLevel);
                    }

                }
            }
        }

        /*if (party[currChar].Act_currHP <= 0)
        {
            currChar--;
                if (currChar < 0)
                    currChar = 2;
                for (int i = 0; i < 2; i++)
                {
                    if (party[currChar].Act_currHP > 0)
                        break;
                    else
                        currChar--;
                        if (currChar < 0)
                            currChar = 2;
                }
        }
        if (party[currChar].Act_currHP <= 0)
            Application.LoadLevel(Application.loadedLevel);*/

        if (party[currChar].state != ACT_CHAR_Base.STATES.DYING)
        {
            if (party[currChar].state != ACT_CHAR_Base.STATES.DASHING
                && party[currChar].state != ACT_CHAR_Base.STATES.ATTACK_1
                && party[currChar].state != ACT_CHAR_Base.STATES.ATTACK_2
                && party[currChar].state != ACT_CHAR_Base.STATES.ATTACK_3
                && party[currChar].state != ACT_CHAR_Base.STATES.SPECIAL
                && party[currChar].state != ACT_CHAR_Base.STATES.USE)
            {
                // Get axis movement
                if (Input.GetAxis("Horizontal") != 0)
                    horz = Input.GetAxis("Horizontal");
                if (Input.GetAxis("Vertical") != 0)
                    vert = Input.GetAxis("Vertical");

                // take the greater between keyboard and gamepad axes
                if (Mathf.Abs(horz) < Mathf.Abs(Input.GetAxis("Pad_Horizontal")))
                    horz = Input.GetAxis("Pad_Horizontal");
                if (Mathf.Abs(vert) < Mathf.Abs(Input.GetAxis("Pad_Vertical")))
                    vert = Input.GetAxis("Pad_Vertical");

                // less vertical movement because we're 2.5d
                vert *= 0.5f;

                // random bugfix
                if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0
                    && Input.GetAxis("Pad_Horizontal") == 0 && Input.GetAxis("Pad_Vertical") == 0
                    && party[currChar].state == ACT_CHAR_Base.STATES.WALKING)
                {
                    horz = 0.0f;
                    vert = 0.0f;
                }

                // manual deadzones
                if (Mathf.Abs(horz) < 0.1f )
                    horz = 0.0f;
                if (Mathf.Abs(vert) < 0.1f)
                    vert = 0.0f;

                if (horz == 0.0f && vert == 0.0f
                    && party[currChar].state == ACT_CHAR_Base.STATES.WALKING)
                    party[currChar].state = ACT_CHAR_Base.STATES.IDLE;
            }

            if (horz > 0 && (party[currChar].state == ACT_CHAR_Base.STATES.WALKING || party[currChar].state == ACT_CHAR_Base.STATES.IDLE))
            {
                party[currChar].Act_facingRight = true;
                if (party[currChar].state == ACT_CHAR_Base.STATES.IDLE)
                {
                    party[currChar].state = ACT_CHAR_Base.STATES.WALKING;
                    curTmr = maxTmr[(int)party[currChar].state];
                }
                loop = true;
            }
            else if (horz < 0 && (party[currChar].state == ACT_CHAR_Base.STATES.WALKING || party[currChar].state == ACT_CHAR_Base.STATES.IDLE))
            {
                party[currChar].Act_facingRight = false;
                if (party[currChar].state == ACT_CHAR_Base.STATES.IDLE)
                {
                    party[currChar].state = ACT_CHAR_Base.STATES.WALKING;
                    curTmr = maxTmr[(int)party[currChar].state];
                }
                loop = true;
                //GetComponent<Rigidbody2D>().velocity = new Vector2(horz, vert);
            }
            else if (vert != 0 && (party[currChar].state == ACT_CHAR_Base.STATES.WALKING || party[currChar].state == ACT_CHAR_Base.STATES.IDLE))
            {
                if (party[currChar].state == ACT_CHAR_Base.STATES.IDLE)
                {
                    party[currChar].state = ACT_CHAR_Base.STATES.WALKING;
                    curTmr = maxTmr[(int)party[currChar].state];
                }
                loop = true;
                //GetComponent<Rigidbody2D>().velocity = new Vector2(horz, vert);
            }
            /*else if (party[currChar].state == ACT_CHAR_Base.STATES.WALKING)
            {
                party[currChar].state = ACT_CHAR_Base.STATES.IDLE;
                //GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                horz = 0.0f;
                vert = 0.0f;
                curTmr = maxTmr[(int)party[currChar].state];
            }
            else if (party[currChar].state != ACT_CHAR_Base.STATES.DASHING)
            {
                horz = 0.0f;
                vert = 0.0f;
            }*/


            if ((Input.GetButtonDown("Attack/Confirm") || Input.GetButtonDown("Pad_Attack/Confirm"))
                && party[currChar].state != ACT_CHAR_Base.STATES.USE)
            {
                // Testing projectile firing
                //Instantiate(testProjectile, transform.position, new Quaternion(0, 0, 0, 0));

                if (party[currChar].state != ACT_CHAR_Base.STATES.ATTACK_1
                    && party[currChar].state != ACT_CHAR_Base.STATES.ATTACK_2
                    && party[currChar].state != ACT_CHAR_Base.STATES.ATTACK_3)
                {
                    party[currChar].state = ACT_CHAR_Base.STATES.ATTACK_1;
                    curTmr = maxTmr[(int)party[currChar].state];
                    //GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    horz = 0.0f;
                    vert = 0.0f;
                }
                else if (party[currChar].state == ACT_CHAR_Base.STATES.ATTACK_1)
                {
                    nextState = ACT_CHAR_Base.STATES.ATTACK_2;
                }
                else if (party[currChar].state == ACT_CHAR_Base.STATES.ATTACK_2)
                {
                    nextState = ACT_CHAR_Base.STATES.ATTACK_3;
                }
                loop = false;
            }
            else if ((Input.GetButton("Special/Cancel") || Input.GetButtonDown("Pad_Special/Cancel"))
                && party[currChar].cooldownTmr == 0
                && party[currChar].state != ACT_CHAR_Base.STATES.USE)
            {
                party[currChar].state = ACT_CHAR_Base.STATES.SPECIAL;
                curTmr = maxTmr[(int)party[currChar].state];
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                horz = 0.0f;
                vert = 0.0f;
                loop = false;

                party[currChar].cooldownTmr = party[currChar].cooldownTmrBase;
            }
            else if ((Input.GetButtonDown("SwitchRight") || Input.GetButtonDown("Pad_SwitchRight"))
                && party[currChar].state != ACT_CHAR_Base.STATES.USE)
            {
                int loopz = 0;
                while (true)
                {
                    currChar++;
                    if (currChar > 2)
                        currChar = 0;
                    if (party[currChar].Act_currHP > 0)
                        break;
                    else if (loopz < 5)
                        loopz++;
                    else
                        break;
                }

                /*for (int i = 0; i < 2; i++)
                {
                    if (party[currChar].Act_currHP > 0)
                        break;
                    else if (curTmr <= 0)
                        currChar++;
                    if (currChar > 2)
                        currChar = 0;

                }*/
            }
            else if ((Input.GetButtonDown("SwitchLeft") || Input.GetButtonDown("Pad_SwitchLeft"))
                && party[currChar].state != ACT_CHAR_Base.STATES.USE)
            {
                int loopz = 0;
                while (true)
                {
                    currChar--;
                    if (currChar < 0)
                        currChar = 2;
                    if (party[currChar].Act_currHP > 0)
                        break;
                    else if (loopz < 5)
                        loopz++;
                    else
                        break;
                }
                /*for (int i = 0; i < 2; i++)
                {
                    if (party[currChar].Act_currHP > 0)
                        break;
                    else if (curTmr <= 0)
                        currChar--;
                    if (currChar < 0)
                        currChar = 2;
                }*/
            }
            // currently does nothing
            else if ((Input.GetButton("Use") || Input.GetButton("Pad_Use"))
                && party[currChar].state != ACT_CHAR_Base.STATES.USE)
            {
                party[currChar].state = ACT_CHAR_Base.STATES.USE;
                curTmr = maxTmr[(int)party[currChar].state];
                loop = false;
            }
            // 
            else if ((Input.GetButtonDown("Dodge") && (Mathf.Abs(horz) != 0 || Mathf.Abs(vert) != 0)
                || party[currChar].state == ACT_CHAR_Base.STATES.DASHING)
                && party[currChar].state != ACT_CHAR_Base.STATES.USE)
            {
                if (party[currChar].state != ACT_CHAR_Base.STATES.DASHING)
                {
                    party[currChar].state = ACT_CHAR_Base.STATES.DASHING;
                    curTmr = maxTmr[(int)party[currChar].state];
                    nextState = ACT_CHAR_Base.STATES.IDLE;
                    loop = false;
                }
                float dashmax = 15.0f;
                if (Mathf.Abs(horz) < dashmax)
                    horz *= dashmax;
                if (horz > dashmax)
                    horz = dashmax;
                else if (horz < -dashmax)
                    horz = -dashmax;

                // less vertical movement
                dashmax *= 0.75f;
                if (Mathf.Abs(vert) < dashmax)
                    vert *= dashmax;
                if (vert > dashmax)
                    vert = dashmax;
                else if (vert < -dashmax)
                    vert = -dashmax;
            }
            else if ((Input.GetAxis("Pad_DodgeHorizontal") != 0 || Input.GetAxis("Pad_DodgeVertical") != 0
                || party[currChar].state == ACT_CHAR_Base.STATES.DASHING) && !notjoydash
                && party[currChar].state != ACT_CHAR_Base.STATES.USE)
            {
                notjoydash = true;
                if (party[currChar].state != ACT_CHAR_Base.STATES.DASHING)
                {
                    party[currChar].state = ACT_CHAR_Base.STATES.DASHING;
                    curTmr = maxTmr[(int)party[currChar].state];
                    nextState = ACT_CHAR_Base.STATES.IDLE;
                    loop = false;
                }
                float dashmax = 15.0f;
                float joyHorz = Input.GetAxis("Pad_DodgeHorizontal");
                float joyVert = Input.GetAxis("Pad_DodgeVertical");
                if (joyHorz > 0)
                    party[currChar].Act_facingRight = true;
                else
                    party[currChar].Act_facingRight = false;

                if (Mathf.Abs(horz) < dashmax)
                    horz = dashmax * joyHorz;

                // less vertical movement
                dashmax *= 0.75f;
                vert = dashmax * joyVert;
            }
            else if (Input.GetAxis("Pad_DodgeHorizontal") == 0 && Input.GetAxis("Pad_DodgeVertical") == 0)
            {
                notjoydash = false;
            }
            /* deprecated
             else if (curTmr <= 0)
            {
                party[currChar].state = ACT_CHAR_Base.STATES.IDLE;
                curTmr = maxTmr[(int)party[currChar].state];
                loop = true;
            }*/
            // modify velocity only after we set everything else up
            if (party[currChar].state != ACT_CHAR_Base.STATES.SPECIAL)
                GetComponent<Rigidbody2D>().velocity = new Vector2(horz, vert);
        }
        if (Input.GetKey(KeyCode.K))
        {
            party[currChar].Act_currHP -= 1;
            if (party[currChar].Act_currHP <= 0)
            {
                party[currChar].Act_currHP = 0;
                party[currChar].state = ACT_CHAR_Base.STATES.DYING;
            }
            else
            {
                party[currChar].state = ACT_CHAR_Base.STATES.HURT;
            }
            curTmr = maxTmr[(int)party[currChar].state];
            loop = false;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            MNGR_Game.wallet += 10;
            MNGR_Save.OverwriteCurrentSave();
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("Saving Game");
            MNGR_Save.OverwriteCurrentSave();
            MNGR_Save.SaveProfiles();
        }


        switch (currChar)
        {
            case 0:
                warrior_GUI.transform.GetComponent<RectTransform>().anchoredPosition = currentChar_GUI;
                ranger_GUI.transform.GetComponent<RectTransform>().anchoredPosition = rightChar_GUI;
                mage_GUI.transform.GetComponent<RectTransform>().anchoredPosition = leftChar_GUI;

                warrior_GUI.transform.localScale = new Vector3(0.5f, 0.5f, 1.0f);
                ranger_GUI.transform.localScale = new Vector3(0.3f, 0.3f, 1.0f);
                mage_GUI.transform.localScale = new Vector3(0.3f, 0.3f, 1.0f);
                break;
            case 1:
                warrior_GUI.transform.GetComponent<RectTransform>().anchoredPosition = leftChar_GUI;
                ranger_GUI.transform.GetComponent<RectTransform>().anchoredPosition = currentChar_GUI;
                mage_GUI.transform.GetComponent<RectTransform>().anchoredPosition = rightChar_GUI;

                warrior_GUI.transform.localScale = new Vector3(0.3f, 0.3f, 1.0f);
                ranger_GUI.transform.localScale = new Vector3(0.5f, 0.5f, 1.0f);
                mage_GUI.transform.localScale = new Vector3(0.3f, 0.3f, 1.0f);
                break;
            case 2:
                warrior_GUI.transform.GetComponent<RectTransform>().anchoredPosition = rightChar_GUI;
                ranger_GUI.transform.GetComponent<RectTransform>().anchoredPosition = leftChar_GUI;
                mage_GUI.transform.GetComponent<RectTransform>().anchoredPosition = currentChar_GUI;

                warrior_GUI.transform.localScale = new Vector3(0.3f, 0.3f, 1.0f);
                ranger_GUI.transform.localScale = new Vector3(0.3f, 0.3f, 1.0f);
                mage_GUI.transform.localScale = new Vector3(0.5f, 0.5f, 1.0f);
                break;
        }
    }
}
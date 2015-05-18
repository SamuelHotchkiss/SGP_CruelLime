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

        // IDLE, WALK, DODGE, ATT1, ATT2, ATT3, SPEC, HURT, DED,  USE
        maxTmr = new float[] { 2.0f, 0.75f, 0.5f, 0.3f, 0.2f, 0.5f, 1.0f, 0.1f, 1.0f, 1.0f };

        curTmr = maxTmr[(int)party[currChar].state];
        loop = true;
        //current error value.  will not change states if set to this.
        nextState = ACT_CHAR_Base.STATES.IDLE;

        // Initialize other components
        GameObject.Find("GUI_Manager").GetComponent<UI_HUD>().Initialize();
        GetComponent<MNGR_Animation_Player>().Initialize();
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

        // Are we using the keyboard?
        if (keyboard)
        {
            if (party[currChar].state != ACT_CHAR_Base.STATES.DYING)
            {
                // Get axis movement
                float horz = Input.GetAxis("Horizontal");
                float vert = Input.GetAxis("Vertical");

                if (horz > 0 && (party[currChar].state == ACT_CHAR_Base.STATES.WALKING || party[currChar].state == ACT_CHAR_Base.STATES.IDLE))
                {
                    party[currChar].Act_facingRight = true;
                    if (party[currChar].state == ACT_CHAR_Base.STATES.IDLE)
                    {
                        party[currChar].state = ACT_CHAR_Base.STATES.WALKING;
                        curTmr = maxTmr[(int)party[currChar].state];
                    }
                    loop = true;
                    GetComponent<Rigidbody2D>().velocity = new Vector2(horz, vert);
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
                    GetComponent<Rigidbody2D>().velocity = new Vector2(horz, vert);
                }
				else if (vert != 0 && (party[currChar].state == ACT_CHAR_Base.STATES.WALKING || party[currChar].state == ACT_CHAR_Base.STATES.IDLE))
				{
					if (party[currChar].state == ACT_CHAR_Base.STATES.IDLE)
					{
						party[currChar].state = ACT_CHAR_Base.STATES.WALKING;
						curTmr = maxTmr[(int)party[currChar].state];
					}
					loop = true;
					GetComponent<Rigidbody2D>().velocity = new Vector2(horz, vert);
				}
                else if (party[currChar].state == ACT_CHAR_Base.STATES.WALKING)
                {
                    party[currChar].state = ACT_CHAR_Base.STATES.IDLE;
                    GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    curTmr = maxTmr[(int)party[currChar].state];
                }


                if (Input.GetButtonDown("Attack/Confirm"))
                {
                    if (party[currChar].state != ACT_CHAR_Base.STATES.ATTACK_1
                        && party[currChar].state != ACT_CHAR_Base.STATES.ATTACK_2
                        && party[currChar].state != ACT_CHAR_Base.STATES.ATTACK_3)
                    {
                        party[currChar].state = ACT_CHAR_Base.STATES.ATTACK_1;
                        curTmr = maxTmr[(int)party[currChar].state];
                        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
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
                else if (Input.GetButton("Special/Cancel") && party[currChar].cooldownTmr == 0)
                {
                    party[currChar].state = ACT_CHAR_Base.STATES.SPECIAL;
                    curTmr = maxTmr[(int)party[currChar].state];
                    GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    loop = false;

                    party[currChar].cooldownTmr = party[currChar].cooldownTmrBase;
                }
                else if (Input.GetButtonDown("SwitchRight"))
                {
                    currChar++;
                    if (currChar > 2)
                        currChar = 0;
                    for (int i = 0; i < 2; i++)
                    {
                        if (party[currChar].Act_currHP > 0)
                            break;
                        else if (curTmr <= 0)
                            currChar++;
                        if (currChar > 2)
                            currChar = 0;

                    }
                }
                else if (Input.GetButtonDown("SwitchLeft"))
                {
                    currChar--;
                    if (currChar < 0)
                        currChar = 2;
                    for (int i = 0; i < 2; i++)
                    {
                        if (party[currChar].Act_currHP > 0)
                            break;
                        else if (curTmr <= 0)
                            currChar--;
                        if (currChar < 0)
                            currChar = 2;
                    }
                }

                // Use button rotates the object
                else if (Input.GetButton("Use"))
                {
                    party[currChar].state = ACT_CHAR_Base.STATES.USE;
                    curTmr = maxTmr[(int)party[currChar].state];
                    loop = false;
                }
                // Dodge button rotates the object based upon current movement
                else if (Input.GetButton("Dodge"))
                {
                    party[currChar].state = ACT_CHAR_Base.STATES.DASHING;
                    curTmr = maxTmr[(int)party[currChar].state];
                    loop = false;
                }
                else if (curTmr <= 0)
                {
                    party[currChar].state = ACT_CHAR_Base.STATES.IDLE;
                    curTmr = maxTmr[(int)party[currChar].state];
                    loop = true;
                }
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

        }
        // Testing for gamepad input
        // NOT TESTED YET PLZ FIX ERF PRBLMS OKAI BAI.
        else
        {
            // get axis movement
            float horz = Input.GetAxis("Pad_Horizontal");
            float vert = Input.GetAxis("Pad_Vertical");

            // move the object
            GetComponent<Rigidbody2D>().velocity = new Vector2(horz, vert);

            // Confirm button sets scale to 2x
            if (Input.GetButton("Pad_Attack/Confirm"))
            {
                transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
            }
            // Cancel button sets scale to 1/2x
            else if (Input.GetButton("Pad_Special/Cancel"))
            {
                transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }
            // Switch Right button increases scale over time
            else if (Input.GetButton("Pad_SwitchRight"))
            {
                Vector3 scale = transform.localScale;
                scale *= 1.1f;
                transform.localScale = scale;
            }
            // Switch Left button decreases scale over time
            else if (Input.GetButton("Pad_SwitchLeft"))
            {
                Vector3 scale = transform.localScale;
                scale *= 0.9f;
                transform.localScale = scale;
            }
            // Reset scale if these buttons are not pressed
            else
            {
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }

            float rotx = transform.localEulerAngles.x;
            float roty = transform.localEulerAngles.y;
            // Use button rotates the object
            if (Input.GetButton("Pad_Use"))
            {
                rotx++;
                roty++;
            }
            // Dodge button rotates the object based upon current movement
            rotx = Input.GetAxis("Pad_DodgeVertical");
            roty = Input.GetAxis("Pad_DodgeHorizontal");

            // reset stuff when it goes bad
            if (roty >= 360)
                roty -= 360;
            else if (roty < 0)
                roty += 360;
            // gimbal locking grrr
            if (rotx > 90)
                rotx -= 90;
            else if (rotx < 0)
                rotx += 85;

            transform.localEulerAngles = new Vector3(rotx, roty, 0);

            // Pause button makes the object invisible
            if (Input.GetButton("Pad_Pause"))
            {
                if (GetComponent<MeshRenderer>() != null)
                    GetComponent<MeshRenderer>().enabled = false;
                else if (GetComponent<SpriteRenderer>() != null)
                    GetComponent<SpriteRenderer>().enabled = false;

                transform.position = new Vector3(0.0f, 0.0f, 0.0f);
                transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
            else
            {
                if (GetComponent<MeshRenderer>() != null)
                    GetComponent<MeshRenderer>().enabled = true;
                else if (GetComponent<SpriteRenderer>() != null)
                    GetComponent<SpriteRenderer>().enabled = true;
            }

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
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    // S: for use with buffs and debuffs ////////////////////////////////
    public MNGR_Item.BuffStates buffState = MNGR_Item.BuffStates.NEUTRAL;

    public List<MOD_Base> myBuffs = new List<MOD_Base>();
    public void KillBuffs()
    {
        for (int i = 0; i < myBuffs.Count; i++)
        {
            myBuffs[i].EndModifyActor();
        }
        myBuffs.Clear();
    }
    /////////////////////////////////////////////////////////////////////

    //public bool keyboard = true;

    public ACT_CHAR_Base[] party;
    public int currChar = 0;

    public GameObject warrior_GUI, ranger_GUI, mage_GUI;
    public Object[] Projs;

    Vector3 currentChar_GUI, rightChar_GUI, leftChar_GUI;

    public int coins;

    public float[] maxTmr;
    public float curTmr;
    public bool loop;
    public ACT_CHAR_Base.STATES nextState;

    // L: just works better this way
    public float horz;
    public float vert;
    bool notjoydash;

    // J: Need this for Knockback
    public bool Pc_HasKnockBack;

    // S: check if we're alive
    public bool isAlive;

    // Use this for initialization
    void Start()
    {
        isAlive = true;

        party = new ACT_CHAR_Base[3];

        //party[0] = new CHAR_Swordsman();
        //party[1] = new CHAR_Archer();
        //party[2] = new CHAR_Wizard();

        party = MNGR_Game.currentParty;

        currentChar_GUI = new Vector3(150.0f, -100.0f, 0.0f);
        rightChar_GUI = new Vector3(250.0f, -50.0f, 0.0f);
        leftChar_GUI = new Vector3(50.0f, -50.0f, 0.0f);

        Projs = new GameObject[3];
        Projs[0] = Resources.Load(party[currChar].ProjFilePaths[0]);
        Projs[1] = Resources.Load(party[currChar].ProjFilePaths[1]);
        Projs[2] = Resources.Load(party[currChar].ProjFilePaths[2]);

        // Slick as doody
        //maxTmr = party[currChar].StateTmrs;

        // Ok, not as slick as doody.  more like a wet floor.
        maxTmr = new float[party[currChar].StateTmrs.Length];
        for (int i = 0; i < party[currChar].StateTmrs.Length; i++)
        {
            maxTmr[i] = party[currChar].StateTmrs[i];
        }

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

        // for testing
        //MNGR_Game.dangerZone = true;

        if (MNGR_Game.dangerZone)
            GameObject.Find("_Horde").SetActive(true);
        else
            GameObject.Find("_Horde").SetActive(false);
    }

    void Update()
    {
        // S: Should prevent this from running if player is dead
        if (!isAlive)
            return;

        switch (party[currChar].state)
        {
            case ACT_CHAR_Base.STATES.IDLE:
                loop = true;
                break;
            case ACT_CHAR_Base.STATES.WALKING:
                loop = true;
                break;
            case ACT_CHAR_Base.STATES.DASHING:
                loop = false;
                break;
            case ACT_CHAR_Base.STATES.ATTACK_1:
                loop = false;
                break;
            case ACT_CHAR_Base.STATES.ATTACK_2:
                loop = false;
                break;
            case ACT_CHAR_Base.STATES.ATTACK_3:
                loop = false;
                break;
            case ACT_CHAR_Base.STATES.SPECIAL:
                loop = false;
                break;
            case ACT_CHAR_Base.STATES.HURT:
                loop = false;
                break;
            case ACT_CHAR_Base.STATES.DYING:
                loop = false;
                break;
            case ACT_CHAR_Base.STATES.USE:
                loop = false;
                break;
        }

        if (party[currChar].state == ACT_CHAR_Base.STATES.DYING)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            loop = false;
        }
        if (myBuffs.Count == 0)
            buffState = MNGR_Item.BuffStates.NEUTRAL;

        // Update the timer
        if (curTmr > 0)
        {
            curTmr -= Time.deltaTime;
            if (curTmr < 0)
            {
                // reset to maxTmr if looping, otherwise set to 0 and stop updating timer.
                curTmr = loop ? maxTmr[(int)party[currChar].state] : 0;
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

                        // modify the attack timers if we are now in the attacking state
                        if (party[currChar].state == ACT_CHAR_Base.STATES.ATTACK_1
                            || party[currChar].state == ACT_CHAR_Base.STATES.ATTACK_2
                            || party[currChar].state == ACT_CHAR_Base.STATES.ATTACK_3)
                        {
                            maxTmr[(int)party[currChar].state] = GetAtkSpeed();
                        }

                        curTmr = maxTmr[(int)party[currChar].state];

                        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    }
                    else
                    {
                        SwitchNextPartyMember(true);
                        if (party[currChar].Act_currHP <= 0)
                            Death();
                    }
                }
            }
        }

        if (party[currChar].Act_currHP <= 0)
        {
            party[currChar].Act_currHP = 0;
            party[currChar].state = ACT_CHAR_Base.STATES.DYING;
            //curTmr = maxTmr[(int)party[currChar].state];
            loop = false;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        if (party[currChar].state != ACT_CHAR_Base.STATES.DYING && party[currChar].state != ACT_CHAR_Base.STATES.HURT)
        {
            if (party[currChar].state == ACT_CHAR_Base.STATES.IDLE
                || party[currChar].state == ACT_CHAR_Base.STATES.WALKING)
            {
                // Get axis movement
                if (Input.GetAxis("Horizontal") != 0)
                    horz = Input.GetAxis("Horizontal");
                if (Input.GetAxis("Vertical") != 0)
                    vert = Input.GetAxis("Vertical");

                // add gamepad axis movement
                if (Input.GetAxis("Pad_Horizontal") != 0)
                {
                    horz = Input.GetAxis("Pad_Horizontal");
                }
                if (Input.GetAxis("Pad_Vertical") != 0)
                    vert = Input.GetAxis("Pad_Vertical");

                // but cap it off at 1
                if (horz > 1.0f)
                    horz = 1.0f;
                else if (horz < -1.0f)
                    horz = -1.0f;

                if (vert > 1.0f)
                    vert = 1.0f;
                else if (vert < -1.0f)
                    vert = -1.0f;

                // less vertical movement because we're 2.5d
                vert *= 0.5f;

                // dashing shouldn't be affected by speed
                if (party[currChar].state != ACT_CHAR_Base.STATES.DASHING)
                {
                    horz *= party[currChar].Act_currSpeed * 0.25f;
                    vert *= party[currChar].Act_currSpeed * 0.25f;
                }
                // random bugfix
                if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0
                    && Input.GetAxis("Pad_Horizontal") == 0 && Input.GetAxis("Pad_Vertical") == 0
                    && party[currChar].state == ACT_CHAR_Base.STATES.WALKING)
                {
                    horz = 0.0f;
                    vert = 0.0f;
                }

                // manual deadzones
                if (Mathf.Abs(horz) < 0.1f)
                    horz = 0.0f;
                if (Mathf.Abs(vert) < 0.1f)
                    vert = 0.0f;

                if (horz == 0.0f && vert == 0.0f
                    && party[currChar].state == ACT_CHAR_Base.STATES.WALKING)
                    party[currChar].state = ACT_CHAR_Base.STATES.IDLE;
            }

            // begin walking
            if ((horz != 0 || vert != 0) && (party[currChar].state == ACT_CHAR_Base.STATES.WALKING || party[currChar].state == ACT_CHAR_Base.STATES.IDLE))
            {
                if (horz > 0)
                    party[currChar].Act_facingRight = true;
                else if (horz < 0)
                    party[currChar].Act_facingRight = false;

                if (party[currChar].state == ACT_CHAR_Base.STATES.IDLE)
                {
                    party[currChar].state = ACT_CHAR_Base.STATES.WALKING;
                    curTmr = maxTmr[(int)party[currChar].state];
                }
                loop = true;
            }

            if ((Input.GetButtonDown("Attack/Confirm") || Input.GetButtonDown("Pad_Attack/Confirm"))
                && party[currChar].state != ACT_CHAR_Base.STATES.USE
                && party[currChar].state != ACT_CHAR_Base.STATES.SPECIAL)
            {

                // if we are not attacking go into ATTACK_1
                if (party[currChar].state != ACT_CHAR_Base.STATES.ATTACK_1
                    && party[currChar].state != ACT_CHAR_Base.STATES.ATTACK_2
                    && party[currChar].state != ACT_CHAR_Base.STATES.ATTACK_3)
                {
                    party[currChar].state = ACT_CHAR_Base.STATES.ATTACK_1;
                    maxTmr[(int)party[currChar].state] = GetAtkSpeed();
                    curTmr = maxTmr[(int)party[currChar].state];
                    horz = 0.0f;
                    vert = 0.0f;
                }
                // if we are in ATTACK_1 go into ATTACK_2
                else if (party[currChar].state == ACT_CHAR_Base.STATES.ATTACK_1)
                {
                    nextState = ACT_CHAR_Base.STATES.ATTACK_2;
                }
                // if we are in ATTACK_2 go into ATTACK_3
                else if (party[currChar].state == ACT_CHAR_Base.STATES.ATTACK_2)
                {
                    nextState = ACT_CHAR_Base.STATES.ATTACK_3;
                }
                loop = false;
            }
            // we can currently do our special when in mid-combo.  Is that good?
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
                SwitchNextPartyMember(true);
            }
            else if ((Input.GetButtonDown("SwitchLeft") || Input.GetButtonDown("Pad_SwitchLeft"))
                && party[currChar].state != ACT_CHAR_Base.STATES.USE)
            {
                SwitchNextPartyMember(false);
            }
            // currently does nothing
            else if ((Input.GetButton("Use") || Input.GetButton("Pad_Use"))
                && party[currChar].state != ACT_CHAR_Base.STATES.USE)
            {
                if (!MNGR_Game.usedItem)
                {
                    MNGR_Game.usedItem = true;
                    MNGR_Item.AttachModifier(MNGR_Game.equippedItem, gameObject);
                }

                party[currChar].state = ACT_CHAR_Base.STATES.USE;
                curTmr = maxTmr[(int)party[currChar].state];
                loop = false;
            }
            // can only dodge at certain times.
            else if ((Input.GetButtonDown("Dodge") && (Mathf.Abs(horz) != 0 || Mathf.Abs(vert) != 0)
                || party[currChar].state == ACT_CHAR_Base.STATES.DASHING)
                && (party[currChar].state == ACT_CHAR_Base.STATES.IDLE
                || party[currChar].state == ACT_CHAR_Base.STATES.WALKING
                || party[currChar].state == ACT_CHAR_Base.STATES.DASHING))
            {
                // special stuff when first initializing the dodge
                if (party[currChar].state != ACT_CHAR_Base.STATES.DASHING)
                {
                    party[currChar].state = ACT_CHAR_Base.STATES.DASHING;
                    curTmr = maxTmr[(int)party[currChar].state];
                    nextState = ACT_CHAR_Base.STATES.IDLE;
                    loop = false;
                }
                // always do this stuff, though
                float dashmax = 25.0f;
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
                && (party[currChar].state == ACT_CHAR_Base.STATES.IDLE
                || party[currChar].state == ACT_CHAR_Base.STATES.WALKING
                || party[currChar].state == ACT_CHAR_Base.STATES.DASHING))
            {
                // buffered input #PutsOnShadesYEEEAAAAAHH
                notjoydash = true;
                // special stuff when first initializing the dodge
                if (party[currChar].state != ACT_CHAR_Base.STATES.DASHING)
                {
                    party[currChar].state = ACT_CHAR_Base.STATES.DASHING;
                    curTmr = maxTmr[(int)party[currChar].state];
                    nextState = ACT_CHAR_Base.STATES.IDLE;
                    loop = false;
                }
                // always do this stuff, though
                float dashmax = 25.0f;
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

        }

        // modify velocity only if we aren't in special state (for custom special movement)
        if (party[currChar].state != ACT_CHAR_Base.STATES.SPECIAL)
        {
            // always calls unless current character is dead.
            GetComponent<Rigidbody2D>().velocity = new Vector2(horz, vert);
        }

        if (party[currChar].state == ACT_CHAR_Base.STATES.HURT)
        {
            if (Pc_HasKnockBack)
            {
                vert = 0.0f;
                if (horz < 0.0f)    //moving Left
                {
                    horz += Time.deltaTime * 50.0f;
                    if (horz >= 0.0f)
                    {
                        horz = 0.0f;
                        Pc_HasKnockBack = false;
                    }
                }
                else if (horz > 0.0f) //moving right
                {
                    horz -= Time.deltaTime * 50.0f;
                    if (horz <= 0.0f)
                    {
                        horz = 0.0f;
                        Pc_HasKnockBack = false;
                    }
                }
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
        else if (Input.GetKeyDown(KeyCode.P))
        {
            //Le Debug Le Knockback
            ApplyKnockBack(20);
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            MNGR_Game.isNight = !MNGR_Game.isNight;
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

    void SwitchNextPartyMember(bool _forward)
    {
        int loopz = 0;
        int next = 1;
        if (!_forward)
            next *= -1;
        while (true)
        {
            currChar += next;
            if (currChar < 0)
                currChar = 2;
            if (currChar > 2)
                currChar = 0;
            if (party[currChar].Act_currHP > 0)
                break;
            else if (loopz < 5)
                loopz++;
            else
                break;
        }

        maxTmr = new float[party[currChar].StateTmrs.Length];
        for (int i = 0; i < party[currChar].StateTmrs.Length; i++)
        {
            maxTmr[i] = party[currChar].StateTmrs[i];
        }
        party[currChar].state = ACT_CHAR_Base.STATES.IDLE;
        curTmr = party[currChar].StateTmrs[(int)party[currChar].state];

        Projs[0] = Resources.Load(party[currChar].ProjFilePaths[0]);
        Projs[1] = Resources.Load(party[currChar].ProjFilePaths[1]);
        Projs[2] = Resources.Load(party[currChar].ProjFilePaths[2]);
    }

    // J: This is for use with Enemy Knockback but can be use for anything 
    // The Force will affect how far and how long the player will be knockback 
    public void ApplyKnockBack(float _Force)
    {
        if (party[currChar].state != ACT_CHAR_Base.STATES.HURT && party[currChar].state != ACT_CHAR_Base.STATES.DYING)
        {

            horz = _Force;
            if (_Force < 0.0f)          //Since _Force is use for the Timer, it needs to alway be positive.
                _Force = -_Force;

            curTmr = _Force / 50.0f;    //This is compensating for Force been larger than 1
            //If this number is change, another most be change in the if check for when the character is in the Hurt State.
            loop = false;
            Pc_HasKnockBack = true;

            party[currChar].state = ACT_CHAR_Base.STATES.HURT;
            nextState = ACT_CHAR_Base.STATES.IDLE;
            vert = 0.0f;
        }
    }

    // L: called in animation manager when we're displaying the right sprite to attack at
    // _index allows us to choose which projectile.
    public bool SpawnProj(int _index = 0)
    {
        /*int num = 1;

        if (party[currChar].characterIndex == 6 && party[currChar].state == ACT_CHAR_Base.STATES.ATTACK_3)
            num = 3;

        for (int i = 0; i < num; i++)
        {*/
        Object test = Projs[_index];
        GameObject clone = (GameObject)Instantiate(Projs[_index], transform.position, new Quaternion(0, 0, 0, 0));
        clone.GetComponent<PROJ_Base>().owner = gameObject;
        clone.GetComponent<PROJ_Base>().Initialize();

        // this makes me puke a little inside.
        if (party[currChar].characterIndex == 6 && _index == 1)
        {
            clone.GetComponent<PROJ_Explosive>().sprites = Resources.LoadAll<Sprite>("Sprites/Projectile/FireballStrong");
        }
        /*Vector3 rot = clone.transform.localEulerAngles;
        Vector2 vel = clone.GetComponent<PROJ_Base>().velocity;
        if (i == 1)
        {
            rot.z = 25.0f;
            vel = new Vector2(vel.x, 0.278f);
        }
        else if (i == 2)
        {
            rot.z = -25.0f;
            vel = new Vector2(vel.x, -0.278f);
        }
        clone.transform.localEulerAngles = rot;
        clone.GetComponent<PROJ_Base>().velocity = vel;

   }
}*/

        // disables a one-way boolean in the animation manager.  goofy, I know.  Harmless, hopefully.
        return false;
    }

    float GetAtkSpeed()
    {
        float limiter = 1.0f;      // return to this value when testing how high stats can go and all that.
        float ratio = party[currChar].Act_currAspeed * party[currChar].Act_currSpeed;

        if (limiter - ratio < 0.2f)
            ratio = 0.2f;

        return party[currChar].StateTmrs[(int)party[currChar].state] * (limiter - ratio);
    }

    // S: til this do us part
    public void Death()
    {
        isAlive = false;

        MNGR_Game.hordePosition++;
        MNGR_Game.isNight = !MNGR_Game.isNight;

        Application.LoadLevel("WorldMap");
    }

    public void MurderEveryone()
    {
        isAlive = false;

        currChar = 0;
        for (int i = 0; i < 3; i++)
        {
            party[currChar].Act_currHP = 0;
            currChar++;
        }

        MNGR_Save.saveFiles[MNGR_Save.currSave] = new MNGR_GameData(); // clears savedata
        MNGR_Save.SaveProfiles();

        Application.LoadLevel("MainMenu");
    }
}
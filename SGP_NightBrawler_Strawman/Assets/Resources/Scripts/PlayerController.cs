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
    ACT_CHAR_Base.STATES currentState;
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

    // aka The Situation.
    void Update()
    {
        // S: Should prevent this from running if player is dead
        if (!isAlive)
            return;
        if (currentState != party[currChar].state)
            ChangeState(party[currChar].state);

        // Sexy.
        UpdateTimers(currentState);

        // Buff stuff.
        if (myBuffs.Count == 0)
            buffState = MNGR_Item.BuffStates.NEUTRAL;

        // Initialize death state.
        if (party[currChar].Act_currHP <= 0 && currentState != ACT_CHAR_Base.STATES.DYING)
        {
            party[currChar].Act_currHP = 0;
            ChangeState(ACT_CHAR_Base.STATES.DYING);
        }

        // The meat of The Situation.
        switch (currentState)
        {
            case ACT_CHAR_Base.STATES.IDLE:
            // should inherit behavior from walking?
            //loop = true;
            //break;
            case ACT_CHAR_Base.STATES.WALKING:
                // Get axis movement
                if (Input.GetAxis("Horizontal") != 0)
                    horz = Input.GetAxis("Horizontal");
                if (Input.GetAxis("Vertical") != 0)
                    vert = Input.GetAxis("Vertical");

                // add gamepad axis movement
                if (Input.GetAxis("Pad_Horizontal") != 0)
                    horz = Input.GetAxis("Pad_Horizontal");
                if (Input.GetAxis("Pad_Vertical") != 0)
                    vert = Input.GetAxis("Pad_Vertical");

                // but cap it off at 1
                if (horz > 1.0f)        horz = 1.0f;
                else if (horz < -1.0f)  horz = -1.0f;

                if (vert > 1.0f)        vert = 1.0f;
                else if (vert < -1.0f)  vert = -1.0f;

                // less vertical movement because we're 2.5d
                vert *= 0.85f;

                horz *= party[currChar].Act_currSpeed * 0.25f;
                vert *= party[currChar].Act_currSpeed * 0.25f;

                // random bugfix
                if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0
                    && Input.GetAxis("Pad_Horizontal") == 0 && Input.GetAxis("Pad_Vertical") == 0
                    /*&& party[currChar].state == ACT_CHAR_Base.STATES.WALKING*/)
                {
                    horz = 0.0f;
                    vert = 0.0f;
                }

                // manual deadzones
                if (Mathf.Abs(horz) < 0.1f) horz = 0.0f;
                if (Mathf.Abs(vert) < 0.1f) vert = 0.0f;

                if (horz == 0.0f && vert == 0.0f
                    /*&& party[currChar].state == ACT_CHAR_Base.STATES.WALKING*/)
                    ChangeState(ACT_CHAR_Base.STATES.IDLE);

                // we have movement, time to make movement happen.
                if (horz != 0 || vert != 0)
                {
                    if (horz > 0)       party[currChar].Act_facingRight = true;
                    else if (horz < 0)  party[currChar].Act_facingRight = false;

                    if (party[currChar].state == ACT_CHAR_Base.STATES.IDLE)
                    {
                        ChangeState(ACT_CHAR_Base.STATES.WALKING);
                    }
                }

                if (Input.GetButtonDown("Attack/Confirm") || Input.GetButtonDown("Pad_Attack/Confirm"))
                {
                        ChangeState(ACT_CHAR_Base.STATES.ATTACK_1);
                        horz = 0.0f;
                        vert = 0.0f;
                }
                CheckSpecialInput(currentState);
                CheckSwitchInput(currentState);
                CheckUseInput(currentState);
                CheckDodgeInput(currentState);
                break;
            case ACT_CHAR_Base.STATES.DASHING:
                break;
            case ACT_CHAR_Base.STATES.ATTACK_1:
                if (Input.GetButtonDown("Attack/Confirm") || Input.GetButtonDown("Pad_Attack/Confirm"))
                {
                    ChangeState(ACT_CHAR_Base.STATES.ATTACK_2, false);
                    horz = 0.0f;
                    vert = 0.0f;
                }
                break;
            case ACT_CHAR_Base.STATES.ATTACK_2:
                if (Input.GetButtonDown("Attack/Confirm") || Input.GetButtonDown("Pad_Attack/Confirm"))
                {
                    ChangeState(ACT_CHAR_Base.STATES.ATTACK_3, false);
                    horz = 0.0f;
                    vert = 0.0f;
                }
                break;
            case ACT_CHAR_Base.STATES.ATTACK_3:
                break;
            case ACT_CHAR_Base.STATES.SPECIAL:
                break;
            case ACT_CHAR_Base.STATES.HURT:
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
                else
                {
                    horz = 0.0f;
                    vert = 0.0f;
                }
                break;
            case ACT_CHAR_Base.STATES.DYING:
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                break;
            case ACT_CHAR_Base.STATES.USE:
                break;
        }

        if (Input.GetKey(KeyCode.K))
        {
            party[currChar].Act_currHP -= 1;
            if (party[currChar].Act_currHP <= 0)
            {
                party[currChar].Act_currHP = 0;
                ChangeState(ACT_CHAR_Base.STATES.DYING);
            }
            else
            {
                ChangeState(ACT_CHAR_Base.STATES.HURT);
            }
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
        // modify velocity only if we aren't in special state (for custom special movement)
        if (party[currChar].state != ACT_CHAR_Base.STATES.SPECIAL)
        {
            // always calls unless current character is dead.
            GetComponent<Rigidbody2D>().velocity = new Vector2(horz, vert);
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

    // Just put it in a function
    void UpdateTimers(ACT_CHAR_Base.STATES _cur)
    {
        // Update the state timer
        if (curTmr > 0)
        {
            curTmr -= Time.deltaTime;
            if (curTmr < 0)
            {
                // L: reset to maxTmr if looping, otherwise set to 0 and stop updating timer.
                // L: looking back at this, I know it is saving space but cannot read it for the life of me.
                curTmr = loop ? maxTmr[(int)_cur] : 0;

                // aka if we're not looping
                if (curTmr == 0)
                {
                    if (_cur != ACT_CHAR_Base.STATES.DYING)
                    {
                        ChangeState(nextState);
                        nextState = ACT_CHAR_Base.STATES.IDLE;

                    }
                    else
                    {
                        SwitchNextPartyMember(true);
                        if (party[currChar].Act_currHP <= 0)
                            Death();
                    }
                }
                else if (_cur == ACT_CHAR_Base.STATES.HURT)
                    ChangeState(nextState);
            }
        }


    }

    void ChangeState(ACT_CHAR_Base.STATES _next, bool _immediately = true)
    {
        ACT_CHAR_Base.STATES old = party[currChar].state;

        // triggered on state change goes here.
        if (_next == ACT_CHAR_Base.STATES.IDLE
            || _next == ACT_CHAR_Base.STATES.WALKING)
            loop = true;
        else
            loop = false;


        // end triggered on state change FX

        if (_immediately)
        {
            party[currChar].state = _next;
            currentState = _next;
            // modify the attack timers if we are now in the attacking state
            if (_next == ACT_CHAR_Base.STATES.ATTACK_1
                || _next == ACT_CHAR_Base.STATES.ATTACK_2
                || _next == ACT_CHAR_Base.STATES.ATTACK_3)
            {
                maxTmr[(int)_next] = GetAtkSpeed();
            }
            // either way set the timer to the current state's max
            if (_next != old)
                curTmr = maxTmr[(int)_next];
        }
        else
            nextState = _next;

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

            ChangeState(ACT_CHAR_Base.STATES.HURT);
            nextState = ACT_CHAR_Base.STATES.IDLE;
            vert = 0.0f;
        }
    }

    // L: called in animation manager when we're displaying the right sprite to attack at
    // _index allows us to choose which projectile.
    public bool SpawnProj(bool _right = true, int _index = 0)
    {
        /*int num = 1;

        if (party[currChar].characterIndex == 6 && party[currChar].state == ACT_CHAR_Base.STATES.ATTACK_3)
            num = 3;

        for (int i = 0; i < num; i++)
        {*/
        Object test = Projs[_index];
        GameObject clone = (GameObject)Instantiate(Projs[_index], transform.position, new Quaternion(0, 0, 0, 0));
        clone.GetComponent<PROJ_Base>().owner = gameObject;
        clone.GetComponent<PROJ_Base>().Initialize(_right);

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

        for (int i = 0; i < party.Length; i++ )
        {
            party[i].Act_currHP = party[i].Act_baseHP;
        }

        MNGR_Save.saveFiles[MNGR_Save.currSave].CopyGameManager();
        MNGR_Save.SaveProfiles();

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

        MNGR_Save.DeleteCurrentSave(MNGR_Save.currSave);

        Application.LoadLevel("GameOverLose");
    }

    // L: Input functions reduce copy and pasting clutter!
    void CheckSpecialInput(ACT_CHAR_Base.STATES _cur)
    {
        if ((Input.GetButton("Special/Cancel") || Input.GetButtonDown("Pad_Special/Cancel"))
            && party[currChar].cooldownTmr == 0)
        {
            ChangeState(ACT_CHAR_Base.STATES.SPECIAL);
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            party[currChar].cooldownTmr = party[currChar].cooldownTmrBase;
        }
    }
    void CheckSwitchInput(ACT_CHAR_Base.STATES _cur)
    {
        if ((Input.GetButtonDown("SwitchRight") || Input.GetButtonDown("Pad_SwitchRight"))
        && _cur != ACT_CHAR_Base.STATES.USE)
        {
            SwitchNextPartyMember(true);
        }
        else if ((Input.GetButtonDown("SwitchLeft") || Input.GetButtonDown("Pad_SwitchLeft"))
            && _cur != ACT_CHAR_Base.STATES.USE)
        {
            SwitchNextPartyMember(false);
        }
    }
    void CheckUseInput(ACT_CHAR_Base.STATES _cur)
    {
        // currently does nothing
        if ((Input.GetButton("Use") || Input.GetButton("Pad_Use"))
            && party[currChar].state != ACT_CHAR_Base.STATES.USE)
        {
            if (!MNGR_Game.usedItem)
            {
                MNGR_Game.usedItem = true;
                MNGR_Item.AttachModifier(MNGR_Game.equippedItem, gameObject);
            }

            ChangeState(ACT_CHAR_Base.STATES.USE);
        }
    }
    void CheckDodgeInput(ACT_CHAR_Base.STATES _cur)
{
        if (Input.GetButtonDown("Dodge") && (Mathf.Abs(horz) != 0 || Mathf.Abs(vert) != 0))
        {
            // special stuff when first initializing the dodge
            if (_cur != ACT_CHAR_Base.STATES.DASHING)
            {
                ChangeState(ACT_CHAR_Base.STATES.DASHING);
                nextState = ACT_CHAR_Base.STATES.IDLE;
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
               || party[currChar].state == ACT_CHAR_Base.STATES.DASHING) && !notjoydash)
        {
            // buffered input #PutsOnShadesYEEEAAAAAHH
            notjoydash = true;
            // special stuff when first initializing the dodge
            if (_cur != ACT_CHAR_Base.STATES.DASHING)
            {
                ChangeState(ACT_CHAR_Base.STATES.DASHING);
                nextState = ACT_CHAR_Base.STATES.IDLE;
            }
            // always do this stuff, though
            float dashmax = 25.0f;
            float joyHorz = Input.GetAxis("Pad_DodgeHorizontal");
            float joyVert = Input.GetAxis("Pad_DodgeVertical");
            float thres = 0.025f;
            if (joyHorz > thres)
            {
                party[currChar].Act_facingRight = true;
                joyHorz = 1.0f;
            }
            else if (joyHorz < -thres)
            {
                party[currChar].Act_facingRight = false;
                joyHorz = -1.0f;
            }
            if (joyVert > thres)
                joyVert = 1.0f;
            else if (joyVert < -thres)
                joyVert = -1.0f;

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




}
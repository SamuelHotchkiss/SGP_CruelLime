using UnityEngine;
using System.Collections;

// L: This class imitates the PlayerController to allow compatibility with the animation manager.
public class MENU_CharacterGraphic : PlayerController
{

    //public ACT_CHAR_Base[] chosenParty;
    //public GameObject Button;
    //public GameObject Nametag;
    //public bool selected;
    float orgX;
    float orgY;
    //int orgChar;
    public bool DanceBitch;
    public bool DieBitch;

    protected override void Start()
    {
        //orgChar = currChar;
        //currChar /= 3;

        base.Start();

        //currChar = orgChar;
        //chosenParty = party;
        //party = new ACT_CHAR_Base[9];
        //party = MNGR_Game.theCharacters;

        for (int i = 0; i < 3; i++)
        {
            party[i].Start();
            party[i].Act_facingRight = true;
            if (DanceBitch)
            {
                party[i].state = ACT_CHAR_Base.STATES.DANCE;
            }
        }

        if (DanceBitch && party[currChar].Act_ActID == 1) // fix Lancer's dancing offset.
        {
            Vector3 newpos = transform.position;
            newpos.x -= 150;
            transform.position = newpos;
        }
        InitializeTimers();

        /*if (GetComponent<RectTransform>() != null)
        {
            orgY = GetComponent<RectTransform>().anchoredPosition.y;
            orgX = GetComponent<RectTransform>().anchoredPosition.x;
        }*/

        // temp fix for testing gameover screen.
        

        if (DieBitch)
        {
            party[currChar].Act_currHP = 30.0f;
            //party[currChar].state = ACT_CHAR_Base.STATES.HURT;
        }
        /*
        isAlive = false;
        for (int i = 0; i < 9; i++)
        {
            MNGR_Game.theCharacters[i].Act_currHP = 0;
        }
        for (int i = 0; i < 3; i++)
        {
            party[i].state = ACT_CHAR_Base.STATES.HURT;
            party[i].Act_currHP = 0;
        }
        nextState = ACT_CHAR_Base.STATES.DYING;*/
    }

    protected override void Update()
    {

        if (DieBitch)
        {
            party[currChar].invulTmr = 0.0f;
            party[currChar].ChangeHP(-1, false);
            loop = false;
        }

        if (currentState != party[currChar].state)
            ChangeState(party[currChar].state);

        // Sexy.
        UpdateTimers(currentState);


    }
    
    // L: Overridden to remove dying funcitonality because we aren't an actual playercontroller.
    protected override void UpdateTimers(ACT_CHAR_Base.STATES _cur)
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
                    /*else
                    {
                        SwitchNextPartyMember(true);
                        if (party[currChar].Act_currHP <= 0)
                            Death();
                    }*/
                }
                else if (_cur == ACT_CHAR_Base.STATES.HURT)
                    ChangeState(nextState);
            }
        }


    }

    public override void ChangeState(ACT_CHAR_Base.STATES _next, bool _immediately = true)
    {
        currentState = _next;
        curTmr = maxTmr[(int)_next];
    }
}

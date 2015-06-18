using UnityEngine;
using System.Collections;

// L: This class imitates the PlayerController to allow compatibility with the animation manager.
public class MENU_ButtonGraphic : PlayerController
{

    public ACT_CHAR_Base[] chosenParty;
    public GameObject Button;
    public GameObject Nametag;
    public bool selected;
    float orgX;
    float orgY;
    int orgChar;
    protected override void Start()
    {
        orgChar = currChar;
        currChar /= 3;
        
        base.Start();

        currChar = orgChar;
        chosenParty = party;
        party = new ACT_CHAR_Base[9];
        party = MNGR_Game.theCharacters;

        for (int i = 0; i < 9; i++)
        {
            party[i].Start();
        }
        InitializeTimers();

        if (GetComponent<RectTransform>() != null)
        {
            orgY = GetComponent<RectTransform>().anchoredPosition.y;
            orgX = GetComponent<RectTransform>().anchoredPosition.x;
        }
    }

    protected override void Update()
    {

        if (currentState != party[currChar].state)
            ChangeState(party[currChar].state);

        // Sexy.
        UpdateTimers(currentState);


        // Update position of image and button
        if (GetComponent<RectTransform>() != null)
        {
            // modify our position based upon selection.
            Vector3 newpos = GetComponent<RectTransform>().anchoredPosition;
            if (selected)
            {
                newpos.y = orgY - 100; //64
                if (party[currChar].characterIndex < 3)
                {
                    newpos.x = -270;
                    if (party[currChar].characterIndex == 1)
                    {
                        newpos.x = -295;
                        Vector3 tagpos = Nametag.transform.position;
                        //tagpos.x = 205;
                        //Nametag.transform.position = tagpos;
                    }
                }
                else if (party[currChar].characterIndex >= 3 && party[currChar].characterIndex < 6)
                {
                     newpos.x = 25;
                }
                else if (party[currChar].characterIndex >= 6)
                {
                     newpos.x = 310;
                }


                //if (party[currChar].characterIndex == 1)
                //{
                //    newpos.x = orgX - 50;
                //    Vector3 tagpos = Nametag.transform.position;
                //    tagpos.x = 150;
                //    Nametag.transform.position = tagpos;
                //}
                Nametag.SetActive(true);
            }
            else
            {

                newpos.y = orgY;
                newpos.x = orgX;
                if (party[currChar].characterIndex != 1)
                {
                    //newpos.x = orgX;
                    Vector3 tagpos = Nametag.transform.position;
                    //tagpos.x = orgX;
                    Nametag.transform.position = tagpos;
                }
                //else
                //{
                  //  newpos.x = orgX;

                //}

                Nametag.SetActive(false);
            }
            GetComponent<RectTransform>().anchoredPosition = newpos;

            if (Button.GetComponent<RectTransform>() != null)
            {
                // modify our button's position based on the above.
                newpos = Button.GetComponent<RectTransform>().anchoredPosition;
                newpos.y = GetComponent<RectTransform>().anchoredPosition.y - 35;
                Button.GetComponent<RectTransform>().anchoredPosition = newpos;
            }
        }
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

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MENU_CharacterSelection : MonoBehaviour
{

    //public bool selected;
    public int charType;                // ugly name, but describes which group of characters (warriors, rangers, mages) this button is concerned with.
    public GameObject[] ButtonGraphics; // another ugly name, but contains the image objects that sit on top of the buttons.
    public GameObject ContinueButton;
    public GameObject dahArrow;
    public string NextLevel;

    public int Index;
    public int OldIndex;
    int IndexMax = 9;
    bool InputBuffer;

    // Use this for initialization
    void Start()
    {
        Index = 4; // Ninja
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 9; i++)
        {
            MENU_ButtonGraphic btn = ButtonGraphics[i].GetComponent<MENU_ButtonGraphic>();
            if (btn.chosenParty[i / 3].characterIndex == btn.party[i].characterIndex)
            {
                btn.selected = true;
                if (btn.party[i].state != ACT_CHAR_Base.STATES.DYING)
                    btn.party[i].state = ACT_CHAR_Base.STATES.DANCE;
            }
            else
            {
                btn.selected = false;
                if (btn.party[i].state != ACT_CHAR_Base.STATES.DYING)
                    btn.party[i].state = ACT_CHAR_Base.STATES.IDLE;
            }
            
                if (btn.party[i].state == ACT_CHAR_Base.STATES.DYING)
                {
                    btn.loop = false;
                    btn.curTmr = 0.0f;
                }
                btn.party[i].Act_facingRight = true;
        }

        // Input stuff
        CheckInput();

        // Cursor Stuff
        if (dahArrow.GetComponent<RectTransform>() != null)
        {
            Vector3 newpos = dahArrow.GetComponent<RectTransform>().position;

            if (Index < IndexMax)
            {
                newpos = ButtonGraphics[Index].GetComponent<RectTransform>().position;
                newpos.y += 12;
                if (Index == 1 && ButtonGraphics[Index].GetComponent<MENU_ButtonGraphic>().selected)
                    newpos.x += 32;
                else
                    newpos.x -= 8;
                dahArrow.GetComponent<RectTransform>().position = newpos;
            }
            else
            {
                newpos = ContinueButton.GetComponent<RectTransform>().position;
                newpos.y += 32;
                newpos.x += 32;
                dahArrow.GetComponent<RectTransform>().position = newpos;

            }

        }

    }

    void CheckInput()
    {
        float horz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        float deadzone = 0.3f;

        if (Mathf.Abs(horz) < deadzone) horz = 0;
        if (Mathf.Abs(vert) < deadzone) vert = 0;



        if ((horz != 0 || vert != 0) && !InputBuffer)
        {
            if (!dahArrow.activeSelf)
                dahArrow.SetActive(true);
            if (horz > 0)
            {
                if (Index == IndexMax)
                    Index = OldIndex;
                else
                {
                    Index++;
                    if (Index > IndexMax - 1)
                        Index = 0;
                }
            }
            else if (horz < 0)
            {
                if (Index == IndexMax)
                    Index = OldIndex;
                else
                {
                    Index--;
                    if (Index < 0)
                        Index = IndexMax - 1;
                }
            }

            if (vert != 0)
            {
                if (Index == IndexMax)
                    Index = OldIndex;
                else
                {
                    OldIndex = Index;
                    Index = IndexMax;
                }
            }
            InputBuffer = true;
        }
        else if (horz == 0 && vert == 0)
            InputBuffer = false;

        if (Input.GetButtonDown("Submit"))
        {
            if (Index < IndexMax)
                BtnPress(Index);//, false);
            else
                Exit();
        }

        if (Input.GetButtonDown("Cancel"))
            Exit();
    }

    public void BtnPress(int _btnIndex)//, bool _IsMouse = true) // this breaks mouse clicking somehow.
    {

        //dahArrow.SetActive(!_IsMouse);

        MENU_ButtonGraphic btn = ButtonGraphics[_btnIndex].GetComponent<MENU_ButtonGraphic>();
        Index = _btnIndex;

        if (!btn.selected)
        {
            charType = (_btnIndex / 3);

            MNGR_Game.currentParty[charType] = MNGR_Game.theCharacters[_btnIndex];
            /*for (int i = charType; i < charType + 3; i++) // And this trooper's name was Threes.
            {
                MENU_ButtonGraphic btn = ButtonGraphics[i].GetComponent<MENU_ButtonGraphic>();
                if (btn.chosenParty[charType].characterIndex == btn.party[i].characterIndex)
                    btn.selected = true;
                else
                    btn.selected = false;
            }*/
        }
    }

    public void Exit()
    {
        if (MNGR_Game.NextLevel == "" || MNGR_Game.NextLevel == null)
            MNGR_Game.NextLevel = "WorldMap";

        Application.LoadLevel(MNGR_Game.NextLevel);
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class MENU_Inn : MonoBehaviour 
{
    public int Inn_RestCost;
    public int Inn_ReviveCost;
    public int Inn_ButtonPress;

    public GameObject Inn_CharacterPanel;
    public Text Inn_CurrCoin;

    public Text Inn_RestButtonText;

    public Button[] Inn_ReviveButtons;
    public Text[] Inn_ReviveText;

    private List<int> Inn_DeadCharacterIndex;

    public 
	// Use this for initialization
	void Start () {
        Inn_CharacterPanel.SetActive(false);
        Inn_DeadCharacterIndex = new List<int>();
	}
	
	// Update is called once per frame
	void Update () {
        Inn_CurrCoin.text = "X" + MNGR_Game.wallet.ToString();
        Inn_RestCost = 5 * MNGR_Game.playerPosition;

        Inn_RestButtonText.text = "Rest For the Night\nCost:" + Inn_RestCost.ToString();

        for (int i = 0; i < Inn_ReviveButtons.Length; i++)
        {
            if (Inn_ReviveButtons[i].IsActive())
            {
                Text ButtonTex = Inn_ReviveText[i];
                ButtonTex.text = "Revive " + MNGR_Game.currentParty[Inn_DeadCharacterIndex[i]].name + "\nCost: " + (MNGR_Game.currentParty[Inn_DeadCharacterIndex[i]].CalcAverageLvl() * 12 * MNGR_Game.playerPosition).ToString();
                Inn_ReviveText[i].text = ButtonTex.text;
            }
        }
	}

    public void RestTheParty()
    {
        if (MNGR_Game.wallet >= Inn_RestCost)
        {
            for (int i = 0; i < MNGR_Game.currentParty.Length; i++)
            {
                if (MNGR_Game.currentParty[i].Act_currHP > 0)
                {
                    Debug.Log(MNGR_Game.currentParty[i].Act_currHP.ToString());
                    MNGR_Game.currentParty[i].RestoreToBaseHP();
                    Debug.Log(MNGR_Game.currentParty[i].Act_currHP.ToString());
                }
                else
                    Debug.Log("This Brother is Dead");
            }

            MNGR_Game.wallet -= Inn_RestCost;
            MNGR_Game.isNight = !MNGR_Game.isNight;
            MNGR_Game.UpdateHoard();
            Debug.Log("GOOD REST");
        }
        else
            Debug.Log("NO MONEY MO PROBLEMS!");
    }

    public void CheckDeadPlayers()
    {
        int DeadChar = 0;
        for (int i = 0; i < MNGR_Game.currentParty.Length; i++)
        {
            if (MNGR_Game.currentParty[i].Act_currHP <= 0)
            {
                Inn_ReviveButtons[DeadChar].gameObject.SetActive(true);
                Inn_ReviveButtons[DeadChar].interactable = true;
                Inn_ReviveButtons[DeadChar].onClick.AddListener(delegate { RevivePartyMember(); });
                Inn_DeadCharacterIndex.Add(i);
                DeadChar++;
            }
        }
    }

    public void SelectIndex(int _i)
    {
       Inn_ButtonPress = Inn_DeadCharacterIndex[_i];
    }

    public void RevivePartyMember()
    {
        int _ChrIndex = Inn_ButtonPress;
        Inn_ReviveCost = MNGR_Game.currentParty[_ChrIndex].CalcAverageLvl() * 12 * MNGR_Game.playerPosition; //Current villige location may come into this later.

        if (MNGR_Game.wallet >= Inn_ReviveCost)
        {
            Debug.Log(MNGR_Game.currentParty[_ChrIndex].Act_currHP.ToString());
            MNGR_Game.currentParty[_ChrIndex].RestoreToBaseHP();
            MNGR_Game.wallet -= Inn_ReviveCost;
            Debug.Log("IT'S ALIVE!");
            Debug.Log(MNGR_Game.currentParty[_ChrIndex].Act_currHP.ToString());

            for (int i = 0; i < Inn_ReviveButtons.Length; i++)
                Inn_ReviveButtons[i].gameObject.SetActive(false);

            Inn_DeadCharacterIndex.Clear();

            CheckDeadPlayers();

            MNGR_Game.isNight = !MNGR_Game.isNight;
            MNGR_Game.UpdateHoard();
        }
        else
            Debug.Log("NO MONEY MO PROBLEMS!");
    }

}

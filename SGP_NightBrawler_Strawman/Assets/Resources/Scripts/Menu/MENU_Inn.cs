using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MENU_Inn : MonoBehaviour {

    public int Inn_RestCost;
    public int Inn_ReviveCost;
    public GameObject Inn_CharacterPanel;
    public Text Inn_CurrCoin;

    public 
	// Use this for initialization
	void Start () {
        Inn_CharacterPanel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        Inn_CurrCoin.text = "X" + MNGR_Game.wallet.ToString();
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

    public void RevivePartyMember(int _ChrIndex)
    {
        Inn_ReviveCost = MNGR_Game.currentParty[_ChrIndex].CalcAverageLvl() * 12; //Current villige location may come into this later.

        if (MNGR_Game.wallet >= Inn_ReviveCost)
        {
            Debug.Log(MNGR_Game.currentParty[_ChrIndex].Act_currHP.ToString());
            MNGR_Game.currentParty[_ChrIndex].RestoreToBaseHP();
            MNGR_Game.wallet -= Inn_ReviveCost;
            Debug.Log("IT'S ALIVE!");
            Debug.Log(MNGR_Game.currentParty[_ChrIndex].Act_currHP.ToString());

            MNGR_Game.isNight = !MNGR_Game.isNight;
            MNGR_Game.UpdateHoard();
        }
        else
            Debug.Log("NO MONEY MO PROBLEMS!");
    }

}

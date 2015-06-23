using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MENU_Merchant : MonoBehaviour {

    int Mer_RestCost;
    int Mer_ReviveCost;
    int Mer_ButtonPress;
    int Mer_PotionLimit;

    public Text Mer_PotionPrice;
    public Text Mer_PotionCount;
    public Text Mer_CurrCoin;
    public Text Mer_RestButtonText;
    public Text[] Mer_ReviveText;
    public Button[] Mer_ReviveButtons;
    public List<int> Mer_DeadCharacterIndex;
	// Use this for initialization
	void Start () {
        Mer_PotionLimit = 3;
	}
	
	// Update is called once per frame
	void Update () 
    {
        Mer_CurrCoin.text = "X" + MNGR_Game.wallet.ToString();
        Mer_RestCost = 15 * MNGR_Game.playerPosition;

        Mer_RestButtonText.text = "Rest For the Night\nCost:" + Mer_RestCost.ToString();
        Mer_PotionPrice.text = (MNGR_Item.PotionCost(3) * 3).ToString();
        Mer_PotionCount.text = "X " + Mer_PotionLimit.ToString();

        for (int i = 0; i < Mer_ReviveButtons.Length; i++)
        {
            if (Mer_ReviveButtons[i].IsActive())
                Mer_ReviveText[i].text = "Revive " + MNGR_Game.currentParty[Mer_DeadCharacterIndex[i]].name + "\nCost: " 
                    + (MNGR_Game.currentParty[Mer_DeadCharacterIndex[i]].CalcAverageLvl() * 24 * MNGR_Game.playerPosition).ToString();
        }
	}

    public void Buy(int IDs)
    {
        if (MNGR_Game.theInventory.containers[0].count < 5)
        {
            if (MNGR_Game.wallet >= (MNGR_Item.PotionCost(IDs) * 2) && Mer_PotionLimit > 0)
            {
                MNGR_Game.wallet -= (MNGR_Item.PotionCost(IDs) * 2);
                MNGR_Game.theInventory.containers[0].count++;
                Mer_PotionLimit--;
            }
        }
        else
            Debug.Log("You can't carry any more of that item!");
    }

    public void RestTheParty()
    {
        Mer_RestCost = 10 * MNGR_Game.playerPosition;

        if (MNGR_Game.wallet >= Mer_RestCost)
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

            MNGR_Game.wallet -= Mer_RestCost;
            MNGR_Game.isNight = !MNGR_Game.isNight;
          //  MNGR_Game.UpdateHoard();
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
                Mer_ReviveButtons[DeadChar].gameObject.SetActive(true);
                Mer_ReviveButtons[DeadChar].interactable = true;
                Mer_ReviveButtons[DeadChar].onClick.AddListener(delegate { RevivePartyMember(); });
                Mer_DeadCharacterIndex.Add(i);
                DeadChar++;
            }
        }
    }

    public void SelectIndex(int _i)
    {
        Mer_ButtonPress = Mer_DeadCharacterIndex[_i];
    }

    public void RevivePartyMember()
    {
        int _ChrIndex = Mer_ButtonPress;
        Mer_ReviveCost = MNGR_Game.currentParty[_ChrIndex].CalcAverageLvl() * 24 * MNGR_Game.playerPosition; //Current villige location may come into this later.

        if (MNGR_Game.wallet >= Mer_ReviveCost)
        {
            Debug.Log(MNGR_Game.currentParty[_ChrIndex].Act_currHP.ToString());
            MNGR_Game.currentParty[_ChrIndex].RestoreToBaseHP();
            MNGR_Game.wallet -= Mer_ReviveCost;
            Debug.Log("IT'S ALIVE!");
            Debug.Log(MNGR_Game.currentParty[_ChrIndex].Act_currHP.ToString());

            for (int i = 0; i < Mer_ReviveButtons.Length; i++)
                Mer_ReviveButtons[i].gameObject.SetActive(false);

            Mer_DeadCharacterIndex.Clear();

            CheckDeadPlayers();

            MNGR_Game.isNight = !MNGR_Game.isNight;
          //  MNGR_Game.UpdateHoard();
        }
        else
            Debug.Log("NO MONEY MO PROBLEMS!");
    }

}

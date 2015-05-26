using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MENU_Upgrade : MonoBehaviour {

    public Text Upg_CurrCoins;
    public Text[] Upg_CurrCharNames;
    Button[] Upg_Char1;
    Button[] Upg_Char2;
    Button[] Upg_Char3;

    public int Upg_HPCost;
    public int Upg_PowerCost;
    public int Upg_SpeedCost;

	// Use this for initialization
	void Start () {

        Upg_Char1 = new Button[3];
        Upg_Char2 = new Button[3];
        Upg_Char3 = new Button[3];

        int ButtNum = GetComponent<MENU_Controller>().Menu_UIButtons.Length;

        for (int i = 0; i < GetComponent<MENU_Controller>().Menu_UIButtons.Length; i++)
        {
            if (i < 3)
                Upg_Char1[i] = GetComponent<MENU_Controller>().Menu_UIButtons[i];
            else if (i < 6)
                Upg_Char2[i - 3] = GetComponent<MENU_Controller>().Menu_UIButtons[i];
            else if (i < 9)
                Upg_Char3[i - 6] = GetComponent<MENU_Controller>().Menu_UIButtons[i];
        }

        
            
	}
	
	// Update is called once per frame
	void Update () 
    {
        for (int i = 0; i < MNGR_Game.currentParty.Length; i++)
        {
            if (MNGR_Game.currentParty[i].Act_currHP > 0)
            {
                Upg_CurrCharNames[i].text = MNGR_Game.currentParty[i].name;
                if (i == 0)
                    for (int j = 0; j < Upg_Char1.Length; j++)
                        Upg_Char1[j].interactable = true;
                else if (i == 1)
                    for (int j = 0; j < Upg_Char2.Length; j++)
                        Upg_Char2[j].interactable = true;
                else if (i == 2)
                    for (int j = 0; j < Upg_Char3.Length; j++)
                        Upg_Char3[j].interactable = true;
            }
            else
            {
                Upg_CurrCharNames[i].text = "Dead";
                if (i == 0)
                    for (int j = 0; j < Upg_Char1.Length; j++)
                        Upg_Char1[j].interactable = false;
                else if (i == 1)
                    for (int j = 0; j < Upg_Char2.Length; j++)
                        Upg_Char2[j].interactable = false;
                else if (i == 2)
                    for (int j = 0; j < Upg_Char3.Length; j++)
                        Upg_Char3[j].interactable = false;
            }

        }

        Upg_CurrCoins.text = MNGR_Game.wallet.ToString();

        Upg_Char1[0].GetComponentInChildren<Text>().text = "HP Lvl: " + MNGR_Game.currentParty[0].Act_HPLevel.ToString();
        Upg_Char1[1].GetComponentInChildren<Text>().text = "Power Lvl: " + MNGR_Game.currentParty[0].Act_PowerLevel.ToString();
        Upg_Char1[2].GetComponentInChildren<Text>().text = "Speed Lvl: " + MNGR_Game.currentParty[0].Act_SpeedLevel.ToString();

        Upg_Char2[0].GetComponentInChildren<Text>().text = "HP Lvl: " + MNGR_Game.currentParty[1].Act_HPLevel.ToString();
        Upg_Char2[1].GetComponentInChildren<Text>().text = "Power Lvl: " + MNGR_Game.currentParty[1].Act_PowerLevel.ToString();
        Upg_Char2[2].GetComponentInChildren<Text>().text = "Speed Lvl: " + MNGR_Game.currentParty[1].Act_SpeedLevel.ToString();

        Upg_Char3[0].GetComponentInChildren<Text>().text = "HP Lvl: " + MNGR_Game.currentParty[2].Act_HPLevel.ToString();
        Upg_Char3[1].GetComponentInChildren<Text>().text = "Power Lvl: " + MNGR_Game.currentParty[2].Act_PowerLevel.ToString();
        Upg_Char3[2].GetComponentInChildren<Text>().text = "Speed Lvl: " + MNGR_Game.currentParty[2].Act_SpeedLevel.ToString();
	}

    public void UpgradeHP(int _CharIndex)
    {
        Upg_HPCost = (MNGR_Game.currentParty[_CharIndex].Act_HPLevel * 5) + MNGR_Game.currentParty[_CharIndex].Act_HPLevel;
        if (MNGR_Game.wallet >= Upg_HPCost && MNGR_Game.currentParty[_CharIndex].Act_HPLevel < 20)
        {
            MNGR_Game.currentParty[_CharIndex].Act_baseHP += 5;
            MNGR_Game.currentParty[_CharIndex].Act_HPLevel++;
            MNGR_Game.wallet -= Upg_HPCost;
        }
    }

    public void UpgradePower(int _CharIndex)
    {
        Upg_PowerCost = (MNGR_Game.currentParty[_CharIndex].Act_PowerLevel * 5) + MNGR_Game.currentParty[_CharIndex].Act_PowerLevel;
        if (MNGR_Game.wallet >= Upg_PowerCost && MNGR_Game.currentParty[_CharIndex].Act_PowerLevel < 20)
        {
            MNGR_Game.currentParty[_CharIndex].Act_basePower += 3;
            MNGR_Game.currentParty[_CharIndex].RestoreToBasePower();
            MNGR_Game.currentParty[_CharIndex].Act_PowerLevel++;

            MNGR_Game.wallet -= Upg_PowerCost;
        }
    }

    public void UpgradeSpeed(int _CharIndex)
    {
        Upg_SpeedCost = (MNGR_Game.currentParty[_CharIndex].Act_SpeedLevel * 5) + MNGR_Game.currentParty[_CharIndex].Act_SpeedLevel;
        if (MNGR_Game.wallet >= Upg_SpeedCost && MNGR_Game.currentParty[_CharIndex].Act_SpeedLevel < 20)
        {
            MNGR_Game.currentParty[_CharIndex].Act_baseSpeed += 3;
            MNGR_Game.currentParty[_CharIndex].RestoreToBasePower();
            MNGR_Game.currentParty[_CharIndex].Act_SpeedLevel++;

            MNGR_Game.wallet -= Upg_SpeedCost;


        }
    }
}

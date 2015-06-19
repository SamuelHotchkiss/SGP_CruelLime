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
    public int Upg_MaxLevel;

    enum UpgType { HpUpg, PowerUpg, SpeedUpg };
	// Use this for initialization
	void Start () {

        Upg_MaxLevel = 10;

        Upg_Char1 = new Button[3];
        Upg_Char2 = new Button[3];
        Upg_Char3 = new Button[3];

        int ButtNum = GetComponent<MENU_Controller>().Menu_UIButtons.Length;

        for (int i = 0; i < ButtNum; i++)
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
                Upg_CurrCharNames[i].text = MNGR_Game.currentParty[i].name + " Lvl " + MNGR_Game.currentParty[i].CalcAverageLvl().ToString() + "\nHP: " + MNGR_Game.currentParty[i].Act_currHP + " / " + MNGR_Game.currentParty[i].Act_baseHP;
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
        if (Upg_MaxLevel > MNGR_Game.currentParty[0].Act_HPLevel)
            Upg_Char1[0].GetComponentInChildren<Text>().text += "\nCost: " + CalculateUpgradeCost(0, UpgType.HpUpg).ToString() + " Gold";
        else
            Upg_Char1[0].GetComponentInChildren<Text>().text += "\nMax Level";

        Upg_Char1[1].GetComponentInChildren<Text>().text = "Power Lvl: " + MNGR_Game.currentParty[0].Act_PowerLevel.ToString();
        if (Upg_MaxLevel > MNGR_Game.currentParty[0].Act_PowerLevel)
            Upg_Char1[1].GetComponentInChildren<Text>().text += "\nCost: " + CalculateUpgradeCost(0, UpgType.PowerUpg).ToString() + " Gold";
        else
            Upg_Char1[1].GetComponentInChildren<Text>().text += "\nMax Level";

        Upg_Char1[2].GetComponentInChildren<Text>().text = "Speed Lvl: " + MNGR_Game.currentParty[0].Act_SpeedLevel.ToString();
        if (Upg_MaxLevel > MNGR_Game.currentParty[0].Act_SpeedLevel)
            Upg_Char1[2].GetComponentInChildren<Text>().text += "\nCost: " + CalculateUpgradeCost(0, UpgType.SpeedUpg).ToString() + " Gold";
        else
            Upg_Char1[2].GetComponentInChildren<Text>().text += "\nMax Level";

        Upg_Char2[0].GetComponentInChildren<Text>().text = "HP Lvl: " + MNGR_Game.currentParty[1].Act_HPLevel.ToString();
        if (Upg_MaxLevel > MNGR_Game.currentParty[1].Act_HPLevel)
            Upg_Char2[0].GetComponentInChildren<Text>().text += "\nCost: " + CalculateUpgradeCost(1, UpgType.HpUpg).ToString() + " Gold";
        else
            Upg_Char2[0].GetComponentInChildren<Text>().text += "\nMax Level";

        Upg_Char2[1].GetComponentInChildren<Text>().text = "Power Lvl: " + MNGR_Game.currentParty[1].Act_PowerLevel.ToString();
        if (Upg_MaxLevel > MNGR_Game.currentParty[1].Act_PowerLevel)
            Upg_Char2[1].GetComponentInChildren<Text>().text += "\nCost: " + CalculateUpgradeCost(1, UpgType.PowerUpg).ToString() + " Gold";
        else
            Upg_Char2[1].GetComponentInChildren<Text>().text += "\nMax Level";
        Upg_Char2[2].GetComponentInChildren<Text>().text = "Speed Lvl: " + MNGR_Game.currentParty[1].Act_SpeedLevel.ToString(); 
        if (Upg_MaxLevel > MNGR_Game.currentParty[1].Act_SpeedLevel)
            Upg_Char2[2].GetComponentInChildren<Text>().text += "\nCost: " + CalculateUpgradeCost(1, UpgType.SpeedUpg).ToString() + " Gold";
        else
            Upg_Char2[2].GetComponentInChildren<Text>().text += "\nMax Level";

        Upg_Char3[0].GetComponentInChildren<Text>().text = "HP Lvl: " + MNGR_Game.currentParty[2].Act_HPLevel.ToString(); 
        if (Upg_MaxLevel > MNGR_Game.currentParty[2].Act_HPLevel)
            Upg_Char3[0].GetComponentInChildren<Text>().text += "\nCost: " + CalculateUpgradeCost(2, UpgType.HpUpg).ToString() + " Gold";
        else
            Upg_Char3[0].GetComponentInChildren<Text>().text += "\nMax Level";
        Upg_Char3[1].GetComponentInChildren<Text>().text = "Power Lvl: " + MNGR_Game.currentParty[2].Act_PowerLevel.ToString(); 
        if (Upg_MaxLevel > MNGR_Game.currentParty[2].Act_PowerLevel)
            Upg_Char3[1].GetComponentInChildren<Text>().text += "\nCost: " + CalculateUpgradeCost(2, UpgType.PowerUpg).ToString() + " Gold";
        else
            Upg_Char3[1].GetComponentInChildren<Text>().text += "\nMax Level";
        Upg_Char3[2].GetComponentInChildren<Text>().text = "Speed Lvl: " + MNGR_Game.currentParty[2].Act_SpeedLevel.ToString();
        if (Upg_MaxLevel > MNGR_Game.currentParty[2].Act_SpeedLevel)
            Upg_Char3[2].GetComponentInChildren<Text>().text += "\nCost: " + CalculateUpgradeCost(2, UpgType.SpeedUpg).ToString() + " Gold";
        else
            Upg_Char3[2].GetComponentInChildren<Text>().text += "\nMax Level";

	}


    public void UpgradeHP(int _CharIndex)
    {
        //Upg_HPCost = (MNGR_Game.currentParty[_CharIndex].Act_HPLevel * 5) + MNGR_Game.currentParty[_CharIndex].Act_HPLevel;
        CalculateUpgradeCost(_CharIndex, UpgType.HpUpg);
        if (MNGR_Game.wallet >= Upg_HPCost && MNGR_Game.currentParty[_CharIndex].Act_HPLevel < Upg_MaxLevel)
        {
            int NewHP = (int)(MNGR_Game.currentParty[_CharIndex].Act_baseHP * 0.1f);
            MNGR_Game.currentParty[_CharIndex].Act_baseHP += NewHP;
            MNGR_Game.currentParty[_CharIndex].Act_HPLevel++;
            MNGR_Game.wallet -= Upg_HPCost;
        }

        if (MNGR_Game.currentParty[_CharIndex].CalcAverageLvl() >= 5)
            MNGR_Game.currentParty[_CharIndex].BecomeSpecial();
    }

    public void UpgradePower(int _CharIndex)
    {
        //Upg_PowerCost = (MNGR_Game.currentParty[_CharIndex].Act_PowerLevel * 5) + MNGR_Game.currentParty[_CharIndex].Act_PowerLevel;
        CalculateUpgradeCost(_CharIndex, UpgType.PowerUpg);
        if (MNGR_Game.wallet >= Upg_PowerCost && MNGR_Game.currentParty[_CharIndex].Act_PowerLevel < Upg_MaxLevel)
        {
            MNGR_Game.currentParty[_CharIndex].Act_basePower += 4 - _CharIndex; //Fighters increse faster
            MNGR_Game.currentParty[_CharIndex].RestoreToBasePower();
            MNGR_Game.currentParty[_CharIndex].Act_PowerLevel++;

            MNGR_Game.wallet -= Upg_PowerCost;
        }

        if (MNGR_Game.currentParty[_CharIndex].CalcAverageLvl() >= 5)
            MNGR_Game.currentParty[_CharIndex].BecomeSpecial();
    }

    public void UpgradeSpeed(int _CharIndex)
    {
        //Upg_SpeedCost = (MNGR_Game.currentParty[_CharIndex].Act_SpeedLevel * 5) + MNGR_Game.currentParty[_CharIndex].Act_SpeedLevel;
        CalculateUpgradeCost(_CharIndex, UpgType.SpeedUpg);
        if (MNGR_Game.wallet >= Upg_SpeedCost && MNGR_Game.currentParty[_CharIndex].Act_SpeedLevel < Upg_MaxLevel)
        {
            MNGR_Game.currentParty[_CharIndex].Act_baseSpeed++;
            MNGR_Game.currentParty[_CharIndex].RestoreToBasePower();
            MNGR_Game.currentParty[_CharIndex].Act_SpeedLevel++;

            MNGR_Game.wallet -= Upg_SpeedCost;
        }

        if (MNGR_Game.currentParty[_CharIndex].CalcAverageLvl() >= 5)
            MNGR_Game.currentParty[_CharIndex].BecomeSpecial();
    }

    int CalculateUpgradeCost(int _CharIndex, UpgType _UpgType)
    {
        switch (_UpgType)
        {
            case UpgType.HpUpg:
                Upg_HPCost = (MNGR_Game.currentParty[_CharIndex].Act_HPLevel * 10) * MNGR_Game.currentParty[_CharIndex].CalcAverageLvl() / 4;
                return Upg_HPCost;
            case UpgType.PowerUpg:
                Upg_PowerCost = (MNGR_Game.currentParty[_CharIndex].Act_PowerLevel * 20) * MNGR_Game.currentParty[_CharIndex].CalcAverageLvl() / 4;
                return Upg_PowerCost;
            case UpgType.SpeedUpg:
                Upg_SpeedCost = (MNGR_Game.currentParty[_CharIndex].Act_SpeedLevel * 15) * MNGR_Game.currentParty[_CharIndex].CalcAverageLvl() / 4;
                return Upg_SpeedCost;
        }

        return -1;
    }
}

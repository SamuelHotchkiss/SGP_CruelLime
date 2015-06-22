using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class MENU_LoadText : MonoBehaviour
{
    public MENU_Load loadMan;
    public int profileNum;

    public Text lvlName, cashAmount;
    public Text[] hpAmounts, charLevels;
    public Image[] portraits;

    // Use this for initialization
    public void WriteText()
    {
        if (!MNGR_Save.saveFiles[profileNum].isNew)
            lvlName.text = MNGR_Save.saveFiles[profileNum].currentLevel;
        else
            lvlName.text = "NEW GAME";

        cashAmount.text = "Coins : " + MNGR_Save.saveFiles[profileNum].wallet.ToString();

        for (int i = 0; i < 3; i++)
        {
            portraits[i].sprite = Resources.Load<Sprite>("Sprites/GUI/" + loadMan.portraitNames[MNGR_Save.saveFiles[profileNum].currentParty[i].characterIndex]);
            hpAmounts[i].text = "HP : " + MNGR_Save.saveFiles[profileNum].currentParty[i].Act_currHP.ToString("F0");

            MNGR_Save.saveFiles[profileNum].currentParty[i].Act_AverageLevel = MNGR_Save.saveFiles[profileNum].currentParty[i].CalcAverageLvl();
            charLevels[i].text = "LvL : " + MNGR_Save.saveFiles[profileNum].currentParty[i].Act_AverageLevel.ToString();
        }

    }
}

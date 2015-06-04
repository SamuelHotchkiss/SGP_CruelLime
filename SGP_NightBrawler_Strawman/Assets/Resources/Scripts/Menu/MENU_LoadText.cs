using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class MENU_LoadText : MonoBehaviour
{
    public int profileNum;

    public Text lvlName, cashAmount;
    public Text[] hpAmounts, charLevels;

    // Use this for initialization
    public void WriteText()
    {
        lvlName.text = MNGR_Save.saveFiles[profileNum].currentLevel;

        cashAmount.text = "C o i n s : " + MNGR_Save.saveFiles[profileNum].wallet.ToString();

        for (int i = 0; i < 3; i++)
        {
            hpAmounts[i].text = "H P : " + MNGR_Save.saveFiles[profileNum].currentParty[i].Act_currHP.ToString();

            MNGR_Save.saveFiles[profileNum].currentParty[i].Act_AverageLevel = MNGR_Save.saveFiles[profileNum].currentParty[i].CalcAverageLvl();
            charLevels[i].text = "L e v e l : " + MNGR_Save.saveFiles[profileNum].currentParty[i].Act_AverageLevel.ToString();
        }
    }
}

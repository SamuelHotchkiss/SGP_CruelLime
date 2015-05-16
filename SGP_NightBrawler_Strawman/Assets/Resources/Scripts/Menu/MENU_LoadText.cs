using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class MENU_LoadText : MonoBehaviour
{
    public int profileNum;

    // Use this for initialization
    public void WriteText()
    {
        if (!MNGR_Save.saveFiles[profileNum].isNew)
        {
            GetComponent<Text>().text = MNGR_Save.saveFiles[profileNum].theCharacters[0].Act_currHP.ToString()
                + " " + MNGR_Save.saveFiles[profileNum].theCharacters[3].Act_currHP.ToString()
                + " " + MNGR_Save.saveFiles[profileNum].theCharacters[6].Act_currHP.ToString();
        }
        else
            GetComponent<Text>().text = "Start New Game";
    }
}

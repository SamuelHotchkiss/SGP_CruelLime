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
            GetComponent<Text>().text = MNGR_Save.saveFiles[profileNum].wallet.ToString();
        else
            GetComponent<Text>().text = "START NEW GAME";
    }
}

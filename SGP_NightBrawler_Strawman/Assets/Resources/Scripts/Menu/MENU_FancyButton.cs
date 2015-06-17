using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MENU_FancyButton : MonoBehaviour 
{
    public void HighlightText(bool yes)
    {
        if (yes)
            GetComponentInChildren<Text>().color = Color.white;
        else
            GetComponentInChildren<Text>().color = Color.black;
    }
}

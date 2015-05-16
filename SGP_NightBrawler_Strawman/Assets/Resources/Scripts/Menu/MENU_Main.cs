using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MENU_Main : MonoBehaviour {


    public Button[] Menu_UIButtons;
    public int Menu_CurrButton;
	// Use this for initialization
	void Start () {
        Menu_CurrButton = 0;
	}
	
	// Update is called once per frame
	void Update () {

        for (int i = 0; i < Menu_UIButtons.Length; i++)
        {
            if (i != Menu_CurrButton)
            {
                Menu_UIButtons[i].interactable = false;
                Menu_UIButtons[i].interactable = true;
            }
        }


        if (Input.anyKeyDown)
        {
            if (Input.GetAxis("Vertical") > 0.0f)       
            {
                Menu_CurrButton--;
                if (Menu_CurrButton < 0)
                    Menu_CurrButton = 0;
            }
            else if (Input.GetAxis("Vertical") < 0.0f)
            {
                Menu_CurrButton++;
                if (Menu_CurrButton >= Menu_UIButtons.Length)
                    Menu_CurrButton = Menu_UIButtons.Length - 1;
            }
        }

        Menu_UIButtons[Menu_CurrButton].Select();
	}
}

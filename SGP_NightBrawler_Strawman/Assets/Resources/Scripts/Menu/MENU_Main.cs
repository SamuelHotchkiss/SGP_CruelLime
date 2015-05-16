using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MENU_Main : MonoBehaviour {


    public Button[] Menu_UIButtons;
    public int Menu_CurrButton;
    public float Menu_JoyController;

    private float Menu_JoyTimer;

	// Use this for initialization
	void Start () {
        Menu_CurrButton = 0;
        Menu_JoyTimer = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {

        Menu_JoyController = Input.GetAxisRaw("Pad_Vertical");

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

            if (Input.GetKey(KeyCode.LeftAlt))
                if (Input.GetKeyDown(KeyCode.F4))
                {
                    Debug.Log("Quit");
                    //Application.Quit();
                }
        }


        if (Menu_JoyTimer <= 0.0f)
        {
            if (Input.GetAxis("Pad_Vertical") > 0.07f)
            {
                Menu_CurrButton--;
                if (Menu_CurrButton < 0)
                    Menu_CurrButton = 0;
            }
            else if (Input.GetAxis("Pad_Vertical") < -0.07f)
            {
                Menu_CurrButton++;
                if (Menu_CurrButton >= Menu_UIButtons.Length)
                    Menu_CurrButton = Menu_UIButtons.Length - 1;
            }

            Menu_JoyTimer = 0.5f;
        }

        Menu_JoyTimer -= Time.deltaTime;
        Menu_UIButtons[Menu_CurrButton].Select();
	}
}

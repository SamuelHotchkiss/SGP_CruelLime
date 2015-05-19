using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class MENU_Controller : MonoBehaviour {

    public Button[] Menu_UIButtons;         //Can Hold all buttons in the current Scene
    public int Menu_CurrButton;             //Currnt selected Button.
    public float Menu_JoyController;        //Debug Only with use with controllers 
    public AudioClip Menu_MoveSound;    //Clip of Audio the willplay wen a new button is highlighet but no selected.

    private float Menu_JoyTimer;            //Stops controller from jumping all over the menu

    // Use this for initialization
    void Start()
    {
        Menu_CurrButton = 0;
        Menu_JoyTimer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {

        Menu_JoyController = Input.GetAxisRaw("Pad_Vertical");

        //Allows only one button to be highlighted at any one time
        //unless the mouse hover over it.
        for (int i = 0; i < Menu_UIButtons.Length; i++)
        {
            if (i != Menu_CurrButton)
            {
                Menu_UIButtons[i].interactable = false;
                Menu_UIButtons[i].interactable = true;
            }
        }

        int OldCurrbutton = Menu_CurrButton;

        if (Input.anyKeyDown)
        {
            //Go up with the keyboard
            if (Input.GetAxis("Vertical") > 0.0f)
            {
                Menu_CurrButton--;
                if (Menu_CurrButton < 0)
                    Menu_CurrButton = 0;
            }
            //Go down with the Keyboard
            else if (Input.GetAxis("Vertical") < 0.0f)
            {
                Menu_CurrButton++;
                if (Menu_CurrButton >= Menu_UIButtons.Length)
                    Menu_CurrButton = Menu_UIButtons.Length - 1;
            }

            //Leave the Aplication.
            if (Input.GetKey(KeyCode.LeftAlt))
                if (Input.GetKeyDown(KeyCode.F4))
                {
                    Debug.Log("Quit");
                    Application.Quit();
                }
        }

        
        if (Menu_JoyTimer <= 0.0f)
        {
            //Go up with joystick
            if (Input.GetAxis("Pad_Vertical") > 0.07f)
            {
                Menu_CurrButton--;
                if (Menu_CurrButton < 0)
                    Menu_CurrButton = 0;


            }
            //Go down with joystick
            else if (Input.GetAxis("Pad_Vertical") < -0.07f)
            {
                Menu_CurrButton++;
                if (Menu_CurrButton >= Menu_UIButtons.Length)
                    Menu_CurrButton = Menu_UIButtons.Length - 1;
            }

            Menu_JoyTimer = 0.1f; 
        }

        if (OldCurrbutton != Menu_CurrButton)
            AudioSource.PlayClipAtPoint(Menu_MoveSound, new Vector3(0, 0, 0), MNGR_Options.sfxVol);

        Menu_JoyTimer -= Time.deltaTime;
        Menu_UIButtons[Menu_CurrButton].Select(); //Highlights the currently selected option
    }
}

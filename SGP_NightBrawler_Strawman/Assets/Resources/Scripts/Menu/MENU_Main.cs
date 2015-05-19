using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MENU_Main : MonoBehaviour
{

    public AudioClip Menu_SelectedSound;    //Clip of sound that will play wen a button is press.
    private string Menu_Levelname;          //Name that will be use to change scenes

    void Start()
    {
        MNGR_Save.LoadOptions();            // Load in the options file, if there is one

        Screen.fullScreen = MNGR_Options.fullscreen;
    }

    public void ChangeSceneButton(string levelname)
    {
        //Level Name most be the EXACT name of the scene.
        AudioSource.PlayClipAtPoint(Menu_SelectedSound, new Vector3(0, 0, 0), MNGR_Options.sfxVol);
        Menu_Levelname = levelname;
        StartCoroutine(WaitForSound(0));
    }

    public void ExitButton()
    {
        Debug.Log("Quit");
        AudioSource.PlayClipAtPoint(Menu_SelectedSound, new Vector3(0, 0, 0), MNGR_Options.sfxVol);
        StartCoroutine(WaitForSound(1));
    }

    IEnumerator WaitForSound(int _selection)
    {
        yield return new WaitForSeconds(0.35f);     //This is how long the current sound clip takes to play.
        switch (_selection)
        {
            case 0:
                Application.LoadLevel(Menu_Levelname);
                break;
            case 1:
                Application.Quit();
                break;
            default:
                break;
        }
    }
}

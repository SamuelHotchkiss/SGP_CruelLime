using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class MENU_Load : MonoBehaviour
{
    public MENU_LoadText[] profiles;
    public AudioClip Menu_SelectedSound;    //Clip of sound that will play wen a button is press.
    public string[] portraitNames = { "Port_Sword", "Port_Lancer", "Port_Defender", "Port_Archer", "Port_Ninja", 
                                        "Port_Poisoner", "Port_Wizard", "Port_ForceMage", "Port_Spellslinger"};

    // Use this for initialization
    void Start()
    {
        MNGR_Save.LoadProfiles();

        for (int i = 0; i < 3; i++)
        {
            profiles[i].WriteText();
        }
    }


    // Loads the selected profile
    public void LoadProfile(int saveIndex)
    {
        MNGR_Save.currSave = saveIndex;
        MNGR_Save.LoadCurrentSave();
        MNGR_Save.SaveProfiles();

        if (GameObject.Find("DJ") != null)
            Destroy(GameObject.Find("DJ"));

        AudioSource.PlayClipAtPoint(Menu_SelectedSound, new Vector3(0, 0, 0), MNGR_Options.sfxVol);
        StartCoroutine(WaitForSound(1));

    }

    // Clears the selected profile
    public void DeleteProfile(int saveIndex)
    {
        MNGR_Save.DeleteCurrentSave(saveIndex); // clears GameData of the profile

        AudioSource.PlayClipAtPoint(Menu_SelectedSound, new Vector3(0, 0, 0), MNGR_Options.sfxVol);
        StartCoroutine(WaitForSound(2));
    }

    // Go back to Main Menu
    public void Return()
    {
        AudioSource.PlayClipAtPoint(Menu_SelectedSound, new Vector3(0, 0, 0), MNGR_Options.sfxVol);
        StartCoroutine(WaitForSound(0));
        //Application.LoadLevel("MainMenu");
    }

    IEnumerator WaitForSound(int _Selection)
    {
        //This can be use to stop the action from been activated
        //Each time a button is PRESS this sould be call to allow 
        //the sound is play ONLY if the scene is been change. 
        yield return new WaitForSeconds(0.35f);
        switch (_Selection)
        {
            case 0:
                Application.LoadLevel("MainMenu");
                break;
            case 1:
                Application.LoadLevel("WorldMap");
                break;
            case 2:
                Application.LoadLevel(Application.loadedLevelName); // reloads the LoadMenu scene
                break;
            default:
                break;
        }
    }

}

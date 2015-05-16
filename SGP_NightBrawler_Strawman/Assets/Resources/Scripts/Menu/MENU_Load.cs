using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class MENU_Load : MonoBehaviour 
{
    public MENU_LoadText[] profiles;
    public AudioClip Menu_SelectedSound;    //Clip of sound that will play wen a button is press.

	// Use this for initialization
	void Start () 
    {
        MNGR_Save.Load();

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

        Application.LoadLevel("ForestLevel0");
    }

    // Clears the selected profile
    public void DeleteProfile(int saveIndex)
    {
        MNGR_Save.DeleteCurrentSave(saveIndex); // clears GameData of the profile
        Application.LoadLevel(Application.loadedLevelName); // reloads the LoadMenu scene
    }

    // Go back to Main Menu
    public void Return()
    {
        AudioSource.PlayClipAtPoint(Menu_SelectedSound, new Vector3(0, 0, 0), 1.0f);
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
            default:
                break;
        }
    }

}

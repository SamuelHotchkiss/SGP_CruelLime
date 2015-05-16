using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class MENU_Load : MonoBehaviour 
{
    public MENU_LoadText[] profiles;

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
        Application.LoadLevel("MainMenu");
    }
}

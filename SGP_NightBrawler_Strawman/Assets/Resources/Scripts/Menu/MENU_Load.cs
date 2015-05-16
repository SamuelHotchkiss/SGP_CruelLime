using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class MENU_Load : MonoBehaviour 
{
    public Button[] profiles;

	// Use this for initialization
	void Start () 
    {
        MNGR_Save.Load();
	}
	
	// Update is called once per frame
	void Update () 
    {
	
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
}

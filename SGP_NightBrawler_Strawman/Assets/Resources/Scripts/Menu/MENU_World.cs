using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class MENU_World : MonoBehaviour 
{
    int index;

    public Button[] levels;
    public Text playerPos, hordePos;

    public Button arrow;


    public AudioClip Menu_SelectedSound;    //Clip of sound that will play when a button is pressed.


	// Use this for initialization
	void Start () 
    {

        index = MNGR_Game.playerPosition;

        playerPos.text = "Player Position: " + levels[index].GetComponentInChildren<Text>().text;
        hordePos.text = "Horde Position: " + MNGR_Game.hordePosition.ToString();

        Vector3 arrowPos = new Vector3(levels[index].transform.position.x, levels[index].transform.position.y + 73.0f, 0);
        arrow.transform.position = arrowPos;
	}

    public void StartLevel()
    {
        string lvlName = levels[index].name;

        StartCoroutine(WaitForSound(lvlName));
    }

    // Go back to Main Menu
    public void Return()
    {
        AudioSource.PlayClipAtPoint(Menu_SelectedSound, new Vector3(0, 0, 0), MNGR_Options.sfxVol);
        StartCoroutine(WaitForSound());
    }

    IEnumerator WaitForSound()
    {
        //This can be use to stop the action from been activated
        //Each time a button is PRESS this sould be call to allow 
        //the sound is play ONLY if the scene is been change. 
        yield return new WaitForSeconds(0.35f);

        /*TO BE IMPLEMENTED*/
        //MNGR_Save.saveFiles[MNGR_Save.currSave].CopyGameManager();
        //MNGR_Save.SaveProfiles();

        Application.LoadLevel("MainMenu");
    }

    IEnumerator WaitForSound(string lvlName)
    {
        yield return new WaitForSeconds(0.35f);

        Application.LoadLevel(lvlName);
    }
}

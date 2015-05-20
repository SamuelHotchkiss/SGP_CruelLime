using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class MENU_World : MonoBehaviour 
{
    int playIndex, hordeIndex;

    public Button[] levels, hordeSteps;
    public Text playerPos, hordePos;

    public Button playerArrow, hordeArrow;
    public Image theSky;


    public AudioClip Menu_SelectedSound;    //Clip of sound that will play when a button is pressed.


	// Use this for initialization
	void Start () 
    {
        if (MNGR_Game.isNight)
            theSky.sprite = Resources.Load<Sprite>("Sprites/Menu/Decorative_Moon");
        else
            theSky.sprite = Resources.Load<Sprite>("Sprites/Menu/Decorative_Sun");

        playIndex = MNGR_Game.playerPosition;
        hordeIndex = MNGR_Game.hordePosition;

        playerPos.text = "Player Position: " + levels[playIndex].GetComponentInChildren<Text>().text;
        hordePos.text = "Horde Position: " + MNGR_Game.hordePosition.ToString();

        Vector3 playMarker = new Vector3(levels[playIndex].transform.position.x, levels[playIndex].transform.position.y + 73.0f, 0);
        Vector3 hordeMarker = new Vector3(hordeSteps[hordeIndex].transform.position.x, hordeSteps[hordeIndex].transform.position.y - 73.0f, 0);

        playerArrow.transform.position = playMarker;
        hordeArrow.transform.position = hordeMarker;
	}

    public void StartLevel()
    {
        string lvlName = levels[playIndex].name;

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

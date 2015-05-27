using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class MENU_World : MonoBehaviour 
{
    int playIndex, hordeIndex;

    public Button[] levels, hordeSteps;
	public Text[] characterHP, inventoryCounts;
    public Text playerPos, hordePos;

    public Button playerArrow, hordeArrow;
    public Image theSky;
    public GameObject merchantPanel;

    public AudioClip Menu_SelectedSound;    //Clip of sound that will play when a button is pressed.

	public Image[] inventoryImages;


	// Use this for initialization
	void Start () 
    {
        if (MNGR_Game.playerPosition > 2)
            MNGR_Game.playerPosition = 2;

        if ((MNGR_Game.hordePosition / 2) == MNGR_Game.playerPosition && MNGR_Game.hordePosition > 0)
            MNGR_Game.dangerZone = true;
        else
            MNGR_Game.dangerZone = false;

        Debug.Log(MNGR_Game.dangerZone);

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

        for (int i = 0; i < characterHP.Length; i++)
        {
            characterHP[i].text = MNGR_Game.currentParty[i].Act_currHP.ToString();
        }

		//for (int i = 0; i < inventory.Length; i++)
		//{
		//	inventory[i].text = MNGR_Game.theInventory.containers[i].count.ToString();
		//}

		for (int i = 0; i < inventoryCounts.Length; i++)
		{
			inventoryCounts[i].text = MNGR_Game.theInventory.containers[i].count.ToString();
			if (MNGR_Game.theInventory.containers[i].count < 1)
				inventoryImages[i].gameObject.SetActive(false);
			else
				inventoryImages[i].gameObject.SetActive(true);
		}
	}

    public void StartLevel()
    {
        string lvlName = levels[playIndex].name;

        AudioSource.PlayClipAtPoint(Menu_SelectedSound, new Vector3(0,0,0), MNGR_Options.sfxVol);

        StartCoroutine(WaitForSound(lvlName));
    }

    public void VisitMerchant()
    {
        merchantPanel.SetActive(true);
        //GameObject.Find("Canvas").GetComponent<MENU_Controller>().enabled = false;

        AudioSource.PlayClipAtPoint(Menu_SelectedSound, new Vector3(0,0,0), MNGR_Options.sfxVol);
        StartCoroutine(WaitForSound());
    }

    // closes merchant menu
    public void LeaveMerchant()
    {
        merchantPanel.SetActive(false);
        //GameObject.Find("Canvas").GetComponent<MENU_Controller>().enabled = true;

        AudioSource.PlayClipAtPoint(Menu_SelectedSound, new Vector3(0,0,0), MNGR_Options.sfxVol);
        StartCoroutine(WaitForSound(Application.loadedLevelName));
    }

    // heals party and advances time
    public void Rest()
    {
        // use this to heal every member in the party
        //for(int i = 0; i < 3; i++)
        //{
        //    if (MNGR_Game.currentParty[i].Act_currHP > 0)
        //        MNGR_Game.currentParty[i].Act_currHP = MNGR_Game.currentParty[i].Act_baseHP;
        //}

        MNGR_Game.isNight = !MNGR_Game.isNight;
    }

    // buys instant HP potions
    public void BuyPotion()
    {

    }

    // opens up revive menu
    public void Revive()
    {

    }

    // Go back to Main Menu
    public void Return()
    {
        AudioSource.PlayClipAtPoint(Menu_SelectedSound, new Vector3(0, 0, 0), MNGR_Options.sfxVol);
        StartCoroutine(WaitForSound("MainMenu"));
    }

    IEnumerator WaitForSound()
    {
        yield return new WaitForSeconds(0.35f);

        /*TO BE IMPLEMENTED*/
        //MNGR_Save.saveFiles[MNGR_Save.currSave].CopyGameManager();
        //MNGR_Save.SaveProfiles();
    }

    IEnumerator WaitForSound(string lvlName)
    {
        yield return new WaitForSeconds(0.35f);

        Application.LoadLevel(lvlName);
    }
}

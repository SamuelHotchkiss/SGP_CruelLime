using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MENU_Village : MonoBehaviour {

    public GameObject[] Vll_Shops;
    public GameObject Vll_TimePanel;
    public GameObject Vll_SkyBox;
    public int[] Vll_DayShopsID;
    public int[] Vll_NightShopID;

    public Sprite[] Vll_TimeOfDay;
    public Sprite[] Vll_SkyOfDay;

    public string[] Vll_Titles;
    MENU_Controller OpenShops;

	public AudioClip Menu_SelectedSound;    //Clip of sound that will play wen a button is press.
	private string Menu_Levelname;          //Name that will be use to change scenes



    public int Vll_CurrCanvas;
    /*
     * Canvas IDs
     * [0] : Village;
     * [1] : Inn;
     * [2] : Upgrades;
     * [3] : Defence;
     * [4] : Items;
     * [5] : Cult;
     * [6] : Character Selection;
    */

    // Use this for initialization
	void Start () {
        Vll_CurrCanvas = 0;

        if (MNGR_Game.arrowPos == 2)
        {
            if (MNGR_Game.HordeDelayVllOne < 4)
                MNGR_Game.HordeDelayVllOne++;
        }
           // Def_UpgradesNum = MNGR_Game.HordeDelayVllOne - 1;
        else if (MNGR_Game.arrowPos == 8)
        {
            if (MNGR_Game.HordeDelayVllTwo < 4)
                MNGR_Game.HordeDelayVllTwo++;
        }
           // Def_UpgradesNum = MNGR_Game.HordeDelayVllTwo - 1;
        else if (MNGR_Game.arrowPos == 14)
        {
            if (MNGR_Game.HordeDelayVllThree < 4)
                MNGR_Game.HordeDelayVllThree++;
        }
           // Def_UpgradesNum = MNGR_Game.HordeDelayVllThree - 1;

        
        for (int i = 1; i < Vll_Shops.Length; i++)
            Vll_Shops[i].SetActive(false);

        Input.simulateMouseWithTouches = true;

        OpenShops = Vll_Shops[0].GetComponent<MENU_Controller>();
        for (int i = 0; i < Vll_Titles.Length; i++)
            Vll_Titles[i] = OpenShops.Menu_UIButtons[i].GetComponentInChildren<Text>().text;
	}
	
	// Update is called once per frame
	public void Update () {

        if ((MNGR_Game.hordePosition == 2 && MNGR_Game.arrowPos == 2 && MNGR_Game.HordeDelayVllOne == 0) 
            || (MNGR_Game.hordePosition == 8 && MNGR_Game.arrowPos == 8 && MNGR_Game.HordeDelayVllTwo == 0) 
            || (MNGR_Game.hordePosition == 14 && MNGR_Game.arrowPos == 14 && MNGR_Game.HordeDelayVllThree == 0)) 
            ChangeSceneButton("WorldMap");
        
            if (MNGR_Game.isNight)
            {
                //GetComponent<SpriteRenderer>().sprite = Vll_TimeOfDay[1];
                Vll_TimePanel.GetComponent<Image>().sprite = Vll_TimeOfDay[1];
                Vll_SkyBox.GetComponent<Image>().sprite = Vll_SkyOfDay[1];
                if (Vll_CurrCanvas == 0)
                {
                    for (int i = 0; i < Vll_DayShopsID.Length; i++)
                    {
                        OpenShops.Menu_UIButtons[Vll_DayShopsID[i]].interactable = false;
                        OpenShops.Menu_UIButtons[Vll_DayShopsID[i]].GetComponentInChildren<Text>().text = "CLOSE \nFOR THE NIGHT";
                    }
                    for (int i = 0; i < Vll_NightShopID.Length; i++)
                    {
                        OpenShops.Menu_UIButtons[Vll_NightShopID[i]].GetComponentInChildren<Text>().text = Vll_Titles[Vll_NightShopID[i]];
                        OpenShops.Menu_UIButtons[Vll_NightShopID[i]].interactable = true;
                    } 
                }
                    
            }
            else if (!MNGR_Game.isNight)
            {
                Vll_TimePanel.GetComponent<Image>().sprite = Vll_TimeOfDay[0];
                Vll_SkyBox.GetComponent<Image>().sprite = Vll_SkyOfDay[0];
                if (Vll_CurrCanvas == 0)
                {
                    for (int i = 0; i < Vll_DayShopsID.Length; i++)
                    {
                        Text Test = OpenShops.Menu_UIButtons[Vll_DayShopsID[i]].GetComponentInChildren<Text>();
                        OpenShops.Menu_UIButtons[Vll_DayShopsID[i]].GetComponentInChildren<Text>().text = Vll_Titles[Vll_DayShopsID[i]];
                        OpenShops.Menu_UIButtons[Vll_DayShopsID[i]].interactable = true;
                    }

                    for (int i = 0; i < Vll_NightShopID.Length; i++)
                    {
                        OpenShops.Menu_UIButtons[Vll_NightShopID[i]].interactable = false;
                        OpenShops.Menu_UIButtons[Vll_NightShopID[i]].GetComponentInChildren<Text>().text = "CLOSE \nFOR THE DAY";
                    } 
                }
            }
            if (Input.GetKeyDown(KeyCode.N))
                MNGR_Game.isNight = !MNGR_Game.isNight; 

	}

    public void OpenMenus(int _ShopID)
    {
        if (_ShopID == 6) // going to character select
        {
            MNGR_Game.NextLevel = "Village";
            Application.LoadLevel("CharacterSelect");
        }
        else
        {
            Vll_Shops[_ShopID].SetActive(true);
            Vll_Shops[Vll_CurrCanvas].SetActive(false);
            Vll_CurrCanvas = _ShopID;
        }
    }

	public void ChangeSceneButton(string levelname)
	{
		//Level Name most be the EXACT name of the scene.
		AudioSource.PlayClipAtPoint(Menu_SelectedSound, new Vector3(0, 0, 0), MNGR_Options.sfxVol);
		Menu_Levelname = levelname;
        MNGR_Game.UpdateWorld();
		StartCoroutine(WaitForSound(0));
	}

	IEnumerator WaitForSound(int _selection)
	{
		yield return new WaitForSeconds(0.35f);     //This is how long the current sound clip takes to play.
		switch (_selection)
		{
			case 0:
				MNGR_Game.NextLevel = "WorldMap";
				Application.LoadLevel("TransitionScene");
				//Application.LoadLevel(Menu_Levelname);
				break;
			case 1:
				Application.Quit();
				break;
		}
	}
}

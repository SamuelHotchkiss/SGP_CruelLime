using UnityEngine;
using System.Collections;

public class MENU_Village : MonoBehaviour {

    public  GameObject[] Vll_Shops;
    public int[] Vll_DayShopsID;
    public int[] Vll_NightShopID;

    public Sprite[] Vll_TimeOfDay;

    public int Vll_CurrCanvas;

    /*
     * Canvas IDs
     * [0] : Village;
     * [1] : Inn;
     * [2] : Upgrades;
     * [3] : Defence;
     * [4] : Items;
     * [5] : Cult;
    */

    // Use this for initialization
	void Start () {
        Vll_CurrCanvas = 0;
        for (int i = 1; i < Vll_Shops.Length; i++)
            Vll_Shops[i].SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        MENU_Controller OpenShops = Vll_Shops[0].GetComponent<MENU_Controller>();
        if (Vll_CurrCanvas == 0)
        {
            if (MNGR_Game.isNight)
            {
                GetComponent<SpriteRenderer>().sprite = Vll_TimeOfDay[1];
                for (int i = 0; i < Vll_DayShopsID.Length; i++)
                    OpenShops.Menu_UIButtons[Vll_DayShopsID[i]].interactable = false;
                for (int i = 0; i < Vll_NightShopID.Length; i++)
                    OpenShops.Menu_UIButtons[Vll_NightShopID[i]].interactable = true;
            }
            else if (!MNGR_Game.isNight)
            {
                GetComponent<SpriteRenderer>().sprite = Vll_TimeOfDay[0];
                for (int i = 0; i < Vll_DayShopsID.Length; i++)
                    OpenShops.Menu_UIButtons[Vll_DayShopsID[i]].interactable = true;
                for (int i = 0; i < Vll_NightShopID.Length; i++)
                    OpenShops.Menu_UIButtons[Vll_NightShopID[i]].interactable = false;
            }
            if (Input.GetKeyDown(KeyCode.N))
                MNGR_Game.isNight = !MNGR_Game.isNight; 
        }
	}

    public void OpenMenus(int _ShopID)
    {
        Vll_Shops[_ShopID].SetActive(true);
        Vll_Shops[Vll_CurrCanvas].SetActive(false);
        Vll_CurrCanvas = _ShopID;
    }
}

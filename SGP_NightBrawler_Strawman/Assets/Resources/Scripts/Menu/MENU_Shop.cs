using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class MENU_Shop : MonoBehaviour
{
	public Button[] buttons;
	public Text[] inventoryCounts, shopCounts, shopCosts;
	public Text coins;

	public Image[] inventoryImages, shopImages;

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		coins.text = "x" + MNGR_Game.wallet.ToString();
		for (int i = 0; i < inventoryCounts.Length; i++)
		{
			
		}
	}
}

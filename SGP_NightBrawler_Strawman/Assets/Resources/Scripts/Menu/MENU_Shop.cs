using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class MENU_Shop : MonoBehaviour
{
	public int[] costs;
	public int[] counts;
	public int[] potionIDs;

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
			inventoryCounts[i].text = MNGR_Game.theInventory.containers[i].count.ToString();
			if (MNGR_Game.theInventory.containers[i].count < 1)
				inventoryImages[i].gameObject.SetActive(false);
			else
				inventoryImages[i].gameObject.SetActive(true);
		}

		for (int i = 0; i < shopCounts.Length; i++)
			shopCounts[i].text = "x" + counts[i].ToString();

		for (int i = 0; i < shopCosts.Length; i++)
			shopCosts[i].text = costs[i].ToString();
	}

	public void Buy(int _index)
	{
		if (MNGR_Game.theInventory.containers[potionIDs[_index]].count < 5)
		{
			if (MNGR_Game.wallet >= costs[_index])
			{
				MNGR_Game.wallet -= costs[_index];
				counts[_index]--;

				MNGR_Game.theInventory.containers[potionIDs[_index]].count++;

				if (counts[_index] <= 0)
					buttons[_index].gameObject.SetActive(false);
			}
		}
		else
			Debug.Log("You can't carry any more of that item!");
	}


}

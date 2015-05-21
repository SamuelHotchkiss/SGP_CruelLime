using UnityEngine;
using System.Collections;


[System.Serializable]
public class MNGR_Inventory {

	public MNGR_Container[] inventory = new MNGR_Container[9];

	public void Increment(int _ID)
	{
		inventory[_ID].count++;
	}

	public void Decrement(int _ID)
	{
		inventory[_ID].count--;
	}

	public MNGR_Inventory()
	{
		for (int i = 0; i < 9; i++)
		{
			inventory[i].ID = i;
		}
	}
}

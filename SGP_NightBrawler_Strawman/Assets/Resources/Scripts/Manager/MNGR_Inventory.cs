using UnityEngine;
using System.Collections;


[System.Serializable]
public class MNGR_Inventory 
{
	public MNGR_Container[] containers;

	public void Increment(int _ID)
	{
		containers[_ID].count++;
	}

	public void Decrement(int _ID)
	{
		containers[_ID].count--;
	}

	public MNGR_Inventory()
	{
        containers = new MNGR_Container[9];

		for (int i = 0; i < 9; i++)
		{
            containers[i] = new MNGR_Container();
			containers[i].ID = i;
		}
	}
}

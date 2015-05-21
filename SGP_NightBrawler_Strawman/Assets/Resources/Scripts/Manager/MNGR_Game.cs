using UnityEngine;
using System.Collections;


// A public class that ANY class can access data from
public static class MNGR_Game
{
    public static ACT_CHAR_Base[] theCharacters;
    public static ACT_CHAR_Base[] currentParty;

    public static bool usedItem;
    public static int equippedItem;

	public static MNGR_Inventory theInventory = new MNGR_Inventory();
    public static bool isNight;
    public static int hordePosition, playerPosition, wallet;
	public static bool paused;

}
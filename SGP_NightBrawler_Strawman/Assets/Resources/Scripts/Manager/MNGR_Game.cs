using UnityEngine;
using System.Collections;


// A public class that ANY class can access data from
public static class MNGR_Game
{
    public static ACT_CHAR_Base[] theCharacters = new ACT_CHAR_Base[9];
    public static ACT_CHAR_Base[] currentParty = new ACT_CHAR_Base[3];

    public static bool usedItem;
    public static int equippedItem;

	public static MNGR_Inventory theInventory = new MNGR_Inventory();
    public static bool isNight;
    public static int hordePosition, playerPosition, wallet;
	public static bool paused;
	public static bool dangerZone;      // don't they know you live in the DANGAH ZOWN?! // determines if the horde is on the same level as the player
}
using UnityEngine;
using System.Collections;


// A public class that ANY class can access data from
public static class MNGR_Game
{
    public static ACT_CHAR_Base[] theCharacters;
    public static ACT_CHAR_Base[] currentParty;

	public static MNGR_Inventory inventory = new MNGR_Inventory();
    public static bool isNight;
    public static int hordePosition, playerPosition, wallet;
	public static bool paused;

}
using UnityEngine;
using System.Collections;


// A public class that ANY class can access data from
public static class MNGR_Game
{
    static bool theMan = false;

    static bool isMobile = false;   // S: cannot be altered by outside sources, ONLY change if porting to Android

	public static ACT_CHAR_Base[] theCharacters = new ACT_CHAR_Base[9];
	public static ACT_CHAR_Base[] currentParty = new ACT_CHAR_Base[3];

    public static string currentLevel;
	public static bool usedItem;
	public static int equippedItem;

	public static MNGR_Inventory theInventory = new MNGR_Inventory();
	public static bool isNight = false;
	public static int hordePosition, HordeDelay, playerPosition, wallet;
	public static bool paused;
	public static bool dangerZone;      // don't they know you live in the DANGAH ZOWN?! // determines if the horde is on the same level as the player
    public static string NextLevel;     // used by CharacterSelection scene, determines which level to load when exiting that scene.

    public static int arrowPos;         // super hacky, don't have time to fix right now
	public static void Initialize()
	{
        if (theMan)
            return;

        theMan = true;

        currentLevel = "NEW GAME";
		NextLevel = "WorldMap";

        //hordePosition = HordeDelay = playerPosition = wallet = 0;
        //paused = dangerZone = false;
        //wallet = 100;
        //arrowPos = 0;

		theCharacters[0] = new CHAR_Swordsman();
		theCharacters[1] = new CHAR_Lancer();
		theCharacters[2] = new CHAR_Defender();
		theCharacters[3] = new CHAR_Archer();
		theCharacters[4] = new CHAR_Ninja();
		theCharacters[5] = new CHAR_Poisoner();
		theCharacters[6] = new CHAR_Wizard();
		theCharacters[7] = new CHAR_ForceMage();
		theCharacters[8] = new CHAR_Spellslinger();

        // Temporary to be removed later
        currentParty[0] = theCharacters[0];
		currentParty[1] = theCharacters[3];
		currentParty[2] = theCharacters[6];

		usedItem = false;
		equippedItem = -1;
	}

    public static void UpdateWorld()
    {
        UpdatePlayer();
        UpdateHorde();
		if (hordePosition == arrowPos)
			dangerZone = true;
		else
			dangerZone = false;
    }

     public static void UpdatePlayer()
    {
        MNGR_Game.playerPosition++;
        arrowPos += 2;
    }

    public static void UpdateHorde()
    { 
        if (hordePosition == 2 || hordePosition == 8 || hordePosition == 14) 
        {
            if (HordeDelay == 0)
                hordePosition++;
            else
                HordeDelay--; 
        }
    }  
    
    public static bool AmIMobile()
    {
        return isMobile;
    }
}
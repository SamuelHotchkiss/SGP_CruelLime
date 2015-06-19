using UnityEngine;
using System.Collections;

[System.Serializable]

// A helper class that we can make multiple instances of that can be saved out to exterior files and can modify the GameManager
public class MNGR_GameData
{
    public ACT_CHAR_Base[] theCharacters;
    public ACT_CHAR_Base[] currentParty;

	public MNGR_Inventory inventory;

    public string currentLevel;
    public bool isNew, isNight;
    public int hordePosition, playerPosition, wallet, arrowPos;

    // Initializes GameData's value when new is called
    public MNGR_GameData()
    {
        currentLevel = "NEW GAME";
		inventory = new MNGR_Inventory();

        isNew = true;
        isNight = false;
        hordePosition = playerPosition = wallet = 0;
        arrowPos = 0;
		// delete later
		wallet = 10000;

        theCharacters = new ACT_CHAR_Base[9];
        currentParty = new ACT_CHAR_Base[3];

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
    }

    // Reads in GameManager's values so they can be saved out to a file
    public void CopyGameManager()
    {
        currentLevel = MNGR_Game.currentLevel;
        theCharacters = MNGR_Game.theCharacters;
        isNight = MNGR_Game.isNight;
        hordePosition = MNGR_Game.hordePosition;
        playerPosition = MNGR_Game.playerPosition;
        wallet = MNGR_Game.wallet;
		inventory = MNGR_Game.theInventory;

        arrowPos = MNGR_Game.arrowPos;

        currentParty = MNGR_Game.currentParty;
    }

    // Assigns GameManager's values to this GameData's values so they can be loaded in
    public void AssignGameManager()
    {
        MNGR_Game.currentLevel = currentLevel;
        MNGR_Game.theCharacters = theCharacters;
        MNGR_Game.isNight = isNight;
        MNGR_Game.hordePosition = hordePosition;
        MNGR_Game.playerPosition = playerPosition;
        MNGR_Game.wallet = wallet;
		MNGR_Game.theInventory = inventory;

        MNGR_Game.currentParty = currentParty;

        MNGR_Game.arrowPos = arrowPos;
    }
}


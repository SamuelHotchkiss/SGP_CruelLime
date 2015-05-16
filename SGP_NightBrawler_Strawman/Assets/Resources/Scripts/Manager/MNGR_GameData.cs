﻿using UnityEngine;
using System.Collections;

[System.Serializable]

// A helper class that we can make multiple instances of that can be saved out to exterior files and can modify the GameManager
public class MNGR_GameData
{
    public ACT_CHAR_Base[] theCharacters;

    public bool isNew, isNight;
    public int hordePosition, playerPosition;

    // Initializes GameData's value when new is called
    public MNGR_GameData()
    {
        isNew = true;
        isNight = false;
        hordePosition = playerPosition = 0;

        theCharacters = new ACT_CHAR_Base[9];

        theCharacters[0] = new CHAR_Swordsman();
        theCharacters[1] = new CHAR_Lancer();
        theCharacters[2] = new CHAR_Defender();
        theCharacters[3] = new CHAR_Archer();
        theCharacters[4] = new CHAR_Ninja();
        theCharacters[5] = new CHAR_Poisoner();
        theCharacters[6] = new CHAR_Wizard();
        theCharacters[7] = new CHAR_ForceMage();
        theCharacters[8] = new CHAR_Spellslinger();
    }

    // Reads in GameManager's values so they can be saved out to a file
    public void CopyGameManager()
    {
        theCharacters = MNGR_Game.theCharacters;
        isNight = MNGR_Game.isNight;
        hordePosition = MNGR_Game.hordePosition;
        playerPosition = MNGR_Game.playerPosition;
    }

    // Assigns GameManager's values to this GameData's values so they can be loaded in
    public void AssignGameManager()
    {
        MNGR_Game.theCharacters = theCharacters;
        MNGR_Game.isNight = isNight;
        MNGR_Game.hordePosition = hordePosition;
        MNGR_Game.playerPosition = playerPosition;
    }
}


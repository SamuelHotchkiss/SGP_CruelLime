using UnityEngine;
using System.Collections;


// A public class that ANY class can access data from
public static class MNGR_Game
{
    public static ACT_CHAR_Base[] theCharacters = new ACT_CHAR_Base[9];


    public static bool isNight = false;
    public static int hordePosition = 0, playerPosition = 0;
}
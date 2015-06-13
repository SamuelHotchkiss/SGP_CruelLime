using UnityEngine;
using System.Collections;

public static class MNGR_Options
{
    static bool theMan = false;

    public static float musicVol;       // use this variable when playing any background music
    public static float sfxVol;         // use this variable when playing any sound effect

    public static bool fullscreen;      // use this variable when deciding whether or not to play fullscreen
    public static bool colorblind;      // use thsi variable when deciding whether or not to load in color blind assets

    public static void Initialize()
    {
        if (theMan)             // lazy initialization for dayz
            return;

        musicVol = sfxVol = 1.0f;
        fullscreen = false;
        colorblind = false;
    }
}

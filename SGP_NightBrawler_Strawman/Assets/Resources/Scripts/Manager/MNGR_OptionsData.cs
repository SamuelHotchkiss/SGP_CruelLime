using UnityEngine;
using System.Collections;

[System.Serializable]

// A helper class to save out options data
public class MNGR_OptionsData
{
    public float musicVol;
    public float sfxVol;
    public bool fullscreen;

    public MNGR_OptionsData()
    {
        musicVol = sfxVol = 1.0f;
        fullscreen = true;
    }

    public void AssignOptions()
    {
        MNGR_Options.musicVol = musicVol;
        MNGR_Options.sfxVol = sfxVol;
        MNGR_Options.fullscreen = fullscreen;
    }

    public void CopyOptions()
    {
        musicVol = MNGR_Options.musicVol;
        sfxVol = MNGR_Options.sfxVol;
        fullscreen = MNGR_Options.fullscreen;
    }
	
}

using UnityEngine;
using System.Collections;

[System.Serializable]

// A helper class to save out options data
public class MNGR_OptionsData
{
    public float musicVol;
    public float sfxVol;
    public bool fullscreen;
    public bool colorblind;

    public MNGR_OptionsData()
    {
        musicVol = sfxVol = 1.0f;
        fullscreen = false;
        colorblind = false;
    }

    public void AssignOptions()
    {
        MNGR_Options.musicVol = musicVol;
        MNGR_Options.sfxVol = sfxVol;
        MNGR_Options.fullscreen = fullscreen;
        MNGR_Options.colorblind = colorblind;
    }

    public void CopyOptions()
    {
        musicVol = MNGR_Options.musicVol;
        sfxVol = MNGR_Options.sfxVol;
        fullscreen = MNGR_Options.fullscreen;
        colorblind = MNGR_Options.colorblind;
    }
	
}

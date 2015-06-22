using UnityEngine;
using System.Collections;

public class LevelMusicPlayer : MonoBehaviour 
{
    public bool PlyOnce;

	// Use this for initialization
	void Start () 
    {
		GetComponent<AudioSource>().volume = MNGR_Options.musicVol;
        if (PlyOnce)
            GetComponent<AudioSource>().loop = false;
	}
}


using UnityEngine;
using System.Collections;

public class LevelMusicPlayer : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
    {
        GetComponent<AudioSource>().volume = MNGR_Options.musicVol;
	
	}
}

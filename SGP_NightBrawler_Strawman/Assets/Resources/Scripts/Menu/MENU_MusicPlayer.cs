using UnityEngine;
using System.Collections;

public class MENU_MusicPlayer : MonoBehaviour 
{
    static MENU_MusicPlayer mySelf;

	void Awake()
    {
        if (mySelf == null)
            mySelf = this;
        else
            Destroy(gameObject);

        GetComponent<AudioSource>().volume = MNGR_Options.musicVol;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        GetComponent<AudioSource>().volume = MNGR_Options.musicVol;
    }
}

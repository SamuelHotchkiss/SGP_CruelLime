using UnityEngine;
using System.Collections;

public class SunScript : MonoBehaviour 
{

    Light myLight;
    public bool PraiseIt = true;

	// Use this for initialization
	void Start () 
    {
        myLight = GetComponent<Light>();

        if (MNGR_Game.isNight)
            myLight.color = Color.blue;
        else
            myLight.color = Color.white;
	}
	
}

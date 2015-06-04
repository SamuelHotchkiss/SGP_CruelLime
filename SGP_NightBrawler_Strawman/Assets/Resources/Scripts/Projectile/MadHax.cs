using UnityEngine;
using System.Collections;

public class MadHax : MonoBehaviour 
{
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetAxis("Horizontal") < 0)
        {
            GetComponent<ParticleSystem>().startSpeed = -5.0f;
        }
        else if (Input.GetAxis("Horizontal") > 0)
            GetComponent<ParticleSystem>().startSpeed = 5.0f;
	}
}

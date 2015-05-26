using UnityEngine;
using System.Collections;

public class Trigger : MonoBehaviour {

	public string levelName;
	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "Player")
		{
            MNGR_Game.playerPosition++;
			Application.LoadLevel(levelName);
		}
	}
}

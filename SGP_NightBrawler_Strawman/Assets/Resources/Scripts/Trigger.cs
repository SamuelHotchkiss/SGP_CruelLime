using UnityEngine;
using System.Collections;

public class Trigger : MonoBehaviour {

	public string levelName;
	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "Player")
		{
            MNGR_Save.saveFiles[MNGR_Save.currSave].CopyGameManager();
            MNGR_Save.SaveProfiles();

            MNGR_Game.playerPosition++;
            MNGR_Game.hordePosition++;
			Application.LoadLevel(levelName);
		}
	}
}

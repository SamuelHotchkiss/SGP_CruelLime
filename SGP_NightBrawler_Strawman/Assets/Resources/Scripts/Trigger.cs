﻿using UnityEngine;
using System.Collections;

public class Trigger : MonoBehaviour {

	public string levelName;
    public bool IsATrigger;
    public float LodingTimer;
    void Start()
    {
        LodingTimer = 2.304f;
    }
    void Update()
    {
        if (!IsATrigger)
        {
            LodingTimer -= Time.deltaTime;
            if (LodingTimer <= 0f)
            {
                levelName = MNGR_Game.NextLevel;

				//if(MNGR_Game.equippedItem > 0)
				//	MNGR_Game.theInventory.containers[MNGR_Game.equippedItem].count++;

                Application.LoadLevel(levelName); 
            }
        }
    }
	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "Player")
		{
            MNGR_Game.UpdateWorld();
			MNGR_Game.NextLevel = "WorldMap";

            MNGR_Save.OverwriteCurrentSave();

            Application.LoadLevel("TransitionScene");
		}
	}
}

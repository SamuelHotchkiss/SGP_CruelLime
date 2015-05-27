﻿using UnityEngine;
using System.Collections;

public class BHR_Spawner : BHR_Base
{
    public bool Spw_SpawnAllCritters = true;
	// Update is called once per frame
	void Update () 
    {
        if (owner != null)
        {
            owner.Spw_SpawnCoolDown -= Time.deltaTime;
            owner.Spw_SpawnPerSec -= Time.deltaTime;

            if (owner.Spw_SpawnCoolDown <= 0)
            {
                Spw_SpawnAllCritters = !Spw_SpawnAllCritters;
                owner.Spw_SpawnCoolDown = owner.Spw_BaseSpawnCoolDown;
            }

            if (owner.Spw_CritterThreshold == owner.Spw_CrittersCreated)
                Spw_SpawnAllCritters = false; 
        }
	}


	public override void PerformBehavior()
	{
        if (Spw_SpawnAllCritters)
        {
            if (owner.Spw_SpawnPerSec <= 0.0f)
            {
                Vector3 ActSpawn = new Vector3(owner.Spw_SpawnPositionOffset.x + owner.transform.position.x, owner.Spw_SpawnPositionOffset.y);
                GameObject CritterSpwn = Instantiate(owner.Spw_Critter, ActSpawn, new Quaternion()) as GameObject;
                CritterSpwn.GetComponent<Rigidbody2D>().AddForce(owner.Spw_Force);
                owner.Spw_SpawnPerSec = owner.Spw_baseSpawnPerSec;
                owner.Spw_CrittersCreated++;
            } 
        }
		Debug.Log("Spawner Activated!");
	}
}

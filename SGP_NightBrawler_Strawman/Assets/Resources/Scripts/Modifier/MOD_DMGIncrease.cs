﻿using UnityEngine;
using System.Collections;

public class MOD_DMGIncrease : MOD_Base
{
	// Use this for initialization
	public override void Start () 
    {
        buffState = MNGR_Item.BuffStates.BUFFED;

        base.Start();

        //Mod_IsBuff = true;          //This is a positive effect
        Mod_PartyWide = true;       //This Effect wil affect the whole party
        Mod_effectTimer = 20.0f;
        Mod_ModIndexNum = 1;   
	}
	
	// Update is called once per frame
    public override void Update()
    {
        base.Update();
	}

    public override void ModifyActor()
    {
        for (int i = 0; i < Mod_Actor.party.Length; i++)
        {
            float IncreaseDmgPercent = Mod_Actor.party[i].Act_basePower * 1.5f;     //Increse Damage by 50%

            //if (Mod_Actor.party[i].Act_HasMod)
                Mod_Actor.party[i].SetCurrPower((int)IncreaseDmgPercent);
        }
    }

	public override void ModifyEnemy()
	{
		float IncreaseDmgPercent = enemy.Act_basePower * 1.5f;
		if (enemy.Act_HasMod)
			enemy.SetCurrPower((int)IncreaseDmgPercent);
	}

    public override void EndModifyActor()
    {
        for (int i = 0; i < Mod_Actor.party.Length; i++)
        {
            //if (Mod_Actor.party[i].Act_HasMod)
                Mod_Actor.party[i].RestoreToBasePower();
        }
        base.EndModifyActor();
        //Destroy(Mod_Actor.gameObject.GetComponent<MOD_DMGIncrease>());      //Destroy this script once timer is done
        Destroy(this);
    }

	public override void EndModifyEnemy()
	{
		if (enemy.Act_HasMod)
			enemy.RestoreToBasePower();
		base.EndModifyEnemy();
		//Destroy(enemy.gameObject.GetComponent<MOD_DMGIncrease>());
        Destroy(this);
	}

}

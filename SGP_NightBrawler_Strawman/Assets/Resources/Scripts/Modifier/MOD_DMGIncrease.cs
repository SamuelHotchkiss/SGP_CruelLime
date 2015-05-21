using UnityEngine;
using System.Collections;

public class MOD_DMGIncrease : MOD_Base
{
	// Use this for initialization
	public override void Start () 
    {
        buffState = MNGR_Item.BuffStates.BUFFED;
        base.Start();
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
        for (int i = 0; i < player.party.Length; i++)
        {
            float IncreaseDmgPercent = player.party[i].Act_basePower * 1.5f;     //Increse Damage by 50%
            player.party[i].SetCurrPower((int)IncreaseDmgPercent);
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
        for (int i = 0; i < player.party.Length; i++)
            player.party[i].RestoreToBasePower();
        base.EndModifyActor();
        Destroy(this);
    }

	public override void EndModifyEnemy()
	{
		if (enemy.Act_HasMod)
			enemy.RestoreToBasePower();
		base.EndModifyEnemy();
        Destroy(this);
	}

}

using UnityEngine;
using System.Collections;

public class MOD_DMGProtection : MOD_Base
{
    public bool Mod_OnceOnly;

	// Use this for initialization
	void Start () {
        buffState = MNGR_Item.BuffStates.BUFFED;
        base.Start();
        Mod_PartyWide = true;       //This Effect wil affect the whole party
        Mod_effectTimer = 30.0f;
        Mod_BaseEffectTimer = Mod_effectTimer;
        Mod_ModIndexNum = 2;
        Mod_OnceOnly = false;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void ModifyActor()
    {
        if (!Mod_OnceOnly)
        {
            for (int i = 0; i < player.party.Length; i++)
            {
                float NewDmgMod = player.party[i].damageMod * 0.5f;     //Decrese Dmagemod by half.
                player.party[i].ModifyDefense(NewDmgMod);
            }
            Mod_OnceOnly = true;
        }
    }

    public override void ModifyEnemy()
    {
        //float NewDmgMod = enemy.damageMod * 0.5f;     //Decrese Dmagemod by half.
        //enemy.ModifyDefense(NewDmgMod);
    }

    public override void EndModifyActor()
    {
        for (int i = 0; i < player.party.Length; i++)
            player.party[i].RestoreDefense();
        base.EndModifyActor();
        Destroy(this);
    }

    public override void EndModifyEnemy()
    {
        //enemy.RestoreDefense();
        base.EndModifyEnemy();
        Destroy(this);
    }
}

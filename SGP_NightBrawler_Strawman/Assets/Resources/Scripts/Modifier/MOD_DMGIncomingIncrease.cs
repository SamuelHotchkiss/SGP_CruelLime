using UnityEngine;
using System.Collections;

public class MOD_DMGIncomingIncrease : MOD_Base
{
    public bool Mod_OnceOnly;
    public int Mod_CurrCharacter;

    // Use this for initialization
    void Start()
    {
        buffState = MNGR_Item.BuffStates.DEBUFFED;
        base.Start();
        Mod_PartyWide = false;       //This Effect wil affect the whole party
        Mod_effectTimer = 20.0f;
        Mod_BaseEffectTimer = Mod_effectTimer;
        Mod_ModIndexNum = 7;
        Mod_OnceOnly = false;

        Mod_CurrCharacter = player.currChar;
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
            float NewDmgMod = player.party[Mod_CurrCharacter].damageMod * 1.15f;     //Increse Damage by 15%.
            player.party[Mod_CurrCharacter].ModifyDefense(NewDmgMod);
            Mod_OnceOnly = true;
        }
    }

    public override void ModifyEnemy()
    {
        //float NewDmgMod = enemy.damageMod * 1.15f;     //Increse Damage by 15%.
        //enemy.ModifyDefense(NewDmgMod);
    }

    public override void EndModifyActor()
    {
        player.party[Mod_CurrCharacter].RestoreDefense();
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

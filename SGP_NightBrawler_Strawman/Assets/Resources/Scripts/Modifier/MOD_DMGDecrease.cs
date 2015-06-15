using UnityEngine;
using System.Collections;

public class MOD_DMGDecrease : MOD_Base
{
    public int Mod_CurrCharacter;

    public override void Start()
    {
        myColor = Color.magenta;

        buffState = MNGR_Item.BuffStates.DEBUFFED;
        base.Start();
        Mod_PartyWide = false;       //This Effect wil affect the whole party
        Mod_effectTimer = 20.0f;
        Mod_BaseEffectTimer = Mod_effectTimer;
        Mod_ModIndexNum = 6;
        if (player != null)
            Mod_CurrCharacter = player.currChar;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void ModifyActor()
    {
        float DecreseDmgPercent = player.party[Mod_CurrCharacter].Act_basePower * 0.75f;     //Decrese Damage by 25%
        player.party[Mod_CurrCharacter].SetCurrPower(DecreseDmgPercent);
        
    }

    public override void ModifyEnemy()
    {
        float DecreseDmgPercent = enemy.Act_basePower * 1.5f;
        enemy.SetCurrPower(DecreseDmgPercent);
    }

    public override void EndModifyActor()
    {
        player.party[Mod_CurrCharacter].RestoreToBasePower();
        base.EndModifyActor();
        Destroy(this);
    }

    public override void EndModifyEnemy()
    {
        base.EndModifyEnemy();
        Destroy(this);
    }
}

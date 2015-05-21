using UnityEngine;
using System.Collections;

public class MOD_HPRegen : MOD_Base
{
    private float MHPR_Timer;       //Slows the regen from being quick
    // Use this for initialization
    public override void Start()
    {
        buffState = MNGR_Item.BuffStates.BUFFED;

        base.Start();

        //Mod_IsBuff = true;          //This is a positive effect
        Mod_PartyWide = true;       //This Effect wil affect the whole party
        Mod_effectTimer = 10.0f;
        MHPR_Timer = 0.0f;
        Mod_ModIndexNum = 4;        //Regen HP
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (!MNGR_Game.paused)
            MHPR_Timer -= Time.deltaTime;
    }


    public override void ModifyActor()
    {
        if (MHPR_Timer <= 0)
        {
            for (int i = 0; i < player.party.Length; i++)
            {
                float OnePercentHP = player.party[i].Act_baseHP * 0.01f;     //Regen 2% of the Character's Hp every second.

                if (OnePercentHP < 1.0f)
                    OnePercentHP = 1.0f;

                //if (Mod_Actor.party[i].Act_HasMod)
                player.party[i].ChangeHP((int)OnePercentHP);
            }
            MHPR_Timer = 0.5f;
        }
    }

    public override void EndModifyActor()
    {
        base.EndModifyActor();
        //Destroy(Mod_Actor.gameObject.GetComponent<MOD_HPRegen>());      //Destroy this script once timer is done
        Destroy(this);
    }

}

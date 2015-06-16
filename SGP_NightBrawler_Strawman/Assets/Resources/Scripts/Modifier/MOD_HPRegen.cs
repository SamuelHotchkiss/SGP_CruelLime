using UnityEngine;
using System.Collections;

public class MOD_HPRegen : MOD_Base
{
    private float MHPR_Timer;       //Slows the regen from being quick
    // Use this for initialization
    public override void Start()
    {
        //myColor = Color.yellow;

        buffState = MNGR_Item.BuffStates.BUFFED;
        base.Start();
        Mod_EffectColor = new Color(255, 105, 0);
        Mod_Particles.GetComponent<ParticleSystem>().startColor = Mod_EffectColor;
        Mod_PartyWide = true;       //This Effect wil affect the whole party
        Mod_effectTimer = 10.0f;
        Mod_BaseEffectTimer = Mod_effectTimer;
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
                player.party[i].ChangeHP(OnePercentHP);
            }
            MHPR_Timer = 0.5f;
        }
    }

    public override void EndModifyActor()
    {
        base.EndModifyActor();  //Destroy this script once timer is done
        Destroy(this);
    }

}

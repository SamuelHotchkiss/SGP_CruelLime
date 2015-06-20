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
        Mod_Particles = Instantiate(Resources.Load("Prefabs/Item/Heal") as GameObject, transform.position, Quaternion.identity) as GameObject;
        base.Start();
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
        //if (MHPR_Timer <= 0)
        //{
            for (int i = 0; i < player.party.Length; i++)
            {
                float OnePercentHP = player.party[i].Act_baseHP * (Time.deltaTime / 20f);     
                //if (OnePercentHP < 1.0f)
                //    OnePercentHP = 1.0f;
                if(player.party[i].Act_currHP > 0)
                    player.party[i].ChangeHP(OnePercentHP);
            }
            //MHPR_Timer = 0.5f;
        //}
    }

    public override void EndModifyActor()
    {
        base.EndModifyActor();  //Destroy this script once timer is done
        Destroy(this);
    }

}

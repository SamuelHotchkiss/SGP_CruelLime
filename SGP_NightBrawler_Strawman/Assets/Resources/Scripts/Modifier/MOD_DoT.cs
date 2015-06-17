using UnityEngine;
using System.Collections;

public class MOD_DoT : MOD_Base
{
    private float Mod_Timer;       //Slows the regen from being quick
    public int Mod_CurrCharacter;
    // Use this for initialization
    public override void Start()
    {
        //myColor = Color.green;

        buffState = MNGR_Item.BuffStates.DEBUFFED;
        base.Start();
        Mod_EffectColor = Color.green;
        Mod_Particles.GetComponent<ParticleSystem>().startColor = Mod_EffectColor;
        Mod_PartyWide = false;       //This Effect wil affect the whole party
        Mod_effectTimer = 15.0f;
        Mod_BaseEffectTimer = Mod_effectTimer;
        Mod_Timer = 0.0f;
        Mod_ModIndexNum = 8;        //DoT
        if (player != null)
            Mod_CurrCharacter = player.currChar;

    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (!MNGR_Game.paused)
            Mod_Timer -= Time.deltaTime;
    }

    public override void ModifyActor()
    {
        //if (Mod_Timer <= 0)
        //{
        float OnePercentDmg = player.party[Mod_CurrCharacter].Act_baseHP * (0.001f * Time.deltaTime);     //Reduces 2% of the Character's Hp every second.
            //if (OnePercentDmg < 1.0f)
            //    OnePercentDmg = 1.0f;
            player.party[Mod_CurrCharacter].ChangeHP(-OnePercentDmg, false);
            //Mod_Timer = 2.0f;
       // }
    }

    public override void ModifyEnemy()
    {
        //if (Mod_Timer <= 0)

        //{
            float OnePercentDmg = enemy.Act_baseHP * (0.001f * (MNGR_Game.currentParty[1].Act_currPower) * Time.deltaTime) ;     //Reduces 2% of the enemy's Hp every second.
            //if (OnePercentDmg < 1.0f)
            //    OnePercentDmg = 1.0f;
            enemy.ChangeHP(-OnePercentDmg, false);
            //Mod_Timer = 2.0f;
        //}
    }

    public override void EndModifyActor()
    {
        base.EndModifyActor();  //Destroy this script once timer is done
        Destroy(this);
    }

    public override void EndModifyEnemy()
    {
        base.EndModifyEnemy();
        Destroy(this);
    }


}

using UnityEngine;
using System.Collections;

public class MOD_Slowed : MOD_Base
{
    public bool Mod_OnceOnly;

    // Use this for initialization
    public override void Start()
    {
        buffState = MNGR_Item.BuffStates.DEBUFFED;
        Mod_Particles = Instantiate(Resources.Load("Prefabs/Item/SpeedDown") as GameObject, transform.position, Quaternion.identity) as GameObject;
        base.Start();
        Mod_PartyWide = true;       //This Effect wil affect the whole party
        Mod_effectTimer = 30.0f;
        Mod_BaseEffectTimer = Mod_effectTimer;
        Mod_ModIndexNum = 9;
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
                float TripleSpeed = player.party[i].Act_currSpeed * 0.25f;     //Decrese Speed by 25%
                player.party[i].SetCurrSpeed(TripleSpeed);
            }
            Mod_OnceOnly = true;
        }
    }

    public override void ModifyEnemy()
    {
        if (!Mod_OnceOnly)
        {
            float TripleSpeed = enemy.Act_currSpeed * 0.25f;     //Decrese Speed by 25%
            enemy.SetCurrSpeed(TripleSpeed);
            Mod_OnceOnly = true;
        }
    }

    public override void EndModifyActor()
    {
        for (int i = 0; i < player.party.Length; i++)
            player.party[i].RestoreToBaseSpeed();
        base.EndModifyActor();
        Destroy(this);
    }

    public override void EndModifyEnemy()
    {
        enemy.RestoreToBaseSpeed();
        base.EndModifyEnemy();
        Destroy(this);
    }
}

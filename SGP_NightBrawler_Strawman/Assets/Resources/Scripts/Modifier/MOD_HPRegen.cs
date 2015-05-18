using UnityEngine;
using System.Collections;

public class MOD_HPRegen : MOD_Base
{
    private float MHPR_Timer;
	// Use this for initialization
	void Start () 
    {
        Mod_IsBuff = true;
        Mod_PartyWide = true;
        Mod_CurrEffect = new MOD_HPRegen();
        Mod_effectTimer = 10.0f;
        MHPR_Timer = 0.0f;
        Mod_ModIndexNum = 4;
	}
	
	// Update is called once per frame
    public override void Update()
    {
        base.Update();
        MHPR_Timer -= Time.deltaTime;
	}

    public override void ModifyActor()
    {
        if (MHPR_Timer <= 0)
        {
            for (int i = 0; i < Mod_Actor.party.Length; i++)
            {
                float OnePercentHP = Mod_Actor.party[i].Act_baseHP * 0.01f;

                if (OnePercentHP < 1.0f)
                    OnePercentHP = 1.0f;

                if (Mod_Actor.party[i].Act_HasMod)
                    Mod_Actor.party[i].ChangeHP((int)OnePercentHP);
            }
            MHPR_Timer = 0.5f;
        }
    }

    public override void EndModifyActor()
    {
        base.EndModifyActor();
        Destroy(Mod_Actor.gameObject.GetComponent<MOD_HPRegen>());
    }

    public override void OnTriggerEnter2D(Collider2D Col)
    {
        if (Col.name == "PLY_PlayerObject")
        {
            Mod_Actor = Col.GetComponent<PlayerController>();
            if (!NullNewEffects())
            {
                Mod_Actor.party[Mod_Actor.currChar].Act_ModIsBuff = Mod_IsBuff;
                SetModEffect(Mod_IsBuff, Mod_ModIndexNum);
            }
                


            base.OnTriggerEnter2D(Col);
        }
    }
}

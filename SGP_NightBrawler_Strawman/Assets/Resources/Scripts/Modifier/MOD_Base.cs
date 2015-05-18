using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MOD_Base : Item {

    public bool Mod_IsBuff;
    public float Mod_effectTimer;
    public MOD_Base[] Mod_effects;
    public MOD_Base Mod_CurrEffect;
    public PlayerController Item_Actor;
    public bool Mod_PartyWide; 

    // Use this for initialization
    void Start()
    {
        Mod_effects = new MOD_Base[11];
        Mod_CurrEffect = null;
        Item_IsMod = true;

        //Buffs
        Mod_effects[0] = new MOD_CDDecrease();
        Mod_effects[1] = new MOD_DMGIncrease();
        Mod_effects[2] = new MOD_DMGProtection();
        Mod_effects[3] = new MOD_HPInstant();
        Mod_effects[4] = new MOD_HPRegen();
        Mod_effects[5] = new MOD_SPDIncrease();

        //Debuffs
        Mod_effects[6] = new MOD_DMGDecrease();
        Mod_effects[7] = new MOD_DMGIncomingIncrease();
        Mod_effects[8] = new MOD_DoT();
        Mod_effects[9] = new MOD_Slowed();
        Mod_effects[10] = new MOD_Stunned();
    }

	// Update is called once per frame
    public override void Update()
    {
        //base.Update();

        if (Item_Actor != null)
        {
            ModifyActor();
            Mod_effectTimer -= Time.deltaTime;
            if (Mod_effectTimer <= 0.0f)
                EndModifyActor(); 
        }
        else
            Item_Actor = transform.gameObject.GetComponent<PlayerController>();
        

	}

    public virtual void ModifyActor()
    {
        if (Item_Actor.party[Item_Actor.currChar].Act_HasMod)
        {
            if (Mod_CurrEffect.Mod_IsBuff != Item_Actor.party[Item_Actor.currChar].Act_IsModaBuff)
            {
                Mod_effectTimer = 0.0f;
                EndModifyActor();
            }
        }
        else 
        {
            if (!Mod_PartyWide)
                Item_Actor.party[Item_Actor.currChar].Act_HasMod = true;
            else if (Mod_PartyWide)
                for (int i = 0; i < Item_Actor.party.Length; i++)
                {
                    Item_Actor.party[i].Act_HasMod = true;
                }

            Mod_effectTimer = 20.0f;
        }
    }

    public virtual void EndModifyActor()
    {
        Item_Actor.party[Item_Actor.currChar].Act_HasMod = false;
    }

}

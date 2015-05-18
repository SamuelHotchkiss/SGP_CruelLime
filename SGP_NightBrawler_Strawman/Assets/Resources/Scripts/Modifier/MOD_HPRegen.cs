using UnityEngine;
using System.Collections;

public class MOD_HPRegen : MOD_Base
{
    private float MHPR_Timer;
	// Use this for initialization
	void Start () 
    {
        Item_IsMod = true;
        Mod_IsBuff = true;
        Mod_PartyWide = true;
        Mod_CurrEffect = new MOD_HPRegen();
        MHPR_Timer = 0.0f;
	}
	
	// Update is called once per frame
    public override void Update()
    {
        base.Update();
        MHPR_Timer -= Time.deltaTime;
	}

    public override void ModifyActor()
    {
        base.ModifyActor();

        if (MHPR_Timer <= 0)
        {
            for (int i = 0; i < Item_Actor.party.Length; i++)
                if (Item_Actor.party[i].Act_HasMod)
                    Item_Actor.party[i].ChangeHP(1);

            MHPR_Timer = 0.5f;
        }
    }

    public override void EndModifyActor()
    {
        base.EndModifyActor();
        Destroy(Item_Actor.gameObject.GetComponent<MOD_HPRegen>());
    }

    public override void OnTriggerEnter2D(Collider2D Col)
    {
        if (Col.name == "PLY_PlayerObject")
        {
            Item_Actor = Col.GetComponent<PlayerController>();
            Item_Actor.gameObject.AddComponent<MOD_HPRegen>();
            base.OnTriggerEnter2D(Col);
        }
    }
}

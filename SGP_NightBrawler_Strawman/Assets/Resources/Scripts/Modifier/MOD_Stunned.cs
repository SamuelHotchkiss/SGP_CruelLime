using UnityEngine;
using System.Collections;

public class MOD_Stunned : MOD_Base
{
	// Use this for initialization
	public override void Start () 
    {
        buffState = MNGR_Item.BuffStates.DEBUFFED;
        Mod_Particles = Instantiate(Resources.Load("Prefabs/Item/Stun") as GameObject, transform.position, Quaternion.identity) as GameObject;
        base.Start();
        Mod_PartyWide = true;       //This Effect wil affect the whole party
        Mod_effectTimer = 3.0f;

        Mod_BaseEffectTimer = Mod_effectTimer;
        Mod_ModIndexNum = 10;
	}
	
	// Update is called once per frame
	public override void Update () 
    {
        base.Update();
	}

    public override void ModifyActor()   //Just a virtual fuction for its children
    {
        for (int i = 0; i < player.party.Length; i++)
        {
            if(player.party[i].Act_currHP > 0)
                player.party[i].state = ACT_CHAR_Base.STATES.HURT;
        }
    }

    public override void ModifyEnemy()
    {
        enemy.state = ACT_Enemy.STATES.HURT;
    }

    public override void EndModifyActor()    //Reset the characte's HasMod Veriables.
    {
        for (int i = 0; i < player.party.Length; i++)
            player.party[i].state = ACT_CHAR_Base.STATES.IDLE;
        base.EndModifyActor();
        Destroy(this);
    }

    public override void EndModifyEnemy()    //Reset the character's HasMod Veriables.
    {
        enemy.state = ACT_Enemy.STATES.IDLE;
        base.EndModifyEnemy();
        Destroy(this);
    }

    public override void ResetEffecTimer()
    {
        Mod_effectTimer = Mod_BaseEffectTimer;
    }
}

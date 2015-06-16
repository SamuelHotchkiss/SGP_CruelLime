using UnityEngine;
using System.Collections;

public class MOD_HPInstant : MOD_Base
{

	// Use this for initialization
    public override void Start()
    {
        buffState = MNGR_Item.BuffStates.BUFFED;
        base.Start();
        Mod_PartyWide = false;      //This Effect wil affect the whole party
        Mod_effectTimer = 0.01f;
        Mod_BaseEffectTimer = Mod_effectTimer;
        Mod_ModIndexNum = 3;        //Instant HP
	}
	
	// Update is called once per frame
    public override void Update()
    {
        base.Update();
	}

    public override void ModifyActor()   //Just a virtual fuction for its children
    {
        float QuarterHeal = player.party[player.currChar].Act_baseHP * 0.25f;
        player.party[player.currChar].ChangeHP(QuarterHeal);
    }

    public override void ModifyEnemy()
    {
        //ENEMIES DONT GET THIS BUFF U FOOLS 
    }

    public override void EndModifyActor()    //Reset the characte's HasMod Veriables.
    {
        base.EndModifyActor();  //Destroy this script once timer is done
        Destroy(this);
    }

    public override void EndModifyEnemy()
    {
        //NOPE
    }
}

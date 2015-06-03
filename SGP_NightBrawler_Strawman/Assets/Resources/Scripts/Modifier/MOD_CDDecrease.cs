using UnityEngine;
using System.Collections;

public class MOD_CDDecrease : MOD_Base
{

	// Use this for initialization
	public override void Start () {

        buffState = MNGR_Item.BuffStates.BUFFED;
        base.Start();
        Mod_PartyWide = true;
        Mod_ModIndexNum = 0;            //The Number ID of the Effect 
        Mod_effectTimer = 30.0f;
        Mod_BaseEffectTimer = Mod_effectTimer;
	}
	
	// Update is called once per frame
	public override void Update () 
    {
        base.Update();
	}
    public override void ModifyActor()
    {
        for (int i = 0; i < player.party.Length; i++)
            if (player.party[i].cooldownTmr > 0.0f)
                player.party[i].cooldownTmr -= Time.deltaTime; 
    }
    public override void EndModifyActor()
    {
        base.EndModifyActor();
        Destroy(this);
    }
}

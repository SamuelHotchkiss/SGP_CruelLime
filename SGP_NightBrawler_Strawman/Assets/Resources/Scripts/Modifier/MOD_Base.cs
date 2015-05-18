using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MOD_Base : MonoBehaviour {

    public bool Mod_IsBuff;
    public float Mod_effectTimer;
    public MOD_Base[] Mod_effects;

    // Use this for initialization
    void Start()
    {

    }

	// Update is called once per frame
    public virtual void Update(){
        Mod_effectTimer -= Time.deltaTime;
        if (Mod_effectTimer <= 0.0f)
        {
            EndModifyActor();
        }
	}

    public virtual void ModifyActor()
    {
        
    }

    public virtual void EndModifyActor()
    {
        //remove 
    }

}

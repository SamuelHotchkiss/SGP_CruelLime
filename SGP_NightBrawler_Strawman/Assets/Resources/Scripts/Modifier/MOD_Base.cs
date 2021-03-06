﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MOD_Base : MonoBehaviour
{
    public MNGR_Item.BuffStates buffState;

    public bool Mod_IsBuff;             //Is true if it's a Buff and False if it's a Debuff
    public bool Mod_PartyWide;          //Does it affect the whole party? Or only the current player.

    public int Mod_ModIndexNum;         //The Number ID of the Effect 
    public float Mod_effectTimer;       //How long the effect will last
    public float Mod_BaseEffectTimer;   //Original Timer of the Mod.

    public PlayerController player;     //The Actor been afflicted with the Effect
    public ACT_Enemy enemy;
    //public Color Mod_EffectColor;       //Color of the particles.
    public GameObject Mod_Particles;

    public bool isPlayer;

    /*--- Legend ---
     ===============
    0 = MOD_CDDecrease
    1 = MOD_DMGIncrease
    2 = MOD_DMGProtection
    3 = MOD_HPInstant
    4 = MOD_HPRegen
    5 = MOD_SPDIncrease
     * 
    === Debuffs IDs === 
     * 
    6 = MOD_DMGDecrease
    7 = MOD_DMGIncomingIncrease
    8 = MOD_DoT
    9 = MOD_Slowed
    10 = MOD_Stunned
     ===============*/

    // Use this for initialization
    public virtual void Start()
    {
        Mod_ModIndexNum = -1;           //Base class
        Mod_Particles.gameObject.transform.SetParent(gameObject.transform);
        Mod_Particles.GetComponent<ParticleSystemRenderer>().sortingOrder = 0;

        #region WhatAmIAttachedTo?
        if (gameObject.tag == "Enemy")
        {
            enemy = GetComponent<ACT_Enemy>();
            isPlayer = false;

            if (enemy.buffState == MNGR_Item.BuffStates.NEUTRAL)
            {
                enemy.myBuffs.Add(this);
                enemy.buffState = buffState;
            }
            else if (enemy.buffState == buffState)
            {
                enemy.myBuffs.Add(this);
            }
            else if (enemy.buffState != buffState)
            {
                enemy.KillBuffs();
                enemy.buffState = MNGR_Item.BuffStates.NEUTRAL;
                Destroy(this);
            }
        }
        else if (gameObject.tag == "Player")
        {
            player = GetComponent<PlayerController>();
            isPlayer = true;

            if (player != null)
            {
                if (player.buffState == MNGR_Item.BuffStates.NEUTRAL)
                {
                    player.myBuffs.Add(this);
                    player.buffState = buffState;
                }
                else if (player.buffState == buffState)
                {
                    player.myBuffs.Add(this);
                }
                else if (player.buffState != buffState)
                {
                    player.KillBuffs();
                    player.buffState = MNGR_Item.BuffStates.NEUTRAL;
                    Destroy(this);
                }
            }
        }
        #endregion
    }


    // Update is called once per frame
    public virtual void Update()
    {
        if (!MNGR_Game.paused)
        {
            if (isPlayer && player != null)
            {
                ModifyActor();
                Mod_effectTimer -= Time.deltaTime;
                if (Mod_effectTimer <= 0.0f)
                    EndModifyActor();
            }
            else if (enemy != null)
            {
                ModifyEnemy();
                Mod_effectTimer -= Time.deltaTime;
                if (Mod_effectTimer <= 0.0f)
                    EndModifyEnemy();
            }
        }

        #region OldnBusted
#if false
		//bool isEnemy = false;
		if (gameObject.tag == "Enemy")
		{
            enemy = GetComponent<ACT_Enemy>();
			//isEnemy = true;
			ModifyEnemy();
			Mod_effectTimer -= Time.deltaTime;
			if (Mod_effectTimer <= 0.0f)
				EndModifyEnemy();
		}
        //else
        //{
        //    enemy = transform.gameObject.GetComponent<ACT_Enemy>();
        //    isEnemy = true;
        //}
		else if (gameObject.tag == "Player")
		{
            Mod_Actor = GetComponent<PlayerController>();
			if (Mod_Actor != null)                      //If No Actor is selected just ignore the update
			{
				ModifyActor();                          //Actually affect the Actor
				Mod_effectTimer -= Time.deltaTime;      //Reduce the Time of the effect
				if (Mod_effectTimer <= 0.0f)
					EndModifyActor();                   //Once Timer is over kill the effect.
			}
			else
				Mod_Actor = transform.gameObject.GetComponent<PlayerController>();  //Get the Actor once attach to the effect
		}    
#endif
        #endregion
    }

    public virtual void ModifyActor()   //Just a virtual fuction for its children
    {

    }

    public virtual void ModifyEnemy()
    {

    }

    public virtual void EndModifyActor()    //Reset the characte's HasMod Veriables.
    {
        int childs = transform.childCount;
        for (int i = childs - 1; i > 0; i--)
            GameObject.Destroy(transform.GetChild(i).gameObject);
        Destroy(Mod_Particles);
        if (!Mod_PartyWide)
            player.party[player.currChar].Act_HasMod = false;
        else if (Mod_PartyWide)
            for (int i = 0; i < player.party.Length; i++)
                player.party[i].Act_HasMod = false;
        player.myBuffs.Remove(this);

    }

    public virtual void EndModifyEnemy()    //Reset the character's HasMod Veriables.
    {
        int childs = transform.childCount;
        for (int i = childs - 1; i > 0; i--)
            GameObject.Destroy(transform.GetChild(i).gameObject);
        Destroy(Mod_Particles);
        enemy.myBuffs.Remove(this);
    }

    public virtual void ResetEffecTimer()
    {
        Mod_effectTimer = Mod_BaseEffectTimer;
    }
}

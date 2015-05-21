using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MOD_Base : MonoBehaviour
{
    public MNGR_Item.BuffStates buffState;

    public bool Mod_IsBuff;             //Is true if it's a Buff and False if it's a Debuff
    public bool Mod_PartyWide;          //Does it affect the whole party? Or only the current player.

    public int Mod_ModIndexNum;         //The Number ID of the Effect 
    public float Mod_effectTimer;       //How long the effect will last

    public PlayerController Mod_Actor;  //The Actor been afflicted with the Effect
	public ACT_Enemy enemy;

    public bool isPlayer;
    
    // Use this for initialization
    public virtual void Start()
    {
        Mod_ModIndexNum = -1;           //Base class

        #region WhatAmIAttachedTo?
        if (gameObject.tag == "Enemy")
        {
            enemy = GetComponent<ACT_Enemy>();
            isPlayer = false;

            if (enemy.buffState == MNGR_Item.BuffStates.NEUTRAL)
            {
                // Add to buff list
                enemy.buffState = buffState;
            }
            else if (enemy.buffState == buffState)
            {
                // Add to buff list
            }
            else if (enemy.buffState != buffState)
            {
                // Clear buff list
                enemy.buffState = MNGR_Item.BuffStates.NEUTRAL;
                Destroy(this);
            }
        }
        else if (gameObject.tag == "Player")
        {
            Mod_Actor = GetComponent<PlayerController>();
            isPlayer = true;

            if (Mod_Actor.buffState == MNGR_Item.BuffStates.NEUTRAL)
            {
                // Add to buff list
                Mod_Actor.buffState = buffState;
            }
            else if (Mod_Actor.buffState == buffState)
            {
                // Add to buff list
            }
            else if (Mod_Actor.buffState != buffState)
            {
                // Clear buff list
                Mod_Actor.buffState = MNGR_Item.BuffStates.NEUTRAL;
                Destroy(this);
            }
        }
        #endregion
    }

	// Update is called once per frame
    public virtual void Update()
    {
        if (isPlayer)
        {
            ModifyActor();
            Mod_effectTimer -= Time.deltaTime;
            if (Mod_effectTimer <= 0.0f)
                EndModifyActor();
        }
        else
        {
            ModifyEnemy();
            Mod_effectTimer -= Time.deltaTime;
            if (Mod_effectTimer <= 0.0f)
                EndModifyEnemy();
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
        if (!Mod_PartyWide)     
            Mod_Actor.party[Mod_Actor.currChar].Act_HasMod = false;
        else if (Mod_PartyWide)
            for (int i = 0; i < Mod_Actor.party.Length; i++)
                Mod_Actor.party[i].Act_HasMod = false;
    }

	public virtual void EndModifyEnemy()    //Reset the characte's HasMod Veriables.
	{
		enemy.Act_HasMod = false;
	}

#region OldnBusted
#if false
    public void SetModEffectPlayer(bool _IsItBuff, int _IndexNum)     //Selects the Buff or Debuff from the list
    {
        if (_IsItBuff)
        {
            switch (_IndexNum)
            {
                case 0:
                    Mod_Actor.gameObject.AddComponent<MOD_CDDecrease>();
                    break;
                case 1: 
                    Mod_Actor.gameObject.AddComponent<MOD_DMGIncrease>();
                    break;
                case 2:
                    Mod_Actor.gameObject.AddComponent<MOD_DMGProtection>();
                    break;
                case 3:
                    Mod_Actor.gameObject.AddComponent<MOD_HPInstant>();
                    break;
                case 4:
                    Mod_Actor.gameObject.AddComponent<MOD_HPRegen>();
                    break;
                case 5:
                    Mod_Actor.gameObject.AddComponent<MOD_SPDIncrease>();
                    break;
            }
        }
        else if (!_IsItBuff)
        {
            switch (_IndexNum)
            {
                case 0:
                    Mod_Actor.gameObject.AddComponent<MOD_DMGDecrease>();
                    break;
                case 1:
                    Mod_Actor.gameObject.AddComponent<MOD_DMGIncomingIncrease>();
                    break;
                case 2:
                    Mod_Actor.gameObject.AddComponent<MOD_DoT>();
                    break;
                case 3:
                    Mod_Actor.gameObject.AddComponent<MOD_Slowed>();
                    break;
                case 4:
                    Mod_Actor.gameObject.AddComponent<MOD_Stunned>();
                    break;
            }
        }
    }

	public void SetModEffectEnemy(bool _IsItBuff, int _IndexNum)     //Selects the Buff or Debuff from the list
	{
		if (_IsItBuff)
		{
			switch (_IndexNum)
			{
				case 0:
					enemy.gameObject.AddComponent<MOD_CDDecrease>();
					break;
				case 1:
					enemy.gameObject.AddComponent<MOD_DMGIncrease>();
					break;
				case 2:
					enemy.gameObject.AddComponent<MOD_DMGProtection>();
					break;
				case 3:
					enemy.gameObject.AddComponent<MOD_HPInstant>();
					break;
				case 4:
					enemy.gameObject.AddComponent<MOD_HPRegen>();
					break;
				case 5:
					enemy.gameObject.AddComponent<MOD_SPDIncrease>();
					break;
			}
		}
		else if (!_IsItBuff)
		{
			switch (_IndexNum)
			{
				case 0:
					enemy.gameObject.AddComponent<MOD_DMGDecrease>();
					break;
				case 1:
					enemy.gameObject.AddComponent<MOD_DMGIncomingIncrease>();
					break;
				case 2:
					enemy.gameObject.AddComponent<MOD_DoT>();
					break;
				case 3:
					enemy.gameObject.AddComponent<MOD_Slowed>();
					break;
				case 4:
					enemy.gameObject.AddComponent<MOD_Stunned>();
					break;
			}
		}
	}

    public bool NullNewEffectsPlayer()        //Checks if the current actor already has an effect 
    {
        if (Mod_Actor.party[Mod_Actor.currChar].Act_HasMod)
        {
            if (Mod_Actor.party[Mod_Actor.currChar].Act_ModIsBuff != Mod_IsBuff)
            {
                //Remove All effects in Character.
                Debug.Log("Get Reck Potion");
            }
            return true;
        }
        else
        {
            if (!Mod_PartyWide)
                Mod_Actor.party[Mod_Actor.currChar].Act_HasMod = true;
            else if (Mod_PartyWide)
                for (int i = 0; i < Mod_Actor.party.Length; i++)
                    Mod_Actor.party[i].Act_HasMod = true;
            return false;
        }
        
    }

	public bool NullNewEffectsEnemy()        //Checks if the current actor already has an effect 
	{
		if (enemy.Act_HasMod)
		{
			if (enemy.Act_ModIsBuff != Mod_IsBuff)
			{
				//Remove All effects in Character.
				Debug.Log("Get Reck Potion");
			}
			return true;
		}
		else
		{
			enemy.Act_HasMod = true;
			return false;
		}

	}
#endif
#endregion

}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MOD_Base : Item {

    public bool Mod_IsBuff;             //Is true if it's a Buff and False if it's a Debuff
    public bool Mod_PartyWide;          //Does it affect the whole party? Or only the current player.

    public int Mod_ModIndexNum;         //The Number ID of the Effect 
    public float Mod_effectTimer;       //How long the effect will last

    public PlayerController Mod_Actor;  //The Actor been aflicted with the Effect
	public ACT_Enemy enemy;
    
    // Use this for initialization
    void Start()
    {
        Mod_ModIndexNum = -1;           //Base class
        
        ////Buffs IDs
        //[0] = MOD_CDDecrease;
        //[1] = MOD_DMGIncrease();
        //[2] = MOD_DMGProtection();
        //[3] = MOD_HPInstant();
        //[4] = MOD_HPRegen();
        //[5] = MOD_SPDIncrease();
        ////Debuffs IDs
        //[0] = new MOD_DMGDecrease() 
        //[1] = new MOD_DMGIncomingIncrease() 
        //[2] = new MOD_DoT()
        //[3] = new MOD_Slowed()
        //[4] = new MOD_Stunned()
    }

	// Update is called once per frame
    public override void Update()
    {
		bool isEnemy = false;
		if (enemy != null)
		{
			isEnemy = true;
			ModifyEnemy();
			Mod_effectTimer -= Time.deltaTime;
			if (Mod_effectTimer <= 0.0f)
				EndModifyEnemy();
		}
		else
		{
			enemy = transform.gameObject.GetComponent<ACT_Enemy>();
			isEnemy = true;
		}
		if (!isEnemy)
		{
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
}

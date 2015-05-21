﻿using UnityEngine;
using System.Collections;

public static class MNGR_Item
{

    // anything that applies a buff/debuff to another object should just call this function
	public static void AttachModifier(int ID, GameObject victim)
    {
        ////Buffs IDs
        //[0] = MOD_CDDecrease;
        //[1] = MOD_DMGIncrease();
        //[2] = MOD_DMGProtection();
        //[3] = MOD_HPInstant();
        //[4] = MOD_HPRegen();
        //[5] = MOD_SPDIncrease();
        ////Debuffs IDs
        //[6] = new MOD_DMGDecrease() 
        //[7] = new MOD_DMGIncomingIncrease() 
        //[8] = new MOD_DoT()
        //[9] = new MOD_Slowed()
        //[10] = new MOD_Stunned()

        switch(ID)
        {
            case 0:
                victim.AddComponent<MOD_CDDecrease>();
                break;
            case 1:
                victim.AddComponent<MOD_DMGIncrease>();
                break;
            case 2:
                victim.AddComponent<MOD_DMGProtection>();
                break;
            case 3:
                victim.AddComponent<MOD_HPInstant>();
                break;
            case 4:
                victim.AddComponent<MOD_HPRegen>();
                break;
            case 5:
                victim.AddComponent<MOD_SPDIncrease>();
                break;
            case 6:
                victim.AddComponent<MOD_DMGDecrease>();
                break;
            case 7:
                victim.AddComponent<MOD_DMGIncomingIncrease>();
                break;
            case 8:
                victim.AddComponent<MOD_DoT>();
                break;
            case 9:
                victim.AddComponent<MOD_Slowed>();
                break;
            case 10:
                victim.AddComponent<MOD_Stunned>();
                break;
            default:
                break;
        }
    }
}

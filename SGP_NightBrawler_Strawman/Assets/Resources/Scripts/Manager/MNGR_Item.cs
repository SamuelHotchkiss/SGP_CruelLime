using UnityEngine;
using System.Collections;

public static class MNGR_Item
{

	public static void AttachModifier(int ID, GameObject victim)
    {
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
            default:
                break;
        }
    }
}

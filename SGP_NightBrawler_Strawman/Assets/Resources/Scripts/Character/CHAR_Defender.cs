using UnityEngine;
using System.Collections;

[System.Serializable]

public class CHAR_Defender : ACT_CHAR_Base
{
	public CHAR_Defender()
	{
        name = "Defender"; // OF THE POLYVERSE!
		characterIndex = 2;
		cooldownTmr = 0;

		Act_baseHP = 100;
		Act_basePower = 100;
		Act_baseSpeed = 100;
		Act_currHP = 100;
		Act_currPower = 100;
        Act_currSpeed = 100;

        Act_HPLevel = 1;
        Act_PowerLevel = 1;
        Act_SpeedLevel = 1;

        // Remove this comment when the below set of stuff has been modified to be different from the Swordsman's
        ProjFilePaths = new string[3];
        ProjFilePaths[0] = "Prefabs/Projectile/PROJ_Melee";
        ProjFilePaths[1] = "Prefabs/Projectile/PROJ_Melee";
        ProjFilePaths[2] = "Prefabs/Projectile/PROJ_Whirlwind";

        //-----Labels4dayz-----   IDLE, WALK, DODGE, ATT1, ATT2, ATT3, SPEC, HURT, DED,  USE,  DANCE
        StateTmrs = new float[] { 2.0f, 0.75f, 0.1f, 0.6f, 0.5f, 0.8f, 1.0f, 0.1f, 1.0f, 1.0f, 1.2f };

        attack1Sprites = new int[] { 10, 11, 12, 13 };
        attack2Sprites = new int[] { 15, 16, 17 };
        attack3Sprites = new int[] { 20, 21, 22, 23 };
        specialSprites = new int[] { 10, 11, 25, 26 };

        // These should always be the same, Start() just doesn't work well enough in ACT_CHAR_Base
        idleSprites = new int[] { 0, 1, 2 };
        walkSprites = new int[] { 5, 6, 7, 8, 9 };
        hurtSprites = new int[] { 30 };
        deadSprites = new int[] { 30, 31 };
	}

	// Use this for initialization
	public override void Start() 
	{
		base.Start();
	}
	
	// Update is called once per frame
	public override void Update()
	{
		base.Update();
	}


    public override AttackInfo ActivateDance(float _curTmr, float _maxTmr)
    {
        AttackInfo ret = new AttackInfo(0);

        if (_curTmr > _maxTmr * 0.5f)
            ret.spriteIndex = attack3Sprites[0];
        else if (_curTmr >= 0.0)
            ret.spriteIndex = attack3Sprites[1];

        return ret;
    }


}

using UnityEngine;
using System.Collections;

[System.Serializable]
public class CHAR_Wizard : ACT_CHAR_Base
{
	public CHAR_Wizard()
	{
		characterIndex = 6;
		cooldownTmr = 0;

		Act_baseHP = 100;
		Act_basePower = 100;
        Act_baseSpeed = 10;
        //Act_baseSpeed = 20;
        Act_baseAspeed = 0.02f;

        // Remove this comment when the below set of stuff has been modified to be different from the Swordsman's
        ProjFilePaths = new string[3];
        ProjFilePaths[0] = "Prefabs/Projectile/PROJ_Melee";
        ProjFilePaths[1] = "Prefabs/Projectile/PROJ_Melee";
        ProjFilePaths[2] = "Prefabs/Projectile/PROJ_Whirlwind";

        //-----Labels4dayz-----   IDLE, WALK, DODGE, ATT1, ATT2, ATT3, SPEC, HURT, DED,  USE
        StateTmrs = new float[] { 2.0f, 0.75f, 0.1f, 0.6f, 0.5f, 0.8f, 1.0f, 0.1f, 1.0f, 1.0f };
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
}

using UnityEngine;
using System.Collections;

[System.Serializable]

public class CHAR_Ninja : ACT_CHAR_Base
{
	public CHAR_Ninja()
	{
        name = "Ninja"; // She'd tell you her real name, but then she'd have to kill you
		characterIndex = 0;
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

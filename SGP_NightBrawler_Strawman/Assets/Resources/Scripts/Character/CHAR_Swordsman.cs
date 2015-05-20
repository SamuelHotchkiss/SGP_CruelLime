using UnityEngine;
using System.Collections;

[System.Serializable]
public class CHAR_Swordsman : ACT_CHAR_Base {

	public CHAR_Swordsman()
	{
		characterIndex = 0;
		cooldownTmr = 0;

		Act_baseHP = 100;
		Act_basePower = 10;
        Act_baseSpeed = 10;
        //Act_baseSpeed = 25;
        Act_baseAspeed = 0.025f;

        ProjFilePaths = new string[2];
        ProjFilePaths[0] = "Prefabs/Projectile/PROJ_Melee";
        ProjFilePaths[1] = "Prefabs/Projectile/PROJ_Whirlwind";

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

using UnityEngine;
using System.Collections;

[System.Serializable]

public class CHAR_Lancer : ACT_CHAR_Base
{
	public CHAR_Lancer()
	{
		characterIndex = 0;
		cooldownTmr = 0;

		Act_baseHP = 100;
		Act_basePower = 100;
		Act_baseSpeed = 100;
		Act_currHP = 100;
		Act_currPower = 100;
        Act_currSpeed = 100;

        // Remove this comment when the below set of timers has been modified to be different from the Swordsman's
        //-----Labels4dayz-----   IDLE, WALK, DODGE, ATT1, ATT2, ATT3, SPEC, HURT, DED,  USE
        StateTmrs = new float[] { 2.0f, 0.75f, 0.1f, 1.0f, 0.9f, 1.2f, 1.0f, 0.1f, 1.0f, 1.0f };
	}

	// Use this for initialization
	public override void Start()
	{
		base.Start();
	}

	// Update is called once per frame
	public void Update()
	{
		base.Update();
	}
}

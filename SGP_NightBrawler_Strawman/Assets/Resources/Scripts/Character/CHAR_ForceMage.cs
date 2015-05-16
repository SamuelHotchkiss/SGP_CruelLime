using UnityEngine;
using System.Collections;

[System.Serializable]

public class CHAR_ForceMage : ACT_CHAR_Base
{
	public CHAR_ForceMage()
	{
		characterIndex = 0;
		cooldownTmr = 0;

		Act_baseHP = 100;
		Act_basePower = 100;
		Act_baseSpeed = 100;
		Act_currHP = 100;
		Act_currPower = 100;
		Act_currSpeed = 100;
	}

	// Use this for initialization
	void Start()
	{
		base.Start();
	}

	// Update is called once per frame
	public void Update()
	{
		base.Update();
	}
}

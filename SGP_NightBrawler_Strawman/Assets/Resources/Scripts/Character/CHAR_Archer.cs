using UnityEngine;
using System.Collections;

[System.Serializable]

public class CHAR_Archer : ACT_CHAR_Base
{
	public CHAR_Archer()
	{
		characterIndex = 3;
		cooldownTmr = 0;

		Act_baseHP = 100;
		Act_basePower = 100;
		Act_baseSpeed = 15;
		Act_currHP = 100;
		Act_currPower = 100;
		Act_currSpeed = 100;

		//sprites = Resources.LoadAll<Sprite>("Sprites/Player/Archer");
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

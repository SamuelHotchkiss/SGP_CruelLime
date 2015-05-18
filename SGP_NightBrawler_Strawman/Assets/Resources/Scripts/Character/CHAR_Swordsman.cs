using UnityEngine;
using System.Collections;

[System.Serializable]
public class CHAR_Swordsman : ACT_CHAR_Base {

	public CHAR_Swordsman()
	{
		characterIndex = 0;
		cooldownTmr = 0;

		Act_baseHP = 100;
		Act_basePower = 100;
		Act_baseSpeed = 10;
		Act_currHP = 100;
		Act_currPower = 0;
		Act_currSpeed = 100;

		//sprites = Resources.LoadAll<Sprite>("Sprites/Player/Warrior");
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

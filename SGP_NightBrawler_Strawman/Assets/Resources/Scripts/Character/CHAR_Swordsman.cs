using UnityEngine;
using System.Collections;

[System.Serializable]
public class CHAR_Swordsman : ACT_CHAR_Base {


	public CHAR_Swordsman()
	{
		characterIndex = 0;
		cooldownTmr = 0;

		Act_baseHP = 100;
        Act_currHP = Act_baseHP;

		Act_basePower = 10;
        Act_baseSpeed = 10;
        //Act_baseSpeed = 25;
        Act_baseAspeed = 0.025f;

        ProjFilePaths = new string[3];
        ProjFilePaths[0] = "Prefabs/Projectile/PROJ_Melee";
        ProjFilePaths[1] = "Prefabs/Projectile/PROJ_Melee";
        ProjFilePaths[2] = "Prefabs/Projectile/PROJ_Whirlwind";

        //-----Labels4dayz-----   IDLE, WALK, DODGE, ATT1, ATT2, ATT3, SPEC, HURT, DED,  USE
        StateTmrs = new float[] { 2.0f, 0.75f, 0.1f, 0.6f, 0.5f, 0.8f, 1.0f, 0.1f, 1.0f, 1.0f };

        specialSprites = new int[] { 10, 11, 25, 26 };
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

    public override SpecialInfo ActivateSpecial(float _curTmr, float _maxTmr)
    {
        SpecialInfo ret = new SpecialInfo(0, Vector2.zero, false);

        if (_curTmr > _maxTmr * 0.95f)
            ret.spriteIndex = specialSprites[0];
        else if (_curTmr > _maxTmr * 0.7f)
            ret.spriteIndex = specialSprites[1];
        else if (_curTmr + Time.deltaTime * 2.0f > _maxTmr * 0.7f)
        {
            if (Act_facingRight)
                ret.velocity = new Vector2(2.0f, 0.0f);
            else
                ret.velocity = new Vector2(-2.0f, 0.0f);
        }
        else if (_curTmr >= 0)
        {
            if ((int)(_curTmr * 1000) % 27 > 9)
                ret.spriteIndex = specialSprites[2];
            else
                ret.spriteIndex = specialSprites[3];
        }
        if (_curTmr < _maxTmr * 0.7f)
            ret.spawnproj = true;

        return ret;
    }


}

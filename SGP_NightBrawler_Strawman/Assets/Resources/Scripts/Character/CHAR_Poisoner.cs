using UnityEngine;
using System.Collections;

[System.Serializable]

public class CHAR_Poisoner : ACT_CHAR_Base
{
	public CHAR_Poisoner()
	{
        name = "Poisoner"; // Or is it Venomer? Or Toxiner?
		characterIndex = 5;
        cooldownTmr = 0;
        cooldownTmrBase = 3.0f;

        Act_ActID = 5;

        Act_baseHP = 75;
        Act_currHP = Act_baseHP;

		Act_basePower = 7;
		Act_baseSpeed = 15;
		//Act_currHP = 100;
		//Act_currPower = 100;
        //Act_currSpeed = 100;
        Act_baseAspeed = 0.02f;

        Act_HPLevel = 1;
        Act_PowerLevel = 1;
        Act_SpeedLevel = 1;

        // Remove this comment when the below set of stuff has been modified to be different from the Swordsman's
		ProjFilePaths = new string[4];
        ProjFilePaths[0] = "Prefabs/Projectile/PROJ_DartPoison";
        ProjFilePaths[1] = "Prefabs/Projectile/PROJ_DartPoisonStrong";
        ProjFilePaths[2] = "Prefabs/Projectile/PROJ_DartDebuff";
		ProjFilePaths[3] = "Prefabs/Projectile/PROJ_DartDebuff";

        //-----Labels4dayz-----   IDLE, WALK, DODGE, ATT1, ATT2, ATT3, SPEC, HURT, DED,  USE,  DANCE
        StateTmrs = new float[] { 2.0f, 0.75f, 0.1f, 0.6f, 0.5f, 0.8f, 1.0f, 0.1f, 1.0f, 1.0f, 1.0f };

        attack1Sprites = new int[] { 10, 11, 12, 13 };
        attack2Sprites = new int[] { 15, 16, 13 };
        attack3Sprites = new int[] { 15, 16, 13 };
        specialSprites = new int[] { 25, 26, 27, 28 };

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

    public override AttackInfo ActivateDodge(float _curTmr, float _maxTmr)
    {
        AttackInfo ret = new AttackInfo(0, Vector2.zero, Vector3.zero, false);

        ret.spriteIndex = walkSprites[3];

        return ret;
    }
    public override AttackInfo ActivateAttack1(float _curTmr, float _maxTmr)
    {
        AttackInfo ret = new AttackInfo(0, Vector2.zero, Vector3.zero, false);

        if (_curTmr > _maxTmr * 0.75f)
            ret.spriteIndex = attack1Sprites[0];
        else if (_curTmr > _maxTmr * 0.5f)
            ret.spriteIndex = attack1Sprites[1];
        else if (_curTmr > _maxTmr * 0.4f)
            ret.spriteIndex = attack1Sprites[2];
        else if (_curTmr >= 0)
            ret.spriteIndex = attack1Sprites[3];
        if (_curTmr < _maxTmr * 0.5f)
            ret.spawnproj = true;

        return ret;
    }
    public override AttackInfo ActivateAttack2(float _curTmr, float _maxTmr)
    {
        AttackInfo ret = new AttackInfo(0, Vector2.zero, Vector3.zero, false);

        if (_curTmr > _maxTmr * 0.66f)
            ret.spriteIndex = attack2Sprites[0];
        else if (_curTmr > _maxTmr * 0.33f)
            ret.spriteIndex = attack2Sprites[1];
        else if (_curTmr >= 0)
            ret.spriteIndex = attack2Sprites[2];

        return ret;
    }
    public override AttackInfo ActivateAttack3(float _curTmr, float _maxTmr)
    {
        AttackInfo ret = new AttackInfo(0, Vector2.zero, Vector3.zero, false);

        if (_curTmr > _maxTmr * 0.66f)
            ret.spriteIndex = attack2Sprites[0];
        else if (_curTmr > _maxTmr * 0.33f)
            ret.spriteIndex = attack2Sprites[1];
        else if (_curTmr >= 0)
            ret.spriteIndex = attack2Sprites[2];

        return ret;
    }

    public override AttackInfo ActivateSpecial(float _curTmr, float _maxTmr)
    {
        AttackInfo ret = new AttackInfo(0, Vector2.zero, Vector3.zero, false);

        if (_curTmr > _maxTmr * 0.8f)
            ret.spriteIndex = specialSprites[0];
        else if (_curTmr > _maxTmr * 0.6f)
            ret.spriteIndex = specialSprites[1];
        else if (_curTmr > _maxTmr * 0.5f)
            ret.spriteIndex = specialSprites[2];
        else if (_curTmr >= 0)
            ret.spriteIndex = specialSprites[3];

        if (_curTmr < _maxTmr * 0.6f)
            ret.spawnproj = true;

        return ret;
    }

    public override AttackInfo ActivateDance(float _curTmr, float _maxTmr)
    {
        AttackInfo ret = new AttackInfo(0);

        if (_curTmr > _maxTmr * 0.75f)
            ret.spriteIndex = walkSprites[4];
        else if (_curTmr > _maxTmr * 0.5f)
            ret.spriteIndex = attack1Sprites[0];
        else if (_curTmr > _maxTmr * 0.25f)
            ret.spriteIndex = attack1Sprites[1];
        else if (_curTmr >= 0.0)
            ret.spriteIndex = attack1Sprites[0];

        return ret;
    }

	public override AttackInfo ActivateMasterSpecial(float _curTmr, float _maxTmr)
	{
		AttackInfo ret = new AttackInfo(0, Vector2.zero, Vector3.zero, false);

		if (_curTmr > _maxTmr * 0.8f)
			ret.spriteIndex = specialSprites[0];
		else if (_curTmr > _maxTmr * 0.6f)
			ret.spriteIndex = specialSprites[1];
		else if (_curTmr > _maxTmr * 0.5f)
			ret.spriteIndex = specialSprites[2];
		else if (_curTmr >= 0)
			ret.spriteIndex = specialSprites[3];

		if (_curTmr < _maxTmr * 0.6f)
			ret.spawnproj = true;

		return ret;
	}

	public override void UpgradeSpecial()
	{
	}
}

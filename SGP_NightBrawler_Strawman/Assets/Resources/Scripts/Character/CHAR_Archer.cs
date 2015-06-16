using UnityEngine;
using System.Collections;

[System.Serializable]

public class CHAR_Archer : ACT_CHAR_Base
{
    public CHAR_Archer()
    {
        name = "Archer"; // Sterling Archer, Codename: DUCHESS

        characterIndex = 3;
        cooldownTmr = 0;
        cooldownTmrBase = 3.0f;

        Act_ActID = 3;

        Act_baseHP = 75;
        Act_currHP = Act_baseHP;

        Act_basePower = 10;
        Act_baseSpeed = 15;
        Act_currSpeed = Act_baseSpeed;
        //Act_baseSpeed = 32;
        Act_baseAspeed = 0.02f;

        Act_HPLevel = 1;
        Act_PowerLevel = 1;
        Act_SpeedLevel = 1;

        ProjFilePaths = new string[4];
        ProjFilePaths[0] = "Prefabs/Projectile/PROJ_Arrow";
        ProjFilePaths[1] = "Prefabs/Projectile/PROJ_ArrowThree";
        ProjFilePaths[2] = "Prefabs/Projectile/PROJ_ArrowPierce";
		ProjFilePaths[3] = "Prefabs/Projectile/PROJ_ArrowPierce";

        //-----Labels4dayz-----   IDLE, WALK, DODGE, ATT1, ATT2, ATT3, SPEC, HURT, DED,  USE,  DANCE
        StateTmrs = new float[] { 2.0f, 0.75f, 0.1f, 0.5f, 0.4f, 0.7f, 0.7f, 0.1f, 1.0f, 1.0f, 0.8f };

        attack1Sprites = new int[] { 10, 11, 12, 13 };
        attack2Sprites = new int[] { 15, 16, 17 };
        attack3Sprites = new int[] { 20, 21, 22, 23 };
        specialSprites = new int[] { 10, 11, 12, 13 };

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
        //if (_curTmr < _maxTmr * 0.66f)
            //ret.spawnproj = true;

        return ret;
    }
    public override AttackInfo ActivateAttack3(float _curTmr, float _maxTmr)
    {
        AttackInfo ret = new AttackInfo(0, Vector2.zero, Vector3.zero, false);

        if (_curTmr > _maxTmr * 0.75f)
            ret.spriteIndex = attack3Sprites[0];
        else if (_curTmr > _maxTmr * 0.5f)
            ret.spriteIndex = attack3Sprites[1];
        else if (_curTmr > _maxTmr * 0.4f)
            ret.spriteIndex = attack3Sprites[2];
        else if (_curTmr >= 0)
            ret.spriteIndex = attack3Sprites[3];
        //if (_curTmr < _maxTmr * 0.5f)
            //ret.spawnproj = true;

        return ret;
    }

    public override AttackInfo ActivateSpecial(float _curTmr, float _maxTmr)
    {
        AttackInfo ret = new AttackInfo(0, Vector2.zero, Vector3.zero, false);

        if (_curTmr > _maxTmr * 0.9f)
            ret.spriteIndex = specialSprites[0];
        else if (_curTmr > _maxTmr * 0.8f)
            ret.spriteIndex = specialSprites[1];
        else if (_curTmr > _maxTmr * 0.7f)
            ret.spriteIndex = specialSprites[2];
        else if (_curTmr >= 0)
            ret.spriteIndex = specialSprites[3];

        //if (_curTmr < _maxTmr * 0.1f)
            //ret.spawnproj = true;


        return ret;
    }

    public override AttackInfo ActivateDance(float _curTmr, float _maxTmr)
    {
        AttackInfo ret = new AttackInfo(0);

        if (_curTmr > _maxTmr * 0.5f)
            ret.spriteIndex = idleSprites[2];
        else if (_curTmr >= 0.0)
            ret.spriteIndex = hurtSprites[0];

        return ret;
    }

	public override AttackInfo ActivateMasterSpecial(float _curTmr, float _maxTmr)
	{
		AttackInfo ret = new AttackInfo(0, Vector2.zero, Vector3.zero, false);

		if (_curTmr > _maxTmr * 0.9f)
			ret.spriteIndex = specialSprites[0];
		else if (_curTmr > _maxTmr * 0.8f)
			ret.spriteIndex = specialSprites[1];
		else if (_curTmr > _maxTmr * 0.7f)
			ret.spriteIndex = specialSprites[2];
		else if (_curTmr >= 0)
			ret.spriteIndex = specialSprites[3];

		//if (_curTmr < _maxTmr * 0.1f)
		//ret.spawnproj = true;


		return ret;
	}

	public override void UpgradeSpecial()
	{
	}
}

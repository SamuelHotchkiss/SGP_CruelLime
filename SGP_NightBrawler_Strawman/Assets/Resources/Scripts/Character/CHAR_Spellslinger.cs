using UnityEngine;
using System.Collections;

[System.Serializable]

public class CHAR_Spellslinger : ACT_CHAR_Base 
{
	public CHAR_Spellslinger()
	{
        name = "Spellslinger"; // she renamed herself after her brief stint in prison, her street name was Dopeslinger
		characterIndex = 8;
		cooldownTmr = 0;

		Act_baseHP = 50;
		Act_basePower = 5;
		Act_baseSpeed = 10;
		Act_currHP = Act_baseHP;
		Act_currPower = Act_basePower;
        Act_currSpeed = Act_baseSpeed;
        Act_baseAspeed = 0.15f;

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

    public override AttackInfo ActivateDodge(float _curTmr, float _maxTmr)
    {
        AttackInfo ret = new AttackInfo(0, Vector2.zero, Vector3.zero, false);

        ret.spriteIndex = walkSprites[4];

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
        if (_curTmr < _maxTmr * 0.66f)
            ret.spawnproj = true;

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
        if (_curTmr < _maxTmr * 0.5f)
            ret.spawnproj = true;

        ret.newpos = new Vector3(0.35f, 0.0f, 0.0f);

        return ret;
    }

    // called in the animation manager to get information on what to animate, for when, how long, etc
    public override AttackInfo ActivateSpecial(float _curTmr, float _maxTmr)
    {
        AttackInfo ret = new AttackInfo(0, Vector2.zero, Vector3.zero, false);

        if (_curTmr > _maxTmr * 0.9f)
            ret.spriteIndex = specialSprites[0];
        else if (_curTmr > _maxTmr * 0.5f)
            ret.spriteIndex = specialSprites[1];
        else if (_curTmr > _maxTmr * 0.4f)
            ret.spriteIndex = specialSprites[2];
        else if (_curTmr >= 0)
            ret.spriteIndex = specialSprites[3];

        //if (_curTmr < _maxTmr * 0.1f)
        //ret.spawnproj = true;

        return ret;
    }
}

using UnityEngine;
using System.Collections;

[System.Serializable]
public class ACT_CHAR_Base : ACT_Base
{
    public struct AttackInfo
    {
        public int spriteIndex;
        public Vector2 velocity;
        public Vector3 newpos;
        public bool spawnproj;
        public bool enableCollision;
        public int physicsLayer;
        public float damMult;
        public AttackInfo(int _sprdex, Vector2 _vel = default(Vector2), Vector3 _pos = default(Vector3),
            bool _spawn = false, bool _enabCol = true, int _phylay = 8, float _damMult = 1.0f)
        {
            spriteIndex = _sprdex;
            velocity = _vel;
            newpos = _pos;
            spawnproj = _spawn;
            enableCollision = _enabCol;
            physicsLayer = _phylay;
            damMult = _damMult;
        }
    }
                        //      0,      1,      2,
	public enum STATES { IDLE = 0, WALKING, DASHING, 
        /*  3,          4,      5,      6,      7,      8,   9,   10*/
		ATTACK_1, ATTACK_2, ATTACK_3, SPECIAL, HURT, DYING, USE, DANCE };
	public STATES state;

    public float damageMod;             // S: lessens or increases damage taken
    public float chargeTimerMax;
    public float chargeTimer;
    public float chargeDur;         // chargeTimerMax - chargeTimer = chargeDur = the length we held down the button.

	public float cooldownTmrBase;
    public float cooldownTmr;
    public float invulMaxTmr;
    public float invulTmr;
    public float[] StateTmrs;        // durations for different states
    public int characterIndex;

    public string[] ProjFilePaths;

    // taken from the animation manager.  debating whether or not to do this with all sprites.
    // debate over.  prosecution wins.
    public int[] idleSprites;
    public int[] walkSprites;
    public int[] attack1Sprites;
    public int[] attack2Sprites;
    public int[] attack3Sprites;
    public int[] specialSprites;
    public int[] hurtSprites;
    public int[] deadSprites;       

    public string name;

	// Use this for initialization
	public virtual void Start () 
	{
        damageMod = 1.0f;

        cooldownTmr = 0.0f;
		//cooldownTmrBase = 3.0f;
        invulMaxTmr = 2.0f;
        invulTmr = 0.0f;

        Act_currPower = Act_basePower;
        Act_currSpeed = Act_baseSpeed;
        Act_currAspeed = Act_baseAspeed;

	}
	
	// Update is called once per frame
	public virtual void Update () 
	{
		cooldownTmr -= Time.deltaTime;
		if (cooldownTmr < 0)
		{
			cooldownTmr = 0;
		}

        // update  the invulnerability timer
        if (invulTmr > 0.0f)
        {
            invulTmr -= Time.deltaTime;
            if (invulTmr < 0.0f)
            {
                invulTmr = 0.0f;
                state = STATES.IDLE;
            }
        }
	}

    // L: kind of an oxy-moron.
    public virtual AttackInfo ActivateIdle(float _curTmr, float _maxTmr)
    {
        AttackInfo ret = new AttackInfo(0);

        if (_curTmr > _maxTmr * 0.6f)
            ret.spriteIndex = idleSprites[0];
        else if (_curTmr > _maxTmr * 0.5f)
            ret.spriteIndex = idleSprites[1];
        else if (_curTmr > _maxTmr * 0.1f)
            ret.spriteIndex = idleSprites[2];
        else if (_curTmr >= 0)
            ret.spriteIndex = idleSprites[1];

        return ret;
    }
    // L: Not to be confused with ActivateWok.
    public virtual AttackInfo ActivateWalk(float _curTmr, float _maxTmr)
    {
        AttackInfo ret = new AttackInfo(0);

        if (_curTmr > _maxTmr * 0.8f)
            ret.spriteIndex = walkSprites[0];
        else if (_curTmr > _maxTmr * 0.6f)
            ret.spriteIndex = walkSprites[1];
        else if (_curTmr > _maxTmr * 0.4f)
            ret.spriteIndex = walkSprites[2];
        else if (_curTmr > _maxTmr * 0.2f)
            ret.spriteIndex = walkSprites[3];
        else if (_curTmr >= 0)
            ret.spriteIndex = walkSprites[4];

        return ret;
    }
    public virtual AttackInfo ActivateDodge(float _curTmr, float _maxTmr)
    {
        AttackInfo ret = new AttackInfo(0);

        ret.spriteIndex = walkSprites[1];

        return ret;
    }
    public virtual AttackInfo ActivateAttack1(float _curTmr, float _maxTmr)
    {
        AttackInfo ret = new AttackInfo(0);

        return ret;
    }
    public virtual AttackInfo ActivateAttack2(float _curTmr, float _maxTmr)
    {
        AttackInfo ret = new AttackInfo(0);

        return ret;
    }
    public virtual AttackInfo ActivateAttack3(float _curTmr, float _maxTmr)
    {
        AttackInfo ret = new AttackInfo(0);

        return ret;
    }
    public virtual AttackInfo ActivateSpecial(float _curTmr, float _maxTmr)
    {
        AttackInfo ret = new AttackInfo(0);

        return ret;
	}
    public virtual AttackInfo ActivateHurt(float _curTmr, float _maxTmr)
    {
        AttackInfo ret = new AttackInfo(0);

        ret.spriteIndex = hurtSprites[0];

        return ret;
    }
    public virtual AttackInfo ActivateDying(float _curTmr, float _maxTmr)
    {
        AttackInfo ret = new AttackInfo(0);

        if (_curTmr > _maxTmr * 0.5f)
        ret.spriteIndex = deadSprites[0];
        else if (_curTmr >= 0)
            ret.spriteIndex = deadSprites[1];

        return ret;
    }
    public virtual AttackInfo ActivateDance(float _curTmr, float _maxTmr)
    {
        AttackInfo ret = new AttackInfo(0);

        return ret;
    }

    public void ChangeHP(int Dmg, bool Flinch = true)                           //Applies current HP by set amount can be use to Heal as well
	{                                                                           //Damage needs to be negative.
		if (state != STATES.DYING && state != STATES.HURT && invulTmr == 0.0f)
		{

            if (Dmg < 0)
                Act_currHP += (int)(Dmg * damageMod);
            else
                Act_currHP += Dmg;

			if (Act_currHP > Act_baseHP)
				Act_currHP = Act_baseHP;

            if (Dmg < 0 && Flinch)
            {
                state = STATES.HURT;
                invulTmr = StateTmrs[(int)STATES.HURT] + invulMaxTmr;
            }

			if (Act_currHP < 0)
			{
				state = STATES.DYING;
				Act_currHP = 0;
			}
		}
	}

    public void ModifyDefense(float newDefense)
    {
        damageMod = newDefense;
    }

    public void RestoreDefense()
    {
        damageMod = 1.0f;
    }

    virtual protected void ChargeSpecial(bool isCharging)
    {
        
    }
}

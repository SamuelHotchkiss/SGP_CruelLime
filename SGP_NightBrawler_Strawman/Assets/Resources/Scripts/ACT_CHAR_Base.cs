using UnityEngine;
using System.Collections;

[System.Serializable]
public class ACT_CHAR_Base : ACT_Base
{
    public struct AttackInfo
    {
        public int spriteIndex;
        public Vector2 velocity;
        public bool spawnproj;
        public AttackInfo(int _sprdex, Vector2 _vel, bool _spawn)
        {
            spriteIndex = _sprdex;
            velocity = _vel;
            spawnproj = _spawn;
        }
    }
                        //      0,      1,      2,
	public enum STATES { IDLE = 0, WALKING, DASHING, 
        /*  3,          4,      5,      6,      7,      8,   9*/
		ATTACK_1, ATTACK_2, ATTACK_3, SPECIAL, HURT, DYING, USE };
	public STATES state;

	public float cooldownTmrBase;
    public float cooldownTmr;
    public float[] StateTmrs;        // durations for different states
    public int characterIndex;

    public string[] ProjFilePaths;

    // taken from the animation manager.  debating whether or not to do this with all sprites.
    // debate over.  prosecution wins.
    public int[] attack1Sprites;
    public int[] attack2Sprites;
    public int[] attack3Sprites;
    public int[] specialSprites;       

    public string name;

	// Use this for initialization
	public virtual void Start () 
	{
        name = "Default";

		cooldownTmrBase = 3;
		cooldownTmr = 0;

        Act_currHP = Act_baseHP;
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
        
	}


    public virtual AttackInfo ActivateAttack1(float _curTmr, float _maxTmr)
    {
        AttackInfo ret = new AttackInfo(0, Vector2.zero, false);

        return ret;
    }
    public virtual AttackInfo ActivateAttack2(float _curTmr, float _maxTmr)
    {
        AttackInfo ret = new AttackInfo(0, Vector2.zero, false);

        return ret;
    }
    public virtual AttackInfo ActivateAttack3(float _curTmr, float _maxTmr)
    {
        AttackInfo ret = new AttackInfo(0, Vector2.zero, false);

        return ret;
    }
    public virtual AttackInfo ActivateSpecial(float _curTmr, float _maxTmr)
	{
        AttackInfo ret = new AttackInfo(0, Vector2.zero, false);

        return ret;
	}

	public void ChangeHP(int Dmg)       //Applies current HP by set amount can be use to Heal as well
	{                                   //Damage needs to be negative.
		if (state != STATES.DYING && state != STATES.HURT)
		{
			
			Act_currHP += Dmg;

			if (Act_currHP > Act_baseHP)
				Act_currHP = Act_baseHP;

			if (Dmg < 0)
				state = STATES.HURT;


			if (Act_currHP < 0)
			{
				state = STATES.DYING;
				Act_currHP = 0;
			}

		}

	}
}

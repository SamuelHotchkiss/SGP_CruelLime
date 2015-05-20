using UnityEngine;
using System.Collections;

[System.Serializable]
public class ACT_CHAR_Base : ACT_Base 
{
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

	// Use this for initialization
	public virtual void Start () 
	{
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


	public virtual void ActivateSpecial()
	{

	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]

public class CHAR_Lancer : ACT_CHAR_Base
{
    public int NumLeaps;
    List<GameObject> Ltargets;
    public CHAR_Lancer()
    {
        name = "Lancer"; // not to be confused with Lancelot, or his lesser known cousin, Lancealitte
        characterIndex = 1;
        cooldownTmr = 0;
        cooldownTmrBase = 3.0f;
        chargeTimerMax = 1.0f;
        chargeTimer = chargeTimerMax;

        Act_ActID = 1;

        Act_baseHP = 100;
        Act_currHP = Act_baseHP;

        Act_baseHP = 100;
        Act_basePower = 10;
        Act_baseSpeed = 13;
        Act_baseAspeed = 0.015f;

        Act_HPLevel = 1;
        Act_PowerLevel = 1;
        Act_SpeedLevel = 1;

        // Remove this comment when the below set of stuff has been modified to be different from the Swordsman's
        ProjFilePaths = new string[4];
        ProjFilePaths[0] = "Prefabs/Projectile/PROJ_Lancer_Melee";
        ProjFilePaths[1] = "Prefabs/Projectile/PROJ_Lancer_Melee";
        ProjFilePaths[2] = "Prefabs/Projectile/PROJ_Lancer_Melee";
        ProjFilePaths[3] = "Prefabs/Projectile/PROJ_Lancer_Melee";

        //-----Labels4dayz-----   IDLE, WALK, DODGE, ATT1, ATT2, ATT3, SPEC, HURT, DED,  USE,  DANCE
        StateTmrs = new float[] { 1.0f, 0.75f, 0.1f, 0.8f, 0.7f, 0.6f, 0.6f, 0.1f, 1.0f, 1.0f, 0.75f };

        attack1Sprites = new int[] { 10, 11, 12, 13 };
        attack2Sprites = new int[] { 15, 16, 17 };
        attack3Sprites = new int[] { 20, 21, 22, 23 };
        specialSprites = new int[] { 25, 26, 27, 28 };

        // These should always be the same, Start() just doesn't work well enough in ACT_CHAR_Base
        idleSprites = new int[] { 0, 1, 2 };
        walkSprites = new int[] { 5, 6, 7, 8, 9 };
        hurtSprites = new int[] { 30 };
        deadSprites = new int[] { 30, 31 };

        Ltargets = new List<GameObject>();
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
        AttackInfo ret = new AttackInfo(0);

        ret.spriteIndex = walkSprites[4];

        return ret;
    }
    public override AttackInfo ActivateAttack1(float _curTmr, float _maxTmr)
    {
        AttackInfo ret = new AttackInfo(0);

        if (_curTmr > _maxTmr * 0.8f)
            ret.spriteIndex = attack1Sprites[0];
        else if (_curTmr > _maxTmr * 0.6f)
            ret.spriteIndex = attack1Sprites[1];
        else if (_curTmr > _maxTmr * 0.5f)
            ret.spriteIndex = attack1Sprites[2];
        else if (_curTmr >= 0)
            ret.spriteIndex = attack1Sprites[3];
        if (_curTmr < _maxTmr * 0.6f)
            ret.spawnproj = true;

        ret.newpos = new Vector3(0.35f, 0.0f, 0.0f);

        return ret;
    }
    public override AttackInfo ActivateAttack2(float _curTmr, float _maxTmr)
    {
        AttackInfo ret = new AttackInfo(0);

        if (_curTmr > _maxTmr * 0.9f)
            ret.spriteIndex = attack2Sprites[0];
        else if (_curTmr > _maxTmr * 0.8f)
            ret.spriteIndex = attack2Sprites[1];
        else if (_curTmr >= 0)
            ret.spriteIndex = attack2Sprites[2];
        if (_curTmr < _maxTmr * 0.7f)
            ret.spawnproj = true;

        ret.newpos = new Vector3(0.35f, 0.0f, 0.0f);

        return ret;
    }
    public override AttackInfo ActivateAttack3(float _curTmr, float _maxTmr)
    {
        AttackInfo ret = new AttackInfo(0);

        if (_curTmr > _maxTmr * 0.6f)
            ret.spriteIndex = attack3Sprites[0];
        else if (_curTmr > _maxTmr * 0.3f)
            ret.spriteIndex = attack3Sprites[1];
        else if (_curTmr > _maxTmr * 0.2f)
            ret.spriteIndex = attack3Sprites[2];
        else if (_curTmr >= 0)
            ret.spriteIndex = attack3Sprites[3];
        if (_curTmr < _maxTmr * 0.2f)
            ret.spawnproj = true;

        ret.newpos = new Vector3(0.35f, 0.0f, 0.0f);

        return ret;
    }

    public override AttackInfo ActivateSpecial(float _curTmr, float _maxTmr)
    {
        AttackInfo ret = new AttackInfo(0);

        if (chargeTimer > 0 && (Input.GetButton("Special/Cancel")
            || (Input.touchCount > 0 && Input.GetTouch(0).phase != TouchPhase.Ended)))
        {
            chargeTimer -= Time.deltaTime;

            // really weird correction
            if (chargeTimer == 0.0f)
                chargeTimer -= Time.deltaTime;

            if (chargeTimer > chargeTimerMax * 0.8f)
                ret.spriteIndex = attack1Sprites[0];
            else if (chargeTimer >= 0)
                ret.spriteIndex = attack1Sprites[1];

            if (_curTmr < 0.2f)
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().ChangeState(STATES.SPECIAL);
        }
        else if (chargeTimer != 0.0f) // || !Input.GetButton("Special/Cancel") )
        {
            chargeDur = chargeTimerMax - chargeTimer;
            chargeTimer = 0.0f;

            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().ChangeState(STATES.SPECIAL);
        }
        else if (chargeTimer == 0.0f)
        {
            if (_curTmr > _maxTmr * 0.8f)
            {
                if (Act_facingRight)
                    ret.velocity = new Vector2(8.0f * chargeDur, 0.0f);
                else
                    ret.velocity = new Vector2(-8.0f * chargeDur, 0.0f);

                ret.spriteIndex = specialSprites[0];
            }
            else if (_curTmr > _maxTmr * 0.6f)
                ret.spriteIndex = specialSprites[1];
            else if (_curTmr > _maxTmr * 0.5f)
            {
                ret.spriteIndex = specialSprites[2];
                ret.damMult += chargeDur * 10.0f;
                ret.spawnproj = true;
            }
            else if (_curTmr >= 0)
                ret.spriteIndex = specialSprites[3];

            ret.physicsLayer = 17;
            //ret.enableCollision = false;
        }

        return ret;
    }

    public override AttackInfo ActivateDance(float _curTmr, float _maxTmr)
    {
        AttackInfo ret = new AttackInfo(0);

        if (_curTmr > _maxTmr * 0.66f)
            ret.spriteIndex = attack3Sprites[1];
        else if (_curTmr > _maxTmr * 0.33f)
            ret.spriteIndex = attack3Sprites[2];
        else if (_curTmr >= 0.0)
            ret.spriteIndex = attack3Sprites[3];

        ret.newpos = new Vector3(-0.3f, 0.0f, 0.0f);

        return ret;
    }

    public override AttackInfo ActivateMasterSpecial(float _curTmr, float _maxTmr)
    {
        AttackInfo ret = new AttackInfo(0);

        if (chargeTimer > 0 && (Input.GetButton("Special/Cancel")
            || (Input.touchCount > 0 && Input.GetTouch(0).phase != TouchPhase.Ended)))
        {
            chargeTimer -= Time.deltaTime;

            // really weird correction
            if (chargeTimer == 0.0f)
                chargeTimer -= Time.deltaTime;

            if (chargeTimer > chargeTimerMax * 0.8f)
            {
                ret.spriteIndex = attack1Sprites[0];
                NumLeaps = 1;
            }
            else if (chargeTimer >= 0)
            {
                ret.spriteIndex = attack1Sprites[1];
                NumLeaps = 2;
            }

            if (_curTmr < 0.2f)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().ChangeState(STATES.SPECIAL);

                ret.spriteIndex = attack1Sprites[1];
                NumLeaps = 3;
            }
        }
        else if (chargeTimer != 0.0f)
        {
            chargeDur = chargeTimerMax - chargeTimer;
            chargeTimer = 0.0f;
            if (NumLeaps == 0)
                NumLeaps = 4;

            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().ChangeState(STATES.SPECIAL);
        }
        else if (chargeTimer == 0.0f) // this is where the fun begins.
        {
            GameObject[] Atargets;

            if (Ltargets.Count <= 0)
                Atargets = GameObject.FindGameObjectsWithTag("Enemy");
            else
                Atargets = new GameObject[0];

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            float maxdist = 20.0f;

            for (int i = 0; i < Atargets.Length; i++) // loop through targets and remove all entries that are too far away.
            {
                if (Vector3.Distance(player.transform.position, Atargets[i].transform.position) < maxdist)
                {
                    Ltargets.Add(Atargets[i]);
                }
            }

            if (Ltargets.Count > 0 && NumLeaps > 0) // this is where we jump to targets.
            {
                Vector3 targpos = Ltargets[0].transform.position;
                float offset = 2.0f;
                if (targpos.x > player.transform.position.x)
                    targpos.x -= offset;
                else
                    targpos.x += offset;

                player.transform.position = Vector3.Lerp(player.transform.position, targpos, Time.deltaTime * 10);

                if (_curTmr > _maxTmr * 0.8f)
                    ret.spriteIndex = specialSprites[0];
                else if (_curTmr > _maxTmr * 0.6f)
                    ret.spriteIndex = specialSprites[1];
                else if (_curTmr > _maxTmr * 0.5f)
                {
                    ret.spriteIndex = specialSprites[2];
                    ret.damMult += chargeDur * 10.0f;
                    ret.spawnproj = true;
                }
                else if (_curTmr >= 0)
                    ret.spriteIndex = specialSprites[3];

                ret.physicsLayer = 17;

                if (_curTmr < 0.2f) // current special is done, do another if we can.
                {
                    Ltargets.RemoveAt(0);
                    for (int i = Ltargets.Count; i > 0; i--)
                    {
                        if (Ltargets[i - 1].GetComponent<ACT_Enemy>().Act_currHP <= 0)
                            Ltargets.RemoveAt(i - 1);
                    }
                    NumLeaps--;

                    if (Ltargets.Count > 0 && NumLeaps > 0)
                    {
                        player.GetComponent<MNGR_Animation_Player>().ChangeState(STATES.SPECIAL);
                        player.GetComponent<PlayerController>().ChangeState(STATES.SPECIAL);
                    }
                    else
                    {
                        Ltargets = new List<GameObject>();
                        NumLeaps = 0;
                        player.GetComponent<PlayerController>().ChangeState(STATES.IDLE);
                    }
                }

            }
            else // proceed with normal special attack if there are no targets.
            {
                if (_curTmr > _maxTmr * 0.8f)
                {
                    if (Act_facingRight)
                        ret.velocity = new Vector2(8.0f * chargeDur, 0.0f);
                    else
                        ret.velocity = new Vector2(-8.0f * chargeDur, 0.0f);

                    ret.spriteIndex = specialSprites[0];
                }
                else if (_curTmr > _maxTmr * 0.6f)
                    ret.spriteIndex = specialSprites[1];
                else if (_curTmr > _maxTmr * 0.5f)
                {
                    ret.spriteIndex = specialSprites[2];
                    ret.damMult += chargeDur * 10.0f;
                    ret.spawnproj = true;
                }
                else if (_curTmr >= 0)
                    ret.spriteIndex = specialSprites[3];

                ret.physicsLayer = 17;
                //ret.enableCollision = false;
            }

        }

        return ret;
    }

    public override void UpgradeSpecial()
    {
    }
}

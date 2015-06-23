using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MNGR_Animation_Player : MonoBehaviour
{

    string[] filepaths;
    Sprite[] sprites;
    //Sprite lastSprite;
    ACT_CHAR_Base currentCharacter;
    ACT_CHAR_Base.AttackInfo info;
    PlayerController currentController;
    ACT_CHAR_Base.STATES lastState;
    ACT_CHAR_Base.STATES curState;
    //Vector3 lastpos;
    bool lastRight;

    public bool SpawnProj;

    public void Initialize()
    {
        MNGR_Game.Initialize();
        filepaths = new string[] { "Sprites/Player/Swordsman", "Sprites/Player/Lancer",
            "Sprites/Player/Defender", "Sprites/Player/Archer", "Sprites/Player/Ninja",
            "Sprites/Player/Poisoner", "Sprites/Player/Wizard", "Sprites/Player/ForceMage",
            "Sprites/Player/Spellslinger"};
        if(MNGR_Options.colorblind)
        {
            for (int i = 0; i < filepaths.Length; i++)
            {
                filepaths[i] += "_blind";
            }
        }

        if (GetComponent<PlayerController>() != null)
        {
            currentController = GetComponent<PlayerController>();
        }
        else
        {
            currentController = GetComponent<MENU_ButtonGraphic>();
        }
        currentCharacter = currentController.party[currentController.currChar];
        lastState = currentCharacter.state;
        curState = currentCharacter.state;
        sprites = Resources.LoadAll<Sprite>(filepaths[currentCharacter.characterIndex]);
        //lastpos = transform.position;
        lastRight = currentCharacter.Act_facingRight;

        SpawnProj = true;
    }

    void Update()
    {
        if (MNGR_Game.paused)
            return;

        if (GetComponent<SpriteRenderer>() != null && GameObject.Find("Reference_Point") != null)
    		GetComponent<SpriteRenderer>().sortingOrder = (int)((GameObject.Find("Reference_Point").transform.position.y - transform.position.y) * 100.0f);


        // change this script's character if the party changes its character, but don't waste the time otherwise.
        if (GetComponent<PlayerController>() != null)
        {
            //Debug.Log(GetComponent<PlayerController>().currChar);
            if (currentCharacter != GetComponent<PlayerController>().party[GetComponent<PlayerController>().currChar])
            {
                currentCharacter = GetComponent<PlayerController>().party[GetComponent<PlayerController>().currChar];
                sprites = Resources.LoadAll<Sprite>(filepaths[currentCharacter.characterIndex]);
            }
        }
        else if (GetComponent<MENU_ButtonGraphic>() != null)
        {
            if (currentCharacter != GetComponent<MENU_ButtonGraphic>().party[GetComponent<MENU_ButtonGraphic>().currChar])
            {
                currentCharacter = GetComponent<MENU_ButtonGraphic>().party[GetComponent<MENU_ButtonGraphic>().currChar];
                sprites = Resources.LoadAll<Sprite>(filepaths[currentCharacter.characterIndex]);
            }
        }
        else if (GetComponent<MENU_CharacterGraphic>() != null)
        {
            if (currentCharacter != GetComponent<MENU_CharacterGraphic>().party[GetComponent<MENU_CharacterGraphic>().currChar])
            {
                currentCharacter = GetComponent<MENU_CharacterGraphic>().party[GetComponent<MENU_CharacterGraphic>().currChar];
                sprites = Resources.LoadAll<Sprite>(filepaths[currentCharacter.characterIndex]);
            }
        }

        // S: Should prevent this from running if player is dead
        if (!currentController.isAlive)
            return;

        // change this script's state if the character changes his state, but don't waste the time otherwise.
        if (curState != currentCharacter.state)
            ChangeState(currentCharacter.state);

        // rotate based upon facing bool
        Vector3 newscale = transform.localScale;
        if (currentCharacter.Act_facingRight)
            newscale.x = Mathf.Abs(newscale.x);
        else
            newscale.x = -Mathf.Abs(newscale.x);
        transform.localScale = newscale;

        // animate based upon state.  mostly the same code, but has to be unique for each animation
        switch (curState)
        {
            case ACT_CHAR_Base.STATES.IDLE:

                info = currentCharacter.ActivateIdle(currentController.curTmr, currentController.maxTmr[(int)curState]);

                SetSprite(info.spriteIndex);
                break;
            case ACT_CHAR_Base.STATES.WALKING:

                info = currentCharacter.ActivateWalk(currentController.curTmr, currentController.maxTmr[(int)curState]);

                SetSprite(info.spriteIndex);
                break;
            case ACT_CHAR_Base.STATES.DASHING:

                info = currentCharacter.ActivateDodge(currentController.curTmr, currentController.maxTmr[(int)curState]);

                //if (currentCharacter.Act_facingRight)
                //transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                //else
                //transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

                SetSprite(info.spriteIndex);
                break;
            case ACT_CHAR_Base.STATES.ATTACK_1:

                info = currentCharacter.ActivateAttack1(currentController.curTmr, currentController.maxTmr[(int)curState]);
                SetSprite(info.spriteIndex);

                if (SpawnProj && info.spawnproj)
                    SpawnProj = currentController.SpawnProj(currentCharacter.Act_facingRight);
                break;
            case ACT_CHAR_Base.STATES.ATTACK_2:

                info = currentCharacter.ActivateAttack2(currentController.curTmr, currentController.maxTmr[(int)curState]);
                SetSprite(info.spriteIndex);

                if (SpawnProj && info.spawnproj)
                    SpawnProj = currentController.SpawnProj(currentCharacter.Act_facingRight);
                break;
            case ACT_CHAR_Base.STATES.ATTACK_3:

                info = currentCharacter.ActivateAttack3(currentController.curTmr, currentController.maxTmr[(int)curState]);
                SetSprite(info.spriteIndex);

                if (info.velocity.magnitude != 0)
                {
                    currentController.horz = info.velocity.x;
                    currentController.vert = info.velocity.y;
                }

                if (SpawnProj && info.spawnproj)
                    SpawnProj = currentController.SpawnProj(currentCharacter.Act_facingRight, 1);
                break;
            case ACT_CHAR_Base.STATES.SPECIAL:
				if (currentCharacter.hasSpecial)
					info = currentCharacter.ActivateMasterSpecial(currentController.curTmr, currentController.maxTmr[(int)curState]);
				else
					info = currentCharacter.ActivateSpecial(currentController.curTmr, currentController.maxTmr[(int)curState]);
                SetSprite(info.spriteIndex);

                if (info.velocity.magnitude != 0)
                {
                    currentController.horz = info.velocity.x;
                    currentController.vert = info.velocity.y;
                }

                if (SpawnProj && info.spawnproj)
                    SpawnProj = currentController.SpawnProj(currentCharacter.Act_facingRight, info.projIndex, info.damMult);

                if (gameObject.layer != info.physicsLayer)
                    gameObject.layer = info.physicsLayer;
                break;
            case ACT_CHAR_Base.STATES.HURT:

                info = currentCharacter.ActivateHurt(currentController.curTmr, currentController.maxTmr[(int)curState]);

                SetSprite(info.spriteIndex);
                break;
            case ACT_CHAR_Base.STATES.DYING:

                info = currentCharacter.ActivateDying(currentController.curTmr, currentController.maxTmr[(int)curState]);

                SetSprite(info.spriteIndex);
                break;
            case ACT_CHAR_Base.STATES.USE:
                // Special FX because Logan didn't actually animate these
                break;

            case ACT_CHAR_Base.STATES.DANCE:
                info = currentCharacter.ActivateDance(currentController.curTmr, currentController.maxTmr[(int)curState]);

                SetSprite(info.spriteIndex);

                break;
            default:
                SetSprite(0);
                break;
        }

        if (GetComponent<SpriteRenderer>() != null) // Flexibility!
        {
            if (currentCharacter.invulTmr > 0.0f)
                GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;
            else if (!GetComponent<SpriteRenderer>().enabled)
                GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    // handled in one easy-to-access function
    public void ChangeState(ACT_CHAR_Base.STATES _newstate)
    {
        // this allows us to know exactly which transition we're in.  Very useful.
        lastState = curState;

        // position correction when warrior finishes his combo
        /*if (currentCharacter.characterIndex == 0
            && ((lastState == ACT_CHAR_Base.STATES.ATTACK_2 && _newstate == ACT_CHAR_Base.STATES.IDLE)
            || (lastState == ACT_CHAR_Base.STATES.ATTACK_3 && _newstate == ACT_CHAR_Base.STATES.IDLE)))
        {
            // just move him by 0.35 in the direction he's facing, but looks more complicated than that.
            Vector3 newpos = transform.position;
            float move = 0.35f;
            if (!currentCharacter.Act_facingRight)
                move = -move;

            newpos.x += move;
            transform.position = newpos;
        }*/
        if (lastState == ACT_CHAR_Base.STATES.SPECIAL)
        {
            currentController.GetComponent<BoxCollider2D>().enabled = true;
            gameObject.layer = 8;
        }

        if (_newstate == ACT_CHAR_Base.STATES.IDLE)
        {
            Vector3 newpos = transform.position;
            if (!currentCharacter.Act_facingRight)
                info.newpos *= -1.0f;
            transform.position = newpos + info.newpos;
            info.newpos = Vector3.zero;
        }
        else if (lastState != ACT_CHAR_Base.STATES.SPECIAL && _newstate == ACT_CHAR_Base.STATES.SPECIAL)
        {
            if (currentCharacter.chargeTimer <= 0)
            {
                currentCharacter.chargeTimer = currentCharacter.chargeTimerMax;
                currentCharacter.chargeDur = 0.0f;
            }

        }

        // if this state didn't spawn a projectile during animation, spawn it here.
        if (SpawnProj)
        {
            if (lastState == ACT_CHAR_Base.STATES.ATTACK_1
                || lastState == ACT_CHAR_Base.STATES.ATTACK_2)
                SpawnProj = currentController.SpawnProj(lastRight, 0);
            else if (lastState == ACT_CHAR_Base.STATES.ATTACK_3)
                SpawnProj = currentController.SpawnProj(lastRight, 1);
			else if (lastState == ACT_CHAR_Base.STATES.SPECIAL && !currentCharacter.hasSpecial)
				SpawnProj = currentController.SpawnProj(lastRight, 2);
            else if (lastState == ACT_CHAR_Base.STATES.SPECIAL && currentCharacter.hasSpecial)
                SpawnProj = currentController.SpawnProj(lastRight, 3);
        }


        // we have changed states, we should be able to spawn another projectile
        SpawnProj = true;

        // update last pos now.
        //lastpos = transform.position;
        lastRight = currentCharacter.Act_facingRight;

        // the part we've all been waiting for
        curState = _newstate;

    }

    void SetSprite(int _ID)
    {
        if (GetComponent<SpriteRenderer>() != null)
            GetComponent<SpriteRenderer>().sprite = sprites[_ID];
        else
            GetComponent<Image>().sprite = sprites[_ID];
    }



}

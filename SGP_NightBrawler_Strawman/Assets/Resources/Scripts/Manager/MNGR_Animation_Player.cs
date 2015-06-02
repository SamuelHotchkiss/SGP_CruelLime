using UnityEngine;
using System.Collections;

public class MNGR_Animation_Player : MonoBehaviour
{

    string[] filepaths;
    Sprite[] sprites;
    Sprite lastSprite;
    ACT_CHAR_Base currentCharacter;
        ACT_CHAR_Base.AttackInfo info;
    PlayerController currentController;
    ACT_CHAR_Base.STATES lastState;
    ACT_CHAR_Base.STATES curState;
    //Vector3 lastpos;
    bool lastRight;

    // these contain the sprite IDs used for each animaiton
    int[] idleSprites;
    int[] hurtSprites;
    int[] deadSprites;
    public bool SpawnProj;

    public void Initialize()
    {

        filepaths = new string[] { "Sprites/Player/Swordsman", "Sprites/Player/Lancer",
            "Sprites/Player/Defender", "Sprites/Player/Archer", "Sprites/Player/Ninja",
            "Sprites/Player/Poisoner", "Sprites/Player/Wizard", "Sprites/Player/ForceMage",
            "Sprites/Player/Spellslinger"};
        currentController = GetComponent<PlayerController>();
        currentCharacter = currentController.party[currentController.currChar];
        lastState = currentCharacter.state;
        curState = currentCharacter.state;
        sprites = Resources.LoadAll<Sprite>(filepaths[currentCharacter.characterIndex]);
        //lastpos = transform.position;
        lastRight = currentCharacter.Act_facingRight;

        // Works for swordsman...
        idleSprites = new int[] { 0, 1, 2 };
        hurtSprites = new int[] { 30 };
        deadSprites = new int[] { 30, 31 };
        SpawnProj = true;
    }

    void Update()
    {
        // S: Should prevent this from running if player is dead
        if (!currentController.isAlive)
            return;

        // change this script's character if the party changes its character, but don't waste the time otherwise.
        if (currentCharacter != GetComponent<PlayerController>().party[GetComponent<PlayerController>().currChar])
        {
            currentCharacter = GetComponent<PlayerController>().party[GetComponent<PlayerController>().currChar];
            sprites = Resources.LoadAll<Sprite>(filepaths[currentCharacter.characterIndex]);
        }
        // change this script's state if the character changes his state, but don't waste the time otherwise.
        if (curState != currentCharacter.state)
            ChangeState(currentCharacter.state);

        // rotate based upon facing bool
        if (currentCharacter.Act_facingRight)
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        else
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

        // animate based upon state.  mostly the same code, but has to be unique for each animation
        switch (curState)
        {
            case ACT_CHAR_Base.STATES.IDLE:

                if (currentController.curTmr > currentController.maxTmr[(int)curState] * 0.6f)
                    GetComponent<SpriteRenderer>().sprite = sprites[idleSprites[0]];
                else if (currentController.curTmr > currentController.maxTmr[(int)curState] * 0.5f)
                    GetComponent<SpriteRenderer>().sprite = sprites[idleSprites[1]];
                else if (currentController.curTmr > currentController.maxTmr[(int)curState] * 0.1f)
                    GetComponent<SpriteRenderer>().sprite = sprites[idleSprites[2]];
                else if (currentController.curTmr >= 0)
                    GetComponent<SpriteRenderer>().sprite = sprites[idleSprites[1]];
                break;
            case ACT_CHAR_Base.STATES.WALKING:

                info = currentCharacter.ActivateWalk(currentController.curTmr, currentController.maxTmr[(int)curState]);

                GetComponent<SpriteRenderer>().sprite = sprites[info.spriteIndex];
                break;
            case ACT_CHAR_Base.STATES.DASHING:

                info = currentCharacter.ActivateDodge(currentController.curTmr, currentController.maxTmr[(int)curState]);

                //if (currentCharacter.Act_facingRight)
                    //transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                //else
                    //transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

                GetComponent<SpriteRenderer>().sprite = sprites[info.spriteIndex];
                break;
            case ACT_CHAR_Base.STATES.ATTACK_1:

                info = currentCharacter.ActivateAttack1(currentController.curTmr, currentController.maxTmr[(int)curState]);
                GetComponent<SpriteRenderer>().sprite = sprites[info.spriteIndex];

                if (SpawnProj && info.spawnproj)
                    SpawnProj = currentController.SpawnProj(currentCharacter.Act_facingRight);
                //lastpos = transform.position;
                break;
            case ACT_CHAR_Base.STATES.ATTACK_2:

                info = currentCharacter.ActivateAttack2(currentController.curTmr, currentController.maxTmr[(int)curState]);
                GetComponent<SpriteRenderer>().sprite = sprites[info.spriteIndex];

                if (SpawnProj && info.spawnproj)
                    SpawnProj = currentController.SpawnProj(currentCharacter.Act_facingRight);
                //lastpos = transform.position;
                break;
            case ACT_CHAR_Base.STATES.ATTACK_3:

                info = currentCharacter.ActivateAttack3(currentController.curTmr, currentController.maxTmr[(int)curState]);
                GetComponent<SpriteRenderer>().sprite = sprites[info.spriteIndex];

                if (SpawnProj && info.spawnproj)
                    SpawnProj = currentController.SpawnProj(currentCharacter.Act_facingRight, 1);
                //lastpos = transform.position;
                break;
            case ACT_CHAR_Base.STATES.SPECIAL:
                info = currentCharacter.ActivateSpecial(currentController.curTmr, currentController.maxTmr[(int)curState]);
                GetComponent<SpriteRenderer>().sprite = sprites[info.spriteIndex];

                // completely pointless if statement and variable nonsense except it randomly wont work otherwise.
                if (info.velocity.magnitude != 0)
                {
                    //Vector2 test = GetComponent<Rigidbody2D>().velocity;
                    //GetComponent<Rigidbody2D>().velocity = info.velocity; // this is the only line of code in this nonsense that I wish to work.
                    //test = GetComponent<Rigidbody2D>().velocity;

                    currentController.horz = info.velocity.x;
                    currentController.vert = info.velocity.y;
                }
                // end of nonsense

                if (SpawnProj && info.spawnproj)
                    SpawnProj = currentController.SpawnProj(currentCharacter.Act_facingRight, 2);
                //lastpos = transform.position;
                break;
            case ACT_CHAR_Base.STATES.HURT:
                GetComponent<SpriteRenderer>().sprite = sprites[hurtSprites[0]];
                break;
            case ACT_CHAR_Base.STATES.DYING:
                if (currentController.curTmr > currentController.maxTmr[(int)curState] * 0.5f)
                    GetComponent<SpriteRenderer>().sprite = sprites[deadSprites[0]];
                else if (currentController.curTmr >= 0)
                    GetComponent<SpriteRenderer>().sprite = sprites[deadSprites[1]];
                break;
            case ACT_CHAR_Base.STATES.USE:
                // Special FX because Logan didn't actually animate these
                break;
            default:
                GetComponent<SpriteRenderer>().sprite = sprites[idleSprites[0]];
                break;
        }

        if (currentCharacter.invulTmr > 0.0f)
            GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;
        else if (!GetComponent<SpriteRenderer>().enabled)
            GetComponent<SpriteRenderer>().enabled = true;
    }

    // handled in one easy-to-access function
    void ChangeState(ACT_CHAR_Base.STATES _newstate)
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
        if (_newstate == ACT_CHAR_Base.STATES.IDLE)
        {
            Vector3 newpos = transform.position;
            if (!currentCharacter.Act_facingRight)
                info.newpos *= -1.0f;
            transform.position = newpos + info.newpos;
            info.newpos = Vector3.zero;
        }

        // if this state didn't spawn a projectile during animation, spawn it here.
        if (SpawnProj)
        {
            if (lastState == ACT_CHAR_Base.STATES.ATTACK_1
                || lastState == ACT_CHAR_Base.STATES.ATTACK_2)
                SpawnProj = currentController.SpawnProj(lastRight, 0);
            else if (lastState == ACT_CHAR_Base.STATES.ATTACK_3)
                SpawnProj = currentController.SpawnProj(lastRight, 1);
            else if (lastState == ACT_CHAR_Base.STATES.SPECIAL)
                SpawnProj = currentController.SpawnProj(lastRight, 2);
        }


        // we have changed states, we should be able to spawn another projectile
        SpawnProj = true;

        // update last pos now.
        //lastpos = transform.position;
        lastRight = currentCharacter.Act_facingRight;

        // the part we've all been waiting for
        curState = _newstate;

    }


}

using UnityEngine;
using System.Collections;

public class MNGR_Animation_Player : MonoBehaviour {

    string[] filepaths;
    Sprite[] sprites;
    ACT_CHAR_Base currentCharacter;
    PlayerController currentController;
    ACT_CHAR_Base.STATES lastState;
    ACT_CHAR_Base.STATES curState;

    // these contain the sprite IDs used for each animaiton
    int[] idleSprites;
    int[] walkSprites;
    int[] attack1Sprites;
    int[] attack2Sprites;
    int[] attack3Sprites;
    int[] hurtSprites;
    int[] deadSprites;
    public bool SpawnProj;

	public void Initialize () {

        filepaths = new string[] { "Sprites/Player/Swordsman", "Sprites/Player/Lancer",
            "Sprites/Player/Defender", "Sprites/Player/Archer", "Sprites/Player/Ninja",
            "Sprites/Player/Poisoner", "Sprites/Player/Wizard", "Sprites/Player/ForceMage",
            "Sprites/Player/Spellslinger"};
        currentController = GetComponent<PlayerController>();
        currentCharacter = currentController.party[currentController.currChar];
        lastState = currentCharacter.state;
        curState = currentCharacter.state;
        sprites = Resources.LoadAll<Sprite>(filepaths[currentCharacter.characterIndex]);

        // Works for swordsman...
        idleSprites = new int[] { 0, 1, 2 };
        walkSprites = new int[] { 5, 6, 7, 8, 9 };
        attack1Sprites = new int[] { 10, 11, 12, 13 };
        attack2Sprites = new int[] { 15, 16, 17 };
        attack3Sprites = new int[] { 20, 21, 22, 23 };
        hurtSprites = new int[] { 30 };
        deadSprites = new int[] { 30, 31 };
        SpawnProj = true;
	}
	
	void Update () {

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
            transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
        else
            transform.localEulerAngles = new Vector3(0.0f, 180.0f, 0.0f);

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

                if (currentController.curTmr > currentController.maxTmr[(int)curState] * 0.8f)
                    GetComponent<SpriteRenderer>().sprite = sprites[walkSprites[0]];
                else if (currentController.curTmr > currentController.maxTmr[(int)curState] * 0.6f)
                    GetComponent<SpriteRenderer>().sprite = sprites[walkSprites[1]];
                else if (currentController.curTmr > currentController.maxTmr[(int)curState] * 0.4f)
                    GetComponent<SpriteRenderer>().sprite = sprites[walkSprites[2]];
                else if (currentController.curTmr > currentController.maxTmr[(int)curState] * 0.2f)
                    GetComponent<SpriteRenderer>().sprite = sprites[walkSprites[3]];
                else if (currentController.curTmr >= 0)
                    GetComponent<SpriteRenderer>().sprite = sprites[walkSprites[4]];
                break;
            case ACT_CHAR_Base.STATES.DASHING:

                if (currentCharacter.Act_facingRight)
                    transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
                else
                    transform.localEulerAngles = new Vector3(0.0f, 180.0f, 0.0f);

                GetComponent<SpriteRenderer>().sprite = sprites[walkSprites[1]];
                break;
            case ACT_CHAR_Base.STATES.ATTACK_1:

                if (currentController.curTmr > currentController.maxTmr[(int)curState] * 0.75f)
                    GetComponent<SpriteRenderer>().sprite = sprites[attack1Sprites[0]];
                else if (currentController.curTmr > currentController.maxTmr[(int)curState] * 0.5f)
                    GetComponent<SpriteRenderer>().sprite = sprites[attack1Sprites[1]];
                else if (currentController.curTmr > currentController.maxTmr[(int)curState] * 0.4f)
                    GetComponent<SpriteRenderer>().sprite = sprites[attack1Sprites[2]];
                else if (currentController.curTmr >= 0)
                    GetComponent<SpriteRenderer>().sprite = sprites[attack1Sprites[3]];

                if (SpawnProj && currentController.curTmr < currentController.maxTmr[(int)curState] * 0.5f)
                    SpawnProj = currentController.SpawnProj();
                break;
            case ACT_CHAR_Base.STATES.ATTACK_2:

                if (currentController.curTmr > currentController.maxTmr[(int)curState] * 0.66f)
                    GetComponent<SpriteRenderer>().sprite = sprites[attack2Sprites[0]];
                else if (currentController.curTmr > currentController.maxTmr[(int)curState] * 0.56f)
                    GetComponent<SpriteRenderer>().sprite = sprites[attack2Sprites[1]];
                else if (currentController.curTmr >= 0)
                    GetComponent<SpriteRenderer>().sprite = sprites[attack2Sprites[2]];

                if (SpawnProj && currentController.curTmr < currentController.maxTmr[(int)curState] * 0.66f)
                    SpawnProj = currentController.SpawnProj();
                break;
            case ACT_CHAR_Base.STATES.ATTACK_3:

                if (currentController.curTmr > currentController.maxTmr[(int)curState] * 0.75f)
                    GetComponent<SpriteRenderer>().sprite = sprites[attack3Sprites[0]];
                else if (currentController.curTmr > currentController.maxTmr[(int)curState] * 0.5f)
                    GetComponent<SpriteRenderer>().sprite = sprites[attack3Sprites[1]];
                else if (currentController.curTmr > currentController.maxTmr[(int)curState] * 0.45f)
                    GetComponent<SpriteRenderer>().sprite = sprites[attack3Sprites[2]];
                else if (currentController.curTmr >= 0)
                    GetComponent<SpriteRenderer>().sprite = sprites[attack3Sprites[3]];

                if (SpawnProj && currentController.curTmr < currentController.maxTmr[(int)curState] * 0.5f)
                    SpawnProj = currentController.SpawnProj(1);
                break;
            case ACT_CHAR_Base.STATES.SPECIAL:
                ACT_CHAR_Base.SpecialInfo info = currentCharacter.ActivateSpecial(currentController.curTmr, currentController.maxTmr[(int)curState]);
                GetComponent<SpriteRenderer>().sprite = sprites[info.spriteIndex];

                // completely pointless if statement and variable nonsense except it randomly wont work otherwise.
                if (info.velocity.magnitude != 0)
                {
                    Vector2 test = GetComponent<Rigidbody2D>().velocity;
                    GetComponent<Rigidbody2D>().velocity = info.velocity;
                    test = GetComponent<Rigidbody2D>().velocity;
                }

                if (SpawnProj && info.spawnproj)
                    SpawnProj = currentController.SpawnProj(2);
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
	}

    // handled in one easy-to-access function
    void ChangeState(ACT_CHAR_Base.STATES _newstate)
    {
        // this allows us to know exactly which transition we're in.  Very useful.
        lastState = curState;

        // position correction when warrior finishes his combo
        if ( currentCharacter.characterIndex == 0 
            && (lastState == ACT_CHAR_Base.STATES.ATTACK_2 && _newstate == ACT_CHAR_Base.STATES.IDLE)
            || (lastState == ACT_CHAR_Base.STATES.ATTACK_3 && _newstate == ACT_CHAR_Base.STATES.IDLE) )
        {
            // just move him by 0.35 in the direction he's facing, but looks more complicated than that.
            Vector3 newpos = transform.position;
            float move = 0.35f;
            if (!currentCharacter.Act_facingRight)
                move = -move;

            newpos.x += move;
            transform.position = newpos;
        }

        // we have changed states, we should be able to spawn another projectile
        SpawnProj = true;

        // the part we've all been waiting for
        curState = _newstate;

    }


}

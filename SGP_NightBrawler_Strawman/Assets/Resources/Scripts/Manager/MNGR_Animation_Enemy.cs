using UnityEngine;
using System.Collections;

public class MNGR_Animation_Enemy : MonoBehaviour
{

    // Mixin' up a big ol' batch of copy pasta

    string[] filepaths;
    Sprite[] sprites;
    ACT_Enemy currentCharacter;
    ACT_Enemy.STATES lastState;
    ACT_Enemy.STATES curState;
    //PlayerController currentCharacter;

    // these contain the sprite IDs used for each animaiton
    int[] enemySpritesTotal;
    int[] idleSprites;
    int[] walkSprites;
    int[] attack1Sprites;
    //int[] attack2Sprites;
    //int[] attack3Sprites;
    int[] specialSprites;
    int[] hurtSprites;
    int[] deadSprites;

    void Start()
    {

        filepaths = new string[] { "GloblinFighter", "GloblinArcher", "GloblinWarchief", "Sar" };
        for (int i = 0; i < filepaths.Length; i++)
            filepaths[i] = "Sprites/Enemy/" + filepaths[i];
        currentCharacter = GetComponent<ACT_Enemy>();
        lastState = currentCharacter.state;
        curState = currentCharacter.state;
        sprites = Resources.LoadAll<Sprite>(filepaths[currentCharacter.Act_ID]);

        // set up unique sprites for all dis stuff
        InitializeSprites(currentCharacter.Act_ID);
    }
    void InitializeSprites(int _ID)
    {
        switch (_ID)
        {
            case 0: // Globlin Fighter
                idleSprites = new int[] { 0, 1, 2 };
                walkSprites = new int[] { 5, 6, 7, 8, 9 };
                attack1Sprites = new int[] { 10, 11, 12 };
                specialSprites = new int[] { 0 };
                hurtSprites = new int[] { 15 };
                deadSprites = new int[] { 15, 16 };
                break;
            case 1: // Globlin Archer
                idleSprites = new int[] { 0, 1, 2 };
                walkSprites = new int[] { 5, 6, 7, 8, 9 };
                attack1Sprites = new int[] { 10, 11, 12 };
                specialSprites = new int[] { 0 };
                hurtSprites = new int[] { 15 };
                deadSprites = new int[] { 15, 16 };
                break;
            case 2: // Globlin Warchief
                idleSprites = new int[] { 0, 1, 2 };
                walkSprites = new int[] { 5, 6, 7, 8, 9 };
                attack1Sprites = new int[] { 10, 11, 12 };
                specialSprites = new int[] { 17 };
                hurtSprites = new int[] { 15 };
                deadSprites = new int[] { 15, 16 };
                break;
			case 3: // Spiderling
				idleSprites = new int[] { 0, 0, 0 };
                walkSprites = new int[] { 0, 0, 0, 0, 0 };
                attack1Sprites = new int[] { 0, 0, 0 };
                specialSprites = new int[] { 0 };
                hurtSprites = new int[] { 0 };
                deadSprites = new int[] { 0, 0 };
                break;
			case 4: // Spider Mom
				idleSprites = new int[] { 0, 0, 0 };
				walkSprites = new int[] { 0, 0, 0, 0, 0 };
				attack1Sprites = new int[] { 0, 0, 0 };
				specialSprites = new int[] { 0 };
				hurtSprites = new int[] { 0 };
				deadSprites = new int[] { 0, 0 };
				break;
            default:
                idleSprites = new int[] { 0, 1, 2 };
                walkSprites = new int[] { 5, 6, 7, 8, 9 };
                attack1Sprites = new int[] { 10, 11, 12 };
                specialSprites = new int[] { 0 };
                hurtSprites = new int[] { 15 };
                deadSprites = new int[] { 15, 16 };
                break;
        }
    }
    void Update()
    {
		GetComponent<SpriteRenderer>().sortingOrder = (int)(GameObject.Find("Reference_Point").transform.position.y - transform.position.y);
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
            case ACT_Enemy.STATES.IDLE:

                if (currentCharacter.currTime > currentCharacter.stateTime[(int)currentCharacter.state] * 0.6f)
                    GetComponent<SpriteRenderer>().sprite = sprites[idleSprites[0]];
                else if (currentCharacter.currTime > currentCharacter.stateTime[(int)currentCharacter.state] * 0.5f)
                    GetComponent<SpriteRenderer>().sprite = sprites[idleSprites[1]];
                else if (currentCharacter.currTime > currentCharacter.stateTime[(int)currentCharacter.state] * 0.1f)
                    GetComponent<SpriteRenderer>().sprite = sprites[idleSprites[2]];
                else if (currentCharacter.currTime >= 0)
                    GetComponent<SpriteRenderer>().sprite = sprites[idleSprites[1]];
                break;
            case ACT_Enemy.STATES.WALKING:

                if (currentCharacter.currTime > currentCharacter.stateTime[(int)currentCharacter.state] * 0.8f)
                    GetComponent<SpriteRenderer>().sprite = sprites[walkSprites[0]];
                else if (currentCharacter.currTime > currentCharacter.stateTime[(int)currentCharacter.state] * 0.6f)
                    GetComponent<SpriteRenderer>().sprite = sprites[walkSprites[1]];
                else if (currentCharacter.currTime > currentCharacter.stateTime[(int)currentCharacter.state] * 0.4f)
                    GetComponent<SpriteRenderer>().sprite = sprites[walkSprites[2]];
                else if (currentCharacter.currTime > currentCharacter.stateTime[(int)currentCharacter.state] * 0.2f)
                    GetComponent<SpriteRenderer>().sprite = sprites[walkSprites[3]];
                else if (currentCharacter.currTime >= 0)
                    GetComponent<SpriteRenderer>().sprite = sprites[walkSprites[4]];
                break;
            case ACT_Enemy.STATES.RUNNING:

                if (currentCharacter.currTime > currentCharacter.stateTime[(int)currentCharacter.state] * 0.8f)
                    GetComponent<SpriteRenderer>().sprite = sprites[walkSprites[0]];
                else if (currentCharacter.currTime > currentCharacter.stateTime[(int)currentCharacter.state] * 0.6f)
                    GetComponent<SpriteRenderer>().sprite = sprites[walkSprites[1]];
                else if (currentCharacter.currTime > currentCharacter.stateTime[(int)currentCharacter.state] * 0.4f)
                    GetComponent<SpriteRenderer>().sprite = sprites[walkSprites[2]];
                else if (currentCharacter.currTime > currentCharacter.stateTime[(int)currentCharacter.state] * 0.2f)
                    GetComponent<SpriteRenderer>().sprite = sprites[walkSprites[3]];
                else if (currentCharacter.currTime >= 0)
                    GetComponent<SpriteRenderer>().sprite = sprites[walkSprites[4]];
                break;
            case ACT_Enemy.STATES.ATTACKING:

                if (currentCharacter.currTime > currentCharacter.stateTime[(int)currentCharacter.state] * 0.5f)
                    GetComponent<SpriteRenderer>().sprite = sprites[attack1Sprites[0]];
                else if (currentCharacter.currTime > currentCharacter.stateTime[(int)currentCharacter.state] * 0.4f)
                    GetComponent<SpriteRenderer>().sprite = sprites[attack1Sprites[1]];
                else if (currentCharacter.currTime >= 0)
                    GetComponent<SpriteRenderer>().sprite = sprites[attack1Sprites[2]];
                break;
            case ACT_Enemy.STATES.SPECIAL:

                /*
                if (currentCharacter.curTime > currentCharacter.maxTmr[(int)currentCharacter.state] * 0.95f)
                    GetComponent<SpriteRenderer>().sprite = sprites[attack1Sprites[0]];
                else if (currentCharacter.curTime > currentCharacter.maxTmr[(int)currentCharacter.state] * 0.7f)
                    GetComponent<SpriteRenderer>().sprite = sprites[attack1Sprites[1]];
                else if (currentCharacter.curTime + Time.deltaTime * 2.0f > currentCharacter.maxTmr[(int)currentCharacter.state] * 0.7f)
                {
                    if (currentCharacter.Act_facingRight)
                        GetComponent<Rigidbody2D>().velocity = new Vector2(2.0f, 0.0f);
                    else
                        GetComponent<Rigidbody2D>().velocity = new Vector2(-2.0f, 0.0f);
                }
                else if (currentCharacter.curTime >= 0)
                {
                    if ((int)(currentCharacter.curTime * 1000) % 20 > 9)
                        GetComponent<SpriteRenderer>().sprite = sprites[specialSprites[0]];
                    else
                        GetComponent<SpriteRenderer>().sprite = sprites[specialSprites[1]];
                }*/
                GetComponent<SpriteRenderer>().sprite = sprites[specialSprites[0]];
                break;
            case ACT_Enemy.STATES.HURT:
                GetComponent<SpriteRenderer>().sprite = sprites[hurtSprites[0]];
                break;
            case ACT_Enemy.STATES.DEAD:
                if (currentCharacter.currTime > currentCharacter.stateTime[(int)currentCharacter.state] * 0.5f)
                    GetComponent<SpriteRenderer>().sprite = sprites[deadSprites[0]];
                else if (currentCharacter.currTime >= 0)
                    GetComponent<SpriteRenderer>().sprite = sprites[deadSprites[1]];
                break;
            default:
                GetComponent<SpriteRenderer>().sprite = sprites[idleSprites[0]];
                break;
        }
    }

    // handled in one easy-to-access function
    void ChangeState(ACT_Enemy.STATES _newstate)
    {
        // this allows us to know exactly which transition we're in.  Very useful.
        lastState = curState;

        // laststate vs curstate magical space

        // the part we've all been waiting for
        curState = _newstate;

    }
}

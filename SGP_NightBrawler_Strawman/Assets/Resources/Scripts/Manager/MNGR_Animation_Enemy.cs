using UnityEngine;
using System.Collections;

public class MNGR_Animation_Enemy : MonoBehaviour
{

    // Mixin' up a big ol' batch of copy pasta

    string[] filepaths;
    Sprite[] sprites;
    ACT_Enemy currentCharacter;
    //ACT_Enemy.STATES lastState;
    ACT_Enemy.STATES curState;
    //PlayerController currentCharacter;

    // these contain the sprite IDs used for each animaiton
    //int[] enemySpritesTotal;
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
        currentCharacter = GetComponent<ACT_Enemy>();

        // If we're using the newer and better method that each enemy has their own filename already,
        if (currentCharacter.Act_Name != "")
        {
            string path;
            if (currentCharacter.Act_IsBoss)
                path = "Sprites/Boss/" + currentCharacter.Act_Name;
            else
                path = "Sprites/Enemy/" + currentCharacter.Act_Name;

            sprites = Resources.LoadAll<Sprite>(path);

            /*idleSprites = new int[] { 0, 1, 2 };
            walkSprites = new int[] { 5, 6, 7, 8, 9 };
            attack1Sprites = new int[] { 10, 11, 12 };
            specialSprites = new int[] { 0 };
            hurtSprites = new int[] { 15 };
            deadSprites = new int[] { 15, 16 };*/
        }
        else // older method that's still supported for older enemies (if it aint broke...).
        {
            filepaths = new string[] { "GloblinFighter", "GloblinArcher", "GloblinWarchief", "Sar" };
            for (int i = 0; i < filepaths.Length; i++)
                filepaths[i] = "Sprites/Enemy/" + filepaths[i];
            sprites = Resources.LoadAll<Sprite>(filepaths[currentCharacter.Act_ID]);

        }
        // set up unique sprites for all dis stuff
        InitializeSprites(currentCharacter.Act_ID);

        //lastState = currentCharacter.state;
        curState = currentCharacter.state;

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
                idleSprites = new int[] { 0, 1, 2 };
                walkSprites = new int[] { 5, 6, 7, 8, 9 };
                attack1Sprites = new int[] { 10, 11, 12 };
                specialSprites = new int[] { 5, 6, 7, 8, 9 };
                hurtSprites = new int[] { 15 };
                deadSprites = new int[] { 15, 16 };
                break;
            /*case 4: // Spider Mom
                idleSprites = new int[] { 0, 0, 0 };
                walkSprites = new int[] { 0, 0, 0, 0, 0 };
                attack1Sprites = new int[] { 0, 0, 0 };
                specialSprites = new int[] { 0 };
                hurtSprites = new int[] { 0 };
                deadSprites = new int[] { 0, 0 };
                break;*/
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

                if (currentCharacter.currTime > currentCharacter.stateTime[(int)currentCharacter.state] * 0.75f)
                    GetComponent<SpriteRenderer>().sprite = sprites[idleSprites[0]];
                else if (currentCharacter.currTime > currentCharacter.stateTime[(int)currentCharacter.state] * 0.5f)
                    GetComponent<SpriteRenderer>().sprite = sprites[idleSprites[1]];
                else if (currentCharacter.currTime > currentCharacter.stateTime[(int)currentCharacter.state] * 0.25f)
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
                // Beautiful.
                float length = specialSprites.Length;
                for (float i = length; i > 0; i-- ) // for all possible frames,
                {
                    float mult = (1.0f / length) * (i - 1); // determines which frame to play.
                    //float temp = currentCharacter.stateTime[(int)currentCharacter.state] * mult; // degubbin'
                    if (currentCharacter.currTime > currentCharacter.stateTime[(int)currentCharacter.state] * mult)
                    {
                        int frame = (int)(length - i); // invert i to play animation in correct order
                        GetComponent<SpriteRenderer>().sprite = sprites[specialSprites[frame]];
                        break; // break out of the loop pronto.
                    }
                }
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
        //lastState = curState;

        // laststate vs curstate magical space

        // the part we've all been waiting for
        curState = _newstate;

    }
}

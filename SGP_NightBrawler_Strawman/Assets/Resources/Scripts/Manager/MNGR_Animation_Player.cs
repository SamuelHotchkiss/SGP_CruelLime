using UnityEngine;
using System.Collections;

public class MNGR_Animation_Player : MonoBehaviour {

    //Sprite Sprites[];
    ACT_CHAR_Base currentCharacter;
    ACT_CHAR_Base.STATES plyState;
    float maxTmr;
    float curTmr;
    bool loop;
    int[] idleSprites;
    int[] walkSprites;
    int[] attack1Sprites;
    int[] attack2Sprites;
    int[] attack3Sprites;
    int[] specialSprites;
    int[] hurtSprites;
    int[] deadSprites;

	void Start () {

        currentCharacter = GetComponent<PlayerController>().party[GetComponent<PlayerController>().currChar];
        plyState = currentCharacter.state;
        //plyState = ACT_CHAR_Base.STATES.IDLE;

        maxTmr = 2.0f;
        curTmr = maxTmr;
        loop = true;

        // Works for swordsman...
        idleSprites = new int[] { 0, 1, 2 };
        walkSprites = new int[] { 5, 6, 7, 8, 9 };
        attack1Sprites = new int[] { 10, 11, 12, 13 };
        attack2Sprites = new int[] { 15, 16, 17 };
        attack3Sprites = new int[] { 20, 21, 22, 23 };
        specialSprites = new int[] { 25, 26 };
        hurtSprites = new int[] { 30, 31 };
        deadSprites = new int[] { 31 };
	}
	
	void Update () {

        if (currentCharacter != GetComponent<PlayerController>().party[GetComponent<PlayerController>().currChar])
            currentCharacter = GetComponent<PlayerController>().party[GetComponent<PlayerController>().currChar];
        if (plyState != currentCharacter.state)
            ChangeState(currentCharacter.state);

        if (curTmr > 0)
        {
            curTmr -= Time.deltaTime;
            if (curTmr < 0)
            {
                //EndOfAnim(); // Engage things to do when the animation loops/ ends.
                curTmr = loop? maxTmr: 0; // reset to maxTmr if looping, otherwise set to 0 and stop updating timer.
            }
        }

        switch(plyState)
        {
            case ACT_CHAR_Base.STATES.IDLE:
                if (curTmr > maxTmr * 0.6f)
                    GetComponent<SpriteRenderer>().sprite = currentCharacter.sprites[idleSprites[0]];
                else if (curTmr > maxTmr * 0.5f)
                    GetComponent<SpriteRenderer>().sprite = currentCharacter.sprites[idleSprites[1]];
                else if (curTmr > maxTmr * 0.1f)
                    GetComponent<SpriteRenderer>().sprite = currentCharacter.sprites[idleSprites[2]];
                else if (curTmr >= 0)
                    GetComponent<SpriteRenderer>().sprite = currentCharacter.sprites[idleSprites[1]];
                break;
            case ACT_CHAR_Base.STATES.WALKING:
                if (currentCharacter.Act_facingRight)
                    transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
                else
                    transform.localEulerAngles = new Vector3(0.0f, 180.0f, 0.0f);

                if (curTmr > maxTmr * 0.8f)
                    GetComponent<SpriteRenderer>().sprite = currentCharacter.sprites[walkSprites[0]];
                else if (curTmr > maxTmr * 0.6f)
                    GetComponent<SpriteRenderer>().sprite = currentCharacter.sprites[walkSprites[1]];
                else if (curTmr > maxTmr * 0.4f)
                    GetComponent<SpriteRenderer>().sprite = currentCharacter.sprites[walkSprites[2]];
                else if (curTmr > maxTmr * 0.2f)
                    GetComponent<SpriteRenderer>().sprite = currentCharacter.sprites[walkSprites[3]];
                else if (curTmr >= 0)
                    GetComponent<SpriteRenderer>().sprite = currentCharacter.sprites[walkSprites[4]];
                break;
            case ACT_CHAR_Base.STATES.DASHING:
                if (currentCharacter.Act_facingRight)
                    transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
                else
                    transform.localEulerAngles = new Vector3(0.0f, 180.0f, 0.0f);

                if (curTmr > maxTmr * 0.8f)
                    GetComponent<SpriteRenderer>().sprite = currentCharacter.sprites[walkSprites[0]];
                else if (curTmr > maxTmr * 0.6f)
                    GetComponent<SpriteRenderer>().sprite = currentCharacter.sprites[walkSprites[1]];
                else if (curTmr > maxTmr * 0.4f)
                    GetComponent<SpriteRenderer>().sprite = currentCharacter.sprites[walkSprites[2]];
                else if (curTmr > maxTmr * 0.2f)
                    GetComponent<SpriteRenderer>().sprite = currentCharacter.sprites[walkSprites[3]];
                else if (curTmr >= 0)
                    GetComponent<SpriteRenderer>().sprite = currentCharacter.sprites[walkSprites[4]];
                break;
            case ACT_CHAR_Base.STATES.ATTACK_1:
                if (curTmr > maxTmr * 0.75f)
                    GetComponent<SpriteRenderer>().sprite = currentCharacter.sprites[attack1Sprites[0]];
                else if (curTmr > maxTmr * 0.5f)
                    GetComponent<SpriteRenderer>().sprite = currentCharacter.sprites[attack1Sprites[1]];
                else if (curTmr > maxTmr * 0.25f)
                    GetComponent<SpriteRenderer>().sprite = currentCharacter.sprites[attack1Sprites[2]];
                else if (curTmr >= 0)
                    GetComponent<SpriteRenderer>().sprite = currentCharacter.sprites[attack1Sprites[3]];
                break;
            case ACT_CHAR_Base.STATES.ATTACK_2:
                if (curTmr > maxTmr * 0.66f)
                    GetComponent<SpriteRenderer>().sprite = currentCharacter.sprites[attack2Sprites[0]];
                else if (curTmr > maxTmr * 0.66f)
                    GetComponent<SpriteRenderer>().sprite = currentCharacter.sprites[attack2Sprites[1]];
                else if (curTmr >= 0)
                    GetComponent<SpriteRenderer>().sprite = currentCharacter.sprites[attack2Sprites[2]];
                break;
            case ACT_CHAR_Base.STATES.ATTACK_3:
                if (curTmr > maxTmr * 0.75f)
                    GetComponent<SpriteRenderer>().sprite = currentCharacter.sprites[attack3Sprites[0]];
                else if (curTmr > maxTmr * 0.5f)
                    GetComponent<SpriteRenderer>().sprite = currentCharacter.sprites[attack3Sprites[1]];
                else if (curTmr > maxTmr * 0.25f)
                    GetComponent<SpriteRenderer>().sprite = currentCharacter.sprites[attack3Sprites[2]];
                else if (curTmr >= 0)
                    GetComponent<SpriteRenderer>().sprite = currentCharacter.sprites[attack3Sprites[3]];
                break;
            case ACT_CHAR_Base.STATES.SPECIAL:
                break;
            case ACT_CHAR_Base.STATES.HURT:
                break;
            case ACT_CHAR_Base.STATES.DYING:
                break;
            case ACT_CHAR_Base.STATES.USE:
                // Special FX because I didn't actually animate these
                break;
            default:
                GetComponent<SpriteRenderer>().sprite = currentCharacter.sprites[idleSprites[0]];
                break;
        }
	}

    void ChangeState(ACT_CHAR_Base.STATES _newstate)
    {
       plyState = _newstate;

    }


}

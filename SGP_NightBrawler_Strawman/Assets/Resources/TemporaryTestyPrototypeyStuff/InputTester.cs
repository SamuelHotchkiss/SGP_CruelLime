using UnityEngine;
using System.Collections;

public class InputTester : MonoBehaviour {

    // Quick class for testing input.  Object should visibly
    // move, rotate and scale differently for each input.
    public bool keyboard;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            MNGR_Save.Save();
            Debug.Log(MNGR_Save.saveFiles[0].theCharacters[0].Act_currHP);

            MNGR_Save.saveFiles[0].AssignGameManager();

            MNGR_Game.theCharacters[0].ChangeHP(-10);
            Debug.Log(MNGR_Game.theCharacters[0].Act_currHP);

            MNGR_Save.saveFiles[0].CopyGameManager();
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            MNGR_Save.Load();
            Debug.Log(MNGR_Save.saveFiles[0].theCharacters[0].Act_currHP);
            MNGR_Save.saveFiles[0].AssignGameManager();
        }

        // testing for keyboard input
        if (keyboard)
        {
            // get axis movement
            float horz = Input.GetAxis("Horizontal");
            float vert = Input.GetAxis("Vertical");

            // move the object
            GetComponent<Rigidbody2D>().velocity = new Vector2(horz, vert);

            // Confirm button sets scale to 2x
            if (Input.GetButton("Attack/Confirm"))
            {
                transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
            }
            // Cancel button sets scale to 1/2x
            else if (Input.GetButton("Special/Cancel"))
            {
                transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }
            // Switch Right button increases scale over time
            else if (Input.GetButton("SwitchRight"))
            {
                Vector3 scale = transform.localScale;
                scale *= 1.1f;
                transform.localScale = scale;
            }
            // Switch Left button decreases scale over time
            else if (Input.GetButton("SwitchLeft"))
            {
                Vector3 scale = transform.localScale;
                scale *= 0.9f;
                transform.localScale = scale;
            }
            
            float rotx = transform.localEulerAngles.x;
            float roty = transform.localEulerAngles.y;
            // Use button rotates the object
            if (Input.GetButton("Use"))
            {
                rotx ++;
                roty ++;
            }
            // Dodge button rotates the object based upon current movement
            if (Input.GetButton("Dodge"))
            {
                rotx += vert;
                roty += horz;
            }
            // reset stuff when it goes bad
            if (roty >= 360)
                roty -= 360;
            else if (roty < 0)
                roty += 360;
            // gimbal locking grrr
            if (rotx > 90)
                rotx -= 90;
            else if (rotx < 0)
                rotx += 85;

            transform.localEulerAngles = new Vector3(rotx, roty, 0);

            // Pause button makes the object invisible and resets everything
            if (Input.GetButton("Pause"))
            {
                if (GetComponent<MeshRenderer>() != null)
                    GetComponent<MeshRenderer>().enabled = false;
                else if (GetComponent<SpriteRenderer>() != null)
                    GetComponent<SpriteRenderer>().enabled = false;

                transform.position = new Vector3(0.0f, 0.0f, 0.0f);
                transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
            else
            {
                if (GetComponent<MeshRenderer>() != null)
                    GetComponent<MeshRenderer>().enabled = true;
                else if (GetComponent<SpriteRenderer>() != null)
                    GetComponent<SpriteRenderer>().enabled = true;
            }

        }
        // Testing for gamepad input
        // NOT TESTED YET PLZ FIX ERF PRBLMS OKAI BAI.
        else
        {
            // get axis movement
            float horz = Input.GetAxis("Pad_Horizontal");
            float vert = Input.GetAxis("Pad_Vertical");

            // move the object
            GetComponent<Rigidbody2D>().velocity = new Vector2(horz, vert);

            // Confirm button sets scale to 2x
            if (Input.GetButton("Pad_Attack/Confirm"))
            {
                transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
            }
            // Cancel button sets scale to 1/2x
            else if (Input.GetButton("Pad_Special/Cancel"))
            {
                transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }
            // Switch Right button increases scale over time
            else if (Input.GetButton("Pad_SwitchRight"))
            {
                Vector3 scale = transform.localScale;
                scale *= 1.1f;
                transform.localScale = scale;
            }
            // Switch Left button decreases scale over time
            else if (Input.GetButton("Pad_SwitchLeft"))
            {
                Vector3 scale = transform.localScale;
                scale *= 0.9f;
                transform.localScale = scale;
            }
            // Reset scale if these buttons are not pressed
            else
            {
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }

            float rotx = transform.localEulerAngles.x;
            float roty = transform.localEulerAngles.y;
            // Use button rotates the object
            if (Input.GetButton("Pad_Use"))
            {
                rotx++;
                roty++;
            }
            // Dodge button rotates the object based upon current movement
            rotx = Input.GetAxis("Pad_DodgeVertical");
            roty = Input.GetAxis("Pad_DodgeHorizontal");

            // reset stuff when it goes bad
            if (roty >= 360)
                roty -= 360;
            else if (roty < 0)
                roty += 360;
            // gimbal locking grrr
            if (rotx > 90)
                rotx -= 90;
            else if (rotx < 0)
                rotx += 85;

            transform.localEulerAngles = new Vector3(rotx, roty, 0);

            // Pause button makes the object invisible
            if (Input.GetButton("Pad_Pause"))
            {
                if (GetComponent<MeshRenderer>() != null)
                    GetComponent<MeshRenderer>().enabled = false;
                else if (GetComponent<SpriteRenderer>() != null)
                    GetComponent<SpriteRenderer>().enabled = false;

                transform.position = new Vector3(0.0f, 0.0f, 0.0f);
                transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
            else
            {
                if (GetComponent<MeshRenderer>() != null)
                    GetComponent<MeshRenderer>().enabled = true;
                else if (GetComponent<SpriteRenderer>() != null)
                    GetComponent<SpriteRenderer>().enabled = true;
            }

        }
	}
}

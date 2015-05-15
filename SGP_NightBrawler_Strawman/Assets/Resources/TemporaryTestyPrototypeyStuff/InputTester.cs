using UnityEngine;
using System.Collections;

public class InputTester : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    void Update()
    {
        bool keyboard = true;

        if (keyboard)
        {
            float horz = Input.GetAxis("Horizontal");
            float vert = Input.GetAxis("Vertical");

            GetComponent<Rigidbody2D>().velocity = new Vector2(horz, vert);

            if (Input.GetButton("Attack/Confirm"))
            {
                transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
            }
            else if (Input.GetButton("Special/Cancel"))
            {
                transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }
            else if (Input.GetButton("SwitchRight"))
            {
                Vector3 scale = transform.localScale;
                scale *= 1.1f;
                //if (scale.magnitude > 1000.0f)
                    //scale = new Vector3(1.0f, 1.0f, 1.0f);
                transform.localScale = scale;
            }
            else if (Input.GetButton("SwitchLeft"))
            {
                Vector3 scale = transform.localScale;
                scale *= 0.9f;
                //if (scale.magnitude < 0.001f)
                    //scale = new Vector3(1.0f, 1.0f, 1.0f);
                transform.localScale = scale;
            }
            else
            {
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
            
            float rotx = transform.localEulerAngles.x;
            float roty = transform.localEulerAngles.y;
            if (Input.GetButton("Use"))
            {
                rotx += horz;
                roty += vert;
            }
            if (Input.GetButton("Dodge"))
            {
                rotx += (10 * horz);
                roty += (10 * vert);
            }
            if (rotx > 360)
                rotx = 0;
            if (roty > 360)
                roty = 0;

            transform.localEulerAngles = new Vector3(rotx, roty, 0);

            if (Input.GetButton("Pause"))
            {
                GetComponent<MeshRenderer>().enabled = false;
            }
            else
                GetComponent<MeshRenderer>().enabled = true;

        }
	}
}

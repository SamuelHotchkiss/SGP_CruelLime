using UnityEngine;
using System.Collections;

public class PROJ_Twee : PROJ_PiercingArrow 
{

    float rotSpeed = 0.1f;
    float dir = -1.0f;
    float life_time = 0.0f;
    public override void Initialize(bool _r = true, float _damMult = 1.0f)
    {
        base.Initialize(_r, _damMult);

        float playerloc = GameObject.FindGameObjectWithTag("Player").transform.position.x;

        if (playerloc > transform.position.x)
            dir = -dir;
        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    // Update is called once per frame
	public override void Update()
    {

        if ( transform.localEulerAngles.z < 90 || transform.localEulerAngles.z > 270 )
        {
            // awkward this type of manipulation is.
            Vector3 newAng = transform.localEulerAngles;
            newAng.z += rotSpeed * dir;
            transform.localEulerAngles = newAng;

            rotSpeed += rotSpeed * 0.1f;

        }
        else if (life_time == 0.0f)
        {
            // awkward this type of manipulation is.
            Vector3 newAng = transform.localEulerAngles;
            newAng.z = 90.0f * dir;
            transform.localEulerAngles = newAng;

            life_time = 5.0f;
            GetComponent<BoxCollider2D>().enabled = false;
            //GetComponent<BoxCollider2D>().isTrigger = false; //was a cool idea but physics layers, so eh.
        }

        if (life_time > 0.0f)
        {
            life_time -= Time.deltaTime;
            if (life_time < 0.0f)
            {
                ProjectileExpired();
            }
        }

    }
}

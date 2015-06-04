using UnityEngine;
using System.Collections;

public class PROJ_Twee : PROJ_PiercingArrow 
{

    float rotSpeed = 0.1f;
    float dir = -1.0f;
    float lifetime = 0.0f;
    public override void Initialize(bool _r = true, float _damMult = 1.0f)
    {
        base.Initialize(_r, _damMult);

        float playerloc = GameObject.FindGameObjectWithTag("Player").transform.position.x;

        if (playerloc > transform.position.x)
            dir = -dir;
        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {

        if ( transform.localEulerAngles.z < 90 || transform.localEulerAngles.z > 270 )
        {
            // awkward this type of manipulation is.
            Vector3 newAng = transform.localEulerAngles;
            newAng.z += rotSpeed * dir;
            transform.localEulerAngles = newAng;

            rotSpeed += rotSpeed * 0.1f;

        }
        else if (lifetime == 0.0f)
        {
            // awkward this type of manipulation is.
            Vector3 newAng = transform.localEulerAngles;
            newAng.z = 90.0f * dir;
            transform.localEulerAngles = newAng;

            lifetime = 5.0f;
            GetComponent<BoxCollider2D>().enabled = false;
            //GetComponent<BoxCollider2D>().isTrigger = false; //was a cool idea but physics layers, so eh.
        }

        if (lifetime > 0.0f)
        {
            lifetime -= Time.deltaTime;
            if (lifetime < 0.0f)
            {
                ProjectileExpired();
            }
        }

    }
}

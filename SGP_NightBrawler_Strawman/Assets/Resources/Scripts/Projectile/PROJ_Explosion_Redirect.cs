using UnityEngine;
using System.Collections;

public class PROJ_Explosion_Redirect : PROJ_Explosion
{
    public override void Initialize(bool _r = true, float _damMult = 1.0f)
    {
        base.Initialize(_r, _damMult);
        gameObject.layer = 14;
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Projectile")
        {
            collision.gameObject.GetComponent<PROJ_Base>().velocity =
                -collision.gameObject.GetComponent<PROJ_Base>().velocity;
            collision.gameObject.layer = 10;
        }
    }

}

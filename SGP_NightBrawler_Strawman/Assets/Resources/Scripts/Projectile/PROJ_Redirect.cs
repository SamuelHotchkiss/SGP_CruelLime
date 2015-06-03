using UnityEngine;
using System.Collections;

public class PROJ_Redirect : PROJ_Explosive 
{
    public override void Initialize(bool _r = true)
    {
        base.Initialize(_r);
        gameObject.layer = 14;
    }

    public override void Update()
    {
        base.Update();
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Projectile")
            base.ProjectileExpired();
        else
            base.OnTriggerEnter2D(collision);
    }

}

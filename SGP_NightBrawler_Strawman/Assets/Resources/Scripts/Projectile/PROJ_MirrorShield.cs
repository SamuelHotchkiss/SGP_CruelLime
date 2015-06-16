using UnityEngine;
using System.Collections;

public class PROJ_MirrorShield : PROJ_Explosion_Redirect
{

    // Use this for initialization
    public override void Initialize(bool _r = true, float _damMult = 1.0f)
    {
        base.Initialize(_r, _damMult);
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.75f, 0);
        transform.SetParent(owner.transform);
    }
}

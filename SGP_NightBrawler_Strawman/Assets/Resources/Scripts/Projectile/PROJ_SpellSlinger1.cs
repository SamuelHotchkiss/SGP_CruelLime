using UnityEngine;
using System.Collections;

public class PROJ_SpellSlinger1 : PROJ_Base
{
    public GameObject sibling;

    public override void Initialize(bool _r = true, float _damMult = 1.0f)
    {
        base.Initialize(_r);

        GameObject clone1, clone2;

        clone1 = (GameObject)Instantiate(sibling, owner.transform.position, Quaternion.identity);
        clone2 = (GameObject)Instantiate(sibling, owner.transform.position, Quaternion.identity);

        clone1.GetComponent<PROJ_Base>().owner = owner;
        clone2.GetComponent<PROJ_Base>().owner = owner;

        clone1.GetComponent<PROJ_Base>().Initialize(_r);
        clone2.GetComponent<PROJ_Base>().Initialize(_r);

        if(_r)
        {
            clone1.GetComponent<PROJ_Base>().velocity = new Vector2(1, 0.25f);
            clone2.GetComponent<PROJ_Base>().velocity = new Vector2(1, -0.25f);
        }
        else
        {
            clone1.GetComponent<PROJ_Base>().velocity = new Vector2(-1, 0.25f);
            clone2.GetComponent<PROJ_Base>().velocity = new Vector2(-1, -0.25f);
        }
    }

}

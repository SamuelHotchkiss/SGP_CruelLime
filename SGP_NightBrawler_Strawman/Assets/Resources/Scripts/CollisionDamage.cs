using UnityEngine;
using System.Collections;

public class CollisionDamage : MonoBehaviour {

    //This is Just for any Enemy that needs to do damage on contact with the player without any need for 
    //a projectile. ONLY place this in enemies that need it. 
    void OnCollisionEnter2D(Collision2D Col)
    {
        if (Col.gameObject.tag == "Player")
        {
            PlayerController ColPly = Col.gameObject.GetComponent<PlayerController>();
            ACT_Enemy ColEmy  = GetComponent<ACT_Enemy>();
            ColPly.party[ColPly.currChar].ChangeHP(-ColEmy.Act_currPower);
            if (ColEmy.TimeThresh >= 0.0f)
                ColEmy.TimeThresh = 0.0f;
        }
    }

    void OnTriggerEnter2D(Collider2D Col)
    {
        if (Col.gameObject.tag == "Player")
        {
            PlayerController ColPly = Col.gameObject.GetComponent<PlayerController>();
            ACT_Enemy ColEmy = GetComponent<ACT_Enemy>();
            ColPly.party[ColPly.currChar].ChangeHP(-ColEmy.Act_currPower);
            if (ColEmy.TimeThresh >= 0.0f)
                ColEmy.TimeThresh = 0.0f;
        }
    }

}

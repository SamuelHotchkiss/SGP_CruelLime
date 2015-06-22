using UnityEngine;
using System.Collections;

public class PROJ_Melee : PROJ_Base 
{

    public override void Initialize(bool _r = true, float _damMult = 1.0f)
    {
        base.Initialize(_r, _damMult);
    }
	
	// Update is called once per frame
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("HIT!");
        if (collision.gameObject.tag == "Enemy"
            || collision.gameObject.tag == "Obstacle")
        {
            if (collision.gameObject.GetComponent<ACT_Enemy>().Act_currHP <= 0)
                return;
            collision.gameObject.GetComponent<ACT_Enemy>().ChangeHP(-power);
        }
        else if (collision.gameObject.tag == "Player")
        {
            // Find the active character
            if (collision.gameObject.GetComponent<PlayerController>() != null)
            {
                PlayerController player = collision.gameObject.GetComponent<PlayerController>();
                int target = player.currChar;

                // Mess with the active character
                player.party[target].ChangeHP(-power);
            }
        }
    }


}

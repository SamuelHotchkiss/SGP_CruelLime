using UnityEngine;
using System.Collections;

public class PROJ_Melee : PROJ_Base 
{

    public override void Initialize()
    {
        base.Initialize();
    }
	
	// Update is called once per frame
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("HIT!");
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<ACT_Enemy>().ChangeHP(-power);
        }
        else if (collision.gameObject.tag == "Player")
        {
            // Find the active character
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            int target = player.currChar;

            // Mess with the active character
            player.party[target].ChangeHP(-power);
        }
    }

    //public void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Debug.Log("HIT!");
    //    if (collision.gameObject.tag == "Enemy")
    //    {
    //        collision.gameObject.GetComponent<ACT_Enemy>().ChangeHP(-power);
    //    }
    //    else if (collision.gameObject.tag == "Player")
    //    {
    //        // Find the active character
    //        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
    //        int target = player.currChar;

    //        // Mess with the active character
    //        player.party[target].ChangeHP(-power);
    //    }
    //}
}

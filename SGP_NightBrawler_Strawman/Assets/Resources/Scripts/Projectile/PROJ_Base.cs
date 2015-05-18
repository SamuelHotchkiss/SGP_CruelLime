using UnityEngine;
using System.Collections;

public class PROJ_Base : MonoBehaviour
{
    public GameObject owner;  // who fired us?
    Vector2 velocity;         // which way are we going?
    int power;                // how much damage will we deal?

    public float speed;       // how fast are we moving?

    public void Initialize()
    {
        gameObject.layer = owner.layer;

        // Are you my mommy?
        if(owner.tag == "Player")
        {
            owner = GameObject.FindGameObjectWithTag("Player");

            PlayerController player = owner.GetComponent<PlayerController>();
            int target = player.currChar;

            power = player.party[target].Act_currPower;

            bool right = player.party[target].Act_facingRight;

            if (right)
                velocity = new Vector2(1, 0) * speed;
            else
                velocity = new Vector2(-1, 0) * speed;
        }
        else if (owner.tag == "Enemy")
        {
            bool right = owner.GetComponent<ACT_Enemy>().Act_facingRight;

            power = owner.GetComponent<ACT_Enemy>().Act_currPower;

            if (right)
                velocity = new Vector2(1, 0) * speed;
            else
                velocity = new Vector2(-1, 0) * speed;
        }

        GetComponent<Rigidbody2D>().velocity = velocity;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("HIT!");
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<ACT_Enemy>().ChangeHP(-power);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Player")
        {
            // Find the active character
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            int target = player.currChar;

            // Mess with the active character
            player.FindPartyMember(target).ChangeHP(-power);

            Destroy(gameObject);
        }
    }
}

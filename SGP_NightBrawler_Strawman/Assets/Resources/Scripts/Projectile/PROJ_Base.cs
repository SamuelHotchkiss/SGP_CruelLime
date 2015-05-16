using UnityEngine;
using System.Collections;

public class PROJ_Base : MonoBehaviour
{
    public GameObject owner;  // who fired us?
    Vector2 velocity;         // which way are we going?
    int power;                // how much damage will we deal?
    Rigidbody2D myBody;       // PHYSICS
    //bool isPiercing;

    public float speed;       // how fast are we moving?

    // Use this for initialization
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();

        // Are you my mommy?
        if(owner.tag == "Player")
        {
            bool right = owner.GetComponent<PlayerController>().party[owner.GetComponent<PlayerController>().currChar].Act_facingRight;

            if (right)
                velocity = new Vector2(1, 0) * speed;
            else
                velocity = new Vector2(-1, 0) * speed;
        }
        else if (owner.tag == "Enemy")
        {
            bool right = owner.GetComponent<ACT_Enemy>().Act_facingRight;

            if (right)
                velocity = new Vector2(1, 0) * speed;
            else
                velocity = new Vector2(-1, 0) * speed;
        }
    }

    public virtual void OnCollisonEnter2D(Collider2D other)
    {
        // We didn't hit our owner did we?
        if (other.gameObject != owner)
        {
            if (other.gameObject.tag == "Player")
            {
                int target = other.GetComponent<PlayerController>().currChar;
                other.GetComponent<PlayerController>().party[target].ChangeHP(-power);
                Destroy(gameObject); // I'll just show myself out
            }
            else if (other.gameObject.tag == "Enemy")
            {
                other.GetComponent<ACT_Enemy>().ChangeHP(-power);
                Destroy(gameObject); // I'll just show myself out
            }
        }
    }
}

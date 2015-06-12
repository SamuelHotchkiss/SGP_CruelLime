using UnityEngine;
using System.Collections;

public class PROJ_Base : MonoBehaviour
{
    public GameObject owner;  // who fired us?

    protected Vector2 start;            // where are we from?
    public Vector2 velocity;         // which way are we going?
    public int power;         // how much damage will we deal?
    public float m_distance;    // how far have we gone?

    public float speed;       // how fast are we moving?
    public float range;       // how far can we go?
    public float lifetime;
	public bool right;

	public bool knockback;

    public virtual void Initialize(bool _r = true, float _damMult = 1.0f)
    {
		if (owner == null)
			return;
        // Are you my mommy?
        if(owner.tag == "Player")
        {
            //owner = GameObject.FindGameObjectWithTag("Player");
            if (gameObject.layer == 0) // dont change the layer if it's already set to something.
                gameObject.layer = 10;

            PlayerController player = owner.GetComponent<PlayerController>();
            int target = player.currChar;

            power += player.party[target].Act_currPower;
            power = (int)(_damMult * (float)power);

            //bool right = player.party[target].Act_facingRight;
            right = _r;

            if (right)
                velocity = new Vector2(1, 0);// * speed;
            else
            {
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                velocity = new Vector2(-1, 0);// * speed;
            }
        }
        else if (owner.tag == "Enemy")
        {
            if (gameObject.layer == 0) // dont change the layer if it's already set to something.
                gameObject.layer = 11;

            bool right = owner.GetComponent<ACT_Enemy>().Act_facingRight;

            power += owner.GetComponent<ACT_Enemy>().Act_currPower;
            power = (int)(_damMult * (float)power);

            if (right)
                velocity = new Vector2(1, 0);// * speed;
            else
			{
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                velocity = new Vector2(-1, 0);// *speed;
			}
        }

        //GetComponent<Rigidbody2D>().velocity = velocity;
        start = new Vector2(transform.position.x, transform.position.y);
    }

    public virtual void Update()
    {
		if (MNGR_Game.paused)
			return;

        transform.position += (new Vector3(velocity.x * speed, velocity.y * speed, 0) * Time.deltaTime);

        m_distance = Mathf.Sqrt((start.x - transform.position.x) * (start.x - transform.position.x));
        if (m_distance >= range)
            ProjectileExpired();

        if (lifetime > 0)
        {
            lifetime -= Time.deltaTime;
            if (lifetime < 0)
                ProjectileExpired();
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("HIT!");
        if (gameObject.tag == "Player") // for projectiles that are tagged as players! (Defender's wall)
        {
            return;
        }
        if (collision.gameObject.tag == "Enemy"
            || collision.gameObject.tag == "Obstacle")
        {
            if (gameObject != null)
                ProjectileExpired();
            collision.gameObject.GetComponent<ACT_Enemy>().ChangeHP(-power);
        }
        else if (collision.gameObject.tag == "Player")
        {
            // Find the active character
            if (collision.gameObject.GetComponent<PlayerController>())
            {
                PlayerController player = collision.gameObject.GetComponent<PlayerController>();
                int target = player.currChar;


                
				if (knockback)
				{
                    player.party[target].ChangeHP(-power, false);
					if (right)
						player.ApplyKnockBack(power * 5);
					else
						player.ApplyKnockBack(-power * 5);
				}
                else
                    player.party[target].ChangeHP(-power);
                // Mess with the active character
                

            }

            if (gameObject != null)
                ProjectileExpired();
        }
        else if (gameObject.tag != "Player") // weird check for the defender's wall (has to stop enemy projectiles but not ours)
        {
            if (gameObject != null)
                ProjectileExpired();
        }
    }

    protected virtual void ProjectileExpired()
    {
        Destroy(gameObject);
    }


}

using UnityEngine;
using System.Collections;

public class PROJ_Explosion : PROJ_Base
{
    float timer;
    public float timerMax;
    //public float radius;                    // screw you and everyone you knew and loved
    public float forcestr;
    public Vector2 forcedir;
    public Sprite[] sprites;

    public override void Initialize(bool _r = true, float _damMult = 1.0f)
    {
        timer = timerMax;
        speed = 0;
        if (sprites.Length > 0)
            GetComponent<SpriteRenderer>().sprite = sprites[0];

        base.Initialize(_r);
        // flip the circle collider if the owner faces the other way
        // just for swordsman now.
        if (owner == null)
            return;

        if (owner.tag == "Player")
        {
            if (gameObject.layer == 0) // dont change the layer if it's already set to something.
                gameObject.layer = 10;
            if (owner.GetComponent<PlayerController>().party[owner.GetComponent<PlayerController>().currChar].characterIndex == 0)
            {
                power = (int)((float)power * 0.75f);
                power = (int)(_damMult * (float)power);
                if (!owner.GetComponent<PlayerController>().party[owner.GetComponent<PlayerController>().currChar].Act_facingRight)
                {
                    Vector2 offset = GetComponent<CircleCollider2D>().offset;
                    offset.x = -offset.x;
                    GetComponent<CircleCollider2D>().offset = offset;
                }
            }
        }
    }

    // Update is called once per frame
    public override void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
            ProjectileExpired();

        // hacky as chit.
        if (sprites.Length == 8)
        {
            if (timer > timerMax * 0.875)
                GetComponent<SpriteRenderer>().sprite = sprites[0];
            else if (timer > timerMax * 0.75)
                GetComponent<SpriteRenderer>().sprite = sprites[1];
            else if (timer > timerMax * 0.625)
                GetComponent<SpriteRenderer>().sprite = sprites[2];
            else if (timer > timerMax * 0.5)
                GetComponent<SpriteRenderer>().sprite = sprites[3];
            else if (timer > timerMax * 0.375)
                GetComponent<SpriteRenderer>().sprite = sprites[4];
            else if (timer > timerMax * 0.25)
                GetComponent<SpriteRenderer>().sprite = sprites[5];
            else if (timer > timerMax * 0.125)
                GetComponent<SpriteRenderer>().sprite = sprites[6];
            else
                GetComponent<SpriteRenderer>().sprite = sprites[7];
        }

        base.Update();
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy"
            || collision.gameObject.tag == "Obstacle")
        {
            collision.gameObject.GetComponent<ACT_Enemy>().ChangeHP(-power);

            if (forcestr > 0)
            {
                Vector2 ownpos;
                ownpos.x = owner.transform.position.x;
                ownpos.y = owner.transform.position.y;
                Vector2 colpos;
                colpos.x = collision.gameObject.transform.position.x;
                colpos.y = collision.gameObject.transform.position.y;

                forcedir = colpos - ownpos;

                collision.gameObject.GetComponent<ACT_Enemy>().ApplyKnockBack(forcedir * forcestr);
            }
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
}

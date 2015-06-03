using UnityEngine;
using System.Collections;

public class ITM_Coin : MonoBehaviour {

    public int Coin_Amount;
    public AudioClip Coin_PickUp;
    public GameObject player;

    private SpriteRenderer sprite;
    private Color StartColor;
    private Color EndColor;
    private float OriginalTimer;
    private float Blinks;
    public Vector3 ParentObjectPos;

    public float Timer = 10.0f;

    public ITM_Coin(Vector3 Parent)
    {
        ParentObjectPos = Parent;
    }

    // Use this for initialization
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        ParentObjectPos = transform.position;
        StartColor = sprite.color;
        EndColor = new Color(StartColor.r, StartColor.g, StartColor.b, 0.0f);
        Timer = 15.0f;
        Blinks = 0f;
        OriginalTimer = Timer;
    }

    // Update is called once per frame
    void Update()
    {

        Timer -= Time.deltaTime;

        if (Timer <= (OriginalTimer * 0.25f))
        {
            if (Blinks > 0)
                sprite.material.color = EndColor;
            else
                sprite.material.color = StartColor;
        }

        if (Timer <= (OriginalTimer * 0.25f) && Blinks <= 0f)
            Blinks = 0.10f;
        else if (Timer <= 0)
            Destroy(gameObject);


        Blinks -= Time.deltaTime;

        if (transform.position.y < ParentObjectPos.y)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            GetComponent<Rigidbody2D>().gravityScale = 0f;
        }
    }

	void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Player" && transform.position.y < ParentObjectPos.y)
        {
			AudioSource.PlayClipAtPoint(Coin_PickUp, new Vector3(0, 0, 0), 1.0f);
			MNGR_Game.wallet += Coin_Amount;
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * Random.Range(300, 400));
        }
    }
}

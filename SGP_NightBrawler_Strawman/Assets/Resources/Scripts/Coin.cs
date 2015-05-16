using UnityEngine;
using System.Collections;

public class Coin : Item {

    public int Coin_Amount;
    public AudioClip Coin_PickUp;

	// Use this for initialization
	void Start () {
        Coin_Amount = 1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void OnTriggerEnter2D(Collider2D Col)
    {
       
        if (Col.name == "Party")
        {
            AudioSource.PlayClipAtPoint(Coin_PickUp, new Vector3(0, 0, 0), 1.0f);
            MNGR_Game.wallet += Coin_Amount;
            base.OnTriggerEnter2D(Col);
        }

    }
}

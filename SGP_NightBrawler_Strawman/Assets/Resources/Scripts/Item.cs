using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour 
{

    public int effectID;

	// Use this for initialization
	void Start () {
	    
	}
	// Update is called once per frame
    public virtual void Update()
    {

	}

    public virtual void OnTriggerEnter2D(Collider2D Col)
    {
        MNGR_Item.AttachModifier(effectID, Col.gameObject);
        MNGR_Game.theInventory.Increment(effectID);
        Destroy(gameObject);
    }
}

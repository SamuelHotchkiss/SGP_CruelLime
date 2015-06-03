using UnityEngine;
using System.Collections;

public class ITM_Item : MonoBehaviour 
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
        MNGR_Game.usedItem = false;
        MNGR_Game.equippedItem = 3;

        Debug.Log(MNGR_Game.theInventory.containers[3].count);

        if (effectID == 3)
            MNGR_Game.theInventory.Increment(effectID);
        else
            MNGR_Item.AttachModifier(effectID, Col.gameObject);

        Debug.Log(MNGR_Game.theInventory.containers[3].count);

        Destroy(gameObject);
    }
}

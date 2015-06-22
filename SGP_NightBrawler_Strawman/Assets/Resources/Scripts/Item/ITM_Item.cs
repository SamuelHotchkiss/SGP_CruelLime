using UnityEngine;
using System.Collections;

public class ITM_Item : MonoBehaviour
{

    public int effectID;

    // Use this for initialization
    void Start()
    {

    }
    // Update is called once per frame
    public virtual void Update()
    {
        if (GetComponent<SpriteRenderer>() != null && GameObject.Find("Reference_Point") != null)
            GetComponent<SpriteRenderer>().sortingOrder = (int)((GameObject.Find("Reference_Point").transform.position.y - transform.position.y) * 100.0f);
    }

    public virtual void OnTriggerEnter2D(Collider2D Col)
    {
        if (Col.tag == "Player")
        {
            MNGR_Item.AttachModifier(effectID, Col.gameObject);
            Destroy(gameObject);
        }
    }
}

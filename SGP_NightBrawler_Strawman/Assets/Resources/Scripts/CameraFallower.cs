using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {

	public GameObject Cam_CurrTarget;
    public bool Cam_Collision;
    //public bool Cam_CollisionY;
	// Use this for initialization
	void Start () 
	{
        Cam_Collision = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
        if (Cam_Collision)
        {
            float Xpos;
            float Ypos;
            Xpos = Mathf.Lerp(transform.position.x, Cam_CurrTarget.transform.position.x, 0.1f);
            Ypos = Mathf.Lerp(transform.position.y, Cam_CurrTarget.transform.position.y, 0.1f);
            transform.position = new Vector3(Xpos, Ypos, -10);
        }
		//transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
	}

    void OnCollisionEnter2D(Collision2D Col)
    {
        if (Col.gameObject.tag == "Bounds")
        {
            Cam_Collision = false;
        }
    }

    void OnTriggerEnter2D(Collider2D Col)
    {
        if (Col.tag == "Player")
        {
            //if (Col.transform.position.y > transform.position.y || Col.transform.position.y < transform.position.y )
                Cam_Collision = true;
            //if (Col.transform.position.x > transform.position.x || Col.transform.position.x < transform.position.x)
               // Cam_Collision = true;
        }
    }
}

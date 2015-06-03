using UnityEngine;
using System.Collections;

public class CameraFollower : MonoBehaviour
{

	public GameObject Cam_CurrTarget;
    public bool Cam_Collision;
    public GameObject[] Cam_Egdes;

    Camera Cam_This; 

	void Start () 
	{
        Cam_Collision = true;
        Cam_This = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () 
	{
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        if (Cam_Collision)
        {
            float Xpos;
            float Ypos;
            Xpos = Mathf.Lerp(transform.position.x, Cam_CurrTarget.transform.position.x, 0.1f);
            Ypos = Mathf.Lerp(transform.position.y, Cam_CurrTarget.transform.position.y, 0.1f);

            if (Ypos < -1.5f)
                Ypos = -1.5f;
            else if (Ypos > 1.5f)
                Ypos = 1.5f;

            transform.position = new Vector3(Xpos, Ypos, -10);
        }

        for (int i = 0; i < Cam_Egdes.Length; i++)
        {
            BoxCollider2D BoxCol = Cam_Egdes[i].GetComponent<BoxCollider2D>();
            if (i == 0 || i == 2)
                BoxCol.offset = new Vector2(-(Cam_This.orthographicSize * Cam_This.aspect) + (BoxCol.size.x / 2), BoxCol.offset.y);
            if (i == 1 || i == 3)
                BoxCol.offset = new Vector2((Cam_This.orthographicSize * Cam_This.aspect) - (BoxCol.size.x / 2), BoxCol.offset.y); 
        }
	}

    void OnCollisionEnter2D(Collision2D Col)
    {
        if (Col.gameObject.tag == "Bounds")
        {
            Cam_Collision = false;
            Debug.Log("Say What!");
        }
    }

    void OnTriggerExit2D(Collider2D Col)
    {
        if (Col.tag == "Player")
            Cam_Collision = true;
                
    }
}

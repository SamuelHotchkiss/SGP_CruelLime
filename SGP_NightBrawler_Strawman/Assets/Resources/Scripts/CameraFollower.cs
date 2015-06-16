using UnityEngine;
using System.Collections;

public class CameraFollower : MonoBehaviour
{
	public GameObject Cam_CurrTarget;
    public float Cam_Speed;
    void Start()
    {
        if (Cam_Speed <= 0.0f)
            Cam_Speed = 3.5f;
    }

	// Update is called once per frame
	void Update () 
	{
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        float Ypos;
        float Xpos;
        Ypos = Mathf.Lerp(transform.position.y, Cam_CurrTarget.transform.position.y, (Cam_Speed * Time.deltaTime));
        if (Ypos < -1.5f)
               Ypos = -1.5f;
        Xpos = Mathf.Lerp(transform.position.x, Cam_CurrTarget.transform.position.x, (Cam_Speed * Time.deltaTime));
        transform.position = new Vector3(Xpos, Ypos, -10);
	}
}

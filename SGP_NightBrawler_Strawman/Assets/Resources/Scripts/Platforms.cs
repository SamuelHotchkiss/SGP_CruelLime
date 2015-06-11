using UnityEngine;
using System.Collections;

public class Platforms : MonoBehaviour {

    public float Plt_Speed;
    public bool Plt_facingRight;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x, transform.position.y + Plt_Speed);
        if (transform.position.y > 2f || transform.position.y < -2f)
            Plt_Speed = -Plt_Speed;
	}
}

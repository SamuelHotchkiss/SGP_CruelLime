using UnityEngine;
using System.Collections;

public class PROJ_Base : MonoBehaviour {

    Vector2 velocity;
    int power;
    Rigidbody2D myBody;

    public float speed;

	// Use this for initialization
	void Start () 
    {

	}
	
	// Update is called once per frame
	void Update () 
    {
	}

    void OnCollisonEnter2D(Collider2D other)
    {

    }
}

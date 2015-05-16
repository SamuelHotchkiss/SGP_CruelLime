using UnityEngine;
using System.Collections;

public class PROJ_Base : MonoBehaviour 
{

    Vector2 velocity;
    int power;
    Rigidbody2D myBody;
    bool isPiercing;

    public float speed;

	// Use this for initialization
	void Start () 
    {
        myBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () 
    {
	}

    virtual void OnCollisonEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            int target = other.GetComponent<PlayerController>().currChar;
            other.GetComponent<PlayerController>().party[target].ChangeHP(-power);
        }
        else if (other.gameObject.tag == "Enemy")
        {
            other.GetComponent<ACT_Enemy>().ChangeHP(-power);
        }
    }
}

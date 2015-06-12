using UnityEngine;
using System.Collections;

public class WarpSpot : MonoBehaviour 
{
    public GameObject destination;

    // S: hardcoded nonsense to get some basic level functionality
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            other.transform.position = new Vector3(destination.transform.position.x, destination.transform.position.y, 0);
            Camera.main.transform.position = new Vector3(destination.transform.position.x, destination.transform.position.y, -10.0f);
        }
    }
	
}

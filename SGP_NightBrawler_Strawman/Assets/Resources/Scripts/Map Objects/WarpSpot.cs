using UnityEngine;
using System.Collections;

public class WarpSpot : MonoBehaviour 
{
    public GameObject destination;
	public GameObject enemies;
	public bool activateEnemies;

    // S: hardcoded nonsense to get some basic level functionality
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            other.transform.position = new Vector3(destination.transform.position.x, destination.transform.position.y, 0);
			GameObject.Find("Main Camera").gameObject.transform.position = new Vector3(destination.transform.position.x, destination.transform.position.y, -10.0f);
        }
		if (activateEnemies)
		{
			enemies.gameObject.SetActive(true);
		}
    }
	
}

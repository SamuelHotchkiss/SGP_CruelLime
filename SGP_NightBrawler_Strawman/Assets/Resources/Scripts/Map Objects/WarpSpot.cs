using UnityEngine;
using System.Collections;

public class WarpSpot : MonoBehaviour 
{
    public GameObject destination;
	public GameObject enemies;
	public GameObject horde;
    public string BossLvl;
	public bool activateEnemies;
	private bool activateHorde;
    public bool bossRoom;

	void Start()
	{
		activateHorde = MNGR_Game.dangerZone;
		if (horde != null)
		{
			horde.gameObject.SetActive(false);
		}
	}


    // S: hardcoded nonsense to get some basic level functionality
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if (!bossRoom)
            {
                other.transform.position = new Vector3(destination.transform.position.x, destination.transform.position.y, 0);
                GameObject.Find("Main Camera").gameObject.transform.position = new Vector3(destination.transform.position.x, destination.transform.position.y, -10.0f); 
            }
            else
                Application.LoadLevel(BossLvl);
        }
		if (activateEnemies)
		{
			enemies.gameObject.SetActive(true);
		}
		if (activateHorde)
		{
			horde.gameObject.SetActive(true);
		}
    }
	
}

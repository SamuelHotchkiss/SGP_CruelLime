using UnityEngine;
using System.Collections;

public class Horde : MonoBehaviour 
{
	public GameObject player;
	public PROJ_Base projectile;
	public float travelSpeed;
	public float arrowSpeed;
	public float fireRate;
	public float timer;
	public float offset;
	public float distanceToPlayer;

	// Use this for initialization
	void Start () 
	{
		timer = fireRate;
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () 
	{
		distanceToPlayer = Mathf.Abs(player.transform.position.x - transform.position.x);
		fireRate = distanceToPlayer * 0.01f;

		if (fireRate < 0.0f)
			fireRate = 0.2f;

		if (timer > 0)
		{
			timer -= Time.deltaTime;
		}
		else
		{
			Vector3 spawn = new Vector3(transform.position.x, transform.position.y + (Random.value * offset), transform.position.z);
			Instantiate(projectile, spawn, Quaternion.identity);
			projectile.velocity = new Vector2(arrowSpeed, 0.0f);
			timer = fireRate;
		}

		transform.position = new Vector3(transform.position.x + (travelSpeed * Time.deltaTime), transform.position.y, transform.position.z);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Player")
		{
			player.GetComponent<PlayerController>().MurderEveryone();
		}
	}
}

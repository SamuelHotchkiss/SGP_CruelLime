using UnityEngine;
using System.Collections;

public class ITEM_DropLoot : MonoBehaviour 
{
	public int MinCoins = 0;
	public int MaxCoins = 0;
	public float xRange = 50;
	public float yRange = 100;
	public Coin Coin;
	public int potionID = 0;

	public void DropCoin(Vector3 Host)
	{
		float TotalCoins = Random.Range(MinCoins, MaxCoins);
		for (int i = 0; i < TotalCoins; i++)
		{
			Vector3 loc = new Vector3(Host.x + Random.Range((-xRange * 0.01f), (xRange * 0.01f)), Host.y - Random.Range(1.0f, 1.5f));
			Coin Clone = Instantiate(Coin, loc, Quaternion.identity) as Coin;
			Clone.GetComponent<Rigidbody2D>().AddForce(Vector2.right * Random.Range(-xRange, xRange));
			Clone.GetComponent<Rigidbody2D>().AddForce(Vector2.up * Random.Range(yRange, yRange + 300));
		}

	}
}

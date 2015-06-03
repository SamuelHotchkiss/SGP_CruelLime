using UnityEngine;
using System.Collections;

public class ITM_DropLoot : MonoBehaviour 
{
	public int MinCoins = 0;
	public int MaxCoins = 0;
	public float xRange = 50;
	public float yRange = 100;
	public ITM_Coin Coin;

	public float potionChance;

	public ITM_Item[] potions;

	public void DropCoin(Vector3 Host)
	{
		if (Random.value > potionChance)
		{
			float TotalCoins = Random.Range(MinCoins, MaxCoins);
			for (int i = 0; i < TotalCoins; i++)
			{
				Vector3 loc = new Vector3(Host.x + Random.Range((-xRange * 0.01f), (xRange * 0.01f)), Host.y - Random.Range(1.0f, 1.5f));
				ITM_Coin Clone = Instantiate(Coin, loc, Quaternion.identity) as ITM_Coin;
				Clone.GetComponent<Rigidbody2D>().AddForce(Vector2.right * Random.Range(-xRange, xRange));
				Clone.GetComponent<Rigidbody2D>().AddForce(Vector2.up * Random.Range(yRange, yRange + 300));
			}
		}
		else
		{
			int potionIndex = (int)Random.Range(0, 4.99999f);

			Vector3 loc = new Vector3(Host.x + Random.Range((-xRange * 0.01f), (xRange * 0.01f)), Host.y - Random.Range(1.0f, 1.5f));
			ITM_Item Clone = Instantiate(potions[potionIndex], gameObject.transform.position, Quaternion.identity) as ITM_Item;
			Clone.GetComponent<Rigidbody2D>().AddForce(Vector2.right * Random.Range(-xRange, xRange));
			Clone.GetComponent<Rigidbody2D>().AddForce(Vector2.up * Random.Range(yRange, yRange + 300));
		}

	}
}

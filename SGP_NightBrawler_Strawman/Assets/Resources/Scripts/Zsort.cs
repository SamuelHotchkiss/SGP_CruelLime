using UnityEngine;
using System.Collections;

public class Zsort : MonoBehaviour {

	void Update () 
	{
		float y = transform.position.y;
		y += GetComponent<BoxCollider2D>().offset.y;
		transform.position = new Vector3(transform.position.x, transform.position.y, y);
	}
}

using UnityEngine;
using System.Collections;

public class Zsort : MonoBehaviour {

	void Start () 
	{
		if (GetComponent<SpriteRenderer>() != null && GameObject.Find("Reference_Point") != null)
			GetComponent<SpriteRenderer>().sortingOrder = (int)((GameObject.Find("Reference_Point").transform.position.y - transform.position.y) * 100.0f);
	}
}

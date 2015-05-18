using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    
	}
	// Update is called once per frame
    public virtual void Update()
    {
        
	}

    public virtual void OnTriggerEnter2D(Collider2D Col)
    {
        Destroy(gameObject);
    }
}

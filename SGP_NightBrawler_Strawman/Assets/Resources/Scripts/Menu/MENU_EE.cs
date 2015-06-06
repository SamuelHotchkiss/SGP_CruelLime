using UnityEngine;
using System.Collections;

public class MENU_EE : MonoBehaviour {

    float Delay;
    public float DelayMax;
	// Use this for initialization
	void Start () {
        Delay = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        Delay += Time.deltaTime;

        if (Delay > DelayMax && Delay < DelayMax + 3.0f)
            GetComponent<Rigidbody2D>().velocity = new Vector2(-1.0f, 0.0f);
        else if (Delay > DelayMax + 3.0f && Delay < DelayMax + 5.0f)
            GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
        else if (Delay > DelayMax * 1.5f && Delay < DelayMax * 2.0f)
            GetComponent<Rigidbody2D>().velocity = new Vector2(1.0f, 0.0f);
        else
            GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
	}
}

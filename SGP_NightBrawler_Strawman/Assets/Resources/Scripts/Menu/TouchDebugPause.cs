using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TouchDebugPause : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
    {
        GetComponent<Text>().text = Input.simulateMouseWithTouches.ToString();
	}
	
	// Update is called once per frame
	void Update () 
    {
        GetComponent<Text>().text = Input.simulateMouseWithTouches.ToString();
	}
}

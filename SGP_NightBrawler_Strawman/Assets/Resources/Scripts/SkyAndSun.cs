using UnityEngine;
using System.Collections;

public class SkyAndSun : MonoBehaviour {

    public Sprite[] SkyBox;
    public Sprite[] CelestialBodies;

    public GameObject Sky;
    public GameObject Sun;

	// Use this for initialization
	void Start () {

        if (MNGR_Game.isNight)
        {
            Sky.GetComponent<SpriteRenderer>().sprite = SkyBox[0];
            Sun.GetComponent<SpriteRenderer>().sprite = CelestialBodies[0];
        }
        else
        {
            Sky.GetComponent<SpriteRenderer>().sprite = SkyBox[1];
            Sun.GetComponent<SpriteRenderer>().sprite = CelestialBodies[1];
        }

        if (MNGR_Game.dangerZone)
        {
            Sky.GetComponent<SpriteRenderer>().sprite = SkyBox[2];
            Sun.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {

        Camera Cam = Camera.current;

        if (Cam != null)
            transform.position = new Vector3(Cam.transform.position.x, transform.position.y, transform.position.z);

	}
}

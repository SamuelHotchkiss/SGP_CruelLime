using UnityEngine;
using System.Collections;

public class OBS_Pitfall : MonoBehaviour 
{

    public GameObject dest;
    public AudioClip Fall;
    float orgMag;

	// Use this for initialization
	void Start () 
    {
        orgMag = 0.0f;
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
    
    void OnTriggerEnter2D(Collider2D _col)
    {
        AudioSource.PlayClipAtPoint(Fall, new Vector3(0, 0, 0), MNGR_Options.sfxVol);
    }

    public virtual void OnTriggerStay2D(Collider2D _col)
    {
        int type = 0;
        Camera cam = Camera.current;
        
        if (_col.gameObject.GetComponent<PlayerController>() != null)
        {
            type = 1;
            _col.gameObject.GetComponent<PlayerController>().ChangeState(ACT_CHAR_Base.STATES.HURT);
            _col.gameObject.GetComponent<PlayerController>().enabled = false;
            _col.gameObject.GetComponent<MNGR_Animation_Player>().enabled = false;
            if (cam != null && cam.GetComponent<CameraFollower>() != null)
                cam.GetComponent<CameraFollower>().Cam_CurrTarget = gameObject;
			//GameObject cam = GameObject.Find("Main Camera").gameObject;
			//cam.GetComponent<Camera>().transform.position = new Vector3(dest.transform.position.x, dest.transform.position.y, -10.0f);
        }
        if (_col.gameObject.GetComponent<ACT_Enemy>() != null)
        {
            type = -1;
            _col.gameObject.GetComponent<ACT_Enemy>().enabled = false;
            if (_col.gameObject.GetComponent<MNGR_Animation_Enemy>() != null)
                _col.gameObject.GetComponent<MNGR_Animation_Enemy>().enabled = false;
        }
        if (type != 0)
        {
            // set original magnitude
            if (orgMag < _col.transform.localScale.magnitude)
                orgMag = _col.transform.localScale.magnitude;

            // Suck the player in, but only the first time.
            if (orgMag == _col.transform.localScale.magnitude)
            {
                Vector3 dir = transform.position - _col.transform.position;
                dir.Normalize();
                _col.transform.position += dir;// *2.0f;
            }

            // apply these effects 
            _col.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
            _col.transform.localScale *= 0.95f;

            if (_col.transform.localScale.magnitude < 0.2f)
            {
                _col.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                if (type > 0)
                {
                    _col.gameObject.GetComponent<PlayerController>().enabled = true;
                    _col.gameObject.GetComponent<MNGR_Animation_Player>().enabled = true;
                    if (cam != null && cam.GetComponent<CameraFollower>() != null)
                        cam.GetComponent<CameraFollower>().Cam_CurrTarget = _col.gameObject;
                }
                else if (type < 0)
                {
                    DestroyObject(_col.gameObject);
                    //_col.gameObject.GetComponent<ACT_Enemy>().enabled = true;
                    //_col.gameObject.GetComponent<ACT_Enemy>().TimeThresh = 0; // should kill an enemy dead in his tracks.
                    //_col.gameObject.GetComponent<ACT_Enemy>().Act_currHP = 0;
                }
                Start();
                _col.transform.position = dest.transform.position;
                if (GameObject.Find("_Horde") != null)
                {
                    Horde hordey = GameObject.Find("_Horde").GetComponent<Horde>();
                    hordey.transform.position = hordey.OGLoc;
                }
            }
            
        }
    }


}

using UnityEngine;
using System.Collections;

public class OBS_Pitfall : MonoBehaviour
{
    public GameObject dest;
    public AudioClip Fall;
    public Camera cam;
    public int type = 0;
    public float orgMag;
    public float KillTmr;
    public GameObject player;
    public GameObject enemy;

    // Use this for initialization
    void Start()
    {
        cam = Camera.current;
        orgMag = 0.0f;
        GetComponent<SpriteRenderer>().sortingOrder = -10000;
    }

    // Update is called once per frame
    void Update()
    {
        if (KillTmr > 0)
        {
            KillTmr -= Time.deltaTime;
            if (KillTmr < 0)
            {
                Camera cam = Camera.current;

                KillTmr = 0;
                CleanOutDahPit();
            }
        }

    }

    void OnTriggerEnter2D(Collider2D _col)
    {
        if (Fall != null && _col.tag != "Obstacle")
        {
            AudioSource.PlayClipAtPoint(Fall, new Vector3(0, 0, 0), MNGR_Options.sfxVol);
        }

    }

    public virtual void OnTriggerStay2D(Collider2D _col)
    {
        //int type = 0;


        if (_col.gameObject.GetComponent<PlayerController>() != null)
        {
            type = 1;
            //_col.gameObject.GetComponent<PlayerController>().ChangeState(ACT_CHAR_Base.STATES.HURT);
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
            {
                orgMag = _col.transform.localScale.magnitude;
                KillTmr = 5.0f;
                if (type > 0)
                    player = _col.gameObject;
                else
                    enemy = _col.gameObject;
            }

            // Suck the player in, but only the first time.
            if (orgMag == _col.transform.localScale.magnitude)
            {
                Vector3 dir = transform.position - _col.transform.position;
                dir.Normalize();
                //_col.transform.position += dir * 1.5f;
                _col.transform.position = transform.position;
                //_col.GetComponent<Rigidbody2D>().velocity = dir.normalized * 10.0f;
            }

            // apply these effects 
            _col.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
            _col.transform.localScale *= 0.95f;

            if (_col.transform.localScale.magnitude < 0.4f)
            {
                CleanOutDahPit();
                Start();
                if (GameObject.Find("_Horde") != null)
                {
                    Horde hordey = GameObject.Find("_Horde").GetComponent<Horde>();
                    hordey.transform.position = hordey.OGLoc;
                }
            }

        }
    }

    void CleanOutDahPit()
    {
        if (player != null)
        {
            PlayerController pc = player.gameObject.GetComponent<PlayerController>();
            player.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            player.transform.position = dest.transform.position;
            pc.enabled = true;
            pc.party[pc.currChar].ChangeHP(-20.0f);
            player.gameObject.GetComponent<MNGR_Animation_Player>().enabled = true;
            if (cam != null && cam.GetComponent<CameraFollower>() != null)
                cam.GetComponent<CameraFollower>().Cam_CurrTarget = player.gameObject;
            player = null;
        }
        if (enemy != null)
        {
            DestroyObject(enemy.gameObject);
            enemy = null;
        }
        KillTmr = 0.0f;
    }


}

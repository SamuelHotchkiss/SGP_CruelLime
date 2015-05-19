using UnityEngine;
using System.Collections;

public class BHR_Spawner : BHR_Base
{
    public bool Spw_SpawnAllCritters;
    public int Spw_CritterThreshold;
    public int Spw_CrittersCreated;
    public float Spw_SpawnCoolDown;
    public float Spw_SpawnPerSec;

    public Vector3 Spw_SpawnPosition;
    public Vector2 Spw_Force; 
    public GameObject Spw_Critter;

	// Use this for initialization
	void Start () 
    {
        Spw_SpawnAllCritters = true;
        Spw_SpawnCoolDown = 20.0f;
        Spw_SpawnPerSec = 1.0f;
        Spw_SpawnPosition = owner.transform.position;
        Spw_CrittersCreated = 0;
	}
	
	// Update is called once per frame
	void Update () 
    {
        Spw_SpawnCoolDown -= Time.deltaTime;
        Spw_SpawnPerSec -= Time.deltaTime;

        if (Spw_SpawnCoolDown <= 0 )
        {
            Spw_SpawnAllCritters = !Spw_SpawnAllCritters;
            Spw_SpawnCoolDown = 20.0f;
        }

        if (Spw_CritterThreshold == Spw_CrittersCreated)
            Spw_SpawnAllCritters = false;
	}

	public override void PerformBehavior()
	{
        if (Spw_SpawnAllCritters)
        {
            if (Spw_SpawnPerSec <= 0.0f)
            {
                Vector3 ActSpawn = new Vector3(Random.Range(Spw_SpawnPosition.x - 1, Spw_SpawnPosition.x + 1), Random.Range(Spw_SpawnPosition.y - 1, Spw_SpawnPosition.y + 1));

                Instantiate(Spw_Critter, ActSpawn, new Quaternion());
                Spw_Critter.GetComponent<Rigidbody2D>().AddForce(Spw_Force);
                Spw_SpawnPerSec = 1.0f;
                Spw_CrittersCreated++;
            } 
        }
		Debug.Log("Spawner Activated!");
	}
}

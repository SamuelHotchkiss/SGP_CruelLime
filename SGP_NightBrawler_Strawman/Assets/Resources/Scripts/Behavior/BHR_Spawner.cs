using UnityEngine;
using System.Collections;

public class BHR_Spawner : BHR_Base
{
    public Vector3 Spw_SpawnPosition;
    public GameObject[] Spw_CrittersTypes;
    public float Spw_SpawnCoolDown;
    public float Spw_SpawnPerSec;
    public int Spw_SpawnCritterNum;
    public bool Spw_SpawnAllCritters;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        Spw_SpawnCoolDown -= Time.deltaTime;
        Spw_SpawnPerSec -= Time.deltaTime;
        
	}
	public override void PerformBehavior()
	{
        Vector3 TargetPos = owner.target.transform.position;
        Vector3 HostPos = owner.transform.position;

        //distThresh Is suppose to be use here but is not use in ACT_Enemy

        float Dist = Vector3.Distance(TargetPos, HostPos);
        if (Spw_SpawnCoolDown > 0.0f)
        {
            if (Dist <= 10.0f)
            {
                Instantiate(Spw_CrittersTypes[0])
            } 
        }

		Debug.Log("Buffer Activated!");
	}
}

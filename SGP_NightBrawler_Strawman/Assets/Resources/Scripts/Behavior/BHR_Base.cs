using UnityEngine;
using System.Collections;

public class BHR_Base : MonoBehaviour
{
    public ACT_Enemy owner;
	public int ID;

	// Use this for initialization
    void Start()
    {
        owner.GetComponent<ACT_Enemy>();
    }

	public virtual void Update()
	{
		if (owner == null)
		{
			Destroy(gameObject);
		}
	}

	public virtual void PerformBehavior()
	{

	}
}

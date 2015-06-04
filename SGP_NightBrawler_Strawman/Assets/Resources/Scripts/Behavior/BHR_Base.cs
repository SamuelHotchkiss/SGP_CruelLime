using UnityEngine;
using System.Collections;

public class BHR_Base : MonoBehaviour
{
    public ACT_Enemy owner;

	// Use this for initialization
    void Start()
    {
        owner.GetComponent<ACT_Enemy>();
    }

	public void Update()
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

using UnityEngine;
using System.Collections;

public class CruddyLevelTransition : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Application.LoadLevel("ForestLevel1");
    }
	
}

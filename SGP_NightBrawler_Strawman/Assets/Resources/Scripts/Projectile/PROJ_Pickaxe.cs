using UnityEngine;
using System.Collections;

public class PROJ_Pickaxe : MonoBehaviour
{
	public GameObject owner;
	Rigidbody2D rigidbody_2D;
	public float range;
	public float elipseWidth;
	public Vector3 dir;
	public float seconds;
	public float power;

	/*
	public override void Initialize(bool _r = true, float _damMult = 1.0f)
	{
		base.Initialize(_r, _damMult);
		//isNegativeX = false;
		//isNegativeY = false;

		//xOrigin = transform.position.x;
		//yOrigin = transform.position.y;

		//yMin = yOrigin - yMin;
		//yMax = yOrigin + yMax;

		//xMax = xOrigin - xMax;

		//velocity.y = 1.0f;

		rigidbody = GetComponent<Rigidbody2D>();

	}
	 */

	void Start()
	{
		if (owner.GetComponent<ACT_BOS_Miner>().Act_facingRight == true)
		{
			dir = Vector3.right;
		}
		else
		{
			dir = Vector3.left;
		}
		power = owner.GetComponent<ACT_BOS_Miner>().Act_currPower;
		rigidbody_2D = GetComponent<Rigidbody2D>();
		StartCoroutine(Throw(range, elipseWidth, dir, seconds));
	}
 
     IEnumerator Throw(float dist, float width, Vector3 direction, float time) 
	 {
         Vector3 pos = transform.position;
         //float height = transform.position.y;
         Quaternion q = Quaternion.FromToRotation (Vector3.forward, direction);
         float timer = 0.0f;
         rigidbody_2D.AddTorque (400.0f);
         while (timer < time) {
             float t = Mathf.PI * 2.0f * timer / time - Mathf.PI/2.0f;
             float x = width * Mathf.Cos(t);
             float z = dist * Mathf.Sin (t);
             Vector3 v = new Vector3(x,x,z+dist);
             rigidbody_2D.MovePosition(pos + (q * v));
             timer += Time.deltaTime;
             yield return null;
         }
 
         rigidbody_2D.angularVelocity = 0;
         rigidbody_2D.velocity = Vector3.zero;
         rigidbody_2D.rotation = 0;
         rigidbody_2D.MovePosition (pos);
		 Destroy(gameObject);
     }

	// Update is called once per frame
	/*
	public override void Update()
    {
		if (MNGR_Game.paused)
			return;

		if (!isNegativeX)
		{
			if (transform.localPosition.x >= xMax && transform.localPosition.y < yMax)
			{
				transform.localPosition += (new Vector3(velocity.x * speed, velocity.y * speed, 0) * Time.deltaTime);
				if (transform.localPosition.y >= yMax)
				{
					isNegativeY = true;
				}
			}
			else if (transform.localPosition.x >= xMax && transform.localPosition.y > yMin && isNegativeY)
			{
				transform.localPosition += (new Vector3(velocity.x * speed, velocity.y * -speed, 0) * Time.deltaTime);
			}
		}
		else
		{
			if (transform.localPosition.x < xOrigin && transform.localPosition.y > yMin && isNegativeY)
			{
				transform.localPosition += (new Vector3(velocity.x * -speed, velocity.y * -speed, 0) * Time.deltaTime);
				if (transform.localPosition.y <= yMin)
				{
					isNegativeY = false;
				}
			}
			else if (transform.localPosition.x < xOrigin && transform.localPosition.y < yOrigin && !isNegativeY)
			{
				transform.localPosition += (new Vector3(velocity.x * -speed, velocity.y * speed, 0) * Time.deltaTime);
			}
		}


		//if (transform.position.y >= yMax)
		//	isNegativeY = true;

		if (transform.position.x < xMax)
			isNegativeX = true;
	}
	 */

	public virtual void OnTriggerEnter2D(Collider2D collision)
    {
		if (collision.gameObject.tag == "Player")
		{
			// Find the active character
			PlayerController player = collision.gameObject.GetComponent<PlayerController>();
			int target = player.currChar;

			// Mess with the active character
			player.party[target].ChangeHP(-power);

			//if (gameObject != null)
			//	ProjectileExpired();
		}
    }
}

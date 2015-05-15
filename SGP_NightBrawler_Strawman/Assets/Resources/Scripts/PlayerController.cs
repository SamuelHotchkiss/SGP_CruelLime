using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public bool keyboard = true;

	public struct character
	{
		public Sprite[] sprites;
	}

	public character[] party;
	public int currChar = 0;

	string[] spriteFiles;


	// Use this for initialization
	void Start()
	{
		spriteFiles = new string[9];

		spriteFiles[0] = "Sprites/Player/Warrior";
		spriteFiles[1] = "Sprites/Player/Archer";
		spriteFiles[2] = "Sprites/Player/Mage";

		party = new character[3];

		party[currChar].sprites = Resources.LoadAll<Sprite>(spriteFiles[currChar]);
		currChar++;
		party[currChar].sprites = Resources.LoadAll<Sprite>(spriteFiles[currChar]);
		currChar++;
		party[currChar].sprites = Resources.LoadAll<Sprite>(spriteFiles[currChar]);
		currChar = 0;
	}

	void Update()
	{
		GetComponent<SpriteRenderer>().sprite = party[currChar].sprites[0];
		// Are we using the keyboard?
		if (keyboard)
		{
			// Get axis movement
			float horz = Input.GetAxis("Horizontal");
			float vert = Input.GetAxis("Vertical");

			// Move the object
			GetComponent<Rigidbody2D>().velocity = new Vector2(horz, vert);

			if (Input.GetButton("Attack/Confirm"))
			{
				GetComponent<SpriteRenderer>().sprite = party[currChar].sprites[5];
			}
			else if (Input.GetButton("Special/Cancel"))
			{
				GetComponent<SpriteRenderer>().sprite = party[currChar].sprites[4];
			}
			else if (Input.GetButtonDown("SwitchRight"))
			{
				currChar++;
				if (currChar > 2)
					currChar = 0;

				GetComponent<SpriteRenderer>().sprite = party[currChar].sprites[0];
			}
			else if (Input.GetButtonDown("SwitchLeft"))
			{
				currChar--;
				if (currChar < 0)
					currChar = 2;

				GetComponent<SpriteRenderer>().sprite = party[currChar].sprites[0];
			}

			float rotx = transform.localEulerAngles.x;
			float roty = transform.localEulerAngles.y;
			// Use button rotates the object
			if (Input.GetButton("Use"))
			{
				rotx++;
				roty++;
			}
			// Dodge button rotates the object based upon current movement
			if (Input.GetButton("Dodge"))
			{
				rotx += vert;
				roty += horz;
			}
			// reset stuff when it goes bad
			if (roty >= 360)
				roty -= 360;
			else if (roty < 0)
				roty += 360;
			// gimbal locking grrr
			if (rotx > 90)
				rotx -= 90;
			else if (rotx < 0)
				rotx += 85;

			transform.localEulerAngles = new Vector3(rotx, roty, 0);

			// Pause button makes the object invisible and resets everything
			if (Input.GetButton("Pause"))
			{
				if (GetComponent<MeshRenderer>() != null)
					GetComponent<MeshRenderer>().enabled = false;
				else if (GetComponent<SpriteRenderer>() != null)
					GetComponent<SpriteRenderer>().enabled = false;

				transform.position = new Vector3(0.0f, 0.0f, 0.0f);
				transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
				transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			}
			else
			{
				if (GetComponent<MeshRenderer>() != null)
					GetComponent<MeshRenderer>().enabled = true;
				else if (GetComponent<SpriteRenderer>() != null)
					GetComponent<SpriteRenderer>().enabled = true;
			}

		}
		// Testing for gamepad input
		// NOT TESTED YET PLZ FIX ERF PRBLMS OKAI BAI.
		else
		{
			// get axis movement
			float horz = Input.GetAxis("Pad_Horizontal");
			float vert = Input.GetAxis("Pad_Vertical");

			// move the object
			GetComponent<Rigidbody2D>().velocity = new Vector2(horz, vert);

			// Confirm button sets scale to 2x
			if (Input.GetButton("Pad_Attack/Confirm"))
			{
				transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
			}
			// Cancel button sets scale to 1/2x
			else if (Input.GetButton("Pad_Special/Cancel"))
			{
				transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
			}
			// Switch Right button increases scale over time
			else if (Input.GetButton("Pad_SwitchRight"))
			{
				Vector3 scale = transform.localScale;
				scale *= 1.1f;
				transform.localScale = scale;
			}
			// Switch Left button decreases scale over time
			else if (Input.GetButton("Pad_SwitchLeft"))
			{
				Vector3 scale = transform.localScale;
				scale *= 0.9f;
				transform.localScale = scale;
			}
			// Reset scale if these buttons are not pressed
			else
			{
				transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			}

			float rotx = transform.localEulerAngles.x;
			float roty = transform.localEulerAngles.y;
			// Use button rotates the object
			if (Input.GetButton("Pad_Use"))
			{
				rotx++;
				roty++;
			}
			// Dodge button rotates the object based upon current movement
			rotx = Input.GetAxis("Pad_DodgeVertical");
			roty = Input.GetAxis("Pad_DodgeHorizontal");

			// reset stuff when it goes bad
			if (roty >= 360)
				roty -= 360;
			else if (roty < 0)
				roty += 360;
			// gimbal locking grrr
			if (rotx > 90)
				rotx -= 90;
			else if (rotx < 0)
				rotx += 85;

			transform.localEulerAngles = new Vector3(rotx, roty, 0);

			// Pause button makes the object invisible
			if (Input.GetButton("Pad_Pause"))
			{
				if (GetComponent<MeshRenderer>() != null)
					GetComponent<MeshRenderer>().enabled = false;
				else if (GetComponent<SpriteRenderer>() != null)
					GetComponent<SpriteRenderer>().enabled = false;

				transform.position = new Vector3(0.0f, 0.0f, 0.0f);
				transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
				transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			}
			else
			{
				if (GetComponent<MeshRenderer>() != null)
					GetComponent<MeshRenderer>().enabled = true;
				else if (GetComponent<SpriteRenderer>() != null)
					GetComponent<SpriteRenderer>().enabled = true;
			}

		}
	}
}
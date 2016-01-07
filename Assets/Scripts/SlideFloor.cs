using UnityEngine;
using System.Collections;

public class SlideFloor : MonoBehaviour {
	Vector3 vel;
	GameObject victim;
	RaycastHit hit;
	Vector3 endPos;
	bool moveUp;
	bool moveDown;
	bool moveRight;
	bool moveLeft;

	bool isMoving;
	// Use this for initialization
	void Start () {
		victim = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Debug.Log(victim.GetComponent<Rigidbody>().velocity.sqrMagnitude);
		if (moveUp && isMoving)
		{
		//	victim.transform.position += transform.forward/5;
			victim.GetComponent<Rigidbody>().AddForce(transform.forward/2, ForceMode.VelocityChange);
			if (victim.GetComponent<Rigidbody>().velocity.sqrMagnitude < 1)
			{
				moveUp = false;
				isMoving = false;
			}
		}
		if (moveDown && isMoving)
		{
			//victim.transform.position -= transform.forward / 5;
			victim.GetComponent<Rigidbody>().AddForce(-transform.forward/2, ForceMode.VelocityChange);
			if (victim.GetComponent<Rigidbody>().velocity.sqrMagnitude < 1)
			{
				moveDown = false;
				isMoving = false;
			}
		}
		if (moveLeft && isMoving)
		{
			//victim.transform.position -= transform.right / 5;
			victim.GetComponent<Rigidbody>().AddForce(-transform.right/2, ForceMode.VelocityChange);
			if (victim.GetComponent<Rigidbody>().velocity.sqrMagnitude < 1)
			{
				moveLeft = false;
				isMoving = false;
			}
		}
		if (moveRight && isMoving)
		{
			//victim.transform.position += transform.right / 5;
			victim.GetComponent<Rigidbody>().AddForce(transform.right/2, ForceMode.VelocityChange);
			if (victim.GetComponent<Rigidbody>().velocity.sqrMagnitude < 1)
			{
				moveRight = false;
				isMoving = false;
			}
		}

	}
	void OnTriggerStay(Collider col)
	{
 		if(col.tag == "Player")
		{
			col.gameObject.GetComponent<MovementController>().bForcedMove = true;
			if (GamepadManager.dpadUp && !isMoving)
			{
				moveUp = true;
				isMoving = true;
				col.gameObject.transform.eulerAngles = new Vector3(col.gameObject.transform.eulerAngles.x, 0, col.gameObject.transform.eulerAngles.z);
				//col.gameObject.transform.position += transform.forward/10;
			}
			if (GamepadManager.dpadDown && !isMoving)
			{
				moveDown = true;
				isMoving = true;
				col.gameObject.transform.eulerAngles = new Vector3(col.gameObject.transform.eulerAngles.x, 180, col.gameObject.transform.eulerAngles.z);
				//col.gameObject.transform.position -= transform.forward/10;
			}
			if (GamepadManager.dpadLeft && !isMoving)
			{
				moveLeft = true;
				isMoving = true;
				col.gameObject.transform.eulerAngles = new Vector3(col.gameObject.transform.eulerAngles.x, 270, col.gameObject.transform.eulerAngles.z);
				//col.gameObject.transform.position -= transform.right/10;
			}
			if(GamepadManager.dpadRight && !isMoving)
			{
				moveRight = true;
				isMoving = true;
				col.gameObject.transform.eulerAngles = new Vector3(col.gameObject.transform.eulerAngles.x, 90, col.gameObject.transform.eulerAngles.z);
				//col.gameObject.transform.position += transform.right/10;
			}
		}
	}
	void OnTriggerExit(Collider col)
	{
		if (col.tag == "Player")
		{
			col.gameObject.GetComponent<MovementController>().bForcedMove = false;
			isMoving = false;
			moveUp = false;
			moveDown = false;
			moveRight = false;
			moveLeft = false;
			victim.GetComponent<Rigidbody>().velocity = Vector3.zero;
		}
	}
	//void OnTriggerEnter(Collider col)
	//{
	//	if (col.tag == "Player")
	//	{
			
	//		col.gameObject.GetComponent<Rigidbody>().velocity = col.gameObject.GetComponent<Rigidbody>().velocity.normalized * 30;

	//		Debug.Log(col.gameObject.GetComponent<Rigidbody>().velocity.normalized);
			
	//	}
	//}
	//void OnTriggerExit(Collider col)
	//{
	//	if (col.tag == "Player")
	//	{
	//		//col.gameObject.GetComponent<MovementController>().bForcedMove = true;
	//		//col.GetComponent<MovementController>().bForcedMove = true;
	//		//col.GetComponent<Rigidbody>().AddForce(vel * 50, ForceMode.Impulse);
	//	}
	//}
}

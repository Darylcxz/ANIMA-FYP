using UnityEngine;
using System.Collections;

public class BlockSlide : MonoBehaviour {

	Vector3 FrontCenter;
	Vector3 FrontSide1;
	Vector3 FrontSide2;
	
	Vector3 BackCenter;
	Vector3 BackSide1;
	Vector3 BackSide2;
	
	Vector3 RightCenter;
	Vector3 RightSide1;
	Vector3 RightSide2;
	
	Vector3 LeftCenter;
	Vector3 LeftSide1;
	Vector3 LeftSide2;

	
	Vector3 rightOffset;
	Vector3 frontOffset;


	RaycastHit hit;
	bool move;

	public enum MovementStuff
	{
 		FORWARD,
		BACKWARD,
		LEFT,
		RIGHT,
		IDLE
	}
	public MovementStuff MoveDir;
	// Use this for initialization
	void Start () {
		MoveDir = MovementStuff.IDLE;
		rightOffset = new Vector3(0.2f, 0, 0);
		frontOffset = new Vector3(0, 0, 0.2f);


	}
	
	// Update is called once per frame
	void FixedUpdate () {
		CheckRay();
		CalculateRay();
		ShowLines();
		CheckMove();
		

		
	
	}
	void CheckRay()
	{

		if (Physics.Raycast(transform.localPosition, transform.forward, out hit, 0.5f) || Physics.Raycast(transform.localPosition + rightOffset, transform.forward, out hit, 0.5f) || Physics.Raycast(transform.localPosition - rightOffset, transform.forward, out hit, 0.5f))
		{
			Debug.Log("wtf");
			if (hit.collider.name == "Character")
			{
				Debug.Log("meow");
				if (!move)
				{
					if (GamepadManager.buttonB)
					{
						MoveDir = MovementStuff.BACKWARD;
					}
				}
			}
		}
		if (Physics.Raycast(transform.localPosition, -transform.forward, out hit, 0.5f) || Physics.Raycast(transform.localPosition + rightOffset, -transform.forward, out hit, 0.5f) || Physics.Raycast(transform.localPosition - rightOffset, -transform.forward, out hit, 0.5f))
		{
			if (hit.collider.name == "Character")
			{
				Debug.Log("meow");
				if (!move)
				{
					if (GamepadManager.buttonB)
					{
						MoveDir = MovementStuff.FORWARD;
					}
				}
			}
		}
		if (Physics.Raycast(transform.localPosition, -transform.right, out hit, 0.5f) || Physics.Raycast(transform.localPosition + rightOffset, -transform.right, out hit, 0.5f) || Physics.Raycast(transform.localPosition - rightOffset, -transform.right, out hit, 0.5f))
		{
			if (hit.collider.name == "Character")
			{
				Debug.Log("right");
				if (!move)
				{
					if (GamepadManager.buttonB)
					{
						MoveDir = MovementStuff.RIGHT;
					}
				}
			}
		}
		if (Physics.Raycast(transform.localPosition, transform.right, out hit, 0.5f) || Physics.Raycast(transform.localPosition + rightOffset, transform.right, out hit, 0.5f) || Physics.Raycast(transform.localPosition - rightOffset, transform.right, out hit, 0.5f))
		{
			if (hit.collider.name == "Character")
			{
				Debug.Log("Left");
				if (!move)
				{
					if (GamepadManager.buttonB)
					{
						MoveDir = MovementStuff.LEFT;
					}
				}
			}
		}

	}

	void CheckMove()
	{
		switch (MoveDir)
		{
 			case MovementStuff.FORWARD:
				if (Physics.Raycast(transform.localPosition, transform.forward, out hit, 0.5f) || Physics.Raycast(transform.localPosition + rightOffset, transform.forward, out hit, 0.5f) || Physics.Raycast(transform.localPosition - rightOffset, transform.forward, out hit, 0.5f))
				{
					Debug.Log("hit something");
					if (hit.collider.name != "Character")
					{
						MoveDir = MovementStuff.IDLE;
					}
				}
				else
				{
					gameObject.transform.Translate(Vector3.forward / 10, Space.Self);
					move = true;
				}
				break;
			case MovementStuff.BACKWARD:
				if (Physics.Raycast(transform.localPosition, -transform.forward, out hit, 0.5f) || Physics.Raycast(transform.localPosition + rightOffset, -transform.forward, out hit, 0.5f) || Physics.Raycast(transform.localPosition - rightOffset, -transform.forward, out hit, 0.5f))
				{
					Debug.Log("hit something");
					if (hit.collider.name != "Character")
					{
						MoveDir = MovementStuff.IDLE;
					}
				}
				else
				{
					gameObject.transform.Translate(-Vector3.forward / 10, Space.Self);
					move = true;
				}
				break;
			case MovementStuff.LEFT:
				if (Physics.Raycast(transform.localPosition, -transform.right, out hit, 0.5f) || Physics.Raycast(transform.localPosition + rightOffset, -transform.right, out hit, 0.5f) || Physics.Raycast(transform.localPosition - rightOffset, -transform.right, out hit, 0.5f))
				{
					if (hit.collider.name != "Character")
					{
						MoveDir = MovementStuff.IDLE;
					}
				}
				else
				{
					gameObject.transform.Translate(-Vector3.right / 10, Space.Self);
					move = true;
				}
				break;
			case MovementStuff.RIGHT:
				if (Physics.Raycast(transform.localPosition, transform.right, out hit, 0.5f) || Physics.Raycast(transform.localPosition + rightOffset, transform.right, out hit, 0.5f) || Physics.Raycast(transform.localPosition - rightOffset, transform.right, out hit, 0.5f))
				{
					if (hit.collider.name != "Character")
					{
						MoveDir = MovementStuff.IDLE;
					}
				}
				else
				{
					gameObject.transform.Translate(Vector3.right / 10, Space.Self);
					move = true;
				}
				break;
			case MovementStuff.IDLE:
				move = false;
				break;
		}
	}

	void CalculateRay()
	{
		FrontCenter = transform.forward + transform.localPosition;
		FrontSide1 = FrontCenter + rightOffset;
		FrontSide2 = FrontCenter - rightOffset;

		BackCenter = -transform.forward + transform.localPosition;
		BackSide1 = BackCenter + rightOffset;
		BackSide2 = BackCenter - rightOffset;

		RightCenter = transform.right + transform.localPosition;
		RightSide1 = RightCenter + frontOffset;
		RightSide2 = RightCenter - frontOffset;

		LeftCenter = -transform.right + transform.localPosition;
		LeftSide1 = LeftCenter + frontOffset;
		LeftSide2 = LeftCenter - frontOffset;
	}
	void ShowLines()
	{
		Debug.DrawLine(transform.localPosition, FrontCenter, Color.cyan);
		Debug.DrawLine(transform.localPosition + rightOffset, FrontSide1, Color.cyan);
		Debug.DrawLine(transform.localPosition - rightOffset, FrontSide2, Color.cyan);

		Debug.DrawLine(transform.localPosition, BackCenter, Color.green);
		Debug.DrawLine(transform.localPosition + rightOffset, BackSide1, Color.green);
		Debug.DrawLine(transform.localPosition - rightOffset, BackSide2, Color.green);

		Debug.DrawLine(transform.localPosition, RightCenter, Color.red);
		Debug.DrawLine(transform.localPosition + frontOffset, RightSide1, Color.red);
		Debug.DrawLine(transform.localPosition - frontOffset, RightSide2, Color.red);

		Debug.DrawLine(transform.localPosition, LeftCenter, Color.yellow);
		Debug.DrawLine(transform.localPosition + frontOffset, LeftSide1, Color.yellow);
		Debug.DrawLine(transform.localPosition - frontOffset, LeftSide2, Color.yellow);
	}
	

}

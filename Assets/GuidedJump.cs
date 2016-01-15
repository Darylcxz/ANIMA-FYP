using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GuidedJump : MonoBehaviour {

	[SerializeField]List<Transform> wayPoints = new List<Transform>();
	public bool jump;
	[SerializeField]Vector3 startPos;
	[SerializeField]Vector3 endPos;
	[SerializeField]float _jumpTime;
	[SerializeField]int jumpIndex; //jump index
	[SerializeField]Vector3 jumpLerp;
	[SerializeField]bool starting; // first ever jump 
	GameObject player;
	float dotProduct;
	[SerializeField]
	bool isFacing;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	

	void Update()
	{
		_jumpTime += Time.deltaTime;
		if (GamepadManager.buttonADown && CheckFacing(jumpIndex + 1))
		{
			jumpIndex++;
			NextJump();
		}
		if (GamepadManager.buttonADown && CheckFacing(jumpIndex - 1))
		{
			jumpIndex--;
			NextJump();
		}
		if (jump && !starting)
		{
			startPos = player.transform.position;
			endPos = wayPoints[jumpIndex].position;
			jumpLerp = Vector3.Lerp(startPos, endPos, _jumpTime);
			player.transform.position = jumpLerp;
			//player.GetComponent<Collider>().enabled = !jump;
		}
		
		if (_jumpTime > 0.9f && jump)
		{
			_jumpTime = 0;
			//	player.GetComponent<Collider>().enabled = jump;
			jump = false;
			starting = false;
		}
		if (jump && starting)
		{
			//starting = false;
			jumpIndex = 1;
			endPos = wayPoints[1].position;
			jumpLerp = Vector3.Lerp(startPos, endPos, _jumpTime);
			player.transform.position = jumpLerp;
		}

		isFacing = CheckFacing(jumpIndex+1);
	
	}
	void OnTriggerStay(Collider col)
	{
		if (col.gameObject.CompareTag("Player"))
		{
			//player = col.gameObject;
		//	float dotProduct = Vector3.Dot(col.gameObject.transform.forward, (wayPoints[0].position - col.gameObject.transform.position).normalized);
			//jumpIndex = -1;
			//float dotProduct = Vector3.Dot(col.gameObject.transform.forward, (wayPoints[0].position.normalized-GamepadManager.h1);
			if (CheckFacing(1) && GamepadManager.buttonA)
			{
				col.gameObject.GetComponent<MovementController>().charStates = MovementController.States.sequencedjump;
				startPos = col.gameObject.transform.position;
				_jumpTime = 0f;
				starting = true;
				jump = true;
			}
		}
	}
	public void NextJump()
	{
		//if (CheckFacing(jumpIndex - 1))
		//{
		//	jumpIndex--;
		//}
		_jumpTime = 0;
		jump = true;
	}
	bool CheckFacing(int indexOfJump)
	{
		if (jumpIndex < wayPoints.Count)
		{
			dotProduct = Vector3.Dot(player.gameObject.transform.forward, (wayPoints[indexOfJump].position - player.transform.position).normalized);
		}
		if (dotProduct > 0.7f)
		{
			return true;
		}
		else return false;
	}
}

using UnityEngine;
using System.Collections;

public class FreeWandering : MonoBehaviour {
	public float circleRadius = 2.0f;
	private Vector3 circleCenter;
	public float offset = 5.0f;
	private float moveSpeed = 5.0f;
	private float minDistance = 1.0f;
	private float rotationSpeed = 5.0f;
	private Vector3 currentTargetPosition;
	private float waitTime = 1.0f;
	private float waitTimer;
	enum States{Waiting, Moving}
	private States currentState;
	
	void Start() {
		waitTimer = 0.0f;
		circleCenter = transform.position + new Vector3(0, 0, offset);
		currentTargetPosition = transform.position;
		//currentTargetPosition = FindNewTargetPosition();
		currentState = States.Moving;
	}
	
	void Update() {
		switch(currentState) {
		case States.Waiting:
			Wait();
			break;
		case States.Moving:
			MoveTowardsTarget();
			break;
		}
	}
	
	void MoveTowardsTarget() {
		Vector3 direction = currentTargetPosition - transform.position;
		direction.y = 0;
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
		if(direction.magnitude > minDistance) {
			Vector3 moveVector = direction.normalized * moveSpeed * Time.deltaTime;
			transform.position += moveVector;
		} else {
			FindNewTargetPosition();
			currentState = States.Waiting;
		}
	}
	
	void Wait() {
		waitTimer += Time.deltaTime;
		if(waitTimer > waitTime) {
			waitTimer = 0.0f;
			currentState = States.Moving;
		}
	}
	
	void FindNewTargetPosition() {
		circleCenter = transform.position + Vector3.forward * offset; // + new Vector3(Random.Range(-offset, offset), 0, Random.Range(-offset, offset));
		//circleCenter = transform.position + new Vector3(0, 0, offset);
		currentTargetPosition = circleCenter + (OnUnitCircle() * circleRadius);
	}
	
	Vector3 OnUnitCircle() {
		float angleInRadians = Random.Range(0, 2 * Mathf.PI);
		float x = Mathf.Cos(angleInRadians);
		float z = Mathf.Sin(angleInRadians);
		return new Vector3(x, 0, z);
	}
	
	void OnDrawGizmos() {
		Gizmos.color = Color.black;
		Gizmos.DrawWireSphere(circleCenter, circleRadius);
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(currentTargetPosition, new Vector3(1, 1, 1));
	}
}
using UnityEngine;
using System.Collections;

public class SclorgAI : AIbase {

	//GameObject player;
	GameObject target;
	float _timer;
	Rigidbody _rb;
	Transform shootPoint;
	Rigidbody projectile;
	float distance;
	bool shoot;
	Vector3 targetSpeed;
	Vector3 futurePos;
	Vector3 direction;
	float iterations = 30f;

	[System.Serializable] 
	enum SclorgState
	{
		PURSUE,
		SHOOT
	};
	SclorgState sclorgStates = SclorgState.PURSUE;

	// Use this for initialization
	protected override void Start()
	{
		base.Start();
		canPosses = false;
	}
	protected override void ActivateAbility()
	{
		//throw new System.NotImplementedException();
		switch (sclorgStates)
		{ 
			case SclorgState.PURSUE:
				break;
			case SclorgState.SHOOT:
				break;
		}
	}
	protected override void PassiveAbility()
	{
		//throw new System.NotImplementedException();
		_timer += Time.deltaTime;
	}
	void LookAtPlayer()
	{
		target = player.gameObject;
		targetSpeed = target.GetComponent<Rigidbody>().velocity;
		futurePos = target.transform.position + (targetSpeed * (distance / iterations));
		direction = futurePos - transform.position;
		direction.y = 0;
		Quaternion lookRot = Quaternion.LookRotation(direction);
		_rb.MoveRotation(lookRot);
		//transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 2f * Time.deltaTime);
	}
	void OnCollisionEnter(Collision col)
	{
		if (col.collider.CompareTag("dagger"))
		{
 			//play hit deflect sound
		}
	}
}

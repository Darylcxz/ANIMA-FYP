using UnityEngine;
using System.Collections;

public class SclorgAI : AIbase {

	//GameObject player;
	GameObject target;
	[SerializeField]float _timer;
	//Rigidbody _rb;

	float _distance;
	bool shoot;
	Vector3 targetSpeed;
	Vector3 futurePos;
	Vector3 _direction;
	float iterations = 30f;
	Animator SclorgAnim;

	[SerializeField]
	Rigidbody projectile;
	[SerializeField]
	Transform shootPos;

	[SerializeField] 
	enum SclorgState
	{
		PURSUE,
		SHOOT,
		DEATH
	};
	[SerializeField]SclorgState sclorgStates = SclorgState.PURSUE;

	// Use this for initialization
	protected override void Start()
	{
		base.Start();
		canPosses = false;
		health = 999;
		AISpeed = 1f;
		_rigidBody = gameObject.GetComponent<Rigidbody>();
		SclorgAnim = gameObject.GetComponent<Animator>();
	}
	protected override void Roam()
	{
		base.Roam();
		_distance = Vector3.Distance(gameObject.transform.position, player.transform.position);
	
		if (_distance < 20)
		{
			AIState = States.pursue;
		}
	}
	protected override void ActivateAbility()
	{
	
		currentTargetPosition = player.transform.position;
		switch (sclorgStates)
		{ 
			case SclorgState.PURSUE:
				SclorgAnim.SetBool("isAttacking", false);
				SclorgAnim.SetBool("isMoving",true);
				MoveTowardsTarget();
				if (_distance < 10 && _timer > 0.1f && !SclorgAnim.GetBool("isAttacking"))
				{
					_timer = 0;
					SclorgAnim.SetBool("isAttacking", true);
					SclorgAnim.SetBool("isMoving", false);
					shoot = false;
					sclorgStates = SclorgState.SHOOT;
				}
				break;
			case SclorgState.SHOOT:
				SclorgAnim.SetBool("isAttacking", true);
				SclorgAnim.SetBool("isMoving", false);
				LookAtPlayer();
				if (_timer > 2f && !shoot && SclorgAnim.GetBool("isAttacking"))
				{
					Fire();
				}
				if ((_timer >= 2.24f || _distance > 10) && SclorgAnim.GetBool("isAttacking"))
				{
					SclorgAnim.SetBool("isAttacking", false);
					SclorgAnim.SetBool("isMoving", true);
					sclorgStates = SclorgState.PURSUE;
				}
				break;
			case SclorgState.DEATH:
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
		_direction = futurePos - transform.position;
		_direction.y = 0;
		Quaternion lookRot = Quaternion.LookRotation(_direction);
		_rigidBody.MoveRotation(lookRot);
		//transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 2f * Time.deltaTime);
	}
	void Fire()
	{
		Debug.Log("fire");
		Rigidbody projectileClone = Instantiate(projectile, shootPos.position, transform.rotation) as Rigidbody;
		projectileClone.velocity = transform.forward * 30;
		shoot = true;

	}
	void OnCollisionEnter(Collision col)
	{
		if (col.collider.CompareTag("dagger"))
		{
 			//play hit deflect sound
		}
	}
}

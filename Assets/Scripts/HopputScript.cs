using UnityEngine;
using System.Collections;

public class HopputScript : AIbase {

	float AggroDist = 1000f;
	float jumpRange = 15f;
	float timerThing = 0.0f;
	float playerDist;
	int layerMask = 1 << 18;
	Animator HopputAnim;
	public GameObject childObj;
	RaycastHit hit;
	public enum HopputState
	{
		IDLE,
		MOVE,
		SHAKE,
		ATTACK,
		RECOIL

	};
	public HopputState hopState = HopputState.IDLE;
	// Use this for initialization
	protected override void Start()
	{
		base.Start();
		health = 5;
		childObj = GameObject.Find("BaseRay");
		HopputAnim = gameObject.GetComponentInParent<Animator>();
		_rigidBody = gameObject.GetComponent<Rigidbody>();
	}
	
	protected override void Roam()
	{
		base.Roam();
		playerDist = Vector3.Distance(gameObject.transform.position, player.transform.position);
		if (distance < AggroDist)
		{
			AIState = States.pursue;
		}
	//	Debug.Log(childObj.transform.position);
		Debug.DrawRay(childObj.transform.position, new Vector3(0,-0.5f,0), Color.black);
	}
	protected override void ActivateAbility()
	{
		
		currentTargetPosition = player.transform.position;

		switch (hopState)
		{
 			case HopputState.IDLE:
				if (timerThing > 3)
				{
					timerThing = 0;
					hopState = HopputState.MOVE;
					HopputAnim.SetInteger("AnimState", 1);
				}
				break;
			case HopputState.MOVE:
				MoveTowardsTarget();
				//maybe make it roam instead?
				if (playerDist < jumpRange)
				{
					hopState = HopputState.SHAKE;
					timerThing = 0;
					HopputAnim.SetInteger("AnimState", 2);
				}
				break;
			case HopputState.SHAKE:
				//shake anim
				if (timerThing > 3)
				{
					hopState = HopputState.ATTACK;
					timerThing = 0;
					HopputAnim.SetInteger("AnimState", 4);
				}
				break;
			case HopputState.ATTACK:
				//attack anim
				AISpeed = 5f;
				MoveTowardsTarget();
				if (timerThing > 1f)
				{
					Debug.Log(isGrounded() + " = grounded");
					if (isGrounded())
					{
						Explosion(transform.position, 50); 
						timerThing = 0;
						hopState = HopputState.IDLE;
						AISpeed = 1f;
						HopputAnim.SetInteger("AnimState", 0);
					}
				}
				
				break;
			case HopputState.RECOIL:
				//hurt anim
				if (timerThing > 5)
				{
					hopState = HopputState.MOVE;
					timerThing = 0;
					HopputAnim.SetInteger("AnimState", 1);
				}
				break;

		}

	}
	void Explosion(Vector3 center, float radius)
	{

		Collider[] hitColliders = Physics.OverlapSphere(center, radius, layerMask);
		foreach(Collider hit in hitColliders)
		{
			Rigidbody rb = hit.GetComponent<Rigidbody>();
			if (rb != null)
			{
				rb.AddExplosionForce(20, center, radius, 2,ForceMode.Impulse);
			}
		}

	}
	protected override void PassiveAbility()
	{
		timerThing += Time.deltaTime;
	}

	void OnCollisionEnter(Collision col)
	{
 		if(col.collider.tag == "dagger")
		{
			health--;
			if (health < 1)
			{
				gameObject.GetComponent<AudioSource>().Play();
				Destroy(gameObject, 1f);
			}
		}
	}
	bool isGrounded()
	{
		return Physics.Raycast(childObj.transform.position, -Vector3.up, 0.5f);
	}
}

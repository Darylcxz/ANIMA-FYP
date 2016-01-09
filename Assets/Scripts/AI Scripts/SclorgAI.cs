using UnityEngine;
using System.Collections;

public class SclorgAI : AIbase {

	float _timer;
	Rigidbody _rb;
	Transform shootPoint;
	Rigidbody projectile;
	float distance;
	bool shoot;
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
	}
	
}

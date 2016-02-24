using UnityEngine;
using System.Collections;

public class GolemAI : NewAIBA {


	Animator GolemController;
	float moveSpeed;
	public bool deflect;
	float timer;
	// Use this for initialization
	protected override void Start()
	{
		GolemController = GetComponent<Animator>();
        base.Start();
	}
	protected override void ActivateAbility()
	{
		//throw new System.NotImplementedException();
		GolemController.SetTrigger("Harden");
		gameObject.GetComponent<AudioSource>().Play();
		if(!deflect)
		{
			deflect = true;
			timer = 0;
		}

	}
	protected override void PassiveAbility()
	{
		//throw new System.NotImplementedException();
		moveSpeed = _rbAI.velocity.magnitude;
		GolemController.SetFloat("moveSpeed", moveSpeed);
		timer += Time.deltaTime;
		if (timer > 0.8f)
		{
			deflect = false;
		}
		Debug.Log(moveSpeed);
	}
}

using UnityEngine;
using System.Collections;

public class PigUniqueAI : AIbase {
	public ParticleSystem kaboom;
	public ParticleSystem bigboom;
	public Light glow;
	// Use this for initialization
//	void Start () {


	
//	}
	
	// Update is called once per frame
	protected override void ActivateAbility()
	{
		kaboom.Play();
	}

	protected override void PassiveAbility()
	{
		print("glow");
	}
}

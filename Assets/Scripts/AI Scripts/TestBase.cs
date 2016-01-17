using UnityEngine;
using System.Collections;

public class TestBase : AIbase {

    [SerializeField] ParticleSystem explosion;
	Animator pigAnim;

    protected Vector3 asd;
	// Use this for initialization
	protected override void Start () {
        agent = GetComponent<NavMeshAgent>();
        _rigidBody = GetComponent<Rigidbody>();
        origin = gameObject.transform.position;
		pigAnim = GetComponent<Animator>();
        
	}
	
	// Update is called once per frame
	
    protected override void ActivateAbility()
    {
		if (!explosion.isPlaying)
		{
			explosion.Play();
			pigAnim.SetTrigger("tSneeze");
		}
		
    }
    protected override void PassiveAbility()
    {
      //Plays Pig Anims
		switch (AIState)
		{
 			case States.walk:
				pigAnim.SetBool("isMoving", true);
				break;
			case States.idle:
				pigAnim.SetBool("isMoving", false);
				break;
			case States.possessed:
				pigAnim.SetBool("isMoving", false);
				break;
			case States.doNothing:
				pigAnim.SetBool("isMoving", false);
				break;

		}
       
    }
   
}

using UnityEngine;
using System.Collections;

public class TestBase : AIbase {

    public ParticleSystem explosion;

    protected Vector3 asd;
	// Use this for initialization
	protected override void Start () {
        agent = GetComponent<NavMeshAgent>();
        _rigidBody = GetComponent<Rigidbody>();
        origin = gameObject.transform.position;
        
	}
	
	// Update is called once per frame
	
    protected override void ActivateAbility()
    {
        explosion.Play();
    }
    protected override void PassiveAbility()
    {
      //  throw new System.NotImplementedException();
       
    }
   
}

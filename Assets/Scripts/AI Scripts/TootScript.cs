using UnityEngine;
using System.Collections;

public class TootScript : AIbase {
    Animator anim;
    AudioSource bounce;
	// Use this for initialization
	protected override void Start () {

        _rigidBody = gameObject.GetComponent<Rigidbody>();
        origin = gameObject.transform.position;
        anim = gameObject.GetComponent<Animator>();
        bounce = gameObject.GetComponent<AudioSource>();
	
	}

    protected override void ActivateAbility()
    {
        //throw new System.NotImplementedException();
    }

    protected override void PassiveAbility()
    {
        if(AIState == States.walk || AIState == States.retreat)
        {
            anim.SetFloat("speed", 1);
        }

        else if (AIState == States.possessed)
        {
            anim.SetFloat("speed", _rigidBody.velocity.magnitude);
        }

        else
            anim.SetFloat("speed", 0);
        //throw new System.NotImplementedException();
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.name == "Character")
        {
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            rb.AddForce(Vector3.up * 25, ForceMode.Impulse);
            anim.SetTrigger("Flip");
            bounce.Play();
            print("go up");
        }
    }
}

using UnityEngine;
using System.Collections;

public class TootScript : NewAIBA {
    Animator anim;
    AudioSource bounce;
	// Use this for initialization
	protected override void Start () {
        anim = GetComponent<Animator>();
        bounce = gameObject.GetComponent<AudioSource>();
        base.Start();
	}

    protected override void ActivateAbility()
    {

    }

    protected override void PassiveAbility()
    {
        anim.SetFloat("speed", _rbAI.velocity.magnitude);
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.name == "Character")
        {
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            rb.AddForce(Vector3.up * 50, ForceMode.Impulse);
            _rbAI.velocity = Vector3.zero;
            anim.SetTrigger("Flip");
            bounce.Play();
            print("go up");
        }
    }
}

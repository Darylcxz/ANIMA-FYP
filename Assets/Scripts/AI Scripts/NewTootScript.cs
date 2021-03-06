﻿using UnityEngine;
using System.Collections;

public class NewTootScript : NewAIBA {
    Animator Tootanim;
    AudioSource bounce;
    Rigidbody shit;
	// Use this for initialization
	protected override void Start () {
        Tootanim = GetComponent<Animator>();
        bounce = GetComponent<AudioSource>();
        shit = GetComponent<Rigidbody>();
        base.Start();
	}

    protected override void ActivateAbility()
    {
        Debug.Log("Toot used Tackle! but it failed! Cuz he's a worthless piece of shit");
    }

    protected override void PassiveAbility()
    {
        Tootanim.SetFloat("Speed", shit.velocity.magnitude);
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.collider.CompareTag("Player"))
        {
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            rb.AddForce(Vector3.up * 50, ForceMode.Impulse);
            _rbAI.velocity = Vector3.zero;
            Tootanim.SetTrigger("Flip");
            bounce.Play();
            print("go up");
        }
    }

    protected override void AIStateMachine()
    {
        base.AIStateMachine();
        switch (AIState)
        {
            case StateMachine.IDLE:
                Tootanim.SetFloat("Speed", 0);
                break;
            case StateMachine.WALK:
                Tootanim.SetFloat("Speed", 1);
                break;
        }
    }
}

using UnityEngine;
using System.Collections;
using System;

public class NewGolemAI : NewAIBA {

    Animator GolemController;
    public bool deflect;
    float timer;

    // Use this for initialization
    protected override void Start()
    {
        GolemController = GetComponent<Animator>();
        base.Start();
    }

    protected override void PassiveAbility()
    {
        float _speed;
        _speed = _rbAI.velocity.magnitude;
        GolemController.SetFloat("moveSpeed", _speed);
        timer += Time.deltaTime;
        if (timer > 1)
        {
            deflect = false;
        }
    }
    protected override void ActivateAbility()
    { 
        GolemController.SetTrigger("Harden");
        gameObject.GetComponent<AudioSource>().Play();
        if (!deflect)
        {
            deflect = true;
            timer = 0;
        }
    }
}

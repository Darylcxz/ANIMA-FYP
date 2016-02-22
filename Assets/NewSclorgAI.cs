using UnityEngine;
using System.Collections;
using System;

public class NewSclorgAI : NewAIBA {

    enum SclorgState
    {
        PURSUE,
        AIM,
        SHOOT,
        DEATH
    }
    SclorgState States;
    float gameTime;
    Animator SclorgAnim;

    //Look-at function variables 
    Vector3 targetSpeed;
    Vector3 futurePos;
    float iterations = 30f;
    Vector3 _direction;
    GameObject target;


    float _distance;
    [SerializeField] float shootRange;
    [SerializeField] Transform shootPos;
    [SerializeField] Rigidbody projectile;
    protected override void Start()
    {
        SclorgAnim = GetComponent<Animator>();
        base.Start();
    }
    protected override void ActivateAbility()
    {
        gameTime += Time.deltaTime;
        switch (States)
        {
            case SclorgState.PURSUE:
                SclorgMovement();
                break;
            case SclorgState.AIM:
                ChangeState(0.7f, SclorgState.SHOOT);
                break;
            case SclorgState.SHOOT:
                Fire();
                break;
            case SclorgState.DEATH:
                break;
        }
    }
    protected override void PassiveAbility()
    {
        _distance = DistanceBetween(player.position, transform.position);
    }
    void SclorgMovement(bool isAiming = false)
    {
        target = player.gameObject;
        targetSpeed = target.GetComponent<Rigidbody>().velocity;
        futurePos = target.transform.position + (targetSpeed * (_distance / iterations));
        _direction = futurePos - transform.position;
        _direction.y = 0;
        Quaternion lookRot = Quaternion.LookRotation(_direction);
        _rbAI.MoveRotation(lookRot);
        if(!isAiming)
        {
            transform.localPosition += transform.forward * Time.deltaTime * speedAI;
            if(DistanceBetween(player.position,transform.position) < shootRange)
            {
                States = SclorgState.AIM;
            }
        }
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 2f * Time.deltaTime);
    }
    void ChangeState(float timeBeforeStateChange, SclorgState nextState)//small state changer, takes in time you want to wait for before state change, and the next state
    {
        if (gameTime > timeBeforeStateChange)
        {
            gameTime = 0;
            States = nextState;
        }
    }
    void Fire()
    {
        Rigidbody projectileClone = Instantiate(projectile, shootPos.position, transform.rotation) as Rigidbody;
        projectileClone.velocity = transform.forward * 30;
        States = SclorgState.PURSUE;
    }
}

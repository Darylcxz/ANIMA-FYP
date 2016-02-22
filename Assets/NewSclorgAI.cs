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

    AudioSource sclorgSound;
    protected override void Start()
    {
        SclorgAnim = GetComponent<Animator>();
        sclorgSound = GetComponent<AudioSource>();
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
                SclorgAnim.Play("S_Aim");
                SclorgMovement(true);
                ChangeState(3f, SclorgState.SHOOT);
                break;
            case SclorgState.SHOOT:
                SclorgAnim.Play("S_Shoot");
                Fire();
                break;
            case SclorgState.DEATH:
                SclorgAnim.SetTrigger("tDeath");
                break;
        }
    }
    protected override void AIStateMachine()
    {
        base.AIStateMachine();
        switch(AIState)
        {
            case StateMachine.IDLE:
                SclorgAnim.SetBool("isMoving", false);
                break;
            case StateMachine.WALK:
                SclorgAnim.SetBool("isMoving", true);
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
       if(DistanceBetween(player.position,transform.position)<shootRange)
        {
            States = SclorgState.AIM;
        }
       else if(DistanceBetween(player.position,transform.position) > shootRange)
        {
            SclorgAnim.StopPlayback();
            States = SclorgState.PURSUE;
        }
    }
    public void DeathByLight()
    {
        States = SclorgState.DEATH;
        Invoke("SinkToGround", 5f);
    }
    void SinkToGround()
    {
        sclorgSound.Play();
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        gameObject.GetComponent<Collider>().isTrigger = true;
    }
}

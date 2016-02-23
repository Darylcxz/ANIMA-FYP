using UnityEngine;
using System.Collections;
using System;

public class NewHopputAI : NewAIBA {
    [SerializeField] LayerMask _layerMask;
    [SerializeField] Collider col;
    [SerializeField] float attackRange = 3f;
    [SerializeField] GameObject dustWave;
    Animator HopputAnim;
    AudioSource hopputSound;
    Vector3 lastPos;
    bool hasLanded;
    [SerializeField] enum HopputState
    {
        IDLE,
        MOVE,
        SHAKE,
        JUMP,
        LAND,
        RECOIL,
        DEATH
    }
    HopputState States;
    float gameTime;
    protected override void Start()
    {
        HopputAnim = GetComponent<Animator>();
        hopputSound = GetComponent<AudioSource>();
        col = col.GetComponent<Collider>();
        healthAI = 5;
        base.Start();
    }
    protected override void ActivateAbility()
    {
        gameTime += Time.deltaTime;
        if(healthAI < 1)
        {
            States = HopputState.DEATH;
        }
        switch (States)
        {
            case HopputState.IDLE:
                
                HopputAnim.SetBool("isMoving", false);
                ChangeState(5f, HopputState.MOVE);
                break;
            case HopputState.MOVE:
                HopputAnim.SetBool("isMoving", true);
                HopputMove(transform.position,player.position,States);
                break;
            case HopputState.SHAKE:
                HopputAnim.Play("HOP_SHAKEONLY");
                ChangeState(3f, HopputState.JUMP);
                lastPos = player.position;
                break;
            case HopputState.JUMP:
                HopputAnim.Play("HOP_JUMP");
                HopputMove(transform.position,lastPos,States,2f);
                
                break;
            case HopputState.LAND:
                HopputAnim.Play("HOP_LAND");
                if(hasLanded)
                {
                    ExplosionForce(transform.position, 5);
                }
                HopputAnim.StopPlayback();
                States = HopputState.IDLE;
                break;
            case HopputState.RECOIL:
                HopputAnim.Play("HOP_RECOIL");
                ChangeState(0.5f, HopputState.IDLE);
                HopputAnim.StopPlayback();
                break;
            case HopputState.DEATH:
                HopputAnim.Play("HOP_DEATH");
             
                break;
        }
    }
    protected override void AIStateMachine()
    {
        base.AIStateMachine();
        switch(AIState)
        {
            case StateMachine.IDLE:
                HopputAnim.SetBool("isMoving", false);
                break;
            case StateMachine.WALK:
                HopputAnim.SetBool("isMoving", true);
                break;
        }
    }
    protected override void PassiveAbility()
    {
        //throw new NotImplementedException();
    }
    void HopputMove( Vector3 A,Vector3 B,HopputState state, float speedMultiplier = 1)
    {
        Vector3 dir = (B - A).normalized;
        dir.y = 0;
        Quaternion endRot = Quaternion.LookRotation(dir);
        transform.rotation = endRot;
        transform.position += transform.forward * Time.deltaTime * speedAI * speedMultiplier;
        switch(state)
        {
            case HopputState.MOVE:
                if(DistanceBetween(A,B) < attackRange)
                {
                    ChangeState(0.5f, HopputState.SHAKE);
                }
                break;
            case HopputState.JUMP:
                if(DistanceBetween(A,B) < 2)
                {
                    ChangeState(0.5f, HopputState.LAND);
                }
                break;
        }

    }
    public void Landed()
    {
        hasLanded = true;
    }
    void ExplosionForce(Vector3 center,float radius)
    {
        Collider[] hitCol = Physics.OverlapSphere(center, radius, _layerMask);
        foreach(Collider hit in hitCol)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if(rb!=null)
            {
                rb.AddExplosionForce(20, center, radius, 2, ForceMode.Impulse);
            }
        }
        Vector3 floorPos = center;
        center.y = -0.9f;
        dustWave.transform.position = floorPos;
        dustWave.GetComponent<Animator>().SetTrigger("Explode");
        hasLanded = false;
    }
    public void SeepIntoGround()
    {
        hopputSound.Play();
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        gameObject.GetComponent<Collider>().isTrigger = true;
        col.isTrigger = true;
    }

    void ChangeState(float timeBeforeStateChange, HopputState nextState)
    {
        if(gameTime > timeBeforeStateChange)
        {
            gameTime = 0;
            States = nextState;
        }
    }
    void OnCollisionEnter(Collision col)
    {
        if(col.collider.CompareTag("dagger"))
        {
            healthAI--;
        }
    }
    
}

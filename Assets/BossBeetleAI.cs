using UnityEngine;
using System.Collections;

public class BossBeetleAI : MonoBehaviour {
    [SerializeField] enum BossStates //State machine for the AI
    {
        INGROUND,
        IDLE,
        WALKING,
        ATTACK,
        STUNNED,
        PRYING,
        HIT,
        DEATH
    }
    [SerializeField]BossStates BeetleLogic = BossStates.INGROUND;
    [SerializeField] BossNepto _neptoScript;
    Animator BossAnim; //animator
    [SerializeField] float attackRange; //atack range for the AI to start attacking
    float gameTime; //timer for state transitions
    Transform player; //transform to get the players position
    bool bNotInGround;  //one time use boolean to check if the beetle is in the ground or not
    [SerializeField]int numberOfTimesHit; //how many times the AI has been hit (might work as HP too)

    [SerializeField] enum AttackStates //Attack states for the beetle
    {
        AIMING,
        DASH
    }
    [SerializeField] AttackStates AttackLogic;
    float attackInt;   //Attack int for chance stuff;
    [SerializeField] bool hasDeflected; //checks if the player deflected the shot

    [SerializeField]enum DashStates
    {
        CHARGEUP,
        DASH,
        DASHEND
    }
    [SerializeField] DashStates DashLogic;
    float attackTime;
    Vector3 lastPos;

    [SerializeField] bool isWingOpen;
    [SerializeField] bool hasHit;
    [SerializeField] bool hasTongued;
    float stunTimer = 0;
    float pryTimer = 0;
    bool wingsAnimClosed;
    Collider beetleCollider;
    // Use this for initialization
    void Start () {
        isWingOpen = true; //beetles wings are open at first
        BossAnim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _neptoScript = _neptoScript.GetComponent<BossNepto>();
        beetleCollider = GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void Update () {
        StateMachine();
	}
    void StateMachine()
    {
        gameTime += Time.deltaTime;
        switch (BeetleLogic)
        {
            case BossStates.INGROUND:
                //???? just incase the beetle needs to do some logic here

                if(bNotInGround)
                {
                    ChangeState(0f, BossStates.IDLE);
                }
                break;
            case BossStates.IDLE:
                attackInt = Random.value; //sets the attack int
                _neptoScript.shootCount = 0; //resets the shot count back to zero
                ResetAnim();
                //BossAnim.SetBool("bIsWalking", false);
                ChangeState(5f, BossStates.WALKING);  //after 10 seconds of idling, move to next state
                break;
            case BossStates.WALKING:
                //beetle walks until in range of player then it shoots
                MoveToPlayer(transform.position, player.position, 2f);
                BossAnim.SetBool("bIsWalking", true);
                
                break;
            case BossStates.ATTACK:
                BossAnim.SetBool("bIsWalking", false); //sets walking anim to OFF
                switch (numberOfTimesHit) //checks how many times the beetle has been hit
                {
                    case 0:
                        AttackLogic = AttackStates.AIMING;
                        break;
                    case 1:
                        if(attackInt <= 0.5f)
                        {
                            AttackLogic = AttackStates.AIMING;
                        }
                        else
                        {
                            AttackLogic = AttackStates.DASH;
                        }
                        break;
                    case 2:
                        if (attackInt <= 0.5f)
                        {
                            AttackLogic = AttackStates.AIMING;
                        }
                        else
                        {
                            AttackLogic = AttackStates.DASH;
                        }
                        break;
                    case 3:
                        if (attackInt <= 0.3f)
                        {
                            AttackLogic = AttackStates.AIMING;
                        }
                        else
                        {
                            AttackLogic = AttackStates.DASH;
                        }
                        break;
                    case 4:
                        if (attackInt <= 0.3f)
                        {
                            AttackLogic = AttackStates.AIMING;
                        }
                        else
                        {
                            AttackLogic = AttackStates.DASH;
                        }
                        break;
                    case 5:
                        //dead
                        BossAnim.Play("Boss_ESCAPE");
                        ResetAnim();
                        _neptoScript.enabled = false;
                   
                        break;
                }
                AttackStateMachine();
                break;
            case BossStates.STUNNED:
                hasDeflected = false;
                stunTimer += Time.deltaTime;
                if (!isWingOpen)
                {
                    beetleCollider.isTrigger = true; //you can't hit it if the wings are closed
                    BossAnim.SetBool("bWingsClose", true);
                    //check if has tongued
                    if(hasTongued)
                    {
                        BossAnim.SetBool("bWingsClose", false);
                        BossAnim.SetBool("bHasTongued", true);
                        BeetleLogic = BossStates.PRYING;
                    }
                    else if(stunTimer > 10f)
                    {
                        BossAnim.SetTrigger("tStunEnd"); //stun ends
                        BeetleLogic = BossStates.IDLE; //back to idle
                        gameTime = 0;
                        stunTimer = 0;
                        BossAnim.SetBool("bHasTongued", false);
                    }
                }
                if (isWingOpen) //if the wings are open while its stunned
                {
                    beetleCollider.isTrigger = false;
                    BossAnim.SetBool("bWingsClose", false); //set the correct boolean to play the correct animation
                    if (hasHit) //if the player hits the beetle
                    {
                        BossAnim.SetBool("bWingsClose", true);
                        beetleCollider.isTrigger = true;
                        BossAnim.SetBool("bHasDeflected", false);
                        stunTimer = 0;
                        ChangeState(1f, BossStates.IDLE);
                        hasHit = false;
                        isWingOpen = false;
                    }
                    else if (stunTimer > 10f) //but if the timer exceeds 
                    {
                        BossAnim.SetTrigger("tStunEnd"); //stun ends
                        BeetleLogic = BossStates.IDLE; //back to idle
                        gameTime = 0;
                        stunTimer = 0;
                        BossAnim.SetBool("bHasDeflected", false);
                    }
                }
               
                break;
            case BossStates.PRYING:
                BossAnim.SetBool("bHasTongued", false);
                hasTongued = false;
                pryTimer += Time.deltaTime;
                if (GamepadManager.buttonBDown)
                {
                    BossAnim.SetTrigger("tPry");
                    isWingOpen = true;
                    BeetleLogic = BossStates.STUNNED;
                    gameTime = 0;
                    pryTimer = 0;
                    stunTimer = 0;
                }
                else if (pryTimer > 10f)
                {
                    BossAnim.SetTrigger("tPryEnd");
                    BeetleLogic = BossStates.IDLE;
                    gameTime = 0;
                    pryTimer = 0;
                }                
                break;
            case BossStates.HIT:
                break;
            case BossStates.DEATH:
                break;
        }
    }
    void AttackStateMachine()
    {
        BossAnim.SetBool("bIsAttacking", true);
        switch (AttackLogic)
        {
            case AttackStates.AIMING:
                _neptoScript.range = 20;
                BossAnim.SetInteger("iAttack", 0); //sets animator to this anim
                //aiming logic goes here
                _neptoScript.beetleControl = false; //gives control back to nepto
                if(hasDeflected) //if player deflected
                {
                    _neptoScript.range = 5;
                    BossAnim.SetBool("bHasDeflected", true);
                    ChangeState(0f, BossStates.STUNNED); //change state to stunned
                    _neptoScript.beetleControl = true; //gives control back to beetle, disabling the nepto
                    _neptoScript.shootCount = 0; //resets the shot count back to zero
                    BossAnim.SetBool("bIsAttacking", false); //stops attacking animation from playing
                }
                if(_neptoScript.shootCount == 3 &&!hasDeflected) //if the nepto has shot thrice, 
                {
                    _neptoScript.range = 5;
                    BossAnim.SetBool("bIsAttacking", false); //stops attacking animation from playing
                    _neptoScript.beetleControl = true;//gives control back to the beetle first
                    ChangeState(3f, BossStates.IDLE); //go back to idle after 2 seconds (to allow player time to deflect 
                }
                break;
            case AttackStates.DASH:
                BossAnim.SetInteger("iAttack", 1);
                //AI deshing goes here
                attackTime += Time.deltaTime;
                switch (DashLogic)
                {
                    case DashStates.CHARGEUP:
                        MoveToPlayer(transform.position, player.position, 0); //makes the beetle only face gulnaz
                        if(attackTime > 3f)
                        {
                            BossAnim.SetTrigger("tCharge");
                            lastPos = player.position;
                            attackTime = 0;
                            DashLogic = DashStates.DASH;
                        }          
                        break;
                    case DashStates.DASH:
                        MoveToPlayer(transform.position, lastPos, 5,true);
                        break;
                    case DashStates.DASHEND:
                        BossAnim.SetTrigger("tDashEnd");
                        BossAnim.SetBool("bIsAttacking", false);
                        BeetleLogic = BossStates.IDLE;
                        gameTime = 0;   
                        DashLogic = DashStates.CHARGEUP;
                        attackTime = 0;
                        break;
                }
                break;
        }
    }
    void MoveToPlayer(Vector3 A, Vector3 B,float _speed, bool isDashing = false)
    {
        Vector3 direction = (B - A).normalized; //gets direction
        direction.y = 0;                //makes the AI not fly up by setting U direction to 0
        Quaternion endRot = Quaternion.LookRotation(direction);     //makes a quaternion for that look dir
        if(!isDashing) //if the beetle is NOT dashing and is just walking
        {
            if (DistanceBetween(A, B) < attackRange) //do something when the player is within attacking range
            {
                BeetleLogic = BossStates.ATTACK; //change states to attack
                gameTime = 0;
            }
            transform.rotation = endRot; //rotate the beetle to face the dir 
            Vector3 offSet = transform.rotation.eulerAngles;
            offSet.y -= 90;
            transform.eulerAngles = offSet;
            transform.position += transform.right * Time.deltaTime * _speed; //moves the beetleforward
        }
        if(isDashing) //if however, the beetle IS dashing
        {
            transform.rotation = endRot; //set the rotation to face the position

            Vector3 offSet = transform.rotation.eulerAngles;
            offSet.y -= 90;
            transform.eulerAngles = offSet;
            transform.position += transform.right * Time.deltaTime * _speed; //move it forward
            if (DistanceBetween(A,B)< 2) //if the beetle reaches near it's destination (where player last was_
            {
                DashLogic = DashStates.DASHEND; //change state to end the dash
                gameTime = 0;
            }
        }
        
    }

    void ChangeState(float timeBeforeStateChange, BossStates nextState)
    {
        if(gameTime > timeBeforeStateChange)
        {
            BeetleLogic = nextState;
            gameTime = 0;
        }
    }
    void ChangeAttackState(float timeBeforeStateChange,DashStates nextState)
    {
        if(attackTime > timeBeforeStateChange)
        {
            DashLogic = nextState;
            attackTime = 0;
        }
    }







    public void TrapCardActivated() //nepto was hit by player
    {
        BossAnim.SetTrigger("tHitNepto"); //animation trigger to get beetle out
        _neptoScript.beetleControl = true; //boolean to stop the nepto from shooting
        _neptoScript.shootCount = 0;        //clear the number of shots the nepto has shot
    }
    public void GotDeflected() //function for when the bullet hits the beetle
    {
        hasDeflected = true;
    }
    public void StartIdle()
    {
        bNotInGround = true;
    }
    public void WingsClosed()
    {
        wingsAnimClosed = true;
    }
    public void TongueAction()
    {
        hasTongued = true;
    }
    float DistanceBetween(Vector3 A, Vector3 B)
    {
        return Vector3.Distance(A, B);
    }
    public void ShakeCamNow()
    {
        UtilityScript.instance.CameraShake(2f, 1f);
    }
    void ResetAnim(bool hardReset = false)
    {
        stunTimer = 0;
        BossAnim.SetBool("bHasDeflected", false);
        BossAnim.SetBool("bIsAttacking", false);
        BossAnim.SetBool("bHasTongued", false);
        BossAnim.SetBool("bIsWalking", false);
        if(hardReset) //only reset this if you wanna hard reset the beetle and make it forget its attack and wings stuff
        {
            BossAnim.SetInteger("iAttack", 0);
            BossAnim.SetBool("bWingsClose", false);
        }
    }
    void OnCollisionEnter(Collision col)
    {
        if(col.collider.CompareTag("dagger"))
        {
            BossAnim.SetTrigger("tHitBeetle");
            numberOfTimesHit++;
            hasHit = true;
        }
    }
}

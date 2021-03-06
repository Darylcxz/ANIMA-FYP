﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MovementController : MonoBehaviour {

    //States for Movement
    public enum States
    {
        idle,			//idle state obviously, this state checks all your inputs and stuff
        move,			//move state - controls movements, and what states can transition from hre
        attack,			//Attack state - controls attack logic 
        jump,			//jumping state - needs to check if player is grounded or not for animation
        roll,			//rolling state - needs to allow player Invincibility frames and rolling speed
        possess,		//possess state - possession logic where player can control creature
        climb,			//climb state - state for climbing up shenanigans
        sequencedjump,   //jumping sequence state - state for jumping event thingy
        pushpull
    };

    public States charStates = States.idle;
    Animator _anim;
    

    //input stuff
    public bool bKeyboard = false;     //if this is true, it means you're using keyboard movement
    public bool bTutorial = true;       //if true, disables jump and roll
    public bool bForcedMove = false;		//if true, disables ALL movement

    //bool roll;
    float roll;						//Roll input is a float from 0-1
    bool attack;					//Attack boolean 
    bool jump;						//jump boolean
    bool possess;					//possession boolean
    float hMove;					//horizontal movement axis (left stick)
    float vMove;					//vertical movement axis (left stick)
    [SerializeField]Image atk1;     //Sprite animation for attacks
    [SerializeField]Image atk2;
    Animator a1;
    Animator a2;
    bool attack1 = false;
    bool attack2 = false;

    float vMoveRight;				//right stick vertical movements
    float hMoveRight;				//right stick horizontal movements
    [SerializeField] ParticleSystem grounddust;  // particle dust when walking
    [SerializeField] GameObject jumpdust;    // particle dust when jumping
    [SerializeField] AudioClip footsteps;
    [SerializeField] AudioClip jumpsound;
    AudioSource audio;


    int attackMode = 0; //1: Stab, 2: swing
    float attackSpeed = 1;
    bool isAttacking;               //bool for attacking
    float timeButtonPressed;        //time when you press the button
                                    //float timeSinceButtonPressed;	//time since you pressed the button
    float buttonPressAllowance = 0.5f;      //time allowed before state changes

    //    float groundDist;
    //    float waitTime = 0.5f;
    bool ready = false;				//bool to trigger next state
    bool isRolling = false;         //bool to check if in rolling state


    public float speed = 15.0f;		//speed of player moving
    public float jumpForce = 5.0f;	//amount of force added to lift player up when jumping
    public float smoothDamp = 1.0f;	//smooth damping for rotation

    Rigidbody _rigidBody;           //player's rigidbody
    Collider _dagger;               //collider for dagger
                                    //    Collider _collider;

    Vector3 groundPos;				//position of the ground
    Vector3 playerPos;				//position of the player
    RaycastHit hit;                 //raycasthit component of the raycast for the player

    // Mana Stuff;

    public Image _manaBarUI;        //Mana bar UI component
    public float currMana;          //current mana player has
    float maxMana;                  //max mana player is allowed to have
    bool _mana;                     //if false, player regenerates mana

    //Slow effect stuff;
    float slowTime;
    bool _slow;
    float originalSpeed;

    [SerializeField] bool hasJumpSequence;
    [SerializeField]NewJumpSequence guideJump;


    //Push and pull anim stuff
    bool pushing;
    bool pulling = false;
    RaycastHit hitcube;
    [SerializeField]Transform lefty;      //For triple raycasts, because fuck vector3 calculations
    [SerializeField]Transform righty;

    //Climbing
    Vector3 climbstart;

    // Use this for initialization
    void Start() {
        if (hasJumpSequence)
        {
            guideJump = guideJump.GetComponent<NewJumpSequence>();
        }
        //Dagger stuff, disables and hides the dagger collider and trail renderer so that it doesn't show
        _dagger = GameObject.FindGameObjectWithTag("dagger").GetComponent<Collider>();
        _dagger.enabled = false;


        _rigidBody = gameObject.GetComponent<Rigidbody>();
        //   groundDist = gameObject.GetComponent<Collider>().bounds.center.y;
        //   _collider = gameObject.GetComponent<Collider>();
        _anim = gameObject.GetComponent<Animator>();
        maxMana = 50f;
        currMana = maxMana;
        originalSpeed = speed;
        a1 = atk1.GetComponent<Animator>();
        a2 = atk2.GetComponent<Animator>();
        audio = GameObject.Find("SFX").GetComponent<AudioSource>();




    }
    void Update()
    {
        CheckInput();     //Checks input in the update so it's faster
        CheckMana();	  //Checks mana to update the slider bar
        //        Debug.Log(_rigidBody.velocity.magnitude);
        if (isGrounded())
        {
            _anim.SetFloat("speed", _rigidBody.velocity.magnitude); // changes anim speed value to make it play move anim
        }

        if(attack1)
        {
            a1.SetTrigger("Atk");
            attack1 = false;
        }

        if(attack2)
        {
            a2.SetTrigger("Atk");
            attack2 = false;
        }
                                                       // _anim.SetInteger("attack", attackMode); //1: stab, 2:swing
        _anim.SetBool("isRolling", isRolling);//change param to be the same as bool isRolling

        if (charStates == States.idle)
        {
            if (attack && !GameControl.spiritmode)
            {
                _anim.SetTrigger("tAttack");
                charStates = States.attack;
            }
        }
        if (charStates == States.move)
        {
            if (attack)
            {
                _anim.SetTrigger("tAttack");
                Animator a1 = atk1.GetComponent<Animator>();
                a1.SetTrigger("Atk");
                _rigidBody.velocity = Vector3.zero;
                charStates = States.attack;
            }
        }
        slowTime += Time.deltaTime;
        if (_slow && slowTime > 3f)
        {
            _slow = false;
            speed = originalSpeed;
        }

    }

    // Update is called once per frame
    void FixedUpdate() {


        if (possess && ready == false && isRolling == false && isGrounded() && currMana > 0)
        {
            charStates = States.possess;
        }

        switch (charStates)
        {
            case States.idle:
                //check if player is grounded
                attackMode = 0;
                //	  ready = false;
                isRolling = false;
                attackSpeed = 1;
                if (vMove != 0f || hMove != 0f)
                //if(GamepadManager.v1 !=0 || GamepadManager.h1 !=0)
                {
                    charStates = States.move;
                }
                if (jump && isGrounded())
                {
                    Invoke("Jump", 0.3f);
                    //Jump();
                    _anim.SetTrigger("tJump");
                    charStates = States.jump;
                }

                if (roll != 0)
                {
                    charStates = States.roll;
                    isRolling = true;
                }
                CheckClimb();
                break;
            case States.move:

                RotatingLogic(hMove, vMove);
                MovementLogic(hMove, vMove);
                //RotatingLogic(GamepadManager.h1, GamepadManager.v1);
                //MovementLogic(GamepadManager.h1, GamepadManager.v1);

                if (vMove == 0 && hMove == 0)
                {
                    charStates = States.idle;
                }
                if (jump && isGrounded())
                {
                    Jump();
                    _anim.SetTrigger("tJump");
                    //	_rigidBody.velocity = Vector3.zero;
                    charStates = States.jump;
                }

                if (roll != 0)
                {
                    charStates = States.roll;
                }
                CheckClimb();
                break;
            case States.jump:
                MovementLogic(hMove * 0.5f, vMove * 0.5f);
                //_rigidBody.AddForce(Vector3.up*jumpForce,ForceMode.Impulse);
                if (isGrounded())
                {
                    //_rigidBody.velocity = Vector3.zero;
                    charStates = States.idle;
                }
                CheckClimb();
                break;
            case States.possess:
                _mana = true;
                //MovementLogic(hMoveRight, vMoveRight);
                // RotatingLogic(hMoveRight, vMoveRight);
                //	Debug.Log("possss");
                MovementLogic(GamepadManager.h2, GamepadManager.v2);
                RotatingLogic(GamepadManager.h2, GamepadManager.v2);

                if (GameControl.spiritmode == false)
                {
                    charStates = States.idle;
                    _mana = false;
                    //  Debug.Log("possass");
                }

                break;
            case States.roll:
                // _rigidBody.AddForce(transform.forward/1.5f,ForceMode.Impulse);
                // _rigidBody.AddRelativeForce(transform.forward / 0.9f, ForceMode.Impulse);
                // _rigidBody.AddForceAtPosition(transform.forward *5, transform.localPosition);
                //_rigidBody.velocity += transform.forward/3f;
                gameObject.transform.localPosition += (transform.forward * 5 * Time.deltaTime);
                if (!isRolling || bForcedMove)
                {
                    charStates = States.idle;
                }
                break;
            case States.attack:
                _dagger.enabled = true;
                AttackLogic();
                //attackSpeed+=Time.deltaTime;
                ////   attackMode = 1;
                //   if(attack && attackMode == 1 && !ready)
                //   {                  
                //	   charStates = States.swing;
                //	//   ready = false;
                //	   attackMode = 2;
                //   }
                if (ready)
                {
                    charStates = States.idle;
                    _dagger.enabled = false;
                    ready = false;
                }
                break;
            //case States.swing:
            //	_dagger.GetComponent<TrailRenderer>().enabled = true;
            //	_dagger.enabled = true;
            //	attackSpeed+=Time.deltaTime;
            //   // attackMode = 2;
            //	if (attack && attackMode == 2 && !ready)
            //	{
            //		charStates = States.stab;
            //		//ready = false;
            //		attackMode = 1;
            //	}
            //	else if (ready)
            //	{
            //		charStates = States.idle;
            //		_dagger.GetComponent<TrailRenderer>().enabled = false;
            //		_dagger.enabled = false;
            //	}          
            //	break;

            case States.climb:

                if (Physics.Raycast(transform.position, transform.forward, 1))
                {
                    ClimbLogic(GamepadManager.v1);
                    _anim.SetBool("isClimbing", true);
                }

                else
                {
                    _rigidBody.useGravity = true;
                    _rigidBody.constraints = RigidbodyConstraints.FreezeRotation;
                    _rigidBody.AddForce(transform.forward + transform.up, ForceMode.Impulse);
                    _anim.SetBool("isClimbing", false);
                    charStates = States.idle;

                }
                break;

            case States.sequencedjump:
                if (guideJump.freezeMovement)
                {
                    RotatingLogic(GamepadManager.h1, GamepadManager.v1);
                }
                else if (!guideJump.freezeMovement)
                {
                    charStates = States.idle;
                }

                break;

            case States.pushpull:
                MovementLogic(hMove / 2, vMove / 2);
                Vector3 movementd = new Vector3(hMove, 0, vMove);
                _anim.SetBool("isPulling", pulling);
                _anim.SetBool("isPushing", pushing);
                if (Physics.Raycast(transform.position, movementd, out hitcube, 1.0f) || Physics.Raycast(lefty.position, movementd, out hitcube, 1.0f) || Physics.Raycast(righty.position, movementd, out hitcube, 1.0f))
                {
                    if (hit.collider.CompareTag("movable"))
                    {
                        pushing = true;
                        pulling = false;
                        _anim.speed = 1;
                    }
                }

                else if (hMove == 0 && vMove == 0)
                {
                    pushing = true;
                    pulling = false;
                    _anim.speed = 0;
                }

                else
                {
                    pulling = true;
                    pushing = false;
                    _anim.speed = 1;
                }
                break;
        }
    }

    void CheckInput()
    {
        // roll = Input.GetKeyDown(KeyCode.LeftShift);
        if (bForcedMove)
        {
            hMove = 0;
            vMove = 0;
            roll = 0;
            isRolling = false;
            isAttacking = false;
            _anim.SetBool("isAttacking", false);
            attack = false;
            _rigidBody.velocity = Vector3.zero;
            charStates = States.idle;
            _dagger.enabled = false;

        }
        if (bKeyboard && !bForcedMove)
        {
            hMove = Input.GetAxis("Horizontal");
            vMove = Input.GetAxis("Vertical");
            //roll = Input.GetKeyDown(KeyCode.LeftShift);
            if (Input.GetKeyDown(KeyCode.LeftShift) && !bTutorial)
            {
                roll = 1;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift) && !bTutorial)
            {
                roll = 0;
            }
            attack = Input.GetMouseButtonDown(0);
            if (!bTutorial)
            {
                jump = Input.GetKeyDown(KeyCode.Space);
            }
            possess = Input.GetMouseButtonDown(1);
        }
        else if (!bKeyboard && !bForcedMove)
        {
            hMove = GamepadManager.h1;
            vMove = GamepadManager.v1;

            attack = GamepadManager.buttonXDown;
            possess = GamepadManager.buttonY;
            if (!bTutorial)
            {
                jump = GamepadManager.buttonADown;
                roll = GamepadManager.triggerR;
            }
        }


        //attack = Input.GetMouseButtonDown(0);

        // jump = Input.GetKeyDown(KeyCode.Space);

        // possess = Input.GetMouseButtonDown(1);




    }
    void MovementLogic(float horizontal, float vertical)
    {

        float h2 = horizontal * 2;
        float v2 = vertical * 2;
        Vector3 targetVelocity = new Vector3(h2 + v2, 0, v2 - h2);
        targetVelocity.Normalize();
        targetVelocity *= speed;
        Vector3 velocity = _rigidBody.velocity;
        Vector3 vChange = targetVelocity - velocity;
        vChange = new Vector3(Mathf.Clamp(vChange.x, -speed, speed), 0, (Mathf.Clamp(vChange.z, -speed, speed)));
        _rigidBody.AddForce(vChange, ForceMode.VelocityChange);


    }
    void RotatingLogic(float h, float v)
    {
        if (GulnazGrab.holding == false)
        {
            float h3 = h * 2;
            float v3 = v * 2;
            Vector3 targetDir = new Vector3(h3 + v3, 0, v3 - h3).normalized;

            Quaternion targetRot = Quaternion.LookRotation(targetDir, Vector3.up);
            Quaternion newRot = Quaternion.Lerp(_rigidBody.rotation, targetRot, smoothDamp * Time.deltaTime);
            _rigidBody.MoveRotation(newRot);
        }
    }
    public void attackEnd()
    {
        ready = true;
        //attackMode = 0;
        //  Debug.Log("Attack ended");
    }
    public void attackStart()
    {
        ready = false;
        Debug.Log("Attack started");
    }
    bool isGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, 0.5f);
    }
    public void rollEnd()
    {
        isRolling = false;
    }
    void CheckMana()
    {
        _manaBarUI.fillAmount = currMana / maxMana;
        //	Debug.Log(currMana);
        if (currMana < 0)
        {
            GameControl.spiritmode = false;
            currMana = 0;
            charStates = States.idle;
            _mana = false;
        }
        if (currMana > maxMana && _mana == true) // check if mana is maxed
        {
            currMana = maxMana; //caps it back
            _mana = false;
        }

        else if (currMana < maxMana && _mana == false)
        {
            currMana += Time.deltaTime*5;
        }
    }

    void CheckClimb()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, 1) || Physics.Raycast(lefty.position, transform.forward, out hit, 1) || Physics.Raycast(righty.position, transform.forward, out hit, 1))
        {
            if (GamepadManager.buttonBDown && hit.collider.tag == "Climable")
            {
                charStates = States.climb;
                Quaternion targetRot = Quaternion.LookRotation(-hit.normal, Vector3.up);
                _rigidBody.rotation = targetRot;
                transform.position = new Vector3(hit.transform.position.x - 0.8f, transform.position.y, transform.position.z);
                _rigidBody.constraints = RigidbodyConstraints.FreezeRotation & RigidbodyConstraints.FreezePositionZ & RigidbodyConstraints.FreezePositionX;
                _rigidBody.useGravity = false;
            }
        }
    }

    void ClimbLogic(float v)
    {
        if (v == 0)
        {
            _anim.speed = 0;
        }
        else
        {
            Vector3 ClimbForce = Vector3.up * v;
            ClimbForce.Normalize();
            _rigidBody.velocity = ClimbForce * 4;
            _anim.speed = 1;
        }

    }

    void AttackLogic()
    {
        if (attack)
        {
            timeButtonPressed = Time.time;
            _anim.SetBool("isAttacking", true);

            gameObject.transform.localPosition += (transform.forward * 3 * Time.deltaTime);
        }
        if (Time.time > timeButtonPressed + buttonPressAllowance)
        {
            _anim.SetBool("isAttacking", false);
            ready = true;
        }
    }
    public void Slow() //slows the player down
    {
        float tempSpeed = originalSpeed / 2;
        slowTime = 0;
        _slow = true;
        speed = tempSpeed;
    }

    public void AddMana(float manatoadd)
    {
        currMana += manatoadd;
    }

    public void Footsteps()
    {
        Instantiate(grounddust, transform.position, Quaternion.identity);
        audio.PlayOneShot(footsteps);
    }

    public void PushorPull()
    {
        charStates = States.pushpull;
    }

    public void StopPushPull()
    {
        charStates = States.move;
        _anim.SetBool("isPulling", false);
        _anim.SetBool("isPushing", false);
        _anim.speed = 1;
    }

    void Jump()
    {
        _rigidBody.velocity += Vector3.up * jumpForce;
        audio.PlayOneShot(jumpsound);
        jumpdust.transform.position = transform.position;
        Animator dustboom = jumpdust.GetComponent<Animator>();
        dustboom.SetTrigger("Explode");
        audio.PlayOneShot(jumpsound);
    }

    public void Attack1()
    {
        attack1 = true;
    }

    public void Attack2()
    {
        attack2 = true;
    }
}

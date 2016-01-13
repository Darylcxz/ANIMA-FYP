using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MovementController : MonoBehaviour {

	
	public enum States
    {
        idle,
        move,
        stab,
        swing,
        jump,
        roll,
        possess,
        climb,
    };

   public States charStates = States.idle;
   Animator _anim;

    //input stuff
   public bool bKeyboard = false;
   public bool bTutorial = true;
   public bool bForcedMove = false;

    //bool roll;
    float roll;
    bool attack;
    bool jump;
    bool possess;
    float hMove;
    float vMove;


    float vMoveRight;
    float hMoveRight;

    int attackMode = 0; //1: Stab, 2: swing
	float attackSpeed = 1;

//    float groundDist;
//    float waitTime = 0.5f;
    bool ready = false;
    bool isRolling = false;
    

    public float speed = 15.0f;
    public float jumpForce = 5.0f;
    public float smoothDamp = 1.0f;

    Rigidbody _rigidBody;
	Collider _dagger;
//    Collider _collider;

    Vector3 groundPos;
    Vector3 playerPos;
    RaycastHit hit;

	// Mana Stuff;

	public Image _manaBarUI;
	public float currMana;
	float maxMana;
	bool _mana;
	

	// Use this for initialization
	void Start () {
		_dagger = GameObject.FindGameObjectWithTag("dagger").GetComponent<Collider>();
		_dagger.enabled = false;
		_dagger.GetComponent<TrailRenderer>().enabled = false;
        _rigidBody = gameObject.GetComponent<Rigidbody>();
     //   groundDist = gameObject.GetComponent<Collider>().bounds.center.y;
     //   _collider = gameObject.GetComponent<Collider>();
        _anim = gameObject.GetComponent<Animator>();
		maxMana = 50f;
		currMana = maxMana;

        
        
        
	
	}
    void Update()
    {
        CheckInput();
		CheckMana();
        //        Debug.Log(_rigidBody.velocity.magnitude);
        _anim.SetFloat("speed", _rigidBody.velocity.magnitude); // changes anim speed value to make it play move anim
        _anim.SetInteger("attack", attackMode); //1: stab, 2:swing
        _anim.SetBool("isRolling", isRolling);//change param to be the same as bool isRolling
		//Debug.Log(attackMode);
		_anim.speed = attackSpeed;
		
		if (attackSpeed > 3)
		{
			attackSpeed = 3;
		}
		if (attackSpeed < 1)
		{
			attackSpeed = 1;
		}
    }
	
	// Update is called once per frame
	void FixedUpdate () {
       

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
                    charStates = States.jump;
                }
                if (attack && !GameControl.spiritmode)
                {
                    charStates = States.stab;
					attackMode = 1;
                }
                if (roll!=0)
                {
                    charStates = States.roll;
                    isRolling = true;
                }
                CheckClimb();
                break;
            case States.move:
                RotatingLogic(hMove, vMove);
                MovementLogic(hMove,vMove);
				//RotatingLogic(GamepadManager.h1, GamepadManager.v1);
				//MovementLogic(GamepadManager.h1, GamepadManager.v1);
                attackMode = 0;
                if (vMove == 0 && hMove ==0)
                {
                    charStates = States.idle;
                }
                if (jump && isGrounded())
                {
                    charStates = States.jump;
                }
				if (roll != 0)
				{
					charStates = States.roll;
				}
                CheckClimb();
                break;
            case States.jump:
                _rigidBody.AddForce(Vector3.up*jumpForce,ForceMode.Impulse);
                if (!isGrounded())
                {
                    charStates = States.idle;
                }
                CheckClimb();
                break;
            case States.possess:
				_mana = true;
                //MovementLogic(hMoveRight, vMoveRight);
               // RotatingLogic(hMoveRight, vMoveRight);
				Debug.Log("possss");
				MovementLogic(GamepadManager.h2, GamepadManager.v2);
				RotatingLogic(GamepadManager.h2, GamepadManager.v2);
			
                if (GameControl.spiritmode == false)
                {
                    charStates = States.idle;
                    _mana = false;
                    Debug.Log("possass");
                }
				
                break;
            case States.roll:
               // _rigidBody.AddForce(transform.forward/1.5f,ForceMode.Impulse);
               // _rigidBody.AddRelativeForce(transform.forward / 0.9f, ForceMode.Impulse);
               // _rigidBody.AddForceAtPosition(transform.forward *5, transform.localPosition);
                //_rigidBody.velocity += transform.forward/3f;
                gameObject.transform.localPosition +=( transform.forward*5*Time.deltaTime);
                if (!isRolling)
                {
                    charStates = States.idle;
                }
                break;
            case States.stab:
				_dagger.GetComponent<TrailRenderer>().enabled = true;
				_dagger.enabled = true;
				attackSpeed+=Time.deltaTime;
             //   attackMode = 1;
                if(attack && attackMode == 1 && !ready)
                {                  
                    charStates = States.swing;
                 //   ready = false;
					attackMode = 2;
                }
                else if (ready)
                {
                    charStates = States.idle;
					_dagger.GetComponent<TrailRenderer>().enabled = false;
					_dagger.enabled = false;
                }
                break;
            case States.swing:
				_dagger.GetComponent<TrailRenderer>().enabled = true;
				_dagger.enabled = true;
				attackSpeed+=Time.deltaTime;
               // attackMode = 2;
				if (attack && attackMode == 2 && !ready)
				{
					charStates = States.stab;
					//ready = false;
					attackMode = 1;
				}
                else if (ready)
                {
                    charStates = States.idle;
					_dagger.GetComponent<TrailRenderer>().enabled = false;
					_dagger.enabled = false;
                }          
                break;

            case States.climb:
                
                if(Physics.Raycast(transform.position, transform.forward, 1))
                {
                    ClimbLogic(GamepadManager.v1);
                }

                else
                {
                    _rigidBody.constraints = RigidbodyConstraints.FreezeRotation;
                    _rigidBody.AddForce(transform.forward * 2, ForceMode.Impulse);
                    charStates = States.idle;
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
 				roll =0;
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
			
			attack = GamepadManager.buttonX;
			possess = GamepadManager.buttonY;
			if (!bTutorial)
			{
				jump = GamepadManager.buttonA;
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
		attackMode = 0;
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
			//_mana = true;
		}
		 if (currMana > maxMana && _mana == true) // check if mana is maxed
		{
			currMana = maxMana; //caps it back
			_mana = false;
		}
		
		else if (currMana < maxMana && _mana == false)
		{
			currMana += Time.deltaTime/5;	
		}
	}

    void CheckClimb()
    {
        if(Physics.Raycast(transform.position, transform.forward, out hit, 1))
        {
            if (GamepadManager.buttonBDown && hit.collider.tag == "Climable")
            {
                charStates = States.climb;
                Quaternion targetRot = Quaternion.LookRotation(-hit.normal, Vector3.up);
                _rigidBody.rotation = targetRot;
                transform.position = new Vector3(hit.transform.position.x - 0.8f, transform.position.y, transform.position.z);
                _rigidBody.constraints = RigidbodyConstraints.FreezeRotation & RigidbodyConstraints.FreezePositionZ & RigidbodyConstraints.FreezePositionX;
                //charStates = States.climb;
            }
        }
    }

    void ClimbLogic(float v)
    {
        Vector3 ClimbForce = Vector3.up * v;
        ClimbForce.Normalize();
        _rigidBody.AddForce(ClimbForce * 0.3f, ForceMode.VelocityChange);
    }
}

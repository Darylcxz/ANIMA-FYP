using UnityEngine;
using System.Collections;

[RequireComponent (typeof( NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
//[RequireComponent(typeof(CharacterController))]


public abstract class AIbase : MonoBehaviour {

	protected bool autoFire = false;
//	bool isPossessed = false;
    public bool canPosses = true;

    protected Transform player;
    protected NavMeshAgent agent;
    protected Rigidbody _rigidBody;
	public MovementController playerMana;
    private RaycastHit hit;

   public enum States
    {
        idle,
        walk,
        rotate,
        retreat,
        possessed,
        pursue,
        doNothing
    };

    //states related stuff
    public States AIState = States.walk;
    bool ready = false;
    protected float waitTime = 3.0f;
    protected Vector3 origin;
    protected float distance;
    public float retreatDist;

    //movement stuff
    float vMove;
    float hMove;
    protected float speed = 5f; //player movement speed;
    protected float AISpeed = 1f; // AI selfmove speed;

	//AI Stats and stuff
	protected float health;

	//Free wandering stuff
	public float circleRadius = 2.0f;
	Vector3 circleCenter;
	public float offset = 5.0f;
	float minDistance = 1.0f;
	float rotationSpeed = 5.0f;
	protected Vector3 currentTargetPosition;
	float waitTimer;


    
	 
	protected abstract void ActivateAbility();
	protected abstract void PassiveAbility();
   // protected abstract void AggroAbility();
	// Use this for initialization
    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		playerMana = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementController>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        origin = gameObject.transform.position;
		waitTimer = 0.0f;
		circleCenter = transform.position + new Vector3(0, 0, offset);
		currentTargetPosition = transform.position;
    }
	// Update is called once per frame
	protected void Update () {
        distance = Vector3.Distance(gameObject.transform.position, origin); //distance between you and origin
     
        Roam();
        PassiveAbility();
        CheckPossession();
        
        //if (!isPossessed) {
        //    Roam ();
        //}
        //else
        //{
        //    CheckInput ();
        //}
			
	}

	protected virtual void Roam()
	{
		//logic for AI roaming
        switch (AIState)
        {
            case States.idle:
                //plays AI specific Animations
                Invoke("WaitTimer", waitTime);
				_rigidBody.velocity = Vector3.zero;




                if (ready)
                {
                    AIState = States.walk;
                    ready = false;
                    CancelInvoke();
                }
                break;
            case States.walk:
                //walks forward for a set amount of time (random distance)
            //    Invoke("WaitTimer", waitTime*(Random.Range(0.5f,2f)));
             //   gameObject.transform.localPosition += (transform.forward/35)*AISpeed;
				MoveTowardsTarget();
                if(distance > retreatDist)
                {
                    AIState = States.retreat;
                    ready = false;
                    CancelInvoke();
                }
                else if (ready)
                {
                    AIState = States.idle;
                    ready = false;
                    CancelInvoke();
                }
                break;
            case States.rotate:
                //rotates for set amount of time (random angle)
                Invoke("WaitTimer", waitTime*(Random.Range(0.1f,1.0f)));
                gameObject.transform.eulerAngles += new Vector3(0, Mathf.Ceil(Random.Range(-2,1)), 0);
                if (ready)
                {
                    AIState = States.walk;
                    ready = false;
                    CancelInvoke();
                }
                break;
            case States.retreat:
                //runs back to start
				MoveTowardsTarget();
               // agent.SetDestination(origin);
				currentTargetPosition = origin;
                if (distance < 2 && canPosses)
                {
                  //  agent.ResetPath();
					//FindNewTargetPos();
                    AIState = States.idle;
                    ready = false;
                    CancelInvoke();
                }
                break;
            case States.possessed:
                //logic for when possessed. Most probably disabling the charactercontroller of AI
                //with this, the functions below may need to be placed in here.
				playerMana.currMana -= Time.deltaTime;
                //agent.ResetPath();
                ready = false;
                Camerafollow.targetUnit = gameObject;
                CheckInput();
                AIMove();
                if (GameControl.spiritmode == false)
                {
                    AIState = States.idle;
                    //agent.ResetPath();
                    ready = false;
                    Camerafollow.targetUnit = GameObject.Find("Character");
                    CancelInvoke();
                }
                break;
            case States.pursue:
                //faces player and walks toward player, reserved for enemy behaviour
                ActivateAbility(); //Enemy Behaviour
                break;

            case States.doNothing:
                //literally fucking does nothing
                //agent.ResetPath();
                ready = false;
                CancelInvoke();
                if(GameControl.freeze == false)
                {
                    AIState = States.idle;
                }
                break;


        }

	}

	void CheckInput()
	{
		bool PressedE;
		
		if(autoFire)
		{
			PressedE =  Input.GetKey(KeyCode.E);
            PressedE = GamepadManager.buttonB;
		}
		else
		{
			PressedE = Input.GetKeyDown(KeyCode.E);
            PressedE = GamepadManager.buttonBDown;
		}
		if(PressedE)
		{
			ActivateAbility();
		}



        //if(Input.GetMouseButtonDown(1))
        //{
        //    isPossessed = false;
        //}

	}
	protected void MoveTowardsTarget()
	{
		Vector3 direction = currentTargetPosition - transform.position;
		direction.y = 0;
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
		if (direction.magnitude > minDistance)
		{
			Vector3 moveVector = direction.normalized * AISpeed * Time.deltaTime;
			transform.position += moveVector;
		}
		else if(canPosses)
		{
			FindNewTargetPos();
			ready = true;
		}
	}
	void FindNewTargetPos()
	{
		circleCenter = transform.localPosition + Vector3.right * offset;
		currentTargetPosition = circleCenter + (OnUnitCircle() * circleRadius);
	}

	Vector3 OnUnitCircle()
	{
		float angleInRadians = Random.Range(0, 2 * Mathf.PI);
		float x = Mathf.Cos(angleInRadians);
		float z = Mathf.Sin(angleInRadians);
		return new Vector3(x, 0, z);
	}

    void WaitTimer()
    {
        ready = true;
    }

    protected virtual Vector3 OriginPos()
    {
        origin = gameObject.transform.position;
        return origin;
    }


    void OnMouseDown()
    {
        if (GameControl.spiritmode && canPosses)
        {
            //possess logic
            AIState = States.possessed;

        }
    }

    void CheckPossession()
    {
        if(GameControl.freeze && AIState != States.possessed)
        {
            AIState = States.doNothing;
            Debug.DrawRay(transform.position, Vector3.up * 2);
            if(Physics.Raycast(transform.position, Vector3.up, out hit, 2))
            {
                Debug.Log("hit!!!!!!");
                print(hit.collider.name);

                if (GamepadManager.buttonA && hit.collider.name == "arrow" || Input.GetKeyDown("i") && hit.collider.name == "arrow")
                {
                    AIState = States.possessed;
                    hit.collider.gameObject.SetActive(false);
                    GameControl.freeze = false;
                    print("hithithit");
                }
            }
        }
    }

    void AIMove()
    {
        //hMove = Input.GetAxis("Horizontal");
        //vMove = Input.GetAxis("Vertical");

        //Vector3 targetVelocity = new Vector3(hMove, 0, vMove);
        float h2 = GamepadManager.h1 * 2;
        float v2 = GamepadManager.v1 * 2;
		Vector3 targetVelocity = new Vector3(h2 + v2, 0, v2 - h2);
		targetVelocity.Normalize();
        targetVelocity *= speed;
        Vector3 velocity = _rigidBody.velocity;
        Vector3 vChange = targetVelocity - velocity;
        vChange = new Vector3(Mathf.Clamp(vChange.x, -speed, speed), 0, (Mathf.Clamp(vChange.z, -speed, speed)));
        transform.LookAt(transform.position + targetVelocity);
        _rigidBody.AddForce(vChange, ForceMode.VelocityChange);
    }

	void OnDrawGizmos()
	{
		Gizmos.color = Color.black;
		Gizmos.DrawWireSphere(circleCenter, circleRadius);
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(currentTargetPosition, new Vector3(1, 1, 1));
	}

    //void GoodMovement()
    //{
    //    Vector3 targetVelocity = new Vector3(h1, 0, v1);
    //    targetVelocity.Normalize();
    //    targetVelocity *= speed;
    //    Vector3 v = _rigidbody.velocity;
    //    Vector3 velocityChange = targetVelocity - v;
    //    velocityChange = new Vector3(Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange), 0, Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange));

    //    _rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);

    //}


}

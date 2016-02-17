using UnityEngine;
using System.Collections;

public abstract class NewAIBA : MonoBehaviour {


	protected Transform player;		//to get the player's transform
	protected Rigidbody _rbAI;		//AI's rigidbody

	//State Machine for the AI
	[SerializeField]protected enum StateMachine
	{
		IDLE,
		WALK,
		POSSESSED,
		WAIT,
		RETREAT,
		PURSUE,
        GOTOPOINT
	};
	
	//GameObject Debugger;
	[SerializeField]protected bool canPossess;		//checks if the AI can be possessed 

	RaycastHit possessionRaycastHit;		//Raycast hit info for possession checker
	[SerializeField]protected StateMachine AIState = StateMachine.IDLE;  //sets the default state to IDLE
	float stateTimer;			//State timer that runs with the game time
	float waitTime = 1f;				//Time the state waits before it changes
	float distance;				//distance between AI and things
	float retreatDistance = 5f;		//distance BEFORE AI retreats
	protected Transform spawnPoint;		//place where AI spawned


	//AI variables//
	float healthAI;				//AI Health
	float vMoveAI;				//Horizontal Axis for AI
	float hMoveAI;				//Vertical Axis for AI
	[SerializeField]protected float speedAI = 8f;				//Speed for AI
	[SerializeField]protected float speedPlayer;			//Speed for when player possesses

	//AI Components
	[SerializeField]ParticleSystem possessFire;		//particle for when you possess I think
    GameObject possessExplode;
    Animator possanim;

	//AI Pathfinding variables
	sbyte rotationSpeed = 5;	//how fast the AI should rotate
	short aggroDistance = 10;	//distance before enemy aggro triggers
	[SerializeField]protected Vector3 areaCenter;			//center of the area of roaming
	[SerializeField]protected Vector3 rectSize;			//defines rect size
	[SerializeField]protected float rectMagnitude = 2;		//controls Size of rect
	[SerializeField]protected Vector3 _waypoint;			//waypoint created where the AI paths to that
	[SerializeField]float minDistance = 0.1f;		//min distance to the WAYPOINT before AI considers it reached it


    public Vector3 customWayPoint;
	//Player's Variables (for player when possessing)
	[SerializeField]MovementController playerMana;

    public bool isPossessed; //helps tutorial mode tell if pig is possessed

	
	
	//functions to declare
	protected abstract void ActivateAbility();
	protected abstract void PassiveAbility();



	// Use this for initialization
	protected virtual void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		_rbAI = GetComponent<Rigidbody>();
		_waypoint = areaCenter + (OnUnitRect(rectSize.x,rectSize.z)) * rectMagnitude;
		playerMana = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementController>();
	//	spawnPoint = transform;
        possessExplode = GameObject.Find("SpiritBomb");
        possanim = possessExplode.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

		PassiveAbility();
		if (!canPossess) //If mob is NOT friendly, it can get aggroed to you
		{
			//Check for Aggro here, can be put into walk cycle to allow AI to only "see" you during patrols
			if (DistanceBetween(transform.position, player.position) < aggroDistance)
			{
				ChangeState(0, StateMachine.PURSUE);
			}
		}
		AIStateMachine();
		CheckPossession();
		
	}
	protected virtual void AIStateMachine()//State machine for the AI
	{
		stateTimer += Time.deltaTime;
		switch (AIState)
		{
			case StateMachine.IDLE:
				//Dizzy animation or whatever
				ChangeState(10f, StateMachine.WALK);
				break;
			case StateMachine.WALK:
				//Walk or patrolling behaviur. 
				AIMove(transform.position, _waypoint, speedAI,true);
				//ChangeState(0, StateMachine.IDLE);
				break;
			case StateMachine.PURSUE:
				break;
			case StateMachine.POSSESSED:
                isPossessed = true;
				playerMana.currMana -= Time.deltaTime;
				CheckInput();
				PlayerTakesControl();
				if (!GameControl.spiritmode)
				{
					ChangeState(0f, StateMachine.IDLE);
					possessFire.Play();
                    isPossessed = false;
					Camerafollow.targetUnit = GameObject.FindGameObjectWithTag("Player");
				}
				break;
			case StateMachine.WAIT:
			//	Debug.Log(GameControl.freeze);
				if(!GameControl.freeze)
				{
					ChangeState(1f, StateMachine.IDLE);
				}
				break;
			case StateMachine.RETREAT:
				AIMove(transform.position, spawnPoint.position, speedAI * 2);
				if (DistanceBetween(transform.position, spawnPoint.position) < retreatDistance)
				{
					ChangeState(0f, StateMachine.IDLE);
				}
				break;
            case StateMachine.GOTOPOINT:
                if(customWayPoint!= null)
                {
                    AIMove(transform.position, customWayPoint, speedAI);
                }
                break;
		}
	}
	void ChangeState(float timeBeforeStateChange, StateMachine nextState)//small state changer, takes in time you want to wait for before state change, and the next state
	{
		if (stateTimer > timeBeforeStateChange)
		{
			stateTimer = 0;
			AIState = nextState;
		}
	}
	void AIMove(Vector3 A, Vector3 B, float _speed,bool isWandering = false)//Ai move script, uses vector stuff
	{
		Vector3 direction = (B - A).normalized; //gives you normalised direction to thing you want to go to
		direction.y = 0;
		Quaternion endRotation = Quaternion.LookRotation(direction);
		if (isWandering)
		{
			
			if (DistanceBetween(A,B) < minDistance)
			{
				FindNewTargetPosition();
				//ChangeState(0, StateMachine.IDLE);
			}
		}
		transform.rotation = Quaternion.Slerp(transform.rotation, endRotation, Time.deltaTime * rotationSpeed);
		transform.localPosition += transform.forward * Time.deltaTime * _speed;
	}
	void CheckInput()//basically just checks for whatever input
	{
		bool PressedAttackButton;
		PressedAttackButton = GamepadManager.buttonBDown;
		if(PressedAttackButton)
		{
			ActivateAbility();
		}
	}
	void FindNewTargetPosition() //Finds a new waypoint within the boundary
	{
		//This bit here takes the center of the rect, uses the rand vector function and 
		//finds a point in that said rect. The magnitude scales the rect up
		_waypoint = areaCenter + (OnUnitRect(rectSize.x, rectSize.z) * rectMagnitude);
	}
	Vector3 OnUnitRect(float x, float z) //Takes in 2 floats, randomises them from min to max
	{

		float newX = Random.Range(0, x);
		float newZ = Random.Range(0, z);

		return new Vector3(newX, 0, newZ); //gives it back for you to use
	}
	float DistanceBetween(Vector3 A, Vector3 B)//Utility function to calculate distance between things
	{
		return Vector3.Distance(A, B);
	}
	void CheckPossession()//Checks and decides what to do during possession
	{
		if (GameControl.freeze && AIState !=StateMachine.POSSESSED)
		{
			ChangeState(0f, StateMachine.WAIT);
			Debug.DrawRay(transform.position, Vector3.up * 2);
			if (Physics.Raycast(transform.position, Vector3.up, out possessionRaycastHit, 2))
			{
				if (GamepadManager.buttonA && possessionRaycastHit.collider.name == "TargetSerik" || Input.GetKeyDown("i") && possessionRaycastHit.collider.name == "TargetSerik")
				{
					Camerafollow.targetUnit = gameObject;
					AIState = StateMachine.POSSESSED;
					stateTimer = 0;
					possessionRaycastHit.collider.gameObject.transform.position = transform.position;
					possessionRaycastHit.collider.gameObject.transform.SetParent(gameObject.transform);

					possessFire.Stop();
                    possessExplode.transform.position = transform.position;
                    possanim.SetTrigger("Explode");
					GameControl.freeze = false;
					
				}
			}
		}
	}
    public void GoToPoint(Vector3 newWaypoint, bool isActive)
    {
        if(isActive)
        {
            customWayPoint = newWaypoint;
            AIState = StateMachine.GOTOPOINT;
        }
        if(!isActive)
        {
            customWayPoint = _waypoint;
            AIState = StateMachine.RETREAT;
        }
    }
	
	void PlayerTakesControl()//function for when the player is controlling the AI
	{
 		float h2 = GamepadManager.h1*2;
		float v2 = GamepadManager.v1*2;
		Vector3 targetVelocity = new Vector3(h2 + v2, 0, v2 - h2);
		targetVelocity.Normalize();
		targetVelocity *= speedPlayer;
		Vector3 velocity = _rbAI.velocity;
		Vector3 vChange = targetVelocity - velocity;
		vChange = new Vector3(Mathf.Clamp(vChange.x, -speedPlayer, speedPlayer), 0, (Mathf.Clamp(vChange.z, -speedPlayer, speedPlayer)));
		transform.LookAt(transform.position + targetVelocity);
		_rbAI.AddForce(vChange, ForceMode.VelocityChange);
	}
	void OnDrawGizmos() //Draws the cube and stuff just to show you the bounding boxes of its roaming
	{
		Gizmos.color = Color.black;
		float offsetX = rectSize.x / 2;
		float offsetZ = rectSize.z / 2;
		Vector3 cubePos = new Vector3(offsetX, 0, offsetZ);
		Gizmos.DrawWireCube(areaCenter + (cubePos * rectMagnitude), rectSize * rectMagnitude);
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(_waypoint, new Vector3(1, 1, 1));
	}
}

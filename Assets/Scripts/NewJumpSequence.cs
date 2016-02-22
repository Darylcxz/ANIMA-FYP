using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NewJumpSequence : MonoBehaviour {

    Transform player;
    MovementController moveScript;
    public bool freezeMovement;
    [SerializeField] List <Transform> WaypointList = new List<Transform>();
    [SerializeField] int currWayPointIndex;
    [SerializeField] GameObject SplineRoot;
    [SerializeField] SplineController splineScript;
    int lastWayPoint;
    int currentWayPoint = 0;
    bool hasTriggered;

    enum JumpStates
    {
        NOTSTARTED,
        STARTING,
        JUMPING,
        ENDING
    }
    JumpStates States = JumpStates.NOTSTARTED;
    float dotProduct;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        moveScript = player.gameObject.GetComponent<MovementController>();
        splineScript = splineScript.GetComponent<SplineController>();
	    if(WaypointList.Count > 2) //just to prevent the thing from giving null ref error if the list is too small
        {
            lastWayPoint = WaypointList.Count - 1;
        }
	}
	
	// Update is called once per frame
	void Update () {
        JumpLogic();
	}
    void JumpLogic()
    {
        switch(States)
        {
            case JumpStates.NOTSTARTED:
                if (DistBetween(player.position, WaypointList[0].position) < 2) //check distance between player and waypoint
                {
                    Debug.Log("Near");
                    if (CheckWaypoint(currentWayPoint)) //if facing?
                    {
                        Debug.Log("facing and near");
                        if (GamepadManager.buttonADown) //if player jumps?
                        {
                            Debug.Log("Jumped");
                            SplineJump(0); //moves the spline root to the correct positons
                            splineScript.FollowSpline(); //makes the spline move
                            currentWayPoint++; //sets to ext waypoint
                            freezeMovement = true; //then you freeze
                            moveScript.charStates = MovementController.States.sequencedjump; // and set its state
                            States = JumpStates.STARTING;
                        }
                    }
                }
                break;
            case JumpStates.STARTING:
                if(DistBetween(player.position,WaypointList[currentWayPoint].position) <5)
                {
                    if(CheckWaypoint(currentWayPoint))
                    {
                        if(GamepadManager.buttonADown)
                        {
                            SplineJump(currentWayPoint);
                            splineScript.FollowSpline();
                            currentWayPoint++;
                        }
                    }
                }
                if((currentWayPoint+2) == WaypointList.Count)
                {
                    States = JumpStates.ENDING;
                }
                break;
            case JumpStates.JUMPING:
                break;
            case JumpStates.ENDING:
                if (DistBetween(player.position, WaypointList[currentWayPoint].position) < 5)
                {
                    if (CheckWaypoint(currentWayPoint))
                    {
                        if (GamepadManager.buttonADown)
                        {
                            SplineJump(currentWayPoint);
                            splineScript.FollowSpline();
                            freezeMovement = false; //then you unfreeze
                            currentWayPoint = 0;
                            States = JumpStates.NOTSTARTED;
                           // moveScript.charStates = MovementController.States.sequencedjump; // and set its state

                            //   currentWayPoint++;
                        }
                    }
                }
                break;
        }
    }
    void SplineJump(int currentWP)
    {
        Vector3 _dir = WaypointList[currentWayPoint + 1].position - WaypointList[currentWayPoint].position;
        _dir.y = 0;
        Quaternion endRot = Quaternion.LookRotation(_dir);
        SplineRoot.transform.rotation = endRot; //rotates the spline thing to face the next waypoint
        SplineRoot.transform.position = WaypointList[currentWayPoint].position; //moves the spline thing to the current waypoint
    }

    bool CheckWaypoint(int _waypoint)
    {
        dotProduct = Vector3.Dot(player.gameObject.transform.forward, (WaypointList[_waypoint+1].position - player.transform.position).normalized);
        if (dotProduct > 0.7f) //if facing, return true
        {
            return true;
        }
        else return false;
    }

    float DistBetween(Vector3 A, Vector3 B)
    {
        return Vector3.Distance(A, B);
    }
}

using UnityEngine;
using System.Collections;

public class WanderScripte : MonoBehaviour {

  //  Transform player;
    NavMeshAgent Agent;
    public enum States
    { 
        idle,
        walk,
        rotate,
        pursue,
        retreat,
        possessed,

    };

    public States AIStates = States.idle;
    bool ready = false;
    float waitTime = 3.0f;
    Vector3 origin;
    float distance;

	// Use this for initialization
	void Start () {
      //  player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Agent = GetComponent<NavMeshAgent>();
        origin = gameObject.transform.position;
 
   

	}
	
	// Update is called once per frame
	void Update () {
        distance = Vector3.Distance(gameObject.transform.position, origin); //distance between AI and it's original starting point.
        print(distance);
        FSM();	
	}

    void FSM()
    {
        switch (AIStates)
        {
            case States.idle:
                Invoke("WaitTimer", waitTime);
                //idle logic here



                Debug.Log("penis");
                
                
                if (ready)
                {
                    AIStates = States.walk;
                    ready = false;
                    CancelInvoke();
                }
                break;

            case States.walk:
                Invoke("WaitTimer", waitTime*(Random.Range(0.5f,2f)));
                gameObject.transform.localPosition += transform.forward / 5;
                if (distance > 50)
                {   
                    AIStates = States.retreat;
                    ready = false;
                    CancelInvoke();                    
                }
                else if (ready)
                {
                    AIStates = States.rotate;
                    ready = false;
                    CancelInvoke();
                }
                break;

            case States.rotate:
                Invoke("WaitTimer", waitTime*(Random.Range(0.1f,1.0f)));
                gameObject.transform.eulerAngles += new Vector3(0,Mathf.Ceil(Random.Range(-2,1)),0);
                if (ready)
                {
                    AIStates = States.walk;
                    ready = false;
                    CancelInvoke();
                }
                break;
            case States.retreat:
                Agent.SetDestination(origin);
                if (distance < 10)
                {
                    Agent.ResetPath();
                    AIStates = States.idle;
                    ready = false;
                    CancelInvoke();
                }
                //if (NavMeshPathStatus.PathComplete)
                //{
 
                //}
                break;

        }
    }

    void WaitTimer()
    {
        ready = true;
    }

    float RandomAngle()
    {
        float num = Random.Range(-3, 3);
        return num;
    }

}

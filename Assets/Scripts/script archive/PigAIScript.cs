using UnityEngine;
using System.Collections;

public class PigAIScript : MonoBehaviour {

    Transform player;
    NavMeshAgent pig;

   public enum PigStates { 
        idle,
        roam,
        possesed,
        rotate,
        pursue,
        retreat
    };
   
   public PigStates PigLogic = PigStates.idle;
    bool pigReady = false;
    float idleTime = 3.0f;
    Vector3 originPos;
    float distance;
        
    
	// Use this for initialization
	void Start () {
        
        pig = gameObject.GetComponent<NavMeshAgent>();
        originPos = gameObject.GetComponent<Transform>().position;
	
	}
	
	// Update is called once per frame
	void Update () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Vector3 endPos = player.transform.position;
        distance = Vector3.Distance(gameObject.transform.position, originPos);
        Debug.Log(originPos);
      //  pig.SetDestination(endPos);
        switch (PigLogic)
        {
            case PigStates.idle: // idles and waits for time before next move SNEEZE
             
                Invoke("SetIdleOff", idleTime);
                if (pigReady)
                {
                   // pigReady = false;
                    PigLogic = PigStates.roam;
                   pigReady = false;
                }
                break;
            case PigStates.possesed: //"Empty" state where it should do nothing when it's possessed.
                if (charactermovement.isBeingControlled == false)
                {
                    PigLogic = PigStates.retreat;
                }                
                break;
            case PigStates.retreat: //Runs back to it's original starting point before it was possessed.
              
                pig.SetDestination(originPos);
               // Invoke("SetIdleOff", idleTime);
                
                if (pigReady)
                {
                    pig.Stop();
                    PigLogic = PigStates.roam;
                    pigReady = false;
                    CancelInvoke();
                }            

                break;
            case PigStates.roam: //Roams random direction
                //wallk forward;
                //pigReady = false;
                gameObject.transform.localPosition +=(transform.forward/3);
                Invoke("SetIdleOff", idleTime);
          
                if (pigReady)
                {
                    PigLogic = PigStates.rotate;
                    pigReady = false;
                    CancelInvoke();
          
                }
                break;
            case PigStates.rotate:
                //rotate character
               // pigReady = false;
               // float random = Random.Range(-3.0f, 3.0f);
               // Vector3 rotate = (0,random,0);
                gameObject.transform.eulerAngles += new Vector3(0, 3, 0);
                Invoke("SetIdleOff", idleTime/5);
                if (pigReady)
                {
                    PigLogic = PigStates.roam;
                    pigReady = false;
                    CancelInvoke();
                }
                break;
            case PigStates.pursue:
                pigReady = false;
                pig.SetDestination(endPos);
                if (pigReady)
                {
                    
                    PigLogic = PigStates.rotate;
                    pig.Stop();
                }
                break;
        }
        Debug.Log(pig.pathPending);

        if (distance > 50)
        {
            PigLogic = PigStates.retreat;
        }
        //if (charactermovement.isBeingControlled)
        //{
        //    PigLogic = PigStates.possesed;
        //}


	
	}

    void SetIdleOff()
    {
        pigReady = true;
       // CancelInvoke();
     
    }
}

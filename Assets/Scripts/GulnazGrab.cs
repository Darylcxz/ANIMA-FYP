using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GulnazGrab : MonoBehaviour {
    //private bool touching = false;
    public static bool holding = false;

	[SerializeField]
	AudioSource gateSound;

	bool bLog;
    private Vector3 center;
    private Vector3 side1;
    private Vector3 side2;
    private RaycastHit hit;
    private GameObject currobject;
    private MovementController goonas;
	bool spawnUI;
	bool spawnUI1;
    bool hasSticked;
    [SerializeField] Image stickOfLies;

	// Use this for initialization
	void Start () {
        //gateSound = GetComponent<AudioSource>();
        goonas = gameObject.GetComponentInParent<MovementController>();
	}
	// Update is called once per frame
	void Update () {
        center = transform.position + new Vector3(0, 0.5f, 0);
        side1 = center + new Vector3(0.2f, 0, 0);
        side2 = center + new Vector3(-0.2f, 0, 0);
		Debug.DrawRay(center, transform.forward );
		Debug.DrawRay(side1, transform.forward );
		Debug.DrawRay(side2, transform.forward );
		if (Physics.Raycast(center, transform.forward, out hit, 2f) || Physics.Raycast(side1, transform.forward, out hit, 2f) || Physics.Raycast(side2, transform.forward, out hit, 2f))
		{
			if (hit.collider.tag == "lever" && spawnUI == false)
			{
				hit.collider.transform.GetChild(0).GetComponent<Canvas>().enabled = true;
				spawnUI = true;
			}
			//if(hit.collider.tag=="movable" && spawnUI == false)
			//{
			//	hit.collider.transform.GetChild(0).GetComponent<Canvas>().enabled = true;
			//}
			if (hit.collider.tag == "log" && spawnUI1 == false)
			{
				hit.collider.transform.GetChild(0).GetComponent<Canvas>().enabled = true;
				spawnUI1 = true;
			}
		}

        if (Physics.Raycast(center, transform.forward, out hit, 0.5f) && Input.GetButtonDown("Action") || Physics.Raycast(side1, transform.forward, out hit, 0.5f) && Input.GetButtonDown("Action") || Physics.Raycast(side2, transform.forward, out hit, 0.5f) && Input.GetButtonDown("Action"))
        {
//			Debug.Log(hit.collider.tag);
			if(!holding && hit.collider.tag == "movable")
            {
                hit.collider.transform.SetParent(transform);
                currobject = hit.collider.gameObject;
                holding = true;
                goonas.PushorPull();
                
            }
            else if(holding)
            {
                currobject.transform.parent = null;
                holding = false;
                goonas.StopPushPull();
            }
			//if (hit.collider.tag == "lever")
			//{
                
 		//		hit.collider.gameObject.GetComponent<Animator>().SetBool("bLever",true);
			//	GameObject.Find("gate").GetComponent<Animator>().SetBool("bLever", true);
			//	gateSound.Play();
			//	hit.collider.transform.GetChild(0).GetComponent<Canvas>().enabled = false;
   //             Debug.Log("playsound");
			//}
			if (hit.collider.tag == "log")
			{
				hit.collider.gameObject.GetComponent<Animator>().SetBool("bLog", true);
				GameObject.FindGameObjectWithTag("log").GetComponent<Animator>().SetBool("bLog", true);
				hit.collider.transform.GetChild(0).GetComponent<Canvas>().enabled = false;
			}
			if (hit.collider.tag == "pile")
			{
				GameObject.FindGameObjectWithTag("stick").GetComponent<MeshRenderer>().enabled = true;
                hasSticked = true;
                stickOfLies.enabled = true;
				Debug.Log("STICKKK");
			}
			if (hit.collider.tag == "Torch")
			{
               if(hasSticked)
                {
                    GameObject.FindGameObjectWithTag("stick").gameObject.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
                }
            }
        }
		

	}

    
}

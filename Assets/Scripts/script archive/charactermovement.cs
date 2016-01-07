using UnityEngine;
using System.Collections;

public class charactermovement : MonoBehaviour {

	private CharacterController rb;
	public float movespeed;
	public static bool isBeingControlled = false;
	public float jumpspeed;
	public float gravity;
	Vector3 fallingDown;
	Vector3 NW = new Vector3(0,0,1);
	Vector3 NE = new Vector3(1,0,0);
	Vector3 SW = new Vector3(-1,0,0);
	Vector3 SE = new Vector3(0,0,-1);
	Vector3 N = new Vector3 (1, 0, 1);
	Vector3 S = new Vector3 (-1, 0, -1);
	Vector3 E = new Vector3 (1, 0, -1);
	Vector3 W = new Vector3 (-1, 0, 1);
	Vector3 control = new Vector3 (0, 15, 0);
	private float temp = 45;
	private RaycastHit hit;
	public Transform attachpos;
	private bool isAttached;
	private GameObject moveobject;
	private Animator myAnim;
	
	// Use this for initialization
	void Start () {

		rb = GetComponent<CharacterController>() as CharacterController;
		myAnim = GetComponent<Animator> ();
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if(isBeingControlled == true)

		{
			fallingDown = new Vector3(0,fallingDown.y,0);

			if (Input.GetKey ("a") && Input.GetKey("w")) {
				rb.transform.forward = NW;
				rb.Move(rb.transform.forward * movespeed * Time.deltaTime);
				myAnim.SetInteger("movespeed",1);
			}
			
			else if (Input.GetKey ("w") && Input.GetKey("d")) {
				rb.transform.forward = NE;
				rb.Move(rb.transform.forward * movespeed * Time.deltaTime);
				myAnim.SetInteger("movespeed",1);
			}
			
			else if (Input.GetKey ("d") && Input.GetKey("s")) {
				rb.transform.forward = SE;
				rb.Move(rb.transform.forward * movespeed * Time.deltaTime);
				myAnim.SetInteger("movespeed",1);
			}
			
			else if (Input.GetKey ("s") && Input.GetKey("a")) {
				rb.transform.forward = SW;
				rb.Move(rb.transform.forward * movespeed * Time.deltaTime);
				myAnim.SetInteger("movespeed",1);
			}
			else if (Input.GetKey ("d")) {
				rb.transform.forward = E;
				rb.Move(transform.forward * movespeed * Time.deltaTime);
				myAnim.SetInteger("movespeed",1);
			}
			
			else if (Input.GetKey ("w")) {
				rb.transform.forward = N;
				rb.Move(transform.forward * movespeed * Time.deltaTime);
				myAnim.SetInteger("movespeed",1);
			}
			
			else if (Input.GetKey ("s")) {
				rb.transform.forward = S;
				rb.Move(transform.forward * movespeed * Time.deltaTime);
				myAnim.SetInteger("movespeed",1);
			}
			
			else if (Input.GetKey ("a")) {
				rb.transform.forward = W;
				rb.Move(transform.forward * movespeed * Time.deltaTime);
				myAnim.SetInteger("movespeed",1);
			}

			else
				myAnim.SetInteger("movespeed",0);


			if (rb.isGrounded == false){

				fallingDown -= new Vector3(0,gravity * Time.deltaTime,0);
			}
			else if (rb.isGrounded)
			{
				if (Input.GetButtonDown ("Jump") && rb.isGrounded) {
					fallingDown.y = jumpspeed;
				}
				else
					fallingDown.y = 0;
			}

			rb.Move(fallingDown * Time.deltaTime);
		}
	

	}

	void Update() {

		Debug.DrawRay(transform.position + control,transform.forward*100);
		if (Physics.Raycast (transform.position + control, transform.forward, out hit,100)) {
			if (hit.collider.tag == "movable" && Input.GetKeyDown ("e") && isAttached == false) {
				isAttached = true;
				isBeingControlled = false;
				NewCharacterMovement.isControlling = true;
				moveobject = GameObject.Find("GooStatue");
			}

			else if(NewCharacterMovement.isControlling == true && Input.GetKeyDown("e"))
			{
				isAttached = false;
				print("detach");
				isBeingControlled = true;
				NewCharacterMovement.isControlling = false;
			}

			else if (hit.collider.tag == "Reflecting" && Input.GetKeyDown("e"))
			{
				temp += 90;
				if(temp > 360)
					temp = 45;
				hit.transform.eulerAngles = new Vector3(hit.transform.rotation.x,temp,hit.transform.rotation.y);
			}
		} 
		else if(isBeingControlled == true)
			isAttached = false;


		if (isAttached == true)
			moveobject.transform.position = attachpos.position;
	}


	
}

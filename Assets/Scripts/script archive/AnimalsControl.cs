using UnityEngine;
using System.Collections;

public class AnimalsControl : MonoBehaviour {

	private bool beingControlled = false;
	public float movespeed;
	public int mobnumber;
	public ParticleSystem kaboom;
	public ParticleSystem bigBoom;
	private CharacterController anml;
	public TrailRenderer zoomtrail;
	public TrailRenderer zoomtrail2;
	public Light sweeper;
	Vector3 jumpspeed = new Vector3 (0, 50, 0); 
	Vector3 NW = new Vector3(0,0,1);
	Vector3 NE = new Vector3(1,0,0);
	Vector3 SW = new Vector3(-1,0,0);
	Vector3 SE = new Vector3(0,0,-1);
	Vector3 N = new Vector3 (1, 0, 1);
	Vector3 S = new Vector3 (-1, 0, -1);
	Vector3 E = new Vector3 (1, 0, -1);
	Vector3 W = new Vector3 (-1, 0, 1);
	Vector3 gravity = new Vector3(0,-10,0);


	// Use this for initialization
	void Start () {

		anml = GetComponent<CharacterController>() as CharacterController;
	
	}

	void OnMouseDown()
	{
		if (GameControl.spiritmode == true) {

			print ("being possessed ooooh");
			Camerafollow.targetUnit = this.gameObject;
			beingControlled = true;
			charactermovement.isBeingControlled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (GameControl.spiritmode == false) {

			beingControlled = false;
		}

		if (beingControlled == true) {
			if (Input.GetKey ("a") && Input.GetKey ("w")) {
				anml.transform.forward = NW;
				anml.Move (anml.transform.forward * movespeed * Time.deltaTime);
			} else if (Input.GetKey ("w") && Input.GetKey ("d")) {
				anml.transform.forward = NE;
				anml.Move (anml.transform.forward * movespeed * Time.deltaTime);
			} else if (Input.GetKey ("d") && Input.GetKey ("s")) {
				anml.transform.forward = SE;
				anml.Move (anml.transform.forward * movespeed * Time.deltaTime);
			} else if (Input.GetKey ("s") && Input.GetKey ("a")) {
				anml.transform.forward = SW;
				anml.Move (anml.transform.forward * movespeed * Time.deltaTime);
			} else if (Input.GetKey ("d")) {
				anml.transform.forward = E;
				anml.Move (transform.forward * movespeed * Time.deltaTime);
			} else if (Input.GetKey ("w")) {
				anml.transform.forward = N;
				anml.Move (transform.forward * movespeed * Time.deltaTime);
			} else if (Input.GetKey ("s")) {
				anml.transform.forward = S;
				anml.Move (transform.forward * movespeed * Time.deltaTime);
			} else if (Input.GetKey ("a")) {
				anml.transform.forward = W;
				anml.Move (transform.forward * movespeed * Time.deltaTime);
			}
			
			if (Input.GetKey ("space") && anml.isGrounded) {
				anml.SimpleMove (jumpspeed * Time.deltaTime);
			}
			
			if (anml.isGrounded == false) {
				anml.SimpleMove (gravity * Time.deltaTime);
			}

			if(Input.GetButtonDown("Action") && mobnumber == 1)
			{
				kaboom.Play();
			}

			if(Input.GetButtonDown("Action") && mobnumber == 2)
			{
				zoomtrail.enabled = true;
				zoomtrail2.enabled = true;
				anml.Move(transform.forward * 40);
			}

			else if(Input.GetButtonUp("Action") && mobnumber == 2)
			{
				zoomtrail.enabled = false;
				zoomtrail2.enabled = false;
			}

		} 
	
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.name == "Light Statue" && mobnumber == 2) {
			sweeper.intensity = 0;
			lightStatue.lightworking = false;
		}
	}

}

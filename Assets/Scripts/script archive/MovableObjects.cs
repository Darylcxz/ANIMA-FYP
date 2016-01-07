using UnityEngine;
using System.Collections;

public class MovableObjects : MonoBehaviour {
	
	public GameObject statue;
	private GameObject followpos;
	private bool isAttached = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnTriggerStay (Collider other) {

		if (other.gameObject.name == "Character") {
			if(Input.GetKeyDown("e") && charactermovement.isBeingControlled == true)
			{
				followpos = GameObject.Find("Pullobj");
				isAttached = true;
				charactermovement.isBeingControlled = false;
				NewCharacterMovement.isControlling = true;
				print ("attach");
			}

			else if(Input.GetKeyDown("e") && charactermovement.isBeingControlled == false)
			{
				print("detach");
				charactermovement.isBeingControlled = true;
				NewCharacterMovement.isControlling = false;
				isAttached = false;
			}

		}

	
	}

	void Update() {

		if (isAttached)
			statue.transform.position = followpos.transform.position;
	}
}

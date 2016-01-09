using UnityEngine;
using System.Collections;

public class GolemTrigger : MonoBehaviour {
	public Animator golemjump;
	// Use this for initialization
	void Start () {


	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.name == "Character" && KeyItemSpin.holdingItem == true && Input.GetButtonDown ("Action")) {
			DropTheBridge.dropit = true;
		}

	}
}

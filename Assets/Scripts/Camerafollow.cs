using UnityEngine;
using System.Collections;

public class Camerafollow : MonoBehaviour {

	public static GameObject targetUnit;

	// Use this for initialization
	void Start () {

		targetUnit = GameObject.Find ("Character");
	
	}
	
	// Update is called once per frame
	void Update () {
		
			float moveX = targetUnit.transform.position.x - transform.position.x;
			float moveZ = targetUnit.transform.position.z - transform.position.z;
			float moveY = targetUnit.transform.position.y - transform.position.y;
			Vector3 currLocation = new Vector3 (transform.position.x + moveX / 6, transform.position.y + moveY / 6, transform.position.z + moveZ / 6);
			transform.position = currLocation;
	}
}

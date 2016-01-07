using UnityEngine;
using System.Collections;

public class cameratarget : MonoBehaviour {

	public static GameObject followTarget;

	// Use this for initialization
	void Start () {

		followTarget = GameObject.Find ("Character");
	
	}
	
	// Update is called once per frame
	void Update () {

		transform.position = followTarget.transform.position;
	
	}
}

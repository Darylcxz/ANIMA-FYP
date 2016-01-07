using UnityEngine;
using System.Collections;

public class CloudMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.localPosition += transform.right/35;
	}
}

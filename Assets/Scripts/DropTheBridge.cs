using UnityEngine;
using System.Collections;

public class DropTheBridge : MonoBehaviour {
	public static bool dropit = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (dropit == true) {
			transform.Rotate(Vector3.right * 40 * Time.deltaTime);
			if(transform.rotation.x >= 0)
				dropit = false;
		}
	
	}
}

using UnityEngine;
using System.Collections;

public class Staircase : MonoBehaviour {
	public int slopenum;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Lightsource.lightTriggered == true && slopenum == 1) {
			transform.Translate(Vector3.up * 20 * Time.deltaTime);
			if(transform.position.y >= 176)
				Lightsource.lightTriggered = false;
		}

		else if (Lightsource.lightTriggered2 == true && slopenum == 2)
		{
			transform.Translate (Vector3.up * 20 * Time.deltaTime);

			if(transform.position.y >= 171.8f)
				Lightsource.lightTriggered2 = false;
		}
	
	}
}
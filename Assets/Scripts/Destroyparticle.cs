using UnityEngine;
using System.Collections;

public class Destroyparticle : MonoBehaviour {

	// Use this for initialization
	void OnEnable () {

        Invoke("Destroythis", 3.0f);
	
	}
	
	// Update is called once per frame
	void Destroythis () {
        Destroy(gameObject);
	}
}

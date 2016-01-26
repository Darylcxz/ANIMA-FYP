using UnityEngine;
using System.Collections;

public class BridgeDropSound : MonoBehaviour {
    AudioSource bridgedrop;
	// Use this for initialization
	void Start () {

        bridgedrop = gameObject.GetComponent<AudioSource>();
	
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("floor"))
        {
            bridgedrop.Play();
        }
    }
}

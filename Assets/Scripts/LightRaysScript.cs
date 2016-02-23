using UnityEngine;
using System.Collections;

public class LightRaysScript : MonoBehaviour {
    LineRenderer thisray;
    LineRenderer nextray;

	// Use this for initialization
	void Start () {
        thisray = GetComponentInChildren<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}

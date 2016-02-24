using UnityEngine;
using System.Collections;

public class LightSourceScript : MonoBehaviour {
    LineRenderer thisray;
    RaycastHit hit;
    Vector3 endpoint;
	// Use this for initialization
	void Start () {
        thisray = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(Physics.Raycast(transform.position, transform.forward, out hit, 10))
        {
            endpoint = hit.point;
            if(hit.collider.CompareTag("Reflecting"))
            {
                // Get and set variables of the script of next surface this hits to determine the reflection direction
                LightRaysScript nextlight = hit.collider.gameObject.GetComponent<LightRaysScript>();
                nextlight.lighton = true;
                nextlight.reflectpoint = hit.point;
                nextlight.raydir = Vector3.Reflect(transform.forward, hit.normal);
            }
        }

        thisray.SetPosition(0, transform.position);
        thisray.SetPosition(1, endpoint);
	}
}

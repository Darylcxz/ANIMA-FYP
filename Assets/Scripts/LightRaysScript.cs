using UnityEngine;
using System.Collections;

public class LightRaysScript : MonoBehaviour {
    LineRenderer thisray;
    public bool lighton = true;
    public RaycastHit hit;
    public Vector3 reflectpoint; // start point of this lightray derived from previous ray
    public Vector3 endpoint; // endpoint of this lightray
    public Vector3 raydir;  // direction of this lightray derived from previous ray

	// Use this for initialization
	void Start () {
        thisray = GetComponentInChildren<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

        if(lighton)    // check if there is a light shining on this surface
        {
            if(Physics.Raycast(reflectpoint, raydir, out hit, 10))
            {
                endpoint = hit.point;
                if(hit.collider.CompareTag("Reflecting"))
                {
                    // Get and set variables of the script of next surface this hits to determine the reflection direction
                    LightRaysScript nextlight = hit.collider.gameObject.GetComponent<LightRaysScript>();
                    nextlight.lighton = true;
                    nextlight.reflectpoint = hit.point;
                    nextlight.raydir = Vector3.Reflect(raydir, hit.normal);
                }
            }

            else
            {
                endpoint = transform.position + raydir.normalized * 10;
            }

            thisray.SetPosition(0, reflectpoint);   //Render the ray
            thisray.SetPosition(1, endpoint);
        }
	    
	}
}

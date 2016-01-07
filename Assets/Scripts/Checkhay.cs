using UnityEngine;
using System.Collections;

public class Checkhay : MonoBehaviour {

    private short haynum;
    public static bool got2hay;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if(haynum == 2)
        {
            got2hay = true;
        }

        else if(haynum < 2)
        {
            got2hay = false;
        }
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "movable")
        {
            haynum += 1;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "movable")
        {
            haynum -= 1;
        }
    }
}

using UnityEngine;
using System.Collections;

public class SerikFollow : MonoBehaviour {
    private GameObject targetpoint;
	// Use this for initialization
	void Start () {

        targetpoint = GameObject.Find("TargetSerik");
	
	}
	
	// Update is called once per frame
	void Update () {

        float moveX = targetpoint.transform.position.x - transform.position.x;
        float moveZ = targetpoint.transform.position.z - transform.position.z;
        float moveY = targetpoint.transform.position.y - transform.position.y;
        Vector3 currLocation = new Vector3(transform.position.x + moveX / 10, transform.position.y + moveY / 10, transform.position.z + moveZ / 10);
        transform.position = currLocation;
	
	}
}

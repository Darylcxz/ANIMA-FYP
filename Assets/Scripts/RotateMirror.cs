using UnityEngine;
using System.Collections;

public class RotateMirror : MonoBehaviour {
    public GameObject mirror;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnTriggerStay () {

        if(Input.GetButton("Action"))
        {
            mirror.transform.Rotate(Vector3.up * 40 * Time.deltaTime);
        }
	
	}
}

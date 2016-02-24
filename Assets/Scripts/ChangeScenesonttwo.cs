using UnityEngine;
using System.Collections;

public class ChangeScenesonttwo : MonoBehaviour {
    public int deriguji;
    public GameObject archuraplace;
	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Character")
        {
            if(deriguji == 1)
            {
                Application.LoadLevel("Caves - 1");
            }

            else if(deriguji == 2)
            {
                other.gameObject.transform.position = archuraplace.transform.position;
            }
        }
    }
}

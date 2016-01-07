using UnityEngine;
using System.Collections;

public class DeshTutorial : MonoBehaviour {
	public bool deshDead;

	// Use this for initialization
	void Start () {
		deshDead = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter(Collision _col)
	{
		if (_col.collider.tag == "dagger")
		{
			deshDead = true;
			gameObject.transform.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
			gameObject.GetComponent<BoxCollider>().enabled = false;
		}
	}
}

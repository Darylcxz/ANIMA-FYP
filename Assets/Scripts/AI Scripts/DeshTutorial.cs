using UnityEngine;
using System.Collections;

public class DeshTutorial : MonoBehaviour {
	public bool deshDead;
	[SerializeField]
	AudioSource dieSound;
	// Use this for initialization
	void Start () {
		deshDead = false;
		dieSound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter(Collision _col)
	{
		if (_col.collider.tag == "dagger")
		{
			dieSound.Play();
			deshDead = true;
			gameObject.transform.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
			gameObject.GetComponent<BoxCollider>().enabled = false;
		}
	}
}

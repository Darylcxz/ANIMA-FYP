using UnityEngine;
using System.Collections;

public class FireGate : MonoBehaviour {

	bool fireTrigger;

	public GameObject LeftFire;
	public GameObject RightFire;
	public GameObject GateFire;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (LeftFire.GetComponent<ParticleSystem>().isPlaying && RightFire.GetComponent<ParticleSystem>().isPlaying)
		{
			GateFire.GetComponent<Animator>().SetBool("bFire", true);
			GameObject.FindGameObjectWithTag("stick").SetActive(false);
		}
	
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.collider.tag == "dagger")
		{
			gameObject.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
		}
	}
}

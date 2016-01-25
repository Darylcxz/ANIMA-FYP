using UnityEngine;
using System.Collections;

public class SwordHitSound : MonoBehaviour {

	AudioSource swordHitSound;

	// Use this for initialization
	void Start () {
		swordHitSound = GetComponent<AudioSource>();	
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.collider.gameObject!=null)
		{
			swordHitSound.Play();
		}
	}

}

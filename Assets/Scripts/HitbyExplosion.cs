using UnityEngine;
using System.Collections;

public class HitbyExplosion : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnParticleCollision(GameObject other)
    {
        print("burn!!!!");
        ParticleSystem fire = other.GetComponentInChildren<ParticleSystem>();
        fire.Play();
    }
}

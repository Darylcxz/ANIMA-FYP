using UnityEngine;
using System.Collections;

public class HitbyExplosion : MonoBehaviour {

	
    void OnParticleCollision(GameObject other)
    {
        print("burn!!!!");
		if (other.CompareTag("torch"))
		{
			ParticleSystem fire = other.GetComponentInChildren<ParticleSystem>();
			fire.Play();
		}
		if (other.CompareTag("Rubble"))
		{
			Debug.Log("boom");
		}
    }
}

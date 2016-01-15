using UnityEngine;
using System.Collections;

public class HitbyExplosion : MonoBehaviour {

	
    void OnParticleCollision(GameObject other)
    {
        print("burn!!!!");
		if (other.CompareTag("Torch"))
		{
			ParticleSystem fire = other.GetComponentInChildren<ParticleSystem>();
			fire.Play();
		}
		if (other.CompareTag("Rubble"))
		{
			Debug.Log("boom");
            Rigidbody[] pieces = other.gameObject.GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody piece in pieces)
            {
                piece.isKinematic = false;
                piece.AddExplosionForce(500, other.gameObject.transform.position, 5);
            }
            BoxCollider goocollider = other.GetComponent<BoxCollider>();
            goocollider.enabled = false;
            ParticleSystem bakahatsu = other.GetComponentInChildren<ParticleSystem>();
            bakahatsu.Play();
            GameObject goo = other.transform.FindChild("Goo").gameObject;
            Destroy(goo);

		}
    }
}

using UnityEngine;
using System.Collections;

public class KenaBomb : MonoBehaviour {
	private Rigidbody rb;
	public GameObject exppoint;
	public float power;
	public float radius;
	
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetButtonDown ("Action") && Gooexplode.onGoo == true) {
			rb.AddExplosionForce(power, exppoint.transform.position, radius, 3.0f,ForceMode.Impulse);

			Disappear();
		}
	}

	IEnumerator Disappear()
	{
		yield return new WaitForSeconds (3);
		Destroy (gameObject);
	}
}

using UnityEngine;
using System.Collections;

public class LightTorch : MonoBehaviour {

	public ParticleSystem flame;
	public Light fireGlow;
	private bool holdingTorch = true;
	private bool onFire = false;
	// Use this for initialization
	void OnTriggerEnter (Collider other) {
		if(onFire == false && other.gameObject.name == "Character")
			print ("Press E to ignite");

		else if(other.gameObject.name == "Character" && onFire == true)
			print ("Press E to extinguish");
	
	}
	
	// Update is called once per frame
	IEnumerator OnTriggerStay(Collider other) {

		if (Input.GetKeyDown ("e") && holdingTorch == true && onFire == false && other.gameObject.name == "Character") {
			print ("igniting");
			flame.Play ();
			fireGlow.intensity = 3;
			onFire = true;
		} else if (Input.GetKeyDown ("e") && onFire == true) {
			flame.Stop();
			yield return new WaitForSeconds(2.5f);
			fireGlow.intensity = 0;
			onFire = false;
		}
	
	}
}

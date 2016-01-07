using UnityEngine;
using System.Collections;

public class KeyItemSpin : MonoBehaviour {
	public static bool holdingItem = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		transform.Rotate (Vector3.up * 50 * Time.deltaTime);
	
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "Character") {
			holdingItem = true;
			Destroy (gameObject);
		}
	}


}

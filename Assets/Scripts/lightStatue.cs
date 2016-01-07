using UnityEngine;
using System.Collections;

public class lightStatue : MonoBehaviour {

	private RaycastHit hit;
	private Vector3 raypos;
	public static bool lightworking = true;
	// Use this for initialization
	void Start () {

		raypos = new Vector3(transform.position.x, 158.4f, transform.position.z);
	
	}
	
	// Update is called once per frame
	void Update () {

		transform.Rotate (Vector3.up * 70 * Time.deltaTime);
		Debug.DrawRay(raypos,transform.forward * 250);
		if (Physics.Raycast (raypos, transform.forward, out hit, 250)) {

			if(hit.collider.tag == "Destructible" && lightworking == true)
			{
				Destroy(hit.transform.gameObject);
			}
		}

	}
}

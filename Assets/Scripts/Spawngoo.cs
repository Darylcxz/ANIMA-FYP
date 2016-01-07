using UnityEngine;
using System.Collections;

public class Spawngoo : MonoBehaviour {
	public GameObject Goo;
	public static bool goospawn;
	// Use this for initialization
	void Start () {


		InvokeRepeating ("Flow", 5, 2);
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void Flow ()
	{
		Instantiate (Goo,  new Vector3 (transform.position.x, 158.6f, transform.position.z), Quaternion.identity);
	}
}

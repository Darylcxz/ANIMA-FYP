using UnityEngine;
using System.Collections;

public class VineBreak : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter(Collision col)
	{
		if (col.collider.tag == "dagger" && gameObject.transform.GetChild(0)!= null)
		{
			

			gameObject.transform.GetChild(0).GetComponent<Rigidbody>().isKinematic = false;
			gameObject.transform.GetChild(1).GetComponent<Rigidbody>().isKinematic = false;
			gameObject.transform.GetChild(2).GetComponent<Rigidbody>().isKinematic = false;
			gameObject.transform.GetChild(3).GetComponent<Rigidbody>().isKinematic = false;
			gameObject.transform.GetChild(4).GetComponent<Rigidbody>().isKinematic = false;
			gameObject.transform.GetChild(5).GetComponent<Rigidbody>().isKinematic = false;
			gameObject.transform.GetChild(6).GetComponent<Rigidbody>().isKinematic = false;
			gameObject.transform.GetChild(7).GetComponent<Rigidbody>().isKinematic = false;
			gameObject.transform.GetChild(8).GetComponent<Rigidbody>().isKinematic = false;
			gameObject.transform.GetChild(9).GetComponent<Rigidbody>().isKinematic = false;
			
			Destroy(gameObject.transform.GetChild(0).gameObject,3.5f);
			Destroy(gameObject.transform.GetChild(1).gameObject, 3.5f);
			Destroy(gameObject.transform.GetChild(2).gameObject, 3.5f);
			Destroy(gameObject.transform.GetChild(3).gameObject, 3.5f);
			Destroy(gameObject.transform.GetChild(4).gameObject, 3.5f);
			Destroy(gameObject.transform.GetChild(5).gameObject, 3.5f);
			Destroy(gameObject.transform.GetChild(6).gameObject, 3.5f);
			Destroy(gameObject.transform.GetChild(7).gameObject, 3.5f);
			Destroy(gameObject.transform.GetChild(8).gameObject, 3.5f);
			Destroy(gameObject.transform.GetChild(9).gameObject, 3.5f);

			gameObject.GetComponent<Collider>().enabled = false;

			if (gameObject.name.Contains("Wheel"))
			{
				GameObject.FindGameObjectWithTag("WaterWheel").GetComponent<Animator>().SetBool("bWheel", true);
				GameObject.FindGameObjectWithTag("Saw").GetComponent<Animator>().SetBool("bWheel", true);
			}
			//gameObject.transform.GetChild(0).GetComponent<Rigidbody>().isKinematic = true;
			//Destroy(gameObject,0.1f);

		}
	}
}

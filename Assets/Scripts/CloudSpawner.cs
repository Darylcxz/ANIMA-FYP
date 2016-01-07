using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CloudSpawner : MonoBehaviour {

	public List<GameObject> Clouds = new List<GameObject>();
	int cIndex;
	float zPos;
	

	// Use this for initialization
	void Start () {
		InvokeRepeating("Spawn", 1, 35);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void Spawn()
	{
		cIndex = Random.Range(0, Clouds.Count);
		zPos = Random.Range(-30f, 30f);
		Vector3 temp = new Vector3(transform.position.x, transform.position.y, transform.position.z + zPos);
		Instantiate(Clouds[cIndex], temp, transform.rotation);
	}
}

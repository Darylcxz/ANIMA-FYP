using UnityEngine;
using System.Collections;

public class CaveExit : MonoBehaviour {
	public int caveNum;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player")
		{
			PlayerPrefs.SetInt("caveExit", caveNum);
			Application.LoadLevel("1-2 Exterior");
		}
	}
}

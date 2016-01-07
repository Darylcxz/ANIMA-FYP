using UnityEngine;
using System.Collections;

public class NextLevelTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.P))
		{
			Application.LoadLevel(Application.loadedLevel + 1);
		}
	
	}
    void OnTriggerEnter(Collider _col)
    {
        if (_col.gameObject.tag == "Player")
        {
            Application.LoadLevel(Application.loadedLevel+1);
        }
    }
}

using UnityEngine;
using System.Collections;

public class NextLevelTrigger : MonoBehaviour {
    [SerializeField]
    string levelName;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.P))
		{
			Application.LoadLevel(levelName);
		}
	
	}
    void OnTriggerEnter(Collider _col)
    {
        if (_col.gameObject.tag == "Player")
        {
            Application.LoadLevel(levelName);
        }
    }
}

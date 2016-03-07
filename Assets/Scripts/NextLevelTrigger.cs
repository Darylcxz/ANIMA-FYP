using UnityEngine;
using System.Collections;

public class NextLevelTrigger : MonoBehaviour {
    [SerializeField]
    string levelName;
    GameObject player;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.P))
		{
			Application.LoadLevel(levelName);
		}
        if(Input.GetKeyDown(KeyCode.Z))
        {
            player.transform.position = transform.position;
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

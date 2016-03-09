using UnityEngine;
using System.Collections;

public class BossKillSwitch : MonoBehaviour {
    public GameObject bossBeetle;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	if(Input.GetKeyDown(KeyCode.O))
        {
            bossBeetle.GetComponent<BossBeetleAI>().KillSwitch();
        }
	}
}

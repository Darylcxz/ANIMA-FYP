using UnityEngine;
using System.Collections;

public class PowerSource : MonoBehaviour {

    CameraShake shakeMe;

	// Use this for initialization
	void Start () {
        shakeMe = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collider _col)
    {
        if (_col.gameObject.tag == "Player")
        {
           // Debug.Log("dewdw");
            gameObject.GetComponent<AudioSource>().Play();
            Destroy(gameObject, 1.0f);
            Invoke("ChangeScene", 0.5f);
            shakeMe.ShakeMeBaby(2.0f);
        }
    }
    void ChangeScene()
    {
        //Debug.Log("dwdweff");
        Application.LoadLevel(Application.loadedLevel+1);
    }
}

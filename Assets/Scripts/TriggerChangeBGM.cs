using UnityEngine;
using System.Collections;

public class TriggerChangeBGM : MonoBehaviour {
    AudioSource bgm;
    public AudioClip archura;
	// Use this for initialization
	void Start () {
        bgm = GameObject.Find("BGM").GetComponent<AudioSource>();
	
	}
	
	// Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Character")
        {
            bgm.Stop();
            bgm.PlayOneShot(archura);
        }
    }
}

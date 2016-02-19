using UnityEngine;
using System.Collections;

public class BeetleClapScript : MonoBehaviour {
    [SerializeField]AudioClip clap;
    AudioSource beetle;
	// Use this for initialization
	void Start () {
        beetle = gameObject.GetComponent<AudioSource>();
	}

    public void Clap()
    {
        beetle.PlayOneShot(clap);
        Debug.Log("Clap!");
    }
}

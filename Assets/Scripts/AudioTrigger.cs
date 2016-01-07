using UnityEngine;
using System.Collections;

public class AudioTrigger : MonoBehaviour {

	public AudioSource _audio;
	public bool isRepeatable;
	bool hasPlayed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider col)
	{
 		if(col.tag == "Player")
		{
			if (!hasPlayed)
			{
				_audio.Play();
				hasPlayed = true;
			}
			else if (isRepeatable)
			{
				_audio.Play();
			}
		}
	}
}

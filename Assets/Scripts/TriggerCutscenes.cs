using UnityEngine;
using System.Collections;

public class TriggerCutscenes : MonoBehaviour {
    MovieScript cutscenes;

	// Use this for initialization
	void Start () {
        cutscenes = GameObject.Find("MovieTexture").GetComponent<MovieScript>();
        Invoke("PlaythatVid", 0.5f);
	}
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            cutscenes.PlayCutscene2();
        }
    }
	// Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            cutscenes.PlayCutscene2();
        }
    }

    void PlaythatVid()
    {
        cutscenes.PlayCutscene1();
    }
}

using UnityEngine;
using System.Collections;

public class BarrierTransition : MonoBehaviour {

    public GameObject _hideThis;
    public float duration;
    CameraShake shakeMe;
	// Use this for initialization
	void Start () {
        shakeMe = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
        shakeMe.ShakeMeBaby(duration -0.5f);
       Invoke("HideMe", duration +0.5f);
       // StartCoroutine("ChangeScene");
	}
	
	// Update is called once per frame
	void Update () {
     
	
	}
    void HideMe()
    {

        _hideThis.SetActive(false);
        Application.LoadLevel(Application.loadedLevel + 1);
    }
   
}

using UnityEngine;
using System.Collections;

public class SpiritBombScript : MonoBehaviour {
    MeshRenderer p1;
    MeshRenderer p2;
    MeshRenderer p3;
    MeshRenderer p4;
    MeshRenderer p5;
    float timer;
    bool playing = false;
    // Use this for initialization
    void Start () {

        p1 = transform.GetChild(0).GetComponent<MeshRenderer>();
        p2 = transform.GetChild(1).GetComponent<MeshRenderer>();
        p3 = transform.GetChild(2).GetComponent<MeshRenderer>();
        p4 = transform.GetChild(3).GetComponent<MeshRenderer>();
        p5 = transform.GetChild(4).GetComponent<MeshRenderer>();


    }
	
	// Update is called once per frame
	void Update () {
	    if(playing)
        {
            timer += Time.deltaTime;
            float lerp = timer / 0.8f;
            float alpha = Mathf.Lerp(1.0f, 0.0f, lerp);
            Color tc1 = p1.material.GetColor("_TintColor");
            tc1.a = alpha;
            p1.material.SetColor("_TintColor", tc1);

            Color tc2 = p2.material.GetColor("_TintColor");
            tc2.a = alpha;
            p2.material.SetColor("_TintColor", tc2);

            Color tc3 = p3.material.GetColor("_TintColor");
            tc3.a = alpha;
            p3.material.SetColor("_TintColor", tc3);

            Color tc4 = p4.material.GetColor("_TintColor");
            tc4.a = alpha;
            p4.material.SetColor("_TintColor", tc4);

            Color tc5 = p5.material.GetColor("_TintColor");
            tc5.a = alpha;
            p5.material.SetColor("_TintColor", tc5);
        }
	}

    public void AnimStart()
    {
        playing = true;
        timer = 0;
    }

    public void AnimEnd()
    {
        playing = false;
    }
}

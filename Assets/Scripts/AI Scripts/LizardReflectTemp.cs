using UnityEngine;
using System.Collections;

public class LizardReflectTemp : MonoBehaviour {
    private LineRenderer lightray;
    public LineRenderer lightray2;
    private RaycastHit hit1;
    private RaycastHit hit2;
    public int rayint;
    public static Ray[] rayz = new Ray[3];
    private GameObject originpt;
	// Use this for initialization
	void Start () {
        lightray = GetComponent<LineRenderer>();
        originpt = GameObject.Find("lightsource2");
        rayz[0] = new Ray(originpt.transform.position, originpt.transform.forward);
	}
	
	// Update is called once per frame
	void Update () {

        if(rayint == 0)
        {
            if (Physics.Raycast(rayz[0], out hit1, 200))
            {
                if (hit1.collider.name == "EvilMirror")
                {
                    lightray.SetPosition(0, originpt.transform.position);
                    lightray.SetPosition(1, hit1.point);
                    MirrorLizardAI.gotLight = true;
                    print("trueeeeee");
                }

                else if(hit1.collider.tag == "floor")
                {
                    lightray.SetPosition(0, originpt.transform.position);
                    lightray.SetPosition(1, hit1.point);
                    MirrorLizardAI.gotLight = false;
                }
            }
        }

	}
}

using UnityEngine;
using System.Collections;

public class Lightsource : MonoBehaviour {
	//private Vector3 tempDir = Vector3.zero;
	private RaycastHit hit1;
	private RaycastHit hit2;
	private RaycastHit hit3;
	private RaycastHit hit4;
	private RaycastHit hit5;
	private RaycastHit hit6;
	private RaycastHit hit7;
	public int rayNum;
	public static Ray[] rays = new Ray[7];
	private LineRenderer lightbeam;
	private GameObject originpt;
	public static bool lightTriggered = false;
	public static bool lightTriggered2 = false;
	public GameObject slope;
	public GameObject slope2;
	public AudioSource ding;
	private bool isplaying;

	// Use this for initialization
	void Start () {

		lightbeam = GetComponent<LineRenderer> ();
		originpt = GameObject.Find ("lightsource");
		rays [0] = new Ray (originpt.transform.position, originpt.transform.forward);
	}
	
	// Update is called once per frame
	void Update () {
		if (rayNum == 0) {
			if (Physics.Raycast (rays [0], out hit1, 3000)) {
				if (hit1.collider.name == "Mirror1") {
					lightbeam.SetPosition (0, originpt.transform.position);
					lightbeam.SetPosition (1, hit1.point);
					rays [1] = new Ray (hit1.point, Vector3.Reflect (rays [0].direction, hit1.normal));
				}
			}
		} else if (rayNum == 1) {
			if (Physics.Raycast (rays [1], out hit2, 3000)) {
				if (hit2.collider.name == "Mirror2") {
					lightbeam.SetPosition (0, rays [1].origin);
					lightbeam.SetPosition (1, hit2.point);
					rays [2] = new Ray (hit2.point, Vector3.Reflect (rays [1].direction, hit2.normal));
				}
                else
                {
                    lightbeam.SetPosition(0, rays[1].origin);
                    lightbeam.SetPosition(1, hit2.point);
                    if (hit2.collider.name == "Goo")
                    {

                        Destroy(hit2.collider.gameObject);
                        lightTriggered = true;
                    }

                }
			}
		} else if (rayNum == 2) {
			if (Physics.Raycast (rays [2], out hit3, 3000)) {
				if (hit3.collider.name == "Mirror3") {
					lightbeam.SetPosition (0, rays [2].origin);
					lightbeam.SetPosition (1, hit3.point);
					rays [3] = new Ray (hit3.point, Vector3.Reflect (rays [2].direction, hit3.normal));
				}
			}
		} else if (rayNum == 3) {
			if (Physics.Raycast (rays [3], out hit4, 3000)) {
				if (hit4.collider.name == "Mirror4") {
					lightbeam.SetPosition (0, rays [3].origin);
					lightbeam.SetPosition (1, hit4.point);
					rays [4] = new Ray (hit4.point, Vector3.Reflect (rays [3].direction, hit4.normal));
				} else if (hit4.collider.tag == "switch") {
					lightbeam.SetPosition (0, rays [3].origin);
					lightbeam.SetPosition (1, hit4.point);
					if(!isplaying)
					{
						ding.Play();
						isplaying = true;
					}
					if (lightTriggered == false && slope.transform.position.y < 176)
						lightTriggered = true;

				}
			}
		} else if (rayNum == 4) {
			if (Physics.Raycast (rays [4], out hit5, 4000)) {
				if (hit5.collider.name == "Mirror5") {
					lightbeam.SetPosition (0, rays [4].origin);
					lightbeam.SetPosition (1, hit5.point);
					rays [5] = new Ray (hit5.point, Vector3.Reflect (rays [4].direction, hit5.normal));
				}
			}
		} else if (rayNum == 5) {
			if (Physics.Raycast (rays [5], out hit6, 4000)) {
				if (hit6.collider.tag == "Reflecting") {
					lightbeam.SetPosition (0, rays [5].origin);
					lightbeam.SetPosition (1, hit6.point);
					rays [6] = new Ray (hit6.point, Vector3.Reflect (rays [5].direction, hit6.normal));
				}
			}
		} else if (rayNum == 6) {
			if (Physics.Raycast (rays [6], out hit7, 4000)) {
				if (hit7.collider.tag == "switch") {
					lightbeam.SetPosition (0, rays [6].origin);
					lightbeam.SetPosition (1, hit7.point);
					if(lightTriggered2 == false && slope2.transform.position.y < 171.8f)
						lightTriggered2 = true;
					if(!isplaying)
					{
						ding.Play();
						isplaying = true;
					}
				}
			}
		}

	}
	
}
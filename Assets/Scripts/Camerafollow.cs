using UnityEngine;
using System.Collections;

public class Camerafollow : MonoBehaviour {

	public static GameObject targetUnit;
    [SerializeField]private RectTransform foreground;
    private Camera maincam;

	// Use this for initialization
	void Start () {

		targetUnit = GameObject.Find ("Character");
        maincam = gameObject.GetComponentInChildren<Camera>();
	
	}
	
	// Update is called once per frame
	void Update () {
		
		float moveX = targetUnit.transform.position.x - transform.position.x;
		float moveZ = targetUnit.transform.position.z - transform.position.z;
		float moveY = targetUnit.transform.position.y - transform.position.y;
		Vector3 currLocation = new Vector3 (transform.position.x + moveX / 6, transform.position.y + moveY / 6, transform.position.z + moveZ / 6);
		transform.position = currLocation;

        Vector3 oppdir = transform.position * -1;
        foreground.anchoredPosition = maincam.WorldToScreenPoint(oppdir);
	}
}

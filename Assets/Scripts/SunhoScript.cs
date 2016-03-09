using UnityEngine;
using System.Collections;

public class SunhoScript : MonoBehaviour {
    Vector3 originpt;
    Vector3 targetpt;
    Vector3 movedirection;
    float travelradius = 2;
    float speed = -1;
    float rotationspeed;
	// Use this for initialization
	void Start () {
        originpt = transform.position;
        Debug.Log(originpt);
        Changetargetpt();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        movedirection = targetpt - transform.position;
        movedirection.Normalize();
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movedirection), rotationspeed * Time.deltaTime);
        transform.Translate(movedirection * speed * Time.deltaTime);

        float distsq = (targetpt - transform.position).sqrMagnitude;
        if (distsq < 1)
        {
            Changetargetpt();
        }
	}

    void Changetargetpt()
    {
        float xplus = Random.Range(-travelradius, travelradius);
        float yplus = Random.Range(-travelradius, travelradius);
        float zplus = Random.Range(-travelradius, travelradius);
        Vector3 plus = new Vector3(xplus, yplus, zplus);
        targetpt = originpt + plus;
        Debug.Log(targetpt);
        Debug.Log("Changetarget");
    }
}

using UnityEngine;
using System.Collections;

public class ScoobyDoo : MonoBehaviour {

    public int doorNum;
    private GameObject door11;
    private GameObject door12;
    private GameObject door21;
    private GameObject door22;
    private GameObject door31;
    private GameObject door32;
    private Vector3 targetRot;
    //private GameObject gulnaz;
    //private Rigidbody gul;
	// Use this for initialization
	void Start () {
        door11 = GameObject.Find("Scoobydoor11");
        door12 = GameObject.Find("Scoobydoor12");
        door21 = GameObject.Find("Scoobydoor21");
        door22 = GameObject.Find("Scoobydoor22");
        door31 = GameObject.Find("Scoobydoor31");
        door32 = GameObject.Find("Scoobydoor32");
	}
	
	// Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Character")
        {
            Quaternion lookRot = Quaternion.LookRotation(-Vector3.right, Vector3.up);
            switch(doorNum)
            {
                case 1:
                    other.gameObject.transform.position = door12.transform.position + door12.transform.forward;
                    other.gameObject.transform.rotation = lookRot;
                    break;

                case 2:
                    other.gameObject.transform.position = door11.transform.position + door11.transform.forward;
                    other.gameObject.transform.rotation = lookRot;
                    break;

                case 3:
                    other.gameObject.transform.position = door22.transform.position + door22.transform.forward;
                    other.gameObject.transform.rotation = lookRot;
                    break;

                case 4:
                    other.gameObject.transform.position = door21.transform.position + door21.transform.forward;
                    other.gameObject.transform.rotation = lookRot;
                    break;

                case 5:
                    other.gameObject.transform.position = door32.transform.position + door32.transform.forward;
                    other.gameObject.transform.rotation = lookRot;
                    break;

                case 6:
                    other.gameObject.transform.position = door31.transform.position + door31.transform.forward;
                    other.gameObject.transform.rotation = lookRot;
                    break;

            }
        }
    }
}

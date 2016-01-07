using UnityEngine;
using System.Collections;

public class RopeBreak : MonoBehaviour {
    public static short ropebroken = 0;
    GameObject damBlock;
    GameObject water;
    Rigidbody dam;
    public int triggerbango;
    public static bool dropit1 = false;
    public static bool dropit2 = false;
	// Use this for initialization
	void Start () {

        damBlock = GameObject.Find("damm_block");
        dam = damBlock.GetComponent<Rigidbody>();
        water = GameObject.Find("LoweredWater");
	
	}

    void Update()
    {
        if(dropit1 && dropit2)
        {
            if(water.transform.localPosition.y > -3)
            {
                water.transform.position -= new Vector3(0, Time.deltaTime * 0.2f, 0);
            }

            else
            {
                
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.collider.tag == "dagger")
        {
            if(triggerbango == 1)
            {
                dropit1 = true;
                print("drop1");
            }

            else if(triggerbango == 2)
            {
                dropit2 = true;
                print("drop2");
            }

            if(dropit1 && dropit2)
            {
                dam.constraints = RigidbodyConstraints.FreezeRotation & RigidbodyConstraints.FreezePositionX & RigidbodyConstraints.FreezePositionZ;
            }
        }
    }
}

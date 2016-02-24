using UnityEngine;
using System.Collections;

public class RopeBreak : MonoBehaviour {
    public static short ropebroken = 0;
    Animator damanim;
    GameObject water;
    [SerializeField] int triggerbango;
    public static bool dropit1 = false;
    public static bool dropit2 = false;
	// Use this for initialization
	void Start () {

        water = GameObject.Find("LoweredWater");
        damanim = GetComponentInParent<Animator>();
	
	}

    void Update()
    {
        if(dropit1 && dropit2)
        {
            if(water.transform.localPosition.y > -3)
            {
                water.transform.position -= new Vector3(0, Time.deltaTime * 0.2f, 0);
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.collider.tag == "dagger")
        {
           if(triggerbango == 1 && !dropit1)
            {
                dropit1 = true;
                damanim.SetTrigger("Right");
                Destroy(gameObject);
            }

           else if(triggerbango == 2 && !dropit1)
            {
                dropit1 = true;
                damanim.SetTrigger("Left");
                Destroy(gameObject);
            }

            else if(triggerbango == 1 && !dropit2 && dropit1)
            {
                dropit2 = true;
                damanim.SetTrigger("two");
            }

           else if (triggerbango == 2 && !dropit2 && dropit1)
            {
                dropit2 = true;
                damanim.SetTrigger("two");
            }
        }
    }
}

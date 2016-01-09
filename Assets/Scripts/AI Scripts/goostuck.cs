using UnityEngine;
using System.Collections;

public class goostuck : MonoBehaviour
{
    private Rigidbody rb;
    // Use this for initialization
    void Start()
    {

        rb = GetComponent<Rigidbody>();

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "goo")
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }

}



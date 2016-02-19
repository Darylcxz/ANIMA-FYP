using UnityEngine;
using System.Collections;

public class WallTrigger1 : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponentInChildren<VillageDialogue>().helpserikOn();
        }
    }
}

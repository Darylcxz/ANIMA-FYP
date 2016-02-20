using UnityEngine;
using System.Collections;

public class WallTrigger1 : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && VillageDialogue.cockblock1)
        {
            other.gameObject.transform.GetChild(1).GetComponent<VillageDialogue>().helpserikOn();
            Destroy(gameObject);
        }
    }
}

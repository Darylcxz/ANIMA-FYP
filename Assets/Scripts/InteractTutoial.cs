using UnityEngine;
using System.Collections;

public class InteractTutoial : MonoBehaviour {
    [SerializeField]VillageDialogue village;

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Character")
        {
            village.InteractTrigger();
            Destroy(gameObject);
        }
    }
}

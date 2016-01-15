using UnityEngine;
using System.Collections;

public class Dropsfromvase : MonoBehaviour {
    [SerializeField]private int typeofdrop;
    [SerializeField]private int typeofcard;
    [SerializeField]private GameControl controlstation;
	// Use this for initialization
	void Start () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("collect");
            switch(typeofdrop)
            {
                case 1:
                    other.SendMessage("AddMana", 3);
                    break;

                case 2:
                    controlstation.SendMessage("ShowCard", typeofcard);
                    break;
            }

            Destroy(gameObject);
        }
    }
}

using UnityEngine;
using System.Collections;

public class Dropsfromvase : MonoBehaviour {
    [SerializeField]private int typeofdrop;
    [SerializeField]private int typeofcard;
    
	// Use this for initialization
	void Start () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            switch(typeofdrop)
            {
                case 1:
                    other.SendMessage("AddMana", 3);
                    break;

                case 2:
                    
                    break;
            }
        }
    }
}

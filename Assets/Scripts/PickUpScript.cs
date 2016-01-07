using UnityEngine;
using System.Collections;

public class PickUpScript : MonoBehaviour {


 //   public GameObject _attach;
	Transform _attach;
    

    bool hasItem = false;

	// Use this for initialization
	void Start () {
		_attach = transform;
	
	}
	
	// Update is called once per frame
	
   
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("movable"))
        {
            if (Input.GetKeyDown(KeyCode.E) && !hasItem)
            {
                other.gameObject.transform.parent = _attach;
                //other.gameObject.transform.position = _attach.transform.position;
                hasItem = true;
            }
            else if (Input.GetKeyDown(KeyCode.E) && hasItem)
            {
               _attach.GetChild(0).transform.parent = null;
                hasItem = false;
                Debug.Log("detach");

            }
        }
       
       
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    if (!hasItem)
        //    {
        //        if (other.gameObject.tag == "Torch")
        //        {
        //            other.gameObject.transform.parent = _attach.transform;
        //            other.gameObject.transform.position = _attach.transform.position;
        //            hasItem = true;

        //        }  
        //    }
        //}
    }
   
}

using UnityEngine;
using System.Collections;

public class VineBreak : MonoBehaviour {

    [SerializeField] Rigidbody[] _rigidBodies;
    [SerializeField] GameObject _optionalTrigger;

	// Use this for initialization
	void Start () {
	
	}
	void OnCollisionEnter(Collision col)
	{
		if (col.collider.tag == "dagger" && gameObject.transform.GetChild(0)!= null)
		{
			foreach(Rigidbody _rb in _rigidBodies)
            {
                _rb.isKinematic = false;
                Destroy(_rb.gameObject, 3.5f);
            }
            gameObject.GetComponent<Collider>().enabled = false;
            //gameObject.transform.GetChild(0).GetComponent<Rigidbody>().isKinematic = true;
            //Destroy(gameObject,0.1f);
            if(_optionalTrigger!=null)
            {
                _optionalTrigger.SendMessage("OnVineBreak", SendMessageOptions.DontRequireReceiver);
            }
		}
	}

}

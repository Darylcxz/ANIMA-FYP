using UnityEngine;
using System.Collections;

public class InteractScript : MonoBehaviour {

   // RaycastHit _hit;
    [SerializeField] LayerMask rayMask;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //   Debug.DrawLine(transform.position, transform.forward * 5, Color.red);
        Debug.DrawRay(transform.position, transform.forward * 5, Color.red);
        if (GamepadManager.buttonBDown)
        {
            RaycastHit[] _hits;
            _hits = Physics.SphereCastAll(transform.position, 2f,transform.forward, 1f, rayMask);
            foreach(RaycastHit hit in _hits)
            {
                Debug.Log(hit.collider.gameObject);
                hit.collider.gameObject.SendMessage("PlayAnimation",SendMessageOptions.DontRequireReceiver);
            }
            //if (Physics.SphereCastAll(transform.position, 15f, transform.forward, out _hit, 5f, rayMask))
            //{
            //    Debug.Log(_hit.collider.gameObject);
            //    Debug.DrawLine(transform.position, Vector3.forward, Color.black);
            //    _hit.collider.gameObject.SendMessage("PlayAnimation", SendMessageOptions.DontRequireReceiver);
            //}
        }
	
	}
}

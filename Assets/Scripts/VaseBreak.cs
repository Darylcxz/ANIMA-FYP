using UnityEngine;
using System.Collections;

public class VaseBreak : MonoBehaviour {
    private Rigidbody[] pieces;
    private BoxCollider thisthing;
	// Use this for initialization
	void Start () {

        thisthing = gameObject.GetComponent<BoxCollider>();
	
	}

    void OnCollisionEnter(Collision other)
    {
        if(other.collider.CompareTag("dagger"))
        {
            Debug.Log("hit");
            thisthing.enabled = false;
            pieces = gameObject.GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody piece in pieces)
            {
                piece.isKinematic = false;
            }
            foreach (Rigidbody piece in pieces)
            {
                piece.AddExplosionForce(50,transform.position, 10);
            }
            
        }
    }
}

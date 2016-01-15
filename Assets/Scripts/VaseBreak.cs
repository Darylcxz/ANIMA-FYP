using UnityEngine;
using System.Collections;

public class VaseBreak : MonoBehaviour {
    private Rigidbody[] pieces;
    private BoxCollider thisthing;
    private GameObject objecttospawn;
    public bool spawnornot;
	// Use this for initialization
	void Start () {
        thisthing = gameObject.GetComponent<BoxCollider>();
        objecttospawn = gameObject.GetComponentInChildren<SphereCollider>().gameObject;
        objecttospawn.SetActive(false);
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
                piece.AddExplosionForce(200,transform.position, 10);
            }
            Invoke("DestroyObject", 3);
            Invoke("SpawnObject", 0.5f);
        }
    }

    void DestroyObject()
    {
        foreach (Rigidbody piece in pieces)
        {
            float temp = Random.Range(3.0f,5.0f);
            Destroy(piece.gameObject,temp );
        }
    }

    void SpawnObject()
    {
        if(spawnornot)
        {
            objecttospawn.SetActive(true);
        }
    }
}

using UnityEngine;
using System.Collections;

public class Transparency : MonoBehaviour {

    private MeshRenderer mesh;
    public Material[] yasuo = new Material [2];
	// Use this for initialization
	void Start () {

        mesh = gameObject.GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

        
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "transparency")
        {
            mesh.material = yasuo[1];
        }
        
       
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "transparency")
        {
            mesh.material = yasuo[0];
        }
    }
}

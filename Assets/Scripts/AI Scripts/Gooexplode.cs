using UnityEngine;
using System.Collections;

public class Gooexplode : MonoBehaviour {

	public ParticleSystem bigboom;
	public static bool onGoo;
    public float duration = 2.0f;
    
    public Underground1LevelController levelController;




	// Use this for initialization
	void Start () {
       // _current = gameObject.GetComponent<Renderer>().material.color;
        levelController = GameObject.Find("Level Control").GetComponent<Underground1LevelController>();
        StartCoroutine("SelfDestruct");

	}
	
	// Update is called once per frame
	void Update () {
     
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Torch"))
        {
            Instantiate(bigboom, transform.position, Quaternion.identity);
            gameObject.GetComponent<AudioSource>().Play();
            Explode(gameObject.transform.position, 1.0f);
            Destroy(gameObject,1f);
            
        }
    }

	void OnTriggerStay(Collider other)
	{
        if (other.gameObject != null)
        {
            //Debug.Log(other.gameObject.transform.name);
        }
        if (other.gameObject.name == "Pig" && Input.GetButtonDown("Action")) {
			Instantiate(bigboom,transform.position,Quaternion.identity);
			Destroy(gameObject);
			onGoo = true;
		}
     
	}
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Torch"))
        {
            ParticleSystem clone =  Instantiate(bigboom, transform.position, Quaternion.identity) as ParticleSystem;
            Explode(gameObject.transform.position, 10.0f);
           // isExplosion = true;
            //.isExplosion = true;
          //  levelController.isExplosion = true;
            Destroy(clone, 1.5f);
            Destroy(gameObject);        
        }
    }

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.name == "Pig") {
			onGoo = false;
		}
	}

    IEnumerator SelfDestruct()
    {
//        Debug.Log("die");
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }

    void Explode(Vector3 center,float radius)
    {
        
        Collider[] _objs = Physics.OverlapSphere(center, radius);
        Debug.Log("dwedw");
        for (int i = 0; i < _objs.Length; i++)
        {
            if (_objs[i].gameObject.CompareTag("destroy"))
            {
                Debug.Log(_objs[i].gameObject.name);

                _objs[i].gameObject.SetActive(false);
 
            }
            
            
        }
    }
 
}

using UnityEngine;
using System.Collections;

public class GooBallsScript : MonoBehaviour {

    
    public ParticleSystem bigboom;
    public GameObject _spawnThis;

    public Underground1LevelController levelController;

	// Use this for initialization
	void Start () {
        levelController = GameObject.Find("Level Control").GetComponent<Underground1LevelController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision collide)
    {
        if (collide.gameObject.tag != "Player")
        {
//            GameObject spawnClone = Instantiate(_spawnThis, transform.position, gameObject.GetComponent<Transform>().rotation) as GameObject;
            gameObject.GetComponent<AudioSource>().Play();
            Destroy(gameObject,0.2f);
        }
        if (collide.gameObject.tag == "Player")
        {
            gameObject.GetComponent<AudioSource>().Play();
            Destroy(gameObject,0.2f);
        }

        //if (collide.gameObject.name == "Torch")
        //{
        //    ParticleSystem clone = Instantiate(bigboom, transform.position, Quaternion.identity) as ParticleSystem;
        //    Destroy(clone, 1.5f);
        //    Destroy(gameObject);


        //}
    }
}

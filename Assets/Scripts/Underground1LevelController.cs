using UnityEngine;
using System.Collections;

public class Underground1LevelController : MonoBehaviour {

    public GameObject _destroyable;
    public bool isExplosion;
    public bool isDead;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        CheckExplosion();
	}

    void CheckExplosion()
    {
        if (isExplosion)
        {
            _destroyable.SetActive(false);
        }
        else if (!isExplosion)
        {
            _destroyable.SetActive(true);
        }
    }
}

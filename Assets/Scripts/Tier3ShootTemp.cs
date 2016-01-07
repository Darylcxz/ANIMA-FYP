using UnityEngine;
using System.Collections;

public class Tier3ShootTemp : MonoBehaviour {

    public float _shootInterval = 3.0f;
    public float shotSpeed = 15.0f;
    public GameObject _shootPoint;
    public Rigidbody _projectile;
    
	// Use this for initialization
	void Start () {
        InvokeRepeating("Fire", _shootInterval, _shootInterval);
      
	}

    void Fire()
    {
        Rigidbody _projectileClone = Instantiate(_projectile, _shootPoint.transform.position, _shootPoint.transform.rotation) as Rigidbody;
        _projectileClone.velocity = transform.forward * shotSpeed;
    }
}

using UnityEngine;
using System.Collections;

public class DeshWallScript : MonoBehaviour {

    Rigidbody _rb;
    Collider _col;
    AudioSource _deshSound;
    Animator _anim;
    Transform Player;
    [SerializeField] Canvas alertCanvas;

	// Use this for initialization
	void Start () {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<Collider>();
        _deshSound = GetComponent<AudioSource>();
        _anim = GetComponent<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        alertCanvas = alertCanvas.GetComponent<Canvas>();
	
	}
	
	// Update is called once per frame
	void Update () {
        if(Vector3.Distance(Player.position,transform.position) < 3)
        {
            _anim.Play("DeshTallSHAKE");
            alertCanvas.enabled = true;
        }
        else
        {
            _anim.Play("DeshTallIDLEBREAKER");
            alertCanvas.enabled = false;
        }
	
	}
    void OnCollisionEnter(Collision col)
    {
        if(col.collider.CompareTag("log"))
        {
            _deshSound.Play();
            _rb.constraints = RigidbodyConstraints.None;
            _anim.Play("DeshTallHIT");
            _col.isTrigger = true;            
        }
        if(col.collider.CompareTag("dagger"))
        {
            _deshSound.Play();
            _rb.constraints = RigidbodyConstraints.None;
            _anim.Play("DeshTallHIT");
            _col.isTrigger = true;
        }
    }


}

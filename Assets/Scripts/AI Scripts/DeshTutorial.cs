using UnityEngine;
using System.Collections;

public class DeshTutorial : MonoBehaviour {
	public bool deshDead;
	[SerializeField]
	AudioSource dieSound;
    [SerializeField]
    GameObject jumpSequence;

    [SerializeField]
    Transform Player;
    [SerializeField] float rangeOffset;

    public bool _move;
	// Use this for initialization
	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
		deshDead = false;
		dieSound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if(_move)
        {
            DashToPlayer();
        }
	
	}
	void OnCollisionEnter(Collision _col)
	{
		if (_col.collider.tag == "dagger")
		{
			dieSound.Play();
			deshDead = true;
			gameObject.transform.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
            jumpSequence.GetComponent<Collider>().isTrigger = true;
			gameObject.GetComponent<BoxCollider>().enabled = false;
		}
	}
    void DashToPlayer()
    {
        Vector3 _dir = (Player.position - transform.position).normalized;
        _dir.y = 0;
        Quaternion endRot = Quaternion.LookRotation(_dir);
        if(Vector3.Distance(transform.position,Player.position) > rangeOffset)
        {
            //if the player is not so close to the AI, move the AI to the player
            transform.rotation = endRot;
            transform.position += transform.forward * Time.deltaTime*2;
        }
    }
}

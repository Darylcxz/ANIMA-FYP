using UnityEngine;
using System.Collections;

public class FireGate : MonoBehaviour {

	public bool isGate;
	[SerializeField]AudioSource fireSound;

	[SerializeField]
	AudioSource stoneSound;
	public ParticleSystem LeftFire;
	public ParticleSystem RightFire;
	public Animator GateAnim;
    [SerializeField]
    ParticleSystem OwnFire;
    GameObject stickOfTruth;
	// Use this for initialization
	void Start () {
       if(!isGate)
        {
            OwnFire = OwnFire.GetComponent<ParticleSystem>();
        }
        stickOfTruth = GameObject.FindGameObjectWithTag("stick");
        LeftFire = LeftFire.GetComponent<ParticleSystem>();
        RightFire = RightFire.GetComponent<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update () {
		if(isGate)
        {
            if (LeftFire.isPlaying && RightFire.isPlaying)
            {
                GateAnim.GetComponent<Animator>().SetBool("bFire", true);
                if (stickOfTruth.activeInHierarchy)
                {
                    stickOfTruth.SetActive(false);
                }
                if (stoneSound != null)
                {
                    stoneSound.Play();
                }

            }
        }
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.collider.tag == "dagger" && !isGate)
		{
            if(!OwnFire.isPlaying)
            {
                OwnFire.Play();
            }
			fireSound.Play();
		}
	}
}

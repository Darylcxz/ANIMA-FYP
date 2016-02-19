using UnityEngine;
using System.Collections;

public class DummyScript : MonoBehaviour {
    private Animator dummyanim;
    public ParticleSystem hayeffect;
    private int hitcount;
	// Use this for initialization
	void Start () {

        dummyanim = GetComponent<Animator>();
	
	}

    void OnCollisionEnter(Collision _col)
    {

        if (_col.collider.CompareTag("dagger"))
        {
            dummyanim.SetTrigger("Gethit");
            hayeffect.Play();
            hitcount++;
            if(hitcount >= 5)
            {
                VillageDialogue.hitDummy = true;
            }
        }
    }
}

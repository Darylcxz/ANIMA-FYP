using UnityEngine;
using System.Collections;

public class PressurePlateScript : MonoBehaviour {

	[SerializeField]
	Animator plateAnim;
	[SerializeField]
	Animator gateAnim;
	bool bSteppedOn;
    AudioSource stoneslide;
    int numberOfThings;

	void Update()
	{
        if(numberOfThings>0)
        {
            bSteppedOn = true;
        }
        if(numberOfThings <1)
        {
            bSteppedOn = false;
        }
        plateAnim.SetBool("bActive", bSteppedOn);
		gateAnim.SetBool("bLever", bSteppedOn);
	}
    

    //void OnTriggerStay(Collider col)
    //{
    //	bSteppedOn = true;
    //}
    void OnTriggerExit(Collider col)
    {
        numberOfThings--;

    }

    void OnTriggerEnter()
    {
        stoneslide = gameObject.GetComponent<AudioSource>();
        stoneslide.Play();
        numberOfThings++;
    }
}

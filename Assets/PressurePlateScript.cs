using UnityEngine;
using System.Collections;

public class PressurePlateScript : MonoBehaviour {

	[SerializeField]
	Animator plateAnim;
	[SerializeField]
	Animator gateAnim;
	bool bSteppedOn;

	void Update()
	{
		plateAnim.SetBool("bActive", bSteppedOn);
		gateAnim.SetBool("bLever", bSteppedOn);
	}

	void OnTriggerStay(Collider col)
	{
		bSteppedOn = true;
	}
	void OnTriggerExit(Collider col)
	{
		bSteppedOn = false;
		
	}
}

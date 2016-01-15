using UnityEngine;
using System.Collections;

public class SclorgProjectile : MonoBehaviour {

	void OnCollisionEnter(Collision col)
	{
		if (col.collider.CompareTag("Player"))
		{
			col.gameObject.SendMessage("Slow", SendMessageOptions.DontRequireReceiver);
			Destroy(gameObject, 0.5f);
		}
		else
		{
			Destroy(gameObject, 2f);
		}
	}
}

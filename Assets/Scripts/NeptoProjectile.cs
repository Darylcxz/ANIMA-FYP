using UnityEngine;
using System.Collections;

public class NeptoProjectile : MonoBehaviour {
	Rigidbody _rb;
	Vector3 currentTargetPosition;
	float rotSpeed = 5.0f;
	bool deflect;
	bool toSender;

	// Use this for initialization
	void Start () {
		_rb = GetComponent<Rigidbody>();
		deflect = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (toSender)
		{
			MoveTowardsTarget();
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			deflect = true;
		}
		//else
		//{
		//	deflect = false; 
		//}
		//Debug.Log(deflect);
	}
	void OnCollisionEnter(Collision col)
	{
		if (col.collider.tag == "Player")
		{
			col.gameObject.GetComponent<Rigidbody>().AddExplosionForce(10, transform.position, 3, 0, ForceMode.Impulse);
			Destroy(gameObject, 0.5f);
		}
		if (col.collider.name.Contains( "Golem"))
		{
			_rb.velocity = Vector3.zero;
			if (col.collider.GetComponent<GolemAI>().deflect)
			{
				toSender = true;
			}
			else
			{
				col.gameObject.GetComponent<Rigidbody>().AddExplosionForce(10, transform.position, 3, 0, ForceMode.Impulse);
				Destroy(gameObject,0.5f);
			}
		}
		if (col.collider)
		{
			gameObject.GetComponent<AudioSource>().Play();
			Destroy(gameObject, 2.0f);
		}
	}
	void BackToSender(int test)
	{ 
		//go back to the plant that sent it.
		if (test == 1)
		{
 			//return it back to sender :v
		}
	}
	public void OriginPos(Vector3 origin)
	{
		currentTargetPosition = origin;
	}
	void MoveTowardsTarget()
	{
		Vector3 direction = currentTargetPosition - transform.position;
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 10 * Time.deltaTime);
		
			Vector3 moveVector = direction.normalized * 20 * Time.deltaTime;
			transform.position += moveVector;
		
	}
}
